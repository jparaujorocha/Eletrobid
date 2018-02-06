using Eletrobid.Models;
using System;
using System.Collections.Generic;

namespace Eletrobid.Dal
{
    interface IProdutoLoteDal
    {
        ProdutoLote GetProdutoLote(int idProdutoLote);
        
        ProdutoLote InsereProdutoLote(ProdutoLote dadosProdutoLote);

        ProdutoLote EditaProdutoLote(ProdutoLote dadosProdutoLote);
        
        void ExcluiProdutoLote(int idProdutoLote);

        IEnumerable<ProdutoLote> ListaProdutoLote();

        IEnumerable<ProdutoLote> ListaProdutoLoteNLote(int numeroLote);

        IEnumerable<ProdutoLote> ListaProdutoLoteIdProduto(int idProduto);

        IEnumerable<ProdutoLote> ListaProdutoLoteRevendedora(int idEmpresaRevendedora);
    }
}
