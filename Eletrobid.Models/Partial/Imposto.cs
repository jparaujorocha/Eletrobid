using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models
{
    [MetadataType(typeof(ImpostoMetadata))]
    public partial class Imposto
    {
    }
    public class ImpostoMetadata
    {
        [Display(Name="Identificação")]
        public int IdImposto { get; }

        [Display(Name ="Nome"),Required(ErrorMessage ="Nome Obrigatório"),StringLength(255,MinimumLength = 5)]
        public string Nome { get; set; }

        [Display(Name ="Valor"),Required(ErrorMessage ="Valor Obrigatório")]
        public float Valor { get; set; }

        [Display(Name ="Data de início do imposto"),Required(ErrorMessage ="Data Obrigatória")]
        public DateTime DataInicioCobranca { get; set; }

        [Display(Name ="Observação")]
        public string Observacao { get; set; }
    }
}
