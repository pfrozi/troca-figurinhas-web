using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocaFigurinhas.Models.Business;
using TrocaFigurinhas.Models.Persistence;

namespace TrocaFigurinhas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Bem-vindo ao sistema de troca de figurinhas on-Line!";

            return View();
        }

        public ActionResult MinhasFigurinhas()
        {
            try
            {
                UsuarioBusiness usuarioBusiness = new UsuarioBusiness { Login = User.Identity.Name };
                Usuario usuarioModel;

                usuarioModel = usuarioBusiness.BuscarOfertas();
                return View(usuarioModel);
            }

            catch (BusinessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            

        }

        public ActionResult MinhasTrocas()
        {
            try
            {
                UsuarioBusiness usuarioBusiness = new UsuarioBusiness { Login = User.Identity.Name };
                Usuario usuarioModel;

                usuarioModel = usuarioBusiness.BuscarTrocas();
                return View(usuarioModel);
            }

            catch (BusinessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }


        }

        public ActionResult About()
        {
            return View();
        }
    }
}
