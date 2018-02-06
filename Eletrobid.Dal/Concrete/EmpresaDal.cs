using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class EmpresaDal : IEmpresaDal
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

        public Empresa EditaEmpresa(Empresa dadosEmpresa)
        {
            _dbContext.Empresa.Attach(dadosEmpresa);
            _dbContext.Entry(dadosEmpresa).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return dadosEmpresa;
        }

        public void ExcluiEmpresa(int idEmpresa)
        {
            var dadosEmpresa = GetEmpresa(idEmpresa);
            _dbContext.Empresa.Remove(dadosEmpresa);
            _dbContext.SaveChanges();
        }

        public bool ExisteEmpresa(string cnpj, int idTipoEmpresa)
        {
            var getEmpresa = (from c in _dbContext.Empresa where c.IdTipoEmpresa == idTipoEmpresa && c.Cnpj == cnpj select c).FirstOrDefault();

            if (getEmpresa == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Empresa GetEmpresa(string cnpj)
        {
            return (from c in _dbContext.Empresa where c.Cnpj == cnpj select c).FirstOrDefault();
        }

        public Empresa GetEmpresa(int idEmpresa)
        {
            return (from c in _dbContext.Empresa where c.IdEmpresa == idEmpresa select c).FirstOrDefault();
        }

        public Empresa InsereEmpresa(Empresa dadosEmpresa)
        {
            var getDadosEmpresa =  _dbContext.Empresa.Add(dadosEmpresa);
            _dbContext.SaveChanges();

            return getDadosEmpresa;
        }

        public IEnumerable<Empresa> ListaEmpresas()
        {
            return (from c in _dbContext.Empresa select c).ToList();
        }

        public IEnumerable<Empresa> ListaEmpresas(int idTipoEmpresa)
        {
            return (from c in _dbContext.Empresa where c.IdTipoEmpresa == idTipoEmpresa select c).ToList();
        }
    }
}
