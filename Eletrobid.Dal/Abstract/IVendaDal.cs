using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletrobid.Dal
{
    public interface IVendaDal : IDisposable
    {
        Venda GetVenda(int idVenda);        
        Venda InsereVenda(Venda dadosVenda);
        Venda EditaVenda(Venda dadosVenda);
        void ExcluiVenda(int idVenda);
        IEnumerable<Venda> ListaVendas();
        IEnumerable<Venda> ListaVendasCanceladas();
        IEnumerable<Venda> ListaVendaCodigoReferencia(int codigoReferenciaVenda, int idEmpresaRevendedora);
        IEnumerable<Venda> ListaVendasNaoCanceladas();
        IEnumerable<Venda> ListaVendasRevendedora(int idEmpresaRevendedora);
        IEnumerable<Venda> ListaVendasProduto(int idProduto);
        IEnumerable<Venda> ListaVendasNumeroLote(int numeroLote, int idEmpresaRevendedora);
        IEnumerable<Venda> ListaVendasData(DateTime dataInicio, DateTime dataFim);
    }
}
