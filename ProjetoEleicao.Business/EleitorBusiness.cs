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
    public class EleitorBusiness : IDisposable
    {
        private electionsystemEntities ctx;

        public EleitorBusiness()
        {
            ctx = new electionsystemEntities();
        }

        public Result<EleitorModel> UpdateEleitor(string instance)
        {
            Result<EleitorModel> result = new Result<EleitorModel>();
            try
            {
                EleitorModel eleitor = Converters.ConvertEleitorToEleitorModel(ctx.EleitorSet.Where(o => o.Cpf == instance).FirstOrDefault());
                if (eleitor == null)
                    result = Utils<EleitorModel>.SetResult(null, "Eleitor não encontrado.");
                else
                {
                    eleitor.Votou = true;
                    ctx.SaveChanges();
                    result = Utils<EleitorModel>.SetResult(eleitor, "True");
                }  
            }
            catch (Exception ex)
            {
                result = Utils<EleitorModel>.SetResult(null, ex.Message.ToString());
            }

            return result;
        }

        public Result<EleitorModel> GetEleitorByCode(string cpf)
        {
            Result<EleitorModel> result = new Result<EleitorModel>();
            try
            {
                EleitorModel eleitor = Converters.ConvertEleitorToEleitorModel(ctx.EleitorSet.Where(o => o.Cpf == cpf).FirstOrDefault());
                if (eleitor == null)
                    result = Utils<EleitorModel>.SetResult(null, "Eleitor não encontrado.");
                else
                    result = Utils<EleitorModel>.SetResult(eleitor, "True");
            }
            catch (Exception ex)
            {
                result = Utils<EleitorModel>.SetResult(null, ex.Message.ToString());
            }

            return result;
        }

        public Result<CandidatoModel> Votar(int idCandidato, int tipoCandidato)
        {
            Result<CandidatoModel> result = new Result<CandidatoModel>();

            try
            {
                using (UevBusiness uevBus = new UevBusiness())
                {
                    result = uevBus.ValidaVotacao(idCandidato, tipoCandidato);
                }
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
