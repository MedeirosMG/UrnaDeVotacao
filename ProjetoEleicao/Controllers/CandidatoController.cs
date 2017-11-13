using ProjetoEleicao.Business;
using System.Web.Mvc;

namespace ProjetoEleicao.Web.Controllers
{
    public class CandidatoController : Controller
    {
        [HttpGet]
        public JsonResult GetCandidatoByCode(int code)
        {
            using (CandidatoBusiness candidatoBus = new CandidatoBusiness())
            {
                return Json(candidatoBus.GetCandidatoByCode(code),JsonRequestBehavior.AllowGet);
            }
        }
    }
}