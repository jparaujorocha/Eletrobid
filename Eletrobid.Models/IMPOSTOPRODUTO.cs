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
    
    public partial class ImpostoProduto
    {
        public int IdImpostoProduto { get; set; }
        public int IdProduto { get; set; }
        public int IdImposto { get; set; }
        public double ValorPagar { get; set; }
    
        public virtual Imposto Imposto { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
