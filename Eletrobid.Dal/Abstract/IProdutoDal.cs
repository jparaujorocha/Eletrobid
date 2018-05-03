using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Dal
{
    public interface IProdutoDal : IDisposable
    {
        Produto GetProdutoId(int idProduto);

        Produto GetProdutoCodigoItem(string codigoItem);

        Produto GetProdutoCodigoIdentificador(string codigoIdentificador);

        Produto InsereProduto(Produto dadosProduto);

        Produto EditaProduto(Produto dadosProduto);

        void ExcluiProduto(int idProduto);

        IEnumerable<Produto> ListaProdutos();

        IEnumerable<Produto> ListaProdutosFornecedor(int idEmpresaFornecedora);
        

        IEnumerable<Produto> ListaProdutosTipoProduto(int idTipoProduto);

        IEnumerable<Produto> ListaProdutosLoteProduto(int loteProduto);
    }
}
