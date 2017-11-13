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
    public class TipoCandidatoController : Controller
    {
        [HttpGet]
        public JsonResult GetTipoCandidatoByCode(int code)
        {
            using (TipoCandidatoBusiness tipoCandidatoBus = new TipoCandidatoBusiness())
            {
                return Json(tipoCandidatoBus.GetTipoCandidatoByCode(code), JsonRequestBehavior.AllowGet);
            }
        }
    }
}