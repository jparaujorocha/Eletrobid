using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Eletrobid.Models
{
    public class NfeViewModels
    {


        [Display(Name = "Numero da Nota"), Required(ErrorMessage = "Campo Obrigatório")]
        public int NumeroNota { get; set; }

        [Display(Name = "Destinatário")]
        public string DestinatarioNota { get; set; }

        [Display(Name = "Chave de acesso"), Required(ErrorMessage = "Campo Obrigatório")]
        public string ChaveAcesso { get; set; }

        [Display(Name = "Data de Emissão"), Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Valor"), Required(ErrorMessage = "Campo Obrigatório")]
        public double Valor { get; set; }

        [Display(Name = "CPF Destinatario"), Required(ErrorMessage = "Campo Obrigatório")]
        public string CpfDestinatario { get; set; }

        [Display(Name = "CNPJ Destinatario"), Required(ErrorMessage = "Campo Obrigatório")]
        public string CnpjDestinatario { get; set; }

        [Display(Name = "Descricao Produtos"), Required(ErrorMessage = "Campo Obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "Nome do Cliente"), Required(ErrorMessage = "Campo Obrigatório")]
        public string NomeCliente { get; set; }

        [Display(Name = "Endereço"), Required(ErrorMessage = "Campo Obrigatório")]
        public string Endereco { get; set; }

        [Display(Name = "Numero"), Required(ErrorMessage = "Campo Obrigatório")]
        public int Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Bairro"), Required(ErrorMessage = "Campo Obrigatório")]
        public string Bairro { get; set; }

        [Display(Name = "CEP"), Required(ErrorMessage = "Campo Obrigatório")]
        public string Cep { get; set; }

        [Display(Name = "Cidade"), Required(ErrorMessage = "Campo Obrigatório")]
        public string IdCidade { get; set; }

        [Display(Name = "Estado"), Required(ErrorMessage = "Campo Obrigatório")]
        public string IdEstado { get; set; }

        [Display(Name = "CPF"), Required(ErrorMessage = "Campo Obrigatório")]
        public string Cpf { get; set; }

        [Display(Name = "Tipo"), Required(ErrorMessage = "Campo Obrigatório")]
        public int IdTipoNotaFiscal { get; set; }

        [Display(Name = "Quantidade de Produtos"), Required(ErrorMessage = "Campo Obrigatório")]
        public int QtdeProdutos { get; set; }

        [Display(Name = "Lista para remessa"), Required(ErrorMessage = "Campo Obrigatório")]
        public List<ProdutoRemessa> ListaRemessa { get; set; }
    }
}