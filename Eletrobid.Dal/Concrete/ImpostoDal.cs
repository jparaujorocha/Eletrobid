using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class ImpostoDal : IImpostoDal
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

        public Imposto EditaImposto(Imposto dadosImposto)
        {
            _dbContext.Imposto.Attach(dadosImposto);
            _dbContext.Entry(dadosImposto).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosImposto;
        }

        public void ExcluiImposto(int idImposto)
        {
            var dadosImposto = GetImposto(idImposto);
            _dbContext.Imposto.Remove(dadosImposto);
            _dbContext.SaveChanges();
        }

        public Imposto GetImposto(int idImposto)
        {
            return (from c in _dbContext.Imposto where c.IdImposto == idImposto select c).FirstOrDefault();
        }

        public Imposto InsereImposto(Imposto dadosImposto)
        {
            var getDadosImposto = _dbContext.Imposto.Add(dadosImposto);
            _dbContext.SaveChanges();
            return getDadosImposto;
        }

        public IEnumerable<Imposto> ListaImpostos()
        {
            return (from c in _dbContext.Imposto select c).ToList();
        }

        public IEnumerable<Imposto> ListaImpostos(DateTime DataInicioCobranca)
        {
            return (from c in _dbContext.Imposto where c.DataInicioCobranca == DataInicioCobranca select c).ToList();
        }

        public IEnumerable<Imposto> ListaImpostos(int idTipoImposto)
        {
            return (from c in _dbContext.Imposto where c.IdTipoImposto == idTipoImposto select c).ToList();
        }
    }
}
