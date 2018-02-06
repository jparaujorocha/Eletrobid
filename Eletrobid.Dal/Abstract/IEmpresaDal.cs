using Eletrobid.Models;
using System;
using System.Collections.Generic;

namespace Eletrobid.Dal
{
    public interface IEmpresaDal : IDisposable
    {
        Empresa GetEmpresa(int idEmpresa);

        Empresa GetEmpresa(string cnpj);

        bool ExisteEmpresa(string cnpj, int idTipoEmpresa);
                
        Empresa InsereEmpresa(Empresa dadosEmpresa);

        Empresa EditaEmpresa(Empresa dadosEmpresa);

        void ExcluiEmpresa(int idEmpresa);

        IEnumerable<Empresa> ListaEmpresas();

        IEnumerable<Empresa> ListaEmpresas(int idTipoEmpresa);
        
    }
}
