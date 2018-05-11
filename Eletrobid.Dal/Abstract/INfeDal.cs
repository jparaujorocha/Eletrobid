using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Dal
{
    public interface INfeDal : IDisposable
    {
        Nfe GetNfe(int idNfe);
        int GetNumeroUltimaNfe();
        int GetQuantidadeRemessaProduto(int idProduto);
        Nfe GetNfe(string chaveDeAcesso);
        Nfe InserirNota(Nfe dadosNota);
        Nfe EditarNota(Nfe dadosNota);
        IEnumerable<Nfe> ListaNotasEntrada();
        IEnumerable<Nfe> ListaNotasRemessa();
        IEnumerable<Remessa> GetRemessa(int? idNfeRemessa, int? idProduto, int? idEmpresaRecebedora);
        IEnumerable<Nfe> ListaNotas();
        IEnumerable<Nfe> ListaNotasCpf(string cpf);
        IEnumerable<Nfe> ListaNotasCnpj(string cnpj);
        IEnumerable<TipoNotaFiscal> ListaTipoNfe();
        bool ExisteNota(int idNfe);
        void AtualizaQtde(int idNfe);
    }
}
