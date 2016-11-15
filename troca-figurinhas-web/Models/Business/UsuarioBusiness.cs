using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocaFigurinhas.Models.Persistence;
using System.Data.Objects;

namespace TrocaFigurinhas.Models.Business
{
    public class UsuarioBusiness
    {

        public string login;
        public string Login { get { return login; } set { login = value.ToLower(); } }

        public bool ValidarLogin(string senha)
        {
            bool loginValido;
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                var usuario = contexto.UsuarioSet.Where(a => a.Login == Login && a.Senha == senha);
                loginValido = (usuario.ToList<Usuario>().Count == 1);
            }
            return (loginValido);

        }

        public Usuario BuscarOfertas()
        {
            Usuario dadosUsuario;
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                //var qUserComOferta = from oferta in contexto.OfertasSet 
                //                     join usuario in contexto.UsuarioSet
                //                       on oferta.Usuario.Id equals usuario.Id
                //                    where oferta.TrocaSolicitante != null
                //                       && usuario.Login.ToLower() == login

                //                     select usuario;

                var qUserComOferta = from oferta in contexto.OfertasSet
                                     join usuario in contexto.UsuarioSet
                                       on oferta.Usuario.Id equals usuario.Id
                                     where usuario.Login.ToLower() == login
                                        && oferta.TrocasSolicitado == null

                                     select usuario;

                var qUser = ((ObjectQuery<Usuario>)qUserComOferta)
                    .Include("Ofertas")
                    .Include("Ofertas.TrocaSolicitante")
                    .Include("Ofertas.TrocasSolicitado")
                    .Include("Ofertas.FigurinhasDesejadas")
                    .Include("Ofertas.FigurinhasOfertadas")
                    .Include("Ofertas.FigurinhasDesejadas.Figurinha")
                    .Include("Ofertas.FigurinhasOfertadas.Figurinha")
                    .Include("Ofertas.FigurinhasDesejadas.Figurinha.Album")
                    .Include("Ofertas.FigurinhasOfertadas.Figurinha.Album")
                    .Include("Ofertas.FigurinhasDesejadas.Figurinha.Imagem")
                    .Include("Ofertas.FigurinhasOfertadas.Figurinha.Imagem");

                if (qUser.Count() < 1)
                    dadosUsuario = null;
                else
                    dadosUsuario = qUser.First<Usuario>();
                
            }

            if (dadosUsuario == null)
            {
                throw new BusinessException("Usuário não possui ofertas.");
            }

            return dadosUsuario;
        }

        public Usuario BuscarTrocas()
        {
            Usuario dadosUsuario;
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {

                var qUser = from usuario in contexto.UsuarioSet
                            join oferta in contexto.OfertasSet
                              on usuario equals oferta.Usuario
                            where usuario.Login.ToLower() == login
                               || oferta.TrocaSolicitante != null
                               || oferta.TrocasSolicitado != null

                            select usuario;

                qUser = ((ObjectQuery<Usuario>)qUser)
                    .Include("Ofertas.TrocaSolicitante.OfertaSolicitante.FigurinhasDesejadas.Figurinha.Album")
                    .Include("Ofertas.TrocaSolicitante.OfertaSolicitante.FigurinhasOfertadas.Figurinha.Album")
                    .Include("Ofertas.TrocaSolicitante.OfertaSolicitante.FigurinhasDesejadas.Figurinha.Imagem")
                    .Include("Ofertas.TrocaSolicitante.OfertaSolicitante.FigurinhasOfertadas.Figurinha.Imagem")
                    .Include("Ofertas.TrocaSolicitante.OfertaSolicitado.FigurinhasDesejadas.Figurinha.Album")
                    .Include("Ofertas.TrocaSolicitante.OfertaSolicitado.FigurinhasOfertadas.Figurinha.Album")
                    .Include("Ofertas.TrocaSolicitante.OfertaSolicitado.FigurinhasDesejadas.Figurinha.Imagem")
                    .Include("Ofertas.TrocaSolicitante.OfertaSolicitado.FigurinhasOfertadas.Figurinha.Imagem")
                    .Include("Ofertas.TrocasSolicitado.OfertaSolicitado.FigurinhasDesejadas.Figurinha.Album")
                    .Include("Ofertas.TrocasSolicitado.OfertaSolicitado.FigurinhasOfertadas.Figurinha.Album")
                    .Include("Ofertas.TrocasSolicitado.OfertaSolicitado.FigurinhasDesejadas.Figurinha.Imagem")
                    .Include("Ofertas.TrocasSolicitado.OfertaSolicitado.FigurinhasOfertadas.Figurinha.Imagem")
                    .Include("Ofertas.TrocasSolicitado.OfertaSolicitante.FigurinhasDesejadas.Figurinha.Album")
                    .Include("Ofertas.TrocasSolicitado.OfertaSolicitante.FigurinhasOfertadas.Figurinha.Album")
                    .Include("Ofertas.TrocasSolicitado.OfertaSolicitante.FigurinhasDesejadas.Figurinha.Imagem")
                    .Include("Ofertas.TrocasSolicitado.OfertaSolicitante.FigurinhasOfertadas.Figurinha.Imagem")
                    ;


                dadosUsuario = qUser.First<Usuario>();

                
            }

            if (dadosUsuario == null)
            {
                throw new BusinessException("Usuário não existe.");
            }

            return dadosUsuario;
        }

        //private void loadEntity(System.Data.Objects.DataClasses.EntityObject o)
        //{ 
        //    if(o typeof FigurinhasDesejadas)
        //        ((FigurinhasDesejadas)o)
        //}
    }
}