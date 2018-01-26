using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models.Partial
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

        [Display(Name ="Código do item"), Required(ErrorMessage ="Código Obrigatório")]
        public string CodigoItem { get; set; }

        [Display(Name ="Valor Unitário"),Required(ErrorMessage ="Valor Unitário Obrigatório")]
        public float ValorUnitario { get; set; }

        [Display(Name ="Quantidade"),Required(ErrorMessage ="Quantidade Obrigatória")]
        public int Quantidade { get; set; }

        [Display(Name = "Valor total"), Required(ErrorMessage = "Valor Total Obrigatório")]
        public float ValorTotal { get; set; }

        [Display(Name ="Identificação da NFe")]
        public string IdentificacaoNfe { get; set; }

        [Display(Name = "Identificação da empresa originária"), Required(ErrorMessage = "Campo Obrigatório")]
        public int IdEmpresaOriginaria { get; set; }

        [Display(Name = "Identificação da empresa revendedora"), Required(ErrorMessage = "Campo Obrigatório")]
        public int IdEmpresaRevendedora { get; set; }

        [Display(Name = "Data de Entrada"), Required(ErrorMessage = "Data de Entrada Obrigatória")]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "Data de Saída")]
        public DateTime DataSaida { get; set; }

        [Display(Name = "Data de Venda")]
        public DateTime DataVenda { get; set; }

        [Display(Name = "Valor Unitário do Produto na Venda")]
        public float ValorUnitárioVenda { get; set; }

        [Display(Name = "Valor Total de Venda")]
        public float ValorTotalVenda { get; set; }

        [Display(Name = "Código de Referência da Venda")]
        public string CodigoReferenciaVenda { get; set; }

        [Display(Name = "Código identificador do Produto")]
        public string CodigoIdentificador { get; set; }

        [Display(Name = "Valor Minímo Unitário do Produto")]
        public float ValorMinimoUnitarioVenda { get; set; }
        
        [Display(Name = "Observação sobre o Produto")]
        public string Observacao { get; set; }

        [Display(Name = "Observação sobre a venda do Produto")]
        public string ObservacaoVenda { get; set; }

        [Display(Name ="Tipo do Produto")]
        public int IdTipoProduto { get; set; }

    }
}
