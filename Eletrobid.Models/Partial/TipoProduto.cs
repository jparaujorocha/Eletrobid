using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models.Partial
{
    [MetadataType(typeof(TipoProdutoMetadata))]
    public partial class TipoProduto
    {

    }
    public class TipoProdutoMetadata
    {
        public int IdTipoProduto { get; set; }

        [Display(Name = "Nome"), Required(ErrorMessage = "Nome obrigatório"), StringLength(255, MinimumLength = 5)]
        public string Nome { get; set; }
    }
}
