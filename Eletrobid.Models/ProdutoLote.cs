//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eletrobid.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProdutoLote
    {
        public int IdProdutoLote { get; set; }
        public int IdProduto { get; set; }
        public int NumeroLote { get; set; }
        public int QuantidadeProduto { get; set; }
        public double ValorMinimoUniVenda { get; set; }
        public int IdEmpresaRevendedora { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
