﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EletrobidEntities : DbContext
    {
        public EletrobidEntities()
            : base("name=EletrobidEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Imposto> Imposto { get; set; }
        public virtual DbSet<ImpostoProduto> ImpostoProduto { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<ProdutoLote> ProdutoLote { get; set; }
        public virtual DbSet<TipoEmpresa> TipoEmpresa { get; set; }
        public virtual DbSet<TipoImposto> TipoImposto { get; set; }
        public virtual DbSet<TipoProduto> TipoProduto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
    }
}
