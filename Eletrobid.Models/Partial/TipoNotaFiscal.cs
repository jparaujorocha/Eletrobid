using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models
{
    [MetadataType(typeof(TipoNotaFiscalMetadata))]
    public partial class TipoNotaFiscal
    {

    }
    public class TipoNotaFiscalMetadata
    {
        [Display(Name = "IdTipoNotaFiscal")]
        public int IdTipoNotaFiscal { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
