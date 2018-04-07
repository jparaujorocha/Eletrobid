using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models
{
    [MetadataType(typeof(ImpostoProdutoMetadata))]
    public partial class ImpostoProduto
    {

    }
    public class ImpostoProdutoMetadata
    {
        [Display(Name ="Identificação")]
        public int IdImpostoProduto { get; set; }

        [Display(Name ="Identificação do produto"),Required(ErrorMessage ="Produto obrigatório")]
        public int IdProduto { get; set; }

        [Display(Name ="Identificação do imposto"), Required(ErrorMessage = "Imposto obrigatório")]
        public int IdImposto { get; set; }

        [Display(Name ="Valor a ser pago(R$)")]
        public float ValorPagar { get; set; }
    }
}
