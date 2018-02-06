using Eletrobid.Models;
using System;
using System.Collections.Generic;

namespace Eletrobid.Dal
{
    public interface IImpostoDal : IDisposable
    {
        Imposto GetImposto(int idImposto);

        Imposto InsereImposto(Imposto dadosImposto);

        Imposto EditaImposto(Imposto dadosImposto);

        void ExcluiImposto(int idImposto);

        IEnumerable<Imposto> ListaImpostos();

        IEnumerable<Imposto> ListaImpostos(int idTipoImposto);

        IEnumerable<Imposto> ListaImpostos(DateTime DataInicioCobranca);
    }
}
