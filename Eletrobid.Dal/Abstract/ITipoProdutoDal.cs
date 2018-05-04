using Eletrobid.Models;
using System;
using System.Collections.Generic;

namespace Eletrobid.Dal
{
    public interface ITipoProdutoDal : IDisposable
    {
        TipoProduto GetTipoProduto(int idTipoProduto);
        
        TipoProduto InsereTipoProduto(TipoProduto dadosTipoProduto);

        TipoProduto EditaTipoProduto(TipoProduto dadosTipoProduto);

        IEnumerable<TipoProduto> ListaTipoProduto();

        void ExcluiTipoProduto(int idTipoProduto);

    }
}
