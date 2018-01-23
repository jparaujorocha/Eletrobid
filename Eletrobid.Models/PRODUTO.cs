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
    
    public partial class PRODUTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUTO()
        {
            this.IMPOSTOPRODUTOes = new HashSet<IMPOSTOPRODUTO>();
        }
    
        public int IDPRODUTO { get; set; }
        public string NOME { get; set; }
        public string DESCRICAO { get; set; }
        public string CODIGOITEM { get; set; }
        public double VALORUNITARIO { get; set; }
        public int QUANTIDADE { get; set; }
        public double VALORTOTAL { get; set; }
        public string IDENTIFICACAONFE { get; set; }
        public int IDEMPRESAORIGINARIA { get; set; }
        public Nullable<int> IDEMPRESAREVENDEDORA { get; set; }
        public System.DateTime DATAENTRADA { get; set; }
        public Nullable<System.DateTime> DATASAIDA { get; set; }
        public Nullable<System.DateTime> DATAVENDA { get; set; }
        public Nullable<double> VALORUNITARIOVENDA { get; set; }
        public Nullable<double> VALORTOTALVENDA { get; set; }
        public string CODIGOREFERENCIAVENDA { get; set; }
        public string CODIGOIDENTIFICADOR { get; set; }
        public Nullable<double> VALORMINIMOUNITARIOVENDA { get; set; }
        public string OBSERVACAO { get; set; }
        public string OBSERVACAOVENDA { get; set; }
        public int IDTIPOPRODUTO { get; set; }
    
        public virtual EMPRESAORIGINARIA EMPRESAORIGINARIA { get; set; }
        public virtual EMPRESAREVENDEDORA EMPRESAREVENDEDORA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMPOSTOPRODUTO> IMPOSTOPRODUTOes { get; set; }
        public virtual TIPOPRODUTO TIPOPRODUTO { get; set; }
    }
}
