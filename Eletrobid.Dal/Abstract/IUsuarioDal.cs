using Eletrobid.Models;
using System;
using System.Collections.Generic;

namespace Eletrobid.Dal
{
    public interface IUsuarioDal : IDisposable
    {
        Usuario GetUsuario(int idUsuario);

        Usuario GetUsuario(string email);

        Usuario InsereUsuario(Usuario dadosUsuario);

        bool ExisteEmail(string email);

        bool ExisteApelido(string apelido);
        
        Usuario EditaUsuario(Usuario dadosUsuario);

        void ExcluiUsuario(int idUsuario);

        IEnumerable<Usuario> ListaUsuarios();

        IEnumerable<Usuario> ListaUsuarios(int idPerfil);
    }
}
