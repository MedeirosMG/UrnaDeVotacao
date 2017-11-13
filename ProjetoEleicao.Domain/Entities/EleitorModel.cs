using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEleicao.Domain.Entities
{
    public class EleitorModel
    {
        public int IdEleitor { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public bool Votou { get; set; }
    }
}
