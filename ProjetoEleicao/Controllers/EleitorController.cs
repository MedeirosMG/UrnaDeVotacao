using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoEleicao.Business;
using ProjetoEleicao.Domain.Util;
using ProjetoEleicao.Domain.Entities;

namespace ProjetoEleicao.Web.Controllers
{
    public class EleitorController : Controller
    {
        // GET: Eleitor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Votar()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEleitorByCode(string cpf)
        {
            using (EleitorBusiness eleitorBus = new EleitorBusiness())
            {
                return Json(eleitorBus.GetEleitorByCode(cpf),JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Votar(int idCandidato, int tipoCandidato)
        {
            using (EleitorBusiness eleitorBus = new EleitorBusiness())
            {
                return Json(eleitorBus.Votar(idCandidato, tipoCandidato), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateEleitor(string eleitor)
        {
            using (EleitorBusiness eleitorBus = new EleitorBusiness())
            {
                return Json(eleitorBus.UpdateEleitor(eleitor),JsonRequestBehavior.AllowGet);
            }
        }

    }
}