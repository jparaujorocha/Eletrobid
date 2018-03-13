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

        [Display(Name ="Código"), Required(ErrorMessage ="Código Obrigatório")]
        public string CodigoItem { get; set; }

        [Display(Name ="Valor Unitário"),Required(ErrorMessage ="Valor Unitário Obrigatório")]
        public float ValorUnitario { get; set; }

        [Display(Name ="Quantidade"),Required(ErrorMessage ="Quantidade Obrigatória")]
        public int Quantidade { get; set; }

        [Display(Name = "Valor Total"), Required(ErrorMessage = "Valor Total Obrigatório")]
        public float ValorTotal { get; set; }

        [Display(Name ="Identificação da NFe")]
        public string IdentificacaoNfe { get; set; }

        [Display(Name = "Identificação da Empresa Fornecedora"), Required(ErrorMessage = "Campo Obrigatório")]
        public int IdEmpresaFornecedora { get; set; }

        [Display(Name = "Identificação da Empresa Revendedora"), Required(ErrorMessage = "Campo Obrigatório")]
        public int IdEmpresaRevendedora { get; set; }

        [Display(Name = "Data de Entrada"), Required(ErrorMessage = "Data de Entrada Obrigatória")]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "Data de Saída")]
        public DateTime DataSaida { get; set; }

        [Display(Name = "Código Identificador do Produto")]
        public string CodigoIdentificador { get; set; }

        [Display(Name = "Valor Minímo")]
        public float ValorMinimoUnitarioVenda { get; set; }
        
        [Display(Name = "Observação Sobre o Produto")]
        public string Observacao { get; set; }

        [Display(Name ="Tipo do Produto")]
        public int IdTipoProduto { get; set; }

    }
}
