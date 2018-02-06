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
    
    public partial class Empresa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empresa()
        {
            this.Produto = new HashSet<Produto>();
            this.Produto1 = new HashSet<Produto>();
            this.ProdutoLote = new HashSet<ProdutoLote>();
            this.Venda = new HashSet<Venda>();
        }
    
        public int IdEmpresa { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string TelefoneContato1 { get; set; }
        public string TelefoneContato2 { get; set; }
        public Nullable<double> MargemLucro { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public bool EmpresaLeiloeira { get; set; }
        public int IdTipoEmpresa { get; set; }
    
        public virtual TipoEmpresa TipoEmpresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto> Produto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto> Produto1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdutoLote> ProdutoLote { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venda> Venda { get; set; }
    }
}