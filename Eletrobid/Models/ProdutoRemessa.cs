using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eletrobid.Models
{
    public class ProdutoRemessa
    {        
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}