using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Eletrobid.Nfe
{
    public class Nfe
    {
        private const string NumeroIbgeMg = "31";
        private const string NumeroIbgeBh = "3106200";
        private const int IdAmbiente = 1; //PRODUÇÃO = 1 - HOMOLOGAÇÃO = 0
        private const string InscricaoEstadual = "07826010013";
        private const string InscricaoMunicipal = "4753900";
        private const string CnpjEmitente = "26333116000136";
        private const string EmailContador = "cont.soma@terra.com.br";
        private const int RegimeTributario = 3; //REGIME TRIBUTARIO NORMAL
        private const string emitente = "ELETROBID COMERCIO LTDA";

        static void Main()
        {
            string caminhoArquivo = @"C:\Users\joaopedro.mobilecursos\Desktop\TesteRelatorio.xlsx";
            Console.WriteLine("Digite o tipo de nota: 1 - Remessa - 2 - Venda");
            int tipoNota = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o número da última nota emitida");
            int nUltimaNotaEmitida = int.Parse(Console.ReadLine());
            if (tipoNota == 1)
            {
                GerarTxtNfeVendaExcel(caminhoArquivo, nUltimaNotaEmitida);
            }
            else if (tipoNota == 2)
            {
                GerarTxtNfeRemessaExcel(caminhoArquivo, nUltimaNotaEmitida);
            }
        }
        static void GerarTxtNfeVendaExcel(string caminhoArquivo, int nUltimaNotaEmitida)
        {
            StringBuilder escreverDados = new StringBuilder();

            using (var stream = File.Open(caminhoArquivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet().Tables[0];
                    var conteudo = result.Rows;
                    Random rndNumero = new Random();
                    int numeroNotas = 0;


                    for (int i = 2; i < conteudo.Count; i++)
                    {
                        if (!conteudo[i].IsNull(0))
                        {
                            var dadosLinha = conteudo[i].ItemArray;

                            if (dadosLinha[3].ToString() != "CONDICIONAL" && !string.IsNullOrEmpty(dadosLinha[3].ToString()))
                            {
                                numeroNotas++;
                                nUltimaNotaEmitida++;
                                int codigoEstado = 55; //Buscar no banco
                                var mesEmissao = System.DateTime.Now.Month.ToString("d2");
                                var anoEmissao = System.DateTime.Now;
                                var codigoNfe = rndNumero.Next(100000000, 999999999);

                                //Descobrir o modelo e série da nota fiscal
                                var chaveAcesso = codigoEstado.ToString() + mesEmissao + anoEmissao.ToString("yy") + CnpjEmitente + "02" + "001" + string.Format("{0:000000000}", nUltimaNotaEmitida) + codigoNfe.ToString();

                                int[] multiplicadores = { 2, 3, 4, 5, 6, 7, 8, 9 };
                                int somaValores = 0;
                                int posicaoMultiplicador = 0;
                                for (int a = chaveAcesso.Length - 1; a > 0; a--)
                                {
                                    if (posicaoMultiplicador >= 8)
                                    {
                                        posicaoMultiplicador = 0;
                                    }

                                    somaValores += (Convert.ToInt32(chaveAcesso[a]) * multiplicadores[posicaoMultiplicador]);
                                    posicaoMultiplicador++;
                                }

                                int digitoVerificador = 11 - (somaValores % 11);

                                chaveAcesso = chaveAcesso + digitoVerificador.ToString();

                                var cfop = dadosLinha[12].ToString() == "MG" ? 5949 : 6949;

                                //GRUPO "A" DO ARQUIVO - PADRÃO
                                escreverDados.AppendFormat("A|3.10|NFe{0}|\r\n", chaveAcesso);

                                //GRUPO "B" DO ARQUIVO - INFORMAÇÕES DE IDENTIFICAÇÃO DA NFe
                                var naturezaOperacao = "VENDA";
                                var idDestino = dadosLinha[12].ToString() == "MG" ? 1 : 2;
                                var dataEmissao = string.Format(
                                    DateTime.Now.IsDaylightSavingTime()
                                        ? "{0:yyyy-MM-dd}T{0:HH}:{0:mm}:{0:ss}-02:00"
                                        : "{0:yyyy-MM-dd}T{0:HH}:{0:mm}:{0:ss}-03:00", DateTime.Now);

                                escreverDados.AppendFormat("B|{0}|10607090|{1}|0|55|2|{2}|{3}||{4}|1|{5}|1|1||{6}|1|0|1.0|||1|2|\r\n", NumeroIbgeMg, naturezaOperacao, nUltimaNotaEmitida, dataEmissao, idDestino, NumeroIbgeBh, IdAmbiente);

                                //GRUPO "C" DO ARQUIVO - IDENTIFICAÇÃO DO EMITENTE DA NFe
                                escreverDados.AppendFormat("C|{0}||{1}||{2}||{3}|\r\n", emitente, InscricaoEstadual, InscricaoMunicipal, RegimeTributario);

                                //GRUPO "C02" DO ARQUIVO - CNPJ DO EMIRENTE
                                escreverDados.AppendFormat("C02|{0}|\r\n", CnpjEmitente);

                                //GRUPO CO5 - ENDEREÇO DO EMITENTE
                                escreverDados.AppendFormat("C05|RUA MONTEIRO LOBATO|252|SALA 11|OURO PRETO|{0}|BELO HORIZONTE|MG|31310530|1058|BRASIL||\r\n", NumeroIbgeBh);

                                //GRUPO E - IDENTIFICAÇÃO DO DESTINATÁRIO DA NFe
                                escreverDados.AppendFormat("E|{0}||||9||\r\n", dadosLinha[5].ToString().ToUpper());

                                //GRUPO E02 - CPF DO DESTINATÁRIO DA NFe
                                escreverDados.AppendFormat("E03|{0}|\r\n", dadosLinha[4].ToString().Replace(".", "").Replace("-", ""));

                                //GRUPO E05 - ENDEREÇO DO DESTINATÁRIO DA NFe
                                escreverDados.AppendFormat("E05|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|1058|BRASIL||\r\n", dadosLinha[6].ToString().ToUpper(),
                                    Convert.ToInt32(dadosLinha[7]).ToString(), dadosLinha[8].ToString() == "-" ? "" : dadosLinha[8].ToString(), dadosLinha[9].ToString(), 12.ToString(),
                                    dadosLinha[11].ToString(), dadosLinha[12].ToString(), dadosLinha[10].ToString().Replace("-", "").Replace(".", ""));

                                //GRUPO H - DETALHAMENTO DE PRODUTOS E SERVIÇOS DA NFe
                                escreverDados.AppendFormat("H|1||\r\n");

                                double valorUnitario = Convert.ToDouble(dadosLinha[3]) / Convert.ToInt32(dadosLinha[2]);


                                //GRUPO I - PRODUTOS E SERVIÇOS DA NFe
                                escreverDados.AppendFormat(
                                    "I|1||PRODUTO SUCATEADO|00000000||{0}|UN|{1}|{2:0,00}|{3:0,00}||UN|{1}|{3:0,00}|||||1|||||\r\n",
                                    cfop, Convert.ToInt32(dadosLinha[2]).ToString().Replace(",", "."), valorUnitario, dadosLinha[3]);

                                //GRUPO M - TRIBUTOS INCIDENTES NO PRODUTO OU SERVIÇO
                                escreverDados.Append("M|\r\n");

                                //GRUPO N - ICMS NORMAL E ST
                                //GRUPO N06 - REGIME NORMAL NÃO TRIBUTADO
                                escreverDados.AppendFormat("N06|0|50|||\r\n");

                                //GRUPO Q - PIS
                                escreverDados.Append("Q|\r\n");

                                //GRUPO Q - PIS
                                escreverDados.Append("Q04|08|\r\n");


                                //GRUPO S - COFINS
                                escreverDados.Append("S04|08|\r\n");

                                //GRUPO W - TOTAL DA NF-E
                                escreverDados.Append("W|\r\n");
                                escreverDados.AppendFormat("W02|0|0|0|0|{0:0,00}|0|0|0|0|0|0|0|0|{0:0,00}|0|0|\r\n", dadosLinha[3]);
                                escreverDados.AppendFormat("W04c|0.00|\r\n");
                                escreverDados.AppendFormat("W04c|0.00|\r\n");
                                escreverDados.AppendFormat("W04c|0.00|\r\n");

                                //GRUPO X - INFORMAÇÕES DO TRANSPORTE DA NF-E
                                escreverDados.AppendFormat("X|0|\r\n");
                                escreverDados.AppendFormat("X03|ELETROBID COMERCIO LTDA|0028450920043|R MONTEIRO LOBATO, 252, SALA 11, OURO PRETO|Belo Horizonte|MG|\r\n");
                                escreverDados.AppendFormat("X04|26333116000136|\r\n");

                                //GRUPO Z - INFORMAÇÕES ADICIONAIS
                                escreverDados.AppendFormat("Z||SUSPENSAO DO ICMS PARA VENDA EM LEILÃO CONF. ART. 4 DA PARTE 1 DO ANEXO III DO RICMS/MG - REFERENTE A NF DE COMPRA Nº {0} DO DIA {1} |\r\n", nUltimaNotaEmitida, System.DateTime.Now.ToShortDateString());


                                int numero = Convert.ToInt32(dadosLinha[0]);
                                string palavra = Convert.ToString(dadosLinha[1]);
                                int valor = Convert.ToInt32(dadosLinha[2]);
                            }
                        }
                    }


                    //LINHA 1 DO ARQUIVO - CABEÇALHO PADRÃO
                    escreverDados.Insert(0, "NOTA FISCAL|" + numeroNotas + "|\r\n");
                }
            }

            escreverDados.ToString();
            var fileName = string.Format("NOTA_FISCAL_VENDA_ELETROBID.txt");
            string pathArquivoNota = Path.Combine(@"C:\Users\joaopedro.mobilecursos\Desktop\", fileName);


            using (TextWriter tw = new StreamWriter(pathArquivoNota, false))
            {
                tw.Write(escreverDados);
                tw.Close();
            }


        }


        static void GerarTxtNfeRemessaExcel(string caminhoArquivo, int nUltimaNotaEmitida)
        {

            StringBuilder escreverDados = new StringBuilder();

            using (var stream = File.Open(caminhoArquivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet().Tables[0];
                    var conteudo = result.Rows;

                    //LINHA 1 DO ARQUIVO - CABEÇALHO PADRÃO
                    escreverDados.Append("NOTA FISCAL|1|\r\n");

                    for (int i = 2; i < conteudo.Count; i++)
                    {
                        if (!conteudo[i].IsNull(0))
                        {
                            var dadosLinha = conteudo[i].ItemArray;

                            if (dadosLinha[3].ToString() != "CONDICIONAL" && !string.IsNullOrEmpty(dadosLinha[3].ToString()))
                            {

                                var cfop = dadosLinha[12].ToString() == "MG" ? 5101 : 6101;

                                //GRUPO "A" DO ARQUIVO - PADRÃO
                                escreverDados.Append("A|3.10|NFe|\r\n");

                                //GRUPO "B" DO ARQUIVO - INFORMAÇÕES DE IDENTIFICAÇÃO DA NFe
                                var naturezaOperacao = "REMESSA PARA LEILÃO";
                                var idDestino = dadosLinha[12].ToString() == "MG" ? 1 : 2;
                                var dataEmissao = string.Format(
                                    DateTime.Now.IsDaylightSavingTime()
                                        ? "{0:yyyy-MM-dd}T{0:HH}:{0:mm}:{0:ss}-02:00"
                                        : "{0:yyyy-MM-dd}T{0:HH}:{0:mm}:{0:ss}-03:00", DateTime.Now);

                                nUltimaNotaEmitida++;
                                escreverDados.AppendFormat("B|{0}|10607090|{1}|0|55|2|{2}|{3}||{4}|1|{5}|1|1||{6}|1|0|1.0|||1|2|\r\n", NumeroIbgeMg, naturezaOperacao, nUltimaNotaEmitida, dataEmissao, idDestino, NumeroIbgeBh, IdAmbiente);

                                //GRUPO "C" DO ARQUIVO - IDENTIFICAÇÃO DO EMITENTE DA NFe
                                escreverDados.AppendFormat("C|{0}||{1}||{2}||{3}|\r\n", emitente, InscricaoEstadual, InscricaoMunicipal, RegimeTributario);

                                //GRUPO "C02" DO ARQUIVO - CNPJ DO EMIRENTE
                                escreverDados.AppendFormat("C02|{0}|\r\n", CnpjEmitente);

                                //GRUPO CO5 - ENDEREÇO DO EMITENTE
                                escreverDados.AppendFormat("C05|RUA MONTEIRO LOBATO|252|SALA 11|OURO PRETO|{0}|BELO HORIZONTE|MG|31310530|1058|BRASIL||\r\n", NumeroIbgeBh);

                                //GRUPO E - IDENTIFICAÇÃO DO DESTINATÁRIO DA NFe
                                escreverDados.AppendFormat("E|{0}|||teste@teste.com|9||\r\n", dadosLinha[5].ToString().ToUpper());

                                //GRUPO E02 - CPF DO DESTINATÁRIO DA NFe
                                escreverDados.AppendFormat("E03|{0}|\r\n", dadosLinha[4].ToString().Replace(".", "").Replace("-", ""));

                                //GRUPO E05 - ENDEREÇO DO DESTINATÁRIO DA NFe
                                escreverDados.AppendFormat("E05|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|1058|BRASIL||\r\n", dadosLinha[6].ToString().ToUpper(),
                                    Convert.ToInt32(dadosLinha[7]).ToString(), dadosLinha[8].ToString() == "-" ? "" : dadosLinha[8].ToString(), dadosLinha[9].ToString(), 12.ToString(),
                                    dadosLinha[11].ToString(), dadosLinha[12].ToString(), dadosLinha[10].ToString().Replace("-", "").Replace(".", ""));

                                //GRUPO H - DETALHAMENTO DE PRODUTOS E SERVIÇOS DA NFe
                                escreverDados.AppendFormat("H|1||\r\n");

                                double valorUnitario = Convert.ToDouble(dadosLinha[3]) / Convert.ToInt32(dadosLinha[2]);


                                //GRUPO I - PRODUTOS E SERVIÇOS DA NFe
                                escreverDados.AppendFormat(
                                    "I|1||PRODUTO SUCATEADO|00000000||{0}|UN|{1}|{2:0,00}|{3:0,00}||UN|{1}|{3:0,00}|||||1|||||\r\n",
                                    cfop, Convert.ToInt32(dadosLinha[2]).ToString().Replace(",", "."), valorUnitario, dadosLinha[3]);

                                //GRUPO M - TRIBUTOS INCIDENTES NO PRODUTO OU SERVIÇO
                                escreverDados.Append("M|\r\n");

                                //GRUPO N - ICMS NORMAL E ST
                                //GRUPO N06 - REGIME NORMAL NÃO TRIBUTADO
                                escreverDados.AppendFormat("N06|0|50|||\r\n");

                                //GRUPO Q - PIS
                                escreverDados.Append("Q|\r\n");

                                //GRUPO Q - PIS
                                escreverDados.Append("Q04|08|\r\n");


                                //GRUPO S - COFINS
                                escreverDados.Append("S04|08|\r\n");

                                //GRUPO W - TOTAL DA NF-E
                                escreverDados.Append("W|\r\n");
                                escreverDados.AppendFormat("W02|0|0|0|0|{0:0,00}|0|0|0|0|0|0|0|0|{0:0,00}|0|0|\r\n", dadosLinha[3]);
                                escreverDados.AppendFormat("W04c|0.00|\r\n");
                                escreverDados.AppendFormat("W04c|0.00|\r\n");
                                escreverDados.AppendFormat("W04c|0.00|\r\n");

                                //GRUPO X - INFORMAÇÕES DO TRANSPORTE DA NF-E
                                escreverDados.AppendFormat("X|0|\r\n");
                                escreverDados.AppendFormat("X03|ELETROBID COMERCIO LTDA|0028450920043|R MONTEIRO LOBATO, 252, SALA 11, OURO PRETO|Belo Horizonte|MG|\r\n");
                                escreverDados.AppendFormat("X04|26333116000136|\r\n");

                                //GRUPO Z - INFORMAÇÕES ADICIONAIS
                                escreverDados.AppendFormat("Z||SUSPENSAO DO ICMS PARA VENDA EM LEILÃO CONF. ART. 4 DA PARTE 1 DO ANEXO III DO RICMS/MG - REFERENTE A NF DE COMPRA Nº {0} DO DIA {1} |\r\n", nUltimaNotaEmitida, System.DateTime.Now.ToShortDateString());


                                int numero = Convert.ToInt32(dadosLinha[0]);
                                string palavra = Convert.ToString(dadosLinha[1]);
                                int valor = Convert.ToInt32(dadosLinha[2]);
                            }
                        }
                    }
                }
            }

            escreverDados.ToString();
            var fileName = string.Format("NOTASFISCAISELETROBID.txt");
            string pathArquivoNota = Path.Combine(@"C:\Users\joaopedro.mobilecursos\Desktop\", fileName);


            using (TextWriter tw = new StreamWriter(pathArquivoNota, false))
            {
                tw.Write(escreverDados);
                tw.Close();
            }


        }
    }


}

