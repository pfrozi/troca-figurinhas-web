using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocaFigurinhas.Models.Business;
using System.Configuration;
using TrocaFigurinhas.Models.Persistence;

namespace TrocaFigurinhas.Controllers
{
    public class VisualizarAlbumController : Controller
    {
        //
        // GET: /VisualizarAlbum/

        public ActionResult Index()
        {
            AlbumBusiness AlbumBusiness = new AlbumBusiness();

            try
            {
                int idAlbum = Convert.ToInt32(ConfigurationManager.AppSettings["AlbumUtilizado"].ToString());
                Album album = AlbumBusiness.BuscarAlbum(idAlbum);
                return View(album);
            }
            catch (BusinessException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View();

            }
            
        }

        
    }
}
