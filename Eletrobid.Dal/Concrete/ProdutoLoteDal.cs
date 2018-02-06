using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class ProdutoLoteDal : IProdutoLoteDal
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
        public ProdutoLote EditaProdutoLote(ProdutoLote dadosProduto)
        {
            _dbContext.ProdutoLote.Attach(dadosProduto);
            _dbContext.Entry(dadosProduto).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosProduto;
        }

        public void ExcluiProdutoLote(int idProdutoLote)
        {
            var getDadosProdutoLote = GetProdutoLote(idProdutoLote);
            _dbContext.ProdutoLote.Remove(getDadosProdutoLote);
            _dbContext.SaveChanges();
        }

        public ProdutoLote GetProdutoLote(int idProdutoLote)
        {
            return (from c in _dbContext.ProdutoLote where c.IdProdutoLote == idProdutoLote select c).FirstOrDefault();
        }

        public ProdutoLote InsereProdutoLote(ProdutoLote dadosProdutoLote)
        {
            var getDadosProdutoLote = _dbContext.ProdutoLote.Add(dadosProdutoLote);
            _dbContext.SaveChanges();

            return getDadosProdutoLote;
        }

        public IEnumerable<ProdutoLote> ListaProdutoLote()
        {
            return (from c in _dbContext.ProdutoLote select c).ToList();
        }

        public IEnumerable<ProdutoLote> ListaProdutoLoteIdProduto(int idProduto)
        {
            return (from c in _dbContext.ProdutoLote where c.IdProduto == idProduto select c).ToList();
        }

        public IEnumerable<ProdutoLote> ListaProdutoLoteNLote(int numeroLote)
        {
            return (from c in _dbContext.ProdutoLote where c.NumeroLote == numeroLote select c).ToList();
        }

        public IEnumerable<ProdutoLote> ListaProdutoLoteRevendedora(int idEmpresaRevendedora)
        {
            return (from c in _dbContext.ProdutoLote where c.IdEmpresaRevendedora == idEmpresaRevendedora select c).ToList();
        }
    }
}
