using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models.Partial
{
    [MetadataType(typeof(EstadoMetadata))]
    public partial class Estado
    {

    }
    public class EstadoMetadata
    {
        [Display(Name = "IdTipoNotaFiscal")]
        public int EstadoId { get; }

        [Display(Name = "Sigla")]
        public string Sigla { get; }
        
        [Display(Name = "Nome")]
        public string Nome { get; }

        [Display(Name = "Código IBGE")]
        public string CodigoIbge { get; }
    }
}
