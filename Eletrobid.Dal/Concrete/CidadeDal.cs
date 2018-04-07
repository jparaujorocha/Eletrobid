using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal.Concrete
{
    public class CidadeDal : ICidadeDal
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

        public Cidade getCidade(string nome)
        {
            return (from c in _dbContext.Cidade where c.Nome == nome select c).FirstOrDefault();
        }

        public Cidade getCidade(int idCidade)
        {
            return (from c in _dbContext.Cidade where c.CidadeId == idCidade select c).FirstOrDefault();
        }

        public string getCodigoIbge(int idCidade)
        {
            var cidade = (from c in _dbContext.Cidade where c.CidadeId == idCidade select c).FirstOrDefault();
            if (cidade != null)
            {
                return cidade.CodigoIbge;
            }
            else
            {
                return null;
            }

        }

        public string getCodigoIbge(string nome)
        {
            var cidade = (from c in _dbContext.Cidade where c.Nome == nome select c).FirstOrDefault();
            if (cidade != null)
            {
                return cidade.CodigoIbge;
            }
            else
            {
                return null;
            }
        }
    }
}
