using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEleicao.Domain.Entities
{
    public class UegModel
    {
        public List<EleitorModel> ListEleitores { get; set; }

        public List<CandidatoModel> ListCandidatos { get; set; }

        public List<TipoCandidatoModel> ListTiposCandidatos { get; set; }

        public System.DateTime Fim { get; set; }
    }
}
