using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocaFigurinhas.Models.Persistence;
using TrocaFigurinhas.Models.ModelView;
using System.Data.Objects;

namespace TrocaFigurinhas.Models.Business
{
    public class OfertasBusiness
    {

        public void CriarOferta(string login, int[] idFigurinhasOfertadas, int[] idFigurinhasDesejadas)
        {
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {

                var novaOferta = contexto.OfertasSet.CreateObject();

                foreach (int idOfertada in idFigurinhasOfertadas)
                {
                    var figurinhaOfertada = contexto.FigurinhasOfertadasSet.CreateObject();
                    figurinhaOfertada.Figurinha =
                        contexto.FigurinhaSet.Where(a => a.Id == idOfertada).First();

                    novaOferta.FigurinhasOfertadas.Add(figurinhaOfertada);
                }

                foreach (int idDesejada in idFigurinhasDesejadas)
                {
                    var figurinhaDesejada = contexto.FigurinhasDesejadasSet.CreateObject();
                    figurinhaDesejada.Figurinha =
                        contexto.FigurinhaSet.Where(a => a.Id == idDesejada).First();

                    novaOferta.FigurinhasDesejadas.Add(figurinhaDesejada);
                }

                var qUsuario = from usuario in contexto.UsuarioSet
                               where usuario.Login.ToLower() == login.ToLower()

                               select usuario;

                if (qUsuario.Count() < 1)
                {
                    throw new BusinessException("Usuário não existe.");
                }

                Usuario user = qUsuario.First();
                user.Ofertas.Add(novaOferta);

                contexto.SaveChanges();
            }
        }

        public void ExcluirOferta(int idOferta)
        {
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                var qOferta = from Oferta in contexto.OfertasSet
                              where Oferta.Id == idOferta

                              select Oferta;

                if (qOferta.Count() < 1)
                {
                    throw new BusinessException("Oferta não encontrada.");
                }
                Ofertas oferta = qOferta.First();
                
                while (oferta.FigurinhasDesejadas.Count() > 0)
                    contexto.FigurinhasDesejadasSet.DeleteObject(oferta.FigurinhasDesejadas.First());
                while (oferta.FigurinhasOfertadas.Count() > 0)
                    contexto.FigurinhasOfertadasSet.DeleteObject(oferta.FigurinhasOfertadas.First());

                contexto.OfertasSet.DeleteObject(oferta);
                contexto.SaveChanges();
            }
        }

