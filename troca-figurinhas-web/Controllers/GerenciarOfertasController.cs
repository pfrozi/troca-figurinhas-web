using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocaFigurinhas.Models.Business;
using TrocaFigurinhas.Models.Persistence;
using System.Configuration;

namespace TrocaFigurinhas.Controllers
{
    public class GerenciarOfertasController : Controller
    {
        //
        // GET: /GerenciarOfertas/

        public ActionResult Index()
        {
            UsuarioBusiness usuarioBusiness = new UsuarioBusiness { Login = User.Identity.Name };
            Usuario usuarioModel;
            try
            {
                usuarioModel = usuarioBusiness.BuscarOfertas();
                return View(usuarioModel.Ofertas);
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        
        public ActionResult Edit()
        {
            AlbumBusiness albumBusiness = new AlbumBusiness();
            OfertasBusiness ofertasBusiness = new OfertasBusiness();

            int idAlbum = Convert.ToInt32(ConfigurationManager.AppSettings["AlbumUtilizado"].ToString());
            Album album = albumBusiness.BuscarAlbum(idAlbum);
            ViewBag.Album = album;
            
            Ofertas oferta = ofertasBusiness.BuscarOferta(int.Parse(this.RouteData.Values["id"].ToString()));

            return View(oferta);
        }
        [HttpPost]
        public ActionResult Edit(Ofertas oferta, int[] FigurinhasOfertadas, int[] FigurinhasDesejadas)
        {
            OfertasBusiness ofertasBusiness = new OfertasBusiness();

            ofertasBusiness.EditarOferta(oferta.Id, FigurinhasOfertadas, FigurinhasDesejadas);

            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            OfertasBusiness ofertasBusiness = new OfertasBusiness();
            Ofertas oferta = ofertasBusiness.BuscarOferta(int.Parse(this.RouteData.Values["id"].ToString()));

            return View(oferta);
        }
        [HttpPost]
        public ActionResult Delete(Ofertas oferta)
        {
            OfertasBusiness ofertasBusiness = new OfertasBusiness();
            try
            {
                ofertasBusiness.ExcluirOferta(oferta.Id);
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create() 
        {
            AlbumBusiness albumBusiness = new AlbumBusiness();

            int idAlbum = Convert.ToInt32(ConfigurationManager.AppSettings["AlbumUtilizado"].ToString());
            Album album = albumBusiness.BuscarAlbum(idAlbum);

            ViewBag.Album = album;

            return View();
        }
        [HttpPost]
        public ActionResult Create(int[] FigurinhasOfertadas, int[] FigurinhasDesejadas)
        {
            OfertasBusiness ofertaBusiness = new OfertasBusiness();
            try
            {
                ofertaBusiness.CriarOferta(User.Identity.Name, FigurinhasOfertadas, FigurinhasDesejadas);
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
