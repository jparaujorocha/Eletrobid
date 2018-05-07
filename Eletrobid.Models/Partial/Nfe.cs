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
        [Display(Name = "NumeroNota")]
        public int NumeroNota { get; set; }

        [Display(Name = "Destinatário")]
        public string DestinatarioNota { get; set; }

        [Display(Name = "Chave de acesso")]
        public string ChaveAcesso { get; set; }

        public DateTime DataEmissao { get; set; }

        public double Valor { get; set; }

        public string CpfDestinatario { get; set; }

        public string CnpjDestinatario { get; set; }

        public int IdTipoNotaFiscal { get; set; }

        public int QtdeProdutos { get; set; }
    }
}
