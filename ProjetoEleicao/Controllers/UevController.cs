using ProjetoEleicao.Business;
using ProjetoEleicao.Domain.Entities;
using ProjetoEleicao.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoEleicao.Web.Controllers
{
    public class UevController : Controller
    {
        // GET: Uev
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RegistrarDadosUeg()
        {
            using (UevBusiness uevBus = new UevBusiness())
            {
                return Json(uevBus.RegistrarDadosUeg());
            }
        }

        [HttpGet]
        public JsonResult IniciaVotacao()
        {
            using (UevBusiness uevBus = new UevBusiness())
            {
                return Json(uevBus.IniciaVotacao(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ValidaEleitor(int idEleitor)
        {
            using (UevBusiness uevBus = new UevBusiness())
            {
                return Json(uevBus.ValidaEleitor(idEleitor));
            }
        }
    }
}