        public void EditarOferta(int idOferta, int[] idFigurinhasOfertadas, int[] idFigurinhasDesejadas) 
        {
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                var qOferta = from Oferta in contexto.OfertasSet
                              where Oferta.Id == idOferta

                              select Oferta;

                if (qOferta.Count() < 1)
                {
                    throw new BusinessException("Oferta não encontrada.");
                }
                Ofertas oferta = qOferta.First();

                while (oferta.FigurinhasDesejadas.Count() > 0)
                    contexto.FigurinhasDesejadasSet.DeleteObject(oferta.FigurinhasDesejadas.First());
                while (oferta.FigurinhasOfertadas.Count() > 0)
                    contexto.FigurinhasOfertadasSet.DeleteObject(oferta.FigurinhasOfertadas.First());

                foreach (int idOfertada in idFigurinhasOfertadas)
                {
                    var figurinhaOfertada = contexto.FigurinhasOfertadasSet.CreateObject();
                    figurinhaOfertada.Figurinha =
                        contexto.FigurinhaSet.Where(a => a.Id == idOfertada).First();
                    oferta.FigurinhasOfertadas.Add(figurinhaOfertada);
                }

                foreach (int idDesejada in idFigurinhasDesejadas)
                {
                    var figurinhaDesejada = contexto.FigurinhasDesejadasSet.CreateObject();
                    figurinhaDesejada.Figurinha =
                        contexto.FigurinhaSet.Where(a => a.Id == idDesejada).First();
                    oferta.FigurinhasDesejadas.Add(figurinhaDesejada);
                }

                contexto.SaveChanges();
            }
        }

        public List<Ofertas> BuscarOfertadasPorNome(string user, string nomeFigurinha)
        {
            List<Ofertas> ofertas;
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                var qOferta = from Oferta in contexto.OfertasSet
                              join ofertada in contexto.FigurinhasOfertadasSet
                                on Oferta.Id equals ofertada.OfertasId
                              where ofertada.Figurinha.Nome.Contains(nomeFigurinha)
                                 && Oferta.Usuario.Login.ToLower() != user.ToLower()

                              select Oferta;

                qOferta = ((ObjectQuery<Ofertas>)qOferta
                                 .Where(a => a.TrocasSolicitado == null || !a.TrocasSolicitado.TrocaRealizada))  // Ainda não houve trocas efetivas
                                 .Include("Usuario")
                                 .Include("FigurinhasDesejadas")
                                 .Include("FigurinhasOfertadas")
                                 .Include("FigurinhasDesejadas.Figurinha")
                                 .Include("FigurinhasOfertadas.Figurinha")
                                 .Include("FigurinhasDesejadas.Figurinha.Album")
                                 .Include("FigurinhasOfertadas.Figurinha.Album")
                                 .Include("FigurinhasDesejadas.Figurinha.Imagem")
                                 .Include("FigurinhasOfertadas.Figurinha.Imagem"); 

                ofertas = qOferta.ToList();
            }

            if (ofertas.Count == 0)
            {
                throw new BusinessException("Nenhuma oferta encontrada.");
            }

            return ofertas;
        }

        public List<Ofertas> BuscarDesejadasPorNome(string user, string nomeFigurinha)
        {
            List<Ofertas> ofertas;
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                var qOferta = from Oferta in contexto.OfertasSet
                              join Desejada in contexto.FigurinhasDesejadasSet
                                on Oferta.Id equals Desejada.OfertasId
                              where Desejada.Figurinha.Nome.Contains(nomeFigurinha)
                                 && Oferta.Usuario.Login.ToLower() != user.ToLower()

                              select Oferta;

                qOferta = ((ObjectQuery<Ofertas>)qOferta
                                 .Where(a => a.TrocasSolicitado == null || !a.TrocasSolicitado.TrocaRealizada))  // Ainda não houve trocas efetivas
                                 .Include("Usuario")
                                 .Include("FigurinhasDesejadas")
                                 .Include("FigurinhasOfertadas")
                                 .Include("FigurinhasDesejadas.Figurinha")
                                 .Include("FigurinhasOfertadas.Figurinha")
                                 .Include("FigurinhasDesejadas.Figurinha.Album")
                                 .Include("FigurinhasOfertadas.Figurinha.Album")
                                 .Include("FigurinhasDesejadas.Figurinha.Imagem")
                                 .Include("FigurinhasOfertadas.Figurinha.Imagem"); 

                ofertas = qOferta.ToList();
            }

            if (ofertas.Count == 0)
            {
                throw new BusinessException("Nenhuma oferta encontrada.");
            }

            return ofertas;
        }

        internal Ofertas BuscarOferta(int idOferta)
        {
            Ofertas oferta;
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                var qOferta = from Oferta in contexto.OfertasSet
                              where Oferta.Id == idOferta

                              select Oferta;

                if (qOferta.Count() < 1)
                {
                    throw new BusinessException("Oferta não encontrada.");
                }

                oferta = ((ObjectQuery<Ofertas>)qOferta)
                                 .Include("Usuario")
                                 .Include("FigurinhasDesejadas")
                                 .Include("FigurinhasOfertadas")
                                 .Include("FigurinhasDesejadas.Figurinha")
                                 .Include("FigurinhasOfertadas.Figurinha")
                                 .Include("FigurinhasDesejadas.Figurinha.Album")
                                 .Include("FigurinhasOfertadas.Figurinha.Album")
                                 .Include("FigurinhasDesejadas.Figurinha.Imagem")
                                 .Include("FigurinhasOfertadas.Figurinha.Imagem")
                                 .First();

            }

            return oferta;
        }
    }
}