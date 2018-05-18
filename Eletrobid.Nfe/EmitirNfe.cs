using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Eletrobid.Dal;
using Eletrobid.Dal.Concrete;
using Eletrobid.Models;
using System.Configuration;
using System.Reflection;

namespace Eletrobid.Nfe
{
    public class EmitirNfe
    {
        private const string NumeroIbgeMg = "31";
        private const string NumeroIbgeBh = "3106200";
        private const int IdAmbiente = 1; //PRODUÇÃO = 1 - HOMOLOGAÇÃO = 0
        private const string InscricaoEstadual = "0028450920043";
        private const string InscricaoMunicipal = "07826010013";
        private const string CnpjEmitente = "26333116000136";
        private const string EmailContador = "cont.soma@terra.com.br";
        private const int RegimeTributario = 1; //REGIME TRIBUTARIO NORMAL
        private const string emitente = "ELETROBID COMERCIO LTDA";

        static void Main(string[] args)
        {
            try
            {
                char finalizado = 'N';
                while (finalizado != 'S')
                {
                    NfeDal dalNota = new NfeDal();

                    Console.WriteLine("Digite o caminho do arquivo");
                    var caminhoArquivo = @"C:\Users\joaopedro\Desktop\BDL\Arremates_Eletrobid\RELATÓRIO FINAL ELETROBID 11-03-17.xlsx";
                    Console.WriteLine("Digite o tipo de nota: 1 - Remessa - 2 - Venda");
                    int tipoNota = int.Parse(Console.ReadLine());
                    if (tipoNota == 1)
                    {
                        GerarTxtNfeRemessaExcel(caminhoArquivo, dalNota.GetNumeroUltimaNfe());
                    }
                    else if (tipoNota == 2)
                    {
                        GerarTxtNfeVendaExcel(caminhoArquivo, dalNota.GetNumeroUltimaNfe());
                    }

                    Console.WriteLine("Arquivo txt gerado com sucesso. Finalizar criação de arquivos? Sim = S Não = N");
                    finalizado = char.Parse(Console.ReadLine());
                }
            }
            catch(ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                Console.WriteLine(errorMessage);
            }
        }
        static void GerarTxtNfeVendaExcel(string caminhoArquivo, int nUltimaNotaEmitida)
        {
            CidadeDal getCidade = new CidadeDal();
            EstadoDal getEstado = new EstadoDal();
            NfeDal dalNota = new NfeDal();
            StringBuilder escreverDados = new StringBuilder();
            StringBuilder escreverCpfInvalidos = new StringBuilder();
            string dataLeilao = "";

            using (var stream = File.Open(caminhoArquivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet().Tables[0];
                    var conteudo = result.Rows;
                    Random rndNumero = new Random();
                    int numeroNotas = 0;
                    double digitoVerificador = 0;

                    for (int i = 2; i < conteudo.Count; i++)
                    {
                        if (!conteudo[i].IsNull(0))
                        {
                            var dadosLinha = conteudo[i].ItemArray;
                            dataLeilao = conteudo[0].ItemArray[0].ToString();

                            if (dadosLinha[3].ToString() != "CONDICIONAL" && !string.IsNullOrEmpty(dadosLinha[3].ToString()))
                            {
                                if (Util.ValidaCPF(dadosLinha[4].ToString().Replace(".", "").Replace("-", "")) == true)
                                {
                                    numeroNotas++;
                                    nUltimaNotaEmitida++;
                                    string codigoEstado = getEstado.getCodigoIbge(dadosLinha[12].ToString()); //Buscar no banco
                                    string codigoCidade = getCidade.getCodigoIbge(dadosLinha[11].ToString()); //Buscar no banco
                                    var mesEmissao = DateTime.Now.Month.ToString("d2");
                                    var anoEmissao = DateTime.Now;
                                    var codigoNfe = rndNumero.Next(10000000, 99999999);
                                    double valorTotal = Convert.ToDouble(dadosLinha[3]);

                                    //Descobrir o modelo e série da nota fiscal
                                    var chaveAcesso = codigoEstado.ToString() + mesEmissao + anoEmissao.ToString("yy") + CnpjEmitente + "55" + "001" + string.Format("{0:000000000}", nUltimaNotaEmitida) + "1" + codigoNfe.ToString();

                                    int[] multiplicadores = { 2, 3, 4, 5, 6, 7, 8, 9 };
                                    double somaValores = 0;
                                    int posicaoMultiplicador = 0;
                                    for (int a = chaveAcesso.Length - 1; a >= 0; a--)
                                    {
                                        if (posicaoMultiplicador >= 8)
                                        {
                                            posicaoMultiplicador = 0;
                                        }
                                        somaValores += (Convert.ToDouble(Convert.ToInt32(chaveAcesso[a].ToString())) * Convert.ToDouble(multiplicadores[posicaoMultiplicador]));
                                        posicaoMultiplicador++;
                                    }

                                    int resto = Convert.ToInt32(somaValores) / 11;
                                    resto = Convert.ToInt32(somaValores) - (resto * 11);
                                    
                                    if (resto == 0 || resto == 1)
                                    {
                                        digitoVerificador = 0;
                                    }
                                    else
                                    {
                                        digitoVerificador = 11 - resto;
                                    }


                                    chaveAcesso = chaveAcesso + digitoVerificador.ToString();

                                    var cfop = dadosLinha[12].ToString() == "MG" ? 5102 : 6102;

                                    //GRUPO "A" DO ARQUIVO - PADRÃO
                                    escreverDados.AppendFormat("A|3.10|NFe{0}\r\n", chaveAcesso);

                                    //GRUPO "B" DO ARQUIVO - INFORMAÇÕES DE IDENTIFICAÇÃO DA NFe
                                    var naturezaOperacao = "VENDA ARREMATE LEILÃO";
                                    var idDestino = dadosLinha[12].ToString() == "MG" ? 1 : 2;
                                    var dataEmissao = string.Format(
                                        DateTime.Now.IsDaylightSavingTime()
                                            ? "{0:yyyy-MM-dd}T{0:HH}:{0:mm}:{0:ss}-02:00"
                                            : "{0:yyyy-MM-dd}T{0:HH}:{0:mm}:{0:ss}-03:00", DateTime.Now);

                                    escreverDados.AppendFormat("B|{0}|12587410|{2}|0|55|1|{3}|{4}|{4}|1|{5}|{6}|1|1|{9}|{7}|1|1|1|3|3.20.55|{4}|{8}\r\n", NumeroIbgeMg, codigoNfe, naturezaOperacao, nUltimaNotaEmitida, dataEmissao, idDestino, NumeroIbgeBh, IdAmbiente, "VENDA DE MERCADORIA EM LEILÃO", digitoVerificador);

                                    //GRUPO "C" DO ARQUIVO - IDENTIFICAÇÃO DO EMITENTE DA NFe
                                    escreverDados.AppendFormat("C|{0}||{1}||{2}||{3}|\r\n", emitente, InscricaoEstadual, InscricaoMunicipal, RegimeTributario);

                                    //GRUPO "C02" DO ARQUIVO - CNPJ DO EMITENTE
                                    escreverDados.AppendFormat("C02|{0}|\r\n", CnpjEmitente);

                                    //GRUPO CO5 - ENDEREÇO DO EMITENTE
                                    escreverDados.AppendFormat("C05|RUA MONTEIRO LOBATO|252|SALA 11|OURO PRETO|{0}|BELO HORIZONTE|MG|31310530|1058|BRASIL||\r\n", NumeroIbgeBh);

                                    //GRUPO E - IDENTIFICAÇÃO DO DESTINATÁRIO DA NFe
                                    escreverDados.AppendFormat("E|{0}|9|||||\r\n", dadosLinha[5].ToString().ToUpper());

                                    //GRUPO E02 - CPF DO DESTINATÁRIO DA NFe
                                    escreverDados.AppendFormat("E03|{0}|\r\n", dadosLinha[4].ToString().Replace(".", "").Replace("-", ""));

                                    //GRUPO E05 - ENDEREÇO DO DESTINATÁRIO DA NFe
                                    escreverDados.AppendFormat("E05|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|1058|BRASIL||\r\n", dadosLinha[6].ToString().ToUpper(),
                                        Convert.ToInt32(dadosLinha[7]).ToString(), dadosLinha[8].ToString() == "-" ? "" : dadosLinha[8].ToString(), dadosLinha[9].ToString(), codigoCidade,
                                        dadosLinha[11].ToString(), dadosLinha[12].ToString(), dadosLinha[10].ToString().Replace("-", "").Replace(".", ""));

                                    //GRUPO H - DETALHAMENTO DE PRODUTOS E SERVIÇOS DA NFe
                                    escreverDados.AppendFormat("H|1||\r\n");

                                    double valorUnitario = valorTotal / Convert.ToDouble(dadosLinha[2]);


                                    //GRUPO I - PRODUTOS E SERVIÇOS DA NFe
                                    escreverDados.AppendFormat(
                                        "I|CFOP{0}||PRODUTO SUCATEADO|00000000||{0}|UN|{1}|{2:0.0}|{3:0,00}||UN|{1}|{2:0.00}|||||1||1||\r\n",
                                        cfop, Convert.ToDouble(dadosLinha[2]).ToString("N2").Replace(",", "."), valorUnitario.ToString("N2").Replace(".", "").Replace(",", "."), valorTotal.ToString("N2").Replace(".","").Replace(",", "."));

                                    //GRUPO M - TRIBUTOS INCIDENTES NO PRODUTO OU SERVIÇO
                                    escreverDados.Append("M|\r\n");

                                    //GRUPO N - ICMS NORMAL E ST
                                    escreverDados.Append("N|\r\n");
                                    //GRUPO N10D - REGIME INTEGRALMENTE TRIBUTADO
                                    escreverDados.AppendFormat("N10d|0|102|\r\n"); //CST DO ICMS: 102

                                    //GRUPO Q - PIS
                                    escreverDados.Append("Q|\r\n");

                                    //GRUPO Q - PIS
                                    escreverDados.Append("Q05|99|0.00|\r\n"); //CST 99
                                    escreverDados.Append("Q10|0.00|0.00|\r\n"); //CST 99

                                    //GRUPO S - COFINS
                                    escreverDados.Append("S|\r\n");
                                    escreverDados.Append("S05|99|0.00|0.00|0.00|\r\n"); //CST 99 TIPO CALCULO PERCENTUAL, VALOR DO IMPOSTO ZERAR
                                    escreverDados.Append("S07|0.00|0.00|\r\n"); //CST 99 TIPO CALCULO PERCENTUAL, VALOR DO IMPOSTO ZERAR


                                    //GRUPO W - TOTAL DA NF-E
                                    escreverDados.Append("W|\r\n");
                                    escreverDados.AppendFormat("W02|0.00|0.00|0.00|0.00|0.00|{0:0.00}|0.00|0.00|0.00|0.00|0.00|0.00|0.00|0.00|{0:0.00}|0|\r\n", valorTotal.ToString("N2").Replace(".", "").Replace(",", "."));

                                    //GRUPO X - INFORMAÇÕES DO TRANSPORTE DA NF-E
                                    escreverDados.AppendFormat("X|1|\r\n");

                                    //GRUPO Z - INFORMAÇÕES ADICIONAIS
                                    escreverDados.AppendFormat("Z||REFERENTE AO LOTE {0} DO LEILÃO {1} |\r\n", dadosLinha[0], dataLeilao);

                                    Models.Nfe dadosNota = new Models.Nfe();

                                    dadosNota.ChaveAcesso = chaveAcesso;
                                    dadosNota.CpfDestinatario = dadosLinha[4].ToString().Replace(",", "").Replace(".", "").Replace("-", "").Replace(" ", "");
                                    dadosNota.DataEmissao = DateTime.Now;
                                    dadosNota.DestinatarioNota = dadosLinha[5].ToString();
                                    dadosNota.IdTipoNotaFiscal = 1;
                                    dadosNota.NumeroNota = nUltimaNotaEmitida;
                                    dadosNota.Valor = Convert.ToDouble(dadosLinha[3].ToString().Replace(",", "."));
                                    dadosNota.QtdeProdutos = Convert.ToInt32(dadosLinha[2].ToString());

                                    dalNota.InserirNota(dadosNota);
                                }//Fim do IF da Validação de CPF
                                else
                                {
                                    //Criar txt com CPF Inválidos
                                    if (!string.IsNullOrEmpty(dadosLinha[4].ToString()))
                                    {
                                        escreverCpfInvalidos.AppendFormat("Lote {0} CPF {1}|\r\n", dadosLinha[0].ToString(), dadosLinha[4].ToString());
                                    }
                                    else
                                    {
                                        escreverCpfInvalidos.AppendFormat("Lote {0}|\r\n", dadosLinha[0].ToString());
                                    }
                                }//Fim do ELSE da Validação de CPF
                            }

                            else
                            {
                                //Criar txt com CPF Inválidos
                                if (!string.IsNullOrEmpty(dadosLinha[4].ToString()))
                                {
                                    escreverCpfInvalidos.AppendFormat("Lote {0} CPF {1}|\r\n", dadosLinha[0].ToString(), dadosLinha[4].ToString());
                                }
                                else
                                {
                                    escreverCpfInvalidos.AppendFormat("Lote {0}|\r\n", dadosLinha[0].ToString());
                                }

                            }//Fim do ELSE da Validação de CPF
                        }
                    }

                    //LINHA 1 DO ARQUIVO - CABEÇALHO PADRÃO
                    escreverDados.Insert(0, "NOTA FISCAL|" + numeroNotas + "|\r\n");
                }
            }

            escreverDados.ToString();
            escreverCpfInvalidos.ToString();
            var nomeTxtVendas = string.Format("NFE_VENDAS_LEILAO {0}.txt", dataLeilao);
            var nomeTxtCpf = string.Format("NFE_CPF_INVALIDOS_LEILAO {0}.txt", dataLeilao);
            string pathArquivoNota = Path.Combine(@"C:\Users\joaopedro\Desktop\BDL\Nfe_Eletrobid", nomeTxtVendas);
            string pathArquivoCpf = Path.Combine(@"C:\Users\joaopedro\Desktop\BDL\Nfe_Eletrobid", nomeTxtCpf);


            using (TextWriter tw = new StreamWriter(pathArquivoNota, false))
            {
                tw.Write(escreverDados);
                tw.Close();
            }
            using (TextWriter tw = new StreamWriter(pathArquivoCpf, false))
            {
                tw.Write(escreverCpfInvalidos);
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

