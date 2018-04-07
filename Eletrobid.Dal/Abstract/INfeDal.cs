using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Dal.Abstract
{
    public interface INfeDal : IDisposable
    {
        Nfe GetNfe(int idNfe);
        int GetNumeroUltimaNfe();
        Nfe GetNfe(string chaveDeAcesso);
        Nfe InserirNota(Nfe dadosNota);
        Nfe EditarNota(Nfe dadosNota);
        IEnumerable<Nfe> ListaNotasCpf(string cpf);
        IEnumerable<Nfe> ListaNotasCnpj(string cnpj);
    }
}
