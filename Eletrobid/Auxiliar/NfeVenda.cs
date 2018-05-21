using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Eletrobid.Auxiliar
{
    public class NfeVenda
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

        public Nfe emitirTxtNfe(NfeViewModels dadosNfe, string codigoIbgeEstado, string codigoIbgeCidade, int ultimaNota)
        {
            if (Util.ValidaCPF(dadosNfe.CpfDestinatario.ToString().Replace(".", "").Replace("-", "")) == true)
            {
                StringBuilder escreverDados = new StringBuilder();
                Random rndNumero = new Random();
                double digitoVerificador = 0;
                int nUltimaNotaEmitida = ultimaNota;
                nUltimaNotaEmitida++;
                string codigoEstado = codigoIbgeEstado;
                string codigoCidade = codigoIbgeCidade;
                var mesEmissao = DateTime.Now.Month.ToString("d2");
                var anoEmissao = DateTime.Now;
                var codigoNfe = rndNumero.Next(10000000, 99999999);
                double valorTotal = Convert.ToDouble(dadosNfe.Valor);

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

                var cfop = dadosNfe.SiglaEstado.ToString() == "MG" ? 5102 : 6102;

                //LINHA 1 DO ARQUIVO - CABEÇALHO PADRÃO
                escreverDados.Insert(0, "NOTA FISCAL|" + 1 + "|\r\n");
                //GRUPO "A" DO ARQUIVO - PADRÃO
                escreverDados.AppendFormat("A|3.10|NFe{0}\r\n", chaveAcesso);

                //GRUPO "B" DO ARQUIVO - INFORMAÇÕES DE IDENTIFICAÇÃO DA NFe
                var naturezaOperacao = "VENDA ARREMATE LEILÃO";
                var idDestino = dadosNfe.SiglaEstado.ToString() == "MG" ? 1 : 2;
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
                escreverDados.AppendFormat("E|{0}|9|||||\r\n", dadosNfe.NomeCliente.ToString().ToUpper());

                //GRUPO E02 - CPF DO DESTINATÁRIO DA NFe
                escreverDados.AppendFormat("E03|{0}|\r\n", dadosNfe.Cpf.ToString().Replace(".", "").Replace("-", ""));

                //GRUPO E05 - ENDEREÇO DO DESTINATÁRIO DA NFe
                escreverDados.AppendFormat("E05|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|1058|BRASIL||\r\n", dadosNfe.Endereco.ToString().ToUpper(),
                    Convert.ToInt32(dadosNfe.Numero).ToString(), dadosNfe.Complemento.ToString() == "-" ? "" : dadosNfe.Complemento.ToString(), dadosNfe.Bairro.ToString(), codigoCidade,
                    dadosNfe.Cidade.ToString(), dadosNfe.SiglaEstado.ToString(), dadosNfe.Cep.ToString().Replace("-", "").Replace(".", ""));

                //GRUPO H - DETALHAMENTO DE PRODUTOS E SERVIÇOS DA NFe
                escreverDados.AppendFormat("H|1||\r\n");

                double valorUnitario = valorTotal / Convert.ToDouble(dadosNfe.QtdeProdutos);


                //GRUPO I - PRODUTOS E SERVIÇOS DA NFe
                escreverDados.AppendFormat(
                    "I|CFOP{0}||PRODUTO SUCATEADO|00000000||{0}|UN|{1}|{2:0.0}|{3:0,00}||UN|{1}|{2:0.00}|||||1||1||\r\n",
                    cfop, Convert.ToDouble(dadosNfe.QtdeProdutos).ToString("N2").Replace(",", "."), valorUnitario.ToString("N2").Replace(".", "").Replace(",", "."), valorTotal.ToString("N2").Replace(".", "").Replace(",", "."));

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
                escreverDados.AppendFormat("Z||REFERENTE AO DIA  |\r\n", dadosNfe.DataEmissao);

                Nfe dadosNota = new Nfe();
                dadosNota.ChaveAcesso = chaveAcesso;
                dadosNota.DataEmissao = Convert.ToDateTime(dataEmissao);
                dadosNota.RemetenteNota = emitente;
                dadosNota.CnpjRemetente = CnpjEmitente;
                dadosNota.NumeroNota = nUltimaNotaEmitida;

                escreverDados.ToString();
                var nomeTxtVendas = string.Format("NFE_VENDAS_NUMERO_NOTA_ {0}.txt", nUltimaNotaEmitida);
                string pathArquivoNota = Path.Combine(@"C:\Users\joaopedro\Desktop\BDL\Nfe_Eletrobid", nomeTxtVendas);
                
                using (TextWriter tw = new StreamWriter(pathArquivoNota, false))
                {
                    tw.Write(escreverDados);
                    tw.Close();
                }

                return dadosNota;
            }
            else
            {
                return null;
            }

        }
    }
}