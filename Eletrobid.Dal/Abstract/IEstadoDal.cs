using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Dal.Abstract
{
    public interface IEstadoDal : IDisposable
    {
        Estado getEstado(int idEstado);
        Estado getEstado(string sigla);
        string getCodigoIbge(string sigla);
        string getCodigoIbge(int idEstado);
    }
}
