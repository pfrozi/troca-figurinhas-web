using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocaFigurinhas.Models.Business;
using TrocaFigurinhas.Models.Persistence;

namespace TrocaFigurinhas.Controllers
{
    public class TrocaController : Controller
    {
        //
        // GET: /Troca/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EfetuarTroca()
        {
            ViewBag.TrocaMessage = "";

            try
            {
                int idOferta = Convert.ToInt32(Request.QueryString["idOferta"]);
                int idOfertaSolicitada = Convert.ToInt32(Request.QueryString["idOfertaSolicitada"]);

                TrocaBusiness trocaBusiness = new TrocaBusiness();

                Trocas troca = trocaBusiness.EfetuarTroca(idOferta, idOfertaSolicitada, true);

                ViewBag.TrocaMessage = "Troca efetuada com sucesso!";

                return View(troca);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            
        }

    }
}
