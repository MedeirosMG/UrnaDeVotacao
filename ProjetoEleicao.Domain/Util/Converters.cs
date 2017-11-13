using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoEleicao.Data;
using ProjetoEleicao.Domain.Entities;

namespace ProjetoEleicao.Domain.Util
{
    public static class Converters
    {
        public static EleitorModel ConvertEleitorToEleitorModel(Eleitor instance)
        {
            if (instance == null)
                return null;
            else
                return new EleitorModel()
                {
                    Cpf = instance.Cpf,
                    Foto = instance.Foto,
                    IdEleitor = instance.IdEleitor,
                    Nome = instance.Nome,
                    Votou = instance.Votou,
                };
        }

        public static Eleitor ConvertEleitorModelToEleitor(EleitorModel instance)
        {
            if (instance == null)
                return null;
            else
                return new Eleitor()
                {
                    Cpf = instance.Cpf,
                    Foto = instance.Foto,
                    IdEleitor = instance.IdEleitor,
                    Nome = instance.Nome,
                    Votou = instance.Votou,
                };
        }

        public static CandidatoModel ConvertCandidatoToCandidatoModel(Candidato instance)
        {
            if (instance == null)
                return null;
            else
                return new CandidatoModel()
                {
                    Foto = instance.Foto,
                    IdCandidato = instance.IdCandidato,
                    IdTipoCandidato = instance.IdTipoCandidato,
                    Nome = instance.Nome,
                    QuantidadeVotos = instance.QuantidadeVotos,
                    TipoCandidatoModel = ConvertTipoCandidatoToTipoCandidatoModel(instance.TipoCandidato),
                };
        }

        public static Candidato ConvertCandidatoModelToCandidato(CandidatoModel instance)
        {
            if (instance == null)
                return null;
            else
                return new Candidato()
                {
                    Foto = instance.Foto,
                    IdCandidato = instance.IdCandidato,
                    IdTipoCandidato = instance.IdTipoCandidato,
                    Nome = instance.Nome,
                    QuantidadeVotos = instance.QuantidadeVotos,
                    TipoCandidato = ConvertTipoCandidatoModelToTipoCandidato(instance.TipoCandidatoModel),
                };
        }

        public static TipoCandidatoModel ConvertTipoCandidatoToTipoCandidatoModel(TipoCandidato instance)
        {
            if (instance == null)
                return null;
            else
                return new TipoCandidatoModel()
                {
                    IdTipoCandidato = instance.IdTipoCandidato,
                    Descricao = instance.Descricao
                };
        }

        public static TipoCandidato ConvertTipoCandidatoModelToTipoCandidato(TipoCandidatoModel instance)
        {
            if (instance == null)
                return null;
            else
                return new TipoCandidato()
                {
                    IdTipoCandidato = instance.IdTipoCandidato,
                    Descricao = instance.Descricao
                };
        }

        public static Uev ConvertUevModelToUev(UevModel instance)
        {
            if (instance == null)
                return null;
            else
                return new Uev() {
                    Fim = instance.Fim,
                    IdUev = instance.IdUev,
                    Inicio = instance.Inicio,
                };
        }

        public static UevModel ConvertUevToUevModel(Uev instance)
        {
            if (instance == null)
                return null;
            else
                return new UevModel()
                {
                    Fim = instance.Fim,
                    IdUev = instance.IdUev,
                    Inicio = instance.Inicio,
                };
        }
    }
}
