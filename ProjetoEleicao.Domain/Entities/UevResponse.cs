using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEleicao.Domain.Entities
{
    public class UevResponse
    {
        public List<EleitorModel> ListEleitores { get; set; }

        public List<CandidatoModel> ListCandidato { get; set; }

        public List<TipoCandidatoModel> ListTipoCandidato { get; set; }

        public DateTime InicioEleicao { get; set; }

        public DateTime FimEleicao { get; set; }

        public int IdUev { get; set; }
    }
}
