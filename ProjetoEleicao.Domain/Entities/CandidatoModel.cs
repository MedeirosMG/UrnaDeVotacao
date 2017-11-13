using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEleicao.Domain.Entities
{
    public class CandidatoModel
    {
        public int IdCandidato { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public int IdTipoCandidato { get; set; }
        public int QuantidadeVotos { get; set; }

        public virtual TipoCandidatoModel TipoCandidatoModel { get; set; }
    }
}
