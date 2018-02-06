using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class PerfilDal : IPerfilDal
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

        public Perfil EditaPerfil(Perfil dadosPerfil)
        {
            _dbContext.Perfil.Attach(dadosPerfil);
            _dbContext.Entry(dadosPerfil).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosPerfil;
        }

        public void ExcluiPerfil(int idPerfil)
        {
            var dadosPerfil = GetPerfil(idPerfil);
            _dbContext.Perfil.Remove(dadosPerfil);
            _dbContext.SaveChanges();
        }

        public Perfil GetPerfil(int idPerfil)
        {
            return (from c in _dbContext.Perfil where c.IdPerfil == idPerfil select c).FirstOrDefault();
        }

        public Perfil InserePerfil(Perfil dadosPerfil)
        {
            var getDadosPerfil = _dbContext.Perfil.Add(dadosPerfil);
            _dbContext.SaveChanges();
            return getDadosPerfil;
        }
    }
}
