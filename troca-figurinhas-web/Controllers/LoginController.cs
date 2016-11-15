using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrocaFigurinhas.Models.Business;
using TrocaFigurinhas.Models.Persistence;

namespace TrocaFigurinhas.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /LogOn/
        [HttpPost]
        public ActionResult Index(Usuario usuarioLogado)
        {
            UsuarioBusiness user = new UsuarioBusiness { Login = usuarioLogado.Login };
            
            if (ModelState.IsValid)
            {
                if (user.ValidarLogin(usuarioLogado.Senha))
                {
                    FormsAuthentication.SetAuthCookie(usuarioLogado.Login, false);

                    return RedirectToAction("MinhasFigurinhas", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login do usuário ou a senha estão incorretos.");
                }
            }
            return View(usuarioLogado);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
