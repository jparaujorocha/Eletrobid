using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models
{
    [MetadataType(typeof(CidadeMetadata))]
    public partial class Cidade
    {

    }
    public class CidadeMetadata
    {
        [Display(Name = "IdTipoNotaFiscal")]
        public int CidadeId { get; }

        [Display(Name = "Nome")]
        public string Nome { get; }

        [Display(Name = "Estado")]
        public int EstadoId { get; }

        [Display(Name = "Capital")]
        public bool Capital { get; }

        [Display(Name = "Código IBGE")]
        public string CodigoIbge { get; }
    }
}
