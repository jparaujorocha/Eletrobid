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
    
    public partial class Nfe
    {
        public int IdNfe { get; set; }
        public int NumeroNota { get; set; }
        public string DestinatarioNota { get; set; }
        public string ChaveAcesso { get; set; }
        public System.DateTime DataEmissao { get; set; }
        public double Valor { get; set; }
        public string CpfDestinatario { get; set; }
        public string CnpjDestinatario { get; set; }
        public int IdTipoNotaFiscal { get; set; }
        public int QtdeProdutos { get; set; }
    
        public virtual TipoNotaFiscal TipoNotaFiscal { get; set; }
    }
}
