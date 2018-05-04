using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class TipoProdutoDal : ITipoProdutoDal
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

        public TipoProduto EditaTipoProduto(TipoProduto dadosTipoProduto)
        {
            _dbContext.TipoProduto.Attach(dadosTipoProduto);
            _dbContext.Entry(dadosTipoProduto).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosTipoProduto;
        }

        public IEnumerable<TipoProduto> ListaTipoProduto()
        {
            return (from c in _dbContext.TipoProduto select c).ToList();
        }

        public void ExcluiTipoProduto(int idTipoProduto)
        {
            var dadosTipoProduto = GetTipoProduto(idTipoProduto);
            _dbContext.TipoProduto.Remove(dadosTipoProduto);
            _dbContext.SaveChanges();
        }

        public TipoProduto GetTipoProduto(int idTipoProduto)
        {
            return (from c in _dbContext.TipoProduto where c.IdTipoProduto == idTipoProduto select c).FirstOrDefault();
        }

        public TipoProduto InsereTipoProduto(TipoProduto dadosTipoProduto)
        {
            var getDadosTipoProduto = _dbContext.TipoProduto.Add(dadosTipoProduto);
            _dbContext.SaveChanges();
            return getDadosTipoProduto;
        }
    }
}
