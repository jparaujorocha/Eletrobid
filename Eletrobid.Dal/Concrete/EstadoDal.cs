using Eletrobid.Dal.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal.Concrete
{
    public class EstadoDal : IEstadoDal
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

        public string getCodigoIbge(int idEstado)
        {
            var estado = (from c in _dbContext.Estado where c.EstadoId == idEstado select c).FirstOrDefault();
            if(estado != null)
            {
                return estado.CodigoIbge;
            }
            else
            {
                return null;
            }
        }

        public string getCodigoIbge(string sigla)
        {
            var estado = (from c in _dbContext.Estado where c.Sigla == sigla select c).FirstOrDefault();
            if (estado != null)
            {
                return estado.CodigoIbge;
            }
            else
            {
                return null;
            }
        }

        public Estado getEstado(string sigla)
        {
            return (from c in _dbContext.Estado where c.Sigla == sigla select c).FirstOrDefault();
        }

        public Estado getEstado(int idEstado)
        {
            return (from c in _dbContext.Estado where c.EstadoId == idEstado select c).FirstOrDefault();
        }
    }
}
