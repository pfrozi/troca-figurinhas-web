using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocaFigurinhas.Models.Persistence;
using System.Data.Objects;

namespace TrocaFigurinhas.Models.Business
{
    public class AlbumBusiness
    {
        public List<Album> BuscarAlbuns()
        {
            List<Album> albuns = new List<Album>();

            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {


                var qAlbum = ((ObjectQuery<Album>)contexto.AlbumSet.Where(a => a.Figurinhas != null))
                                 .Include("Figurinha")
                                 .Include("Figurinha.Album")
                                 .Include("Figurinha.Imagem");

                albuns = qAlbum.ToList();
            }

            if (albuns.Count == 0)
            {
                throw new BusinessException("Nenhum album encontrado.");
            }

            return albuns;
        }

        public Album BuscarAlbum(int idAlbum) 
        {
            Album album = new Album();

            using (ModelDBFigurinhasContainer contexto = new ModelDBFigurinhasContainer())
            {


                var qAlbum = ((ObjectQuery<Album>)contexto.AlbumSet.Where(a => a.Id == idAlbum))
                                 .Include("Figurinhas")
                                 .Include("Figurinhas.Album")
                                 .Include("Figurinhas.Imagem");

                album = qAlbum.First();
            }

            if (album == null)
            {
                throw new BusinessException("Nenhum album encontrado.");
            }

            if (album.Figurinhas.Count == 0)
            {
                throw new BusinessException("Nenhuma figurinha encontrada no album.");
            }

            return album;
        }
    }
}