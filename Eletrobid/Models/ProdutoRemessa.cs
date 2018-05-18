using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eletrobid.Models
{
    public class ProdutoRemessa
    {
        [Display(Name = "IdProduto")]
        public int IdProduto { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }
    }
}