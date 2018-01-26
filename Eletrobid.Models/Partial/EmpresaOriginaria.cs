using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models.Partial
{
    [MetadataType(typeof(EmpresaOriginariaMetadata))]
    public partial class EmpresaOriginaria
    {
    }
    public class EmpresaOriginariaMetadata
    {
        [Display(Name = "Identificação")]
        public int IdEmpresaOriginaria { get; }

        [Display(Name = "Razão Social"), Required(ErrorMessage = "Razão Social Obrigatória"), StringLength(255, MinimumLength = 10)]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome Fantasia"), Required(ErrorMessage = "Nome Fantasia Obrigatório"), StringLength(255, MinimumLength = 10)]
        public string NomeFantasia { get; set; }

        [Display(Name = "CNPJ"), Required(ErrorMessage = "CNPJ Obrigatório"), StringLength(14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Display(Name = "Telefone Contato 1"), Required(ErrorMessage = "Telefone de Contato Obrigatório"), StringLength(18, MinimumLength = 11)]
        public string TelefoneContato1 { get; set; }

        [Display(Name = "Telefone Contato 2"), StringLength(18, MinimumLength = 11)]
        public string TelefoneContato2 { get; set; }

        [Display(Name = "Margem de lucro")]
        public float MargemLucro { get; set; }

        [Display(Name ="Endereço"), Required(ErrorMessage ="Endereço da empresa obrigatório"), StringLength(255,MinimumLength = 10)]
        public string Endereco { get; set; }

        [Display(Name ="CEP"), Required(ErrorMessage ="CEP obrigatório"), StringLength(9,MinimumLength =8)]
        public string Cep { get; set; }

    }
}
