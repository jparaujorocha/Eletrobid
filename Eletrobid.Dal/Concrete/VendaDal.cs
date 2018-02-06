using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class VendaDal : IVendaDal
    {
        private readonly EletrobidEntities _dbContext = new EletrobidEntities();
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public Venda EditaVenda(Venda dadosVenda)
        {
            _dbContext.Venda.Attach(dadosVenda);
            _dbContext.Entry(dadosVenda).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosVenda;
        }

        public void ExcluiVenda(int idVenda)
        {
            var getDadosVenda = GetVenda(idVenda);
            _dbContext.Venda.Remove(getDadosVenda);
            _dbContext.SaveChanges();
        }

        public Venda GetVenda(int idVenda)
        {
            return (from c in _dbContext.Venda where c.IdVenda == idVenda select c).FirstOrDefault();
        }

        public IEnumerable<Venda> ListaVendaCodigoReferencia(int codigoReferenciaVenda, int idEmpresaRevendedora)
        {
            return (from c in _dbContext.Venda where c.CodigoReferenciaVenda == codigoReferenciaVenda && c.IdEmpresaRevendedora == idEmpresaRevendedora select c).ToList();
        }

        public Venda InsereVenda(Venda dadosVenda)
        {
            var getDadosVenda = _dbContext.Venda.Add(dadosVenda);
            _dbContext.SaveChanges();

            return getDadosVenda;
        }

        public IEnumerable<Venda> ListaVendas()
        {
            return (from c in _dbContext.Venda select c).ToList();
        }

        public IEnumerable<Venda> ListaVendasCanceladas()
        {
            return (from c in _dbContext.Venda where c.Cancelada == true select c).ToList();
        }

        public IEnumerable<Venda> ListaVendasData(DateTime dataInicio, DateTime dataFim)
        {
            return (from c in _dbContext.Venda where c.DataVenda >= dataInicio && c.DataVenda <= dataFim select c).ToList();
        }

        public IEnumerable<Venda> ListaVendasNaoCanceladas()
        {
            return (from c in _dbContext.Venda where c.Cancelada == false select c).ToList();
        }

        public IEnumerable<Venda> ListaVendasNumeroLote(int numeroLote, int idEmpresaRevendedora)
        {
            return (from c in _dbContext.Venda where c.NumeroLote == numeroLote && c.IdEmpresaRevendedora == idEmpresaRevendedora select c).ToList();
        }

        public IEnumerable<Venda> ListaVendasProduto(int idProduto)
        {
            return (from c in _dbContext.Venda where c.IdProduto == idProduto select c).ToList();
        }

        public IEnumerable<Venda> ListaVendasRevendedora(int idEmpresaRevendedora)
        {
            return (from c in _dbContext.Venda where c.IdEmpresaRevendedora == idEmpresaRevendedora select c).ToList();
        }
    }
}
