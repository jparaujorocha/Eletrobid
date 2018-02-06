using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class UsuarioDal : IUsuarioDal
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

        public Usuario EditaUsuario(Usuario dadosUsuario)
        {
            _dbContext.Usuario.Attach(dadosUsuario);
            _dbContext.Entry(dadosUsuario).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosUsuario;
        }

        public void ExcluiUsuario(int idUsuario)
        {
            var getDadosUsuario = GetUsuario(idUsuario);
            _dbContext.Usuario.Remove(getDadosUsuario);
            _dbContext.SaveChanges();
        }

        public bool ExisteApelido(string apelido)
        {
            var getUsuario = (from c in _dbContext.Usuario where c.Apelido == apelido select c).FirstOrDefault();
            if (getUsuario == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ExisteEmail(string email)
        {
            var getUsuario = (from c in _dbContext.Usuario where c.Email == email select c).FirstOrDefault();

            if (getUsuario == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Usuario GetUsuario(string email)
        {
            return (from c in _dbContext.Usuario where c.Email == email select c).FirstOrDefault();
        }

        public Usuario GetUsuario(int idUsuario)
        {
            return (from c in _dbContext.Usuario where c.IdUsuario == idUsuario select c).FirstOrDefault();
        }

        public Usuario InsereUsuario(Usuario dadosUsuario)
        {
            var getDadosUsuario = _dbContext.Usuario.Add(dadosUsuario);
            _dbContext.SaveChanges();

            return getDadosUsuario;
        }

        public IEnumerable<Usuario> ListaUsuarios()
        {
            return (from c in _dbContext.Usuario select c).ToList();
        }

        public IEnumerable<Usuario> ListaUsuarios(int idPerfil)
        {
            return (from c in _dbContext.Usuario where c.IdPerfil == idPerfil select c).ToList();
        }
    }
}
