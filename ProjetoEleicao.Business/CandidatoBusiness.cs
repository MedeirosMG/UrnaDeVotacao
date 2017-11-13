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
    public class CandidatoBusiness : IDisposable
    {
        private electionsystemEntities ctx;

        public CandidatoBusiness()
        {
            ctx = new electionsystemEntities();
        }

        public Result<CandidatoModel> GetCandidatoByCode(int code)
        {
            Result<CandidatoModel> result = new Result<CandidatoModel>();
            try
            {
                CandidatoModel candidato = Converters.ConvertCandidatoToCandidatoModel(ctx.CandidatoSet.Where(o => o.IdCandidato == code).FirstOrDefault());
                if (candidato == null)
                    result = Utils<CandidatoModel>.SetResult(null, "Candidato inexistente.");
                else
                    result = Utils<CandidatoModel>.SetResult(candidato, "True");
            }
            catch (Exception ex)
            {
                result = Utils<CandidatoModel>.SetResult(null, ex.Message.ToString());
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
