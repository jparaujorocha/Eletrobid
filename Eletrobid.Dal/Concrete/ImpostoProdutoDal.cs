using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class ImpostoProdutoDal : IImpostoProdutoDal
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

        public ImpostoProduto EditaImpostoProduto(ImpostoProduto dadosImpostoProduto)
        {
            _dbContext.ImpostoProduto.Attach(dadosImpostoProduto);
            _dbContext.Entry(dadosImpostoProduto).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosImpostoProduto;
        }

        public void ExcluiImpostoProduto(int idImpostoProduto)
        {
            var dadosImpostoProduto = GetImpostoProduto(idImpostoProduto);
            _dbContext.ImpostoProduto.Remove(dadosImpostoProduto);
            _dbContext.SaveChanges();
        }

        public ImpostoProduto GetImpostoProduto(int idImpostoProduto)
        {
            return (from c in _dbContext.ImpostoProduto where c.IdImpostoProduto == idImpostoProduto select c).FirstOrDefault();
        }

        public ImpostoProduto InsereImpostoProduto(ImpostoProduto dadosImpostoProduto)
        {
            var getDadosImpostoProduto = _dbContext.ImpostoProduto.Add(dadosImpostoProduto);
            _dbContext.SaveChanges();

            return getDadosImpostoProduto;
        }

        public IEnumerable<ImpostoProduto> ListaImpostoProduto()
        {
            return (from c in _dbContext.ImpostoProduto select c).ToList();
        }

        public IEnumerable<ImpostoProduto> ListaImpostoProduto(int idProduto)
        {
            return (from c in _dbContext.ImpostoProduto where c.IdProduto == idProduto select c).ToList();
        }

        public IEnumerable<ImpostoProduto> ListaImpostoProdutoImposto(int idImposto)
        {
            return (from c in _dbContext.ImpostoProduto where c.IdImposto == idImposto select c).ToList();
        }
    }
}
