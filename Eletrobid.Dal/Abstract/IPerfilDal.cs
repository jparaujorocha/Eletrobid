using Eletrobid.Models;
using System;
using System.Collections.Generic;

namespace Eletrobid.Dal
{
    public interface IPerfilDal : IDisposable
    {
        Perfil GetPerfil(int idPerfil);
        
        Perfil InserePerfil(Perfil dadosPerfil);

        Perfil EditaPerfil(Perfil dadosPerfil);

        void ExcluiPerfil(int idPerfil);

    }
}
