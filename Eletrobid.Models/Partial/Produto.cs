using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models
{
    [MetadataType(typeof(ProdutoMetadata))]
    public partial class Produto
    {

    }
    public class ProdutoMetadata
    {
        [Display(Name ="Nome"),Required(ErrorMessage ="Nome Obrigatório"),StringLength(255,MinimumLength =5)]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name ="Código"), Required(ErrorMessage ="Código do Produto Obrigatório")]
        public string CodigoItem { get; set; }

        [Display(Name ="Quantidade"),Required(ErrorMessage ="Quantidade do Produto Obrigatória")]
        public int Quantidade { get; set; }
        
        [Display(Name = "Empresa Fornecedora"), Required(ErrorMessage = "Informe a empresa")]
        public int IdEmpresaFornecedora { get; set; }

        [Display(Name = "Código Identificador do Produto")]
        public string CodigoIdentificador { get; set; }

        [Display(Name = "Tipo do Produto")]
        public int IdTipoProduto { get; set; }

        [Display(Name = "Identificação da Nfe")]
        public int IdNfe { get; set; }

    }
}
