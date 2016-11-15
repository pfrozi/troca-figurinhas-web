using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocaFigurinhas.Models.ModelView;
using TrocaFigurinhas.Models.Business;

namespace TrocaFigurinhas.Controllers
{
    public class BuscaController : Controller
    {
        //
        // GET: /Busca/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(BuscaModelView buscaModel)
        {
            BuscaBusiness busca = new BuscaBusiness();

            if (buscaModel.FigurinhaDesejada == null && buscaModel.FigurinhaOferecida == null)
            {
                ModelState.AddModelError("", "Preencha pelo menos um dos campos.");
            }
            else
            {

                if (buscaModel.FigurinhaDesejada != null)
                {
                    try
                    {
                        buscaModel.OfertasFigurinhaDesejada
                            = busca.BuscarFigurinhasDesejadas(User.Identity.Name, buscaModel.FigurinhaDesejada);
                        
                    }
                    catch (BusinessException ex)
                    {

                        ModelState.AddModelError("", ex.Message);
                        buscaModel.OfertasFigurinhaDesejada = null;

                    }



                }
                if (buscaModel.FigurinhaOferecida != null)
                {
                    try
                    {
                        buscaModel.OfertasFigurinhaOferecida
                            = busca.BuscarFigurinhasOferecidas(User.Identity.Name, buscaModel.FigurinhaOferecida);

                    }
                    catch (BusinessException ex)
                    {

                        ModelState.AddModelError("", ex.Message);
                        buscaModel.OfertasFigurinhaOferecida = null;

                    }
                }
            }

            return View(buscaModel);
        }
    }
}
