using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Dal
{
    public interface ICidadeDal : IDisposable
    {
        Cidade getCidade(int idCidade);
        Cidade getCidade(string nome);
        string getCodigoIbge(string nome);
        string getCodigoIbge(int idCidade);
        IEnumerable<Cidade> ListaCidades();
        IEnumerable<Cidade> ListaCidades(int idEstado);
    }
}
