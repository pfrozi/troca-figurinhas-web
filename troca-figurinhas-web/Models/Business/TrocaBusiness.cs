using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocaFigurinhas.Models.ModelView;
using TrocaFigurinhas.Models.Persistence;
using System.Data.Objects;

namespace TrocaFigurinhas.Models.Business
{
    public class TrocaBusiness 
    {
        
        public Trocas EfetuarTroca(int idOferta, int idOfertaSolicitada,bool confirmada)
        {
            Trocas troca = new Trocas();
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                var qOferta = contexto.OfertasSet.Where(a => a.Id == idOferta).First();
                var qOfertaSolicitada = contexto.OfertasSet.Where(a => a.Id == idOfertaSolicitada).First();
                
                Trocas novaTroca = new Trocas
                {
                    DataDaSolicitacao = DateTime.Now,
                    DataDaTroca = DateTime.Now,
                    OfertaSolicitante = qOferta,
                    OfertaSolicitado = qOfertaSolicitada,
                    TrocaRealizada = confirmada
                };

                contexto.TrocasSet.AddObject(novaTroca);
                contexto.SaveChanges();

                var qTroca = ((ObjectQuery<Trocas>)contexto.TrocasSet
                                 .Where(a => a.OfertaSolicitante.Id == idOferta || a.OfertaSolicitado.Id == idOfertaSolicitada))
                                 .Include("OfertaSolicitante")
                                 .Include("OfertaSolicitante.FigurinhasDesejadas")
                                 .Include("OfertaSolicitante.FigurinhasOfertadas")
                                 .Include("OfertaSolicitante.FigurinhasDesejadas.Figurinha")
                                 .Include("OfertaSolicitante.FigurinhasOfertadas.Figurinha")
                                 .Include("OfertaSolicitante.FigurinhasDesejadas.Figurinha.Album")
                                 .Include("OfertaSolicitante.FigurinhasOfertadas.Figurinha.Album")
                                 .Include("OfertaSolicitante.FigurinhasDesejadas.Figurinha.Imagem")
                                 .Include("OfertaSolicitante.FigurinhasOfertadas.Figurinha.Imagem")
                                 .Include("OfertaSolicitado")
                                 .Include("OfertaSolicitado.FigurinhasDesejadas")
                                 .Include("OfertaSolicitado.FigurinhasOfertadas")
                                 .Include("OfertaSolicitado.FigurinhasDesejadas.Figurinha")
                                 .Include("OfertaSolicitado.FigurinhasOfertadas.Figurinha")
                                 .Include("OfertaSolicitado.FigurinhasDesejadas.Figurinha.Album")
                                 .Include("OfertaSolicitado.FigurinhasOfertadas.Figurinha.Album")
                                 .Include("OfertaSolicitado.FigurinhasDesejadas.Figurinha.Imagem")
                                 .Include("OfertaSolicitado.FigurinhasOfertadas.Figurinha.Imagem");

                troca = qTroca.First();
            }

            return troca;
        }

        public List<Ofertas> buscarTrocasCompletas(string user, int ofertaId)
        {
            List<Ofertas> ofertas;
            
            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {
                var oferta = contexto.OfertasSet.Where(a => a.Id == ofertaId).First();

                var qOfertadas = oferta.FigurinhasOfertadas.Select<FigurinhasOfertadas, string>(a => a.Figurinha.Nome);
                var qDesejadas = oferta.FigurinhasDesejadas.Select<FigurinhasDesejadas, string>(a => a.Figurinha.Nome);

                string[] figurinhasOfertadas = qOfertadas.ToArray();
                string[] figurinhasDesejadas = qDesejadas.ToArray();

                var qOferta = from Oferta in contexto.OfertasSet
                              join ofertado in contexto.FigurinhasOfertadasSet
                                on Oferta.Id equals ofertado.OfertasId
                              join desejado in contexto.FigurinhasDesejadasSet
                                on Oferta.Id equals desejado.OfertasId
                              where figurinhasDesejadas.Contains(ofertado.Figurinha.Nome)
                                 && figurinhasOfertadas.Contains(desejado.Figurinha.Nome)
                                 && Oferta.Usuario.Login.ToLower() == user.ToLower()

                              select Oferta;

                // O código abaixo implementa um match parcial
                //var qOferta = from Oferta in contexto.OfertasSet
                //              join ofertado in contexto.FigurinhasOfertadasSet
                //                on Oferta.Id equals ofertado.OfertasId
                //              join ofertado2 in oferta.FigurinhasOfertadas
                //                on ofertado.Figurinha.Id equals ofertado2.Figurinha.Id
                //              join desejado in contexto.FigurinhasDesejadasSet
                //                on Oferta.Id equals desejado.OfertasId
                //              join desejado2 in oferta.FigurinhasDesejadas
                //                on ofertado.Figurinha.Id equals desejado2.Figurinha.Id
                //              where Oferta.Usuario.Login.ToLower() == user.ToLower()

                //              select Oferta;

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

            return ofertas;
        }
    }
}