using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models
{
    [MetadataType(typeof(NfeMetadata))]
    public partial class Nfe
    {

    }
    public class NfeMetadata
    {
        [Display(Name = "Numero da Nota")]
        public int NumeroNota { get; set; }

        [Display(Name = "Destinatário")]
        public string DestinatarioNota { get; set; }

        [Display(Name = "Chave de acesso")]
        public string ChaveAcesso { get; set; }

        [Display(Name = "Data de Emissão")]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Display(Name = "CPF Destinatario")]
        public string CpfDestinatario { get; set; }

        [Display(Name = "CNPJ Destinatario")]
        public string CnpjDestinatario { get; set; }

        [Display(Name = "Tipo")]
        public int IdTipoNotaFiscal { get; set; }

        [Display(Name = "Quantidade de Produtos")]
        public int QtdeProdutos { get; set; }
    }
}
