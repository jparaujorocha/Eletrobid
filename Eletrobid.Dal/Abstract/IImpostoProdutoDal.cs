using Eletrobid.Models;
using System;
using System.Collections.Generic;

namespace Eletrobid.Dal
{
    public interface IImpostoProdutoDal : IDisposable
    {
        ImpostoProduto GetImpostoProduto(int idImpostoProduto);

        ImpostoProduto InsereImpostoProduto(ImpostoProduto dadosImpostoProduto);

        ImpostoProduto EditaImpostoProduto(ImpostoProduto dadosImpostoProduto);

        void ExcluiImpostoProduto(int idImpostoProduto);

        IEnumerable<ImpostoProduto> ListaImpostoProduto();

        IEnumerable<ImpostoProduto> ListaImpostoProdutoImposto(int idImposto);

        IEnumerable<ImpostoProduto> ListaImpostoProduto(int idProduto);

    }
}
