using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Eletrobid.Models
{
    public class NfeViewModels
    {


        [Display(Name = "Numero da Nota")]
        public int NumeroNota { get; set; }

        [Display(Name = "Destinatário")]
        public string DestinatarioNota { get; set; }

        [Display(Name = "Nome do Remetente")]
        public string RemetenteNota { get; set; }

        [Display(Name = "Chave de acesso")]
        public string ChaveAcesso { get; set; }

        [Display(Name = "Data de Emissão")]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Display(Name = "CPF Destinatario")]
        public string CpfDestinatario { get; set; }

        [Display(Name = "CNPJ Destinatario")]
        public string CnpjDestinatario { get; set; }

        [Display(Name = "CPF Remetente")]
        public string CpfRemetente{ get; set; }

        [Display(Name = "CNPJ Remetente")]
        public string CnpjRemetente { get; set; }

        [Display(Name = "Descricao Produtos")]
        public string Descricao { get; set; }

        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Numero")]
        public int Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Display(Name = "Cidade")]
        public int IdCidade { get; set; }

        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Display(Name = "Estado")]
        public string SiglaEstado { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "Tipo")]
        public int IdTipoNotaFiscal { get; set; }

        [Display(Name = "Quantidade de Produtos")]
        public int QtdeProdutos { get; set; }

        [Display(Name = "Lista para remessa")]
        public List<ProdutoRemessa> ListaRemessa { get; set; }
    }
}