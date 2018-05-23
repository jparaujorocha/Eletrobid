using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models
{
    [MetadataType(typeof(EmpresaMetadata))]
    public partial class Empresa
    {

    }

    public class EmpresaMetadata
    {
        [Display(Name = "Razão Social"), Required(ErrorMessage = "Razão Social Obrigatória"), StringLength(255, MinimumLength = 10)]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome Fantasia"), Required(ErrorMessage = "Nome Fantasia Obrigatório"), StringLength(255, MinimumLength = 10)]
        public string NomeFantasia { get; set; }

        [Display(Name = "CNPJ"), Required(ErrorMessage = "CNPJ Obrigatório"), StringLength(14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Display(Name = "Telefone de Contato 1"), Required(ErrorMessage = "Telefone de Contato Obrigatório"), StringLength(18, MinimumLength = 10)]
        public string TelefoneContato1 { get; set; }

        [Display(Name = "Telefone de Contato 2"), StringLength(18, MinimumLength = 11)]
        public string TelefoneContato2 { get; set; }

        [Display(Name = "Margem de lucro")]
        public float MargemLucro { get; set; }

        [Display(Name = "Logradouro(Rua, Av, etc)"), Required(ErrorMessage = "Endereço da empresa obrigatório"), StringLength(255)]
        public string Endereco { get; set; }

        [Display(Name = "Número"), Required(ErrorMessage = "Número do endereço da empresa obrigatório")]
        public int Numero { get; set; }

        [Display(Name = "Bairro"), Required(ErrorMessage = "Bairro da empresa obrigatório"), StringLength(255)]
        public string Bairro { get; set; }

        [Display(Name = "Complemento"), StringLength(50)]
        public string Complemento { get; set; }

        [Display(Name = "CEP"), Required(ErrorMessage = "CEP obrigatório"), StringLength(9, MinimumLength = 8)]
        public string Cep { get; set; }

        [Display(Name = "Empresa Leiloeira"), Required(ErrorMessage = "Campo obrigatório")]
        public bool EmpresaLeiloeira { get; set; }

        [Display(Name = "Tipo de Empresa"), Required(ErrorMessage = "Campo obrigatório")]
        public int IdTipoEmpresa { get; set; }

        [Display(Name = "Estado"), Required(ErrorMessage = "Campo obrigatório")]
        public int EstadoId { get; set; }

        [Display(Name = "Cidade"), Required(ErrorMessage = "Campo obrigatório")]
        public int CidadeId { get; set; }
    }
}
