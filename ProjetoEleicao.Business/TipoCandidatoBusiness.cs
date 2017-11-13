using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoEleicao.Data;
using ProjetoEleicao.Domain.Entities;
using ProjetoEleicao.Domain.Util;

namespace ProjetoEleicao.Business
{
    public class TipoCandidatoBusiness : IDisposable
    {
        private electionsystemEntities ctx;

        public TipoCandidatoBusiness()
        {
            ctx = new electionsystemEntities();
        }

        public Result<TipoCandidatoModel> GetTipoCandidatoByCode(int code)
        {
            Result<TipoCandidatoModel> result = new Result<TipoCandidatoModel>();
            try
            {
                TipoCandidatoModel tipoCandidato = Converters.ConvertTipoCandidatoToTipoCandidatoModel(ctx.TipoCandidatoSet.Where(o => o.IdTipoCandidato == code).FirstOrDefault());
                if (tipoCandidato == null)
                    result = Utils<TipoCandidatoModel>.SetResult(null, "Tipo Candidato inexistente.");
                else
                    result = Utils<TipoCandidatoModel>.SetResult(tipoCandidato, "True");
            }
            catch (Exception ex)
            {
                result = Utils<TipoCandidatoModel>.SetResult(null, ex.Message.ToString());
            }

            return result;
        }

        void IDisposable.Dispose()
        {
            if (ctx != null)
                ctx.Dispose();
        }
    }
}
