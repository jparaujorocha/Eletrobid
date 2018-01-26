using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Models.Partial
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {

    }
    public class UsuarioMetadata
    {
        private const string _emailPattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        public int IdUsuario { get; set; }

        [Display(Name = "Nome"), Required(ErrorMessage = "Nome obrigatório"), StringLength(255, MinimumLength = 5)]
        public string Nome { get; set; }

        [Display(Name = "Apelido"), Required(ErrorMessage = "Apelido obrigatório"), StringLength(255, MinimumLength = 5)]
        public string Apelido { get; set; }

        [Display(Name = "E-mail"), Required(ErrorMessage = "Email obrigatório"), StringLength(255, MinimumLength = 10)]
        [RegularExpression(_emailPattern, ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Display(Name = "Senha"), Required(ErrorMessage = "Senha obrigatório"), StringLength(255, MinimumLength = 8)]
        public string Senha { get; set; }


        [Display(Name = "Perfil")]
        public int IdPerfil { get; set; }
    }
}
