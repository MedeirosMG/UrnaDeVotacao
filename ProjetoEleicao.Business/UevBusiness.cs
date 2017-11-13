using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoEleicao.Data;
using ProjetoEleicao.Domain.Entities;
using ProjetoEleicao.Domain.Util;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace ProjetoEleicao.Business
{
    public class UevBusiness : IDisposable
    {
        private electionsystemEntities ctx;
        private static string EnderecoBase = "http://localhost/Ueg.WebAPI/api/";

        public UevBusiness()
        {
            ctx = new electionsystemEntities();
        }

        public Result<UevResponse> ReceberDadosUeg()
        {
            Result<UevResponse> result = new Result<UevResponse>();

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(EnderecoBase);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //StringContent content = new StringContent(ctx.UevSet.FirstOrDefault().IdUev.ToString(), System.Text.Encoding.UTF8, "application/json");
                System.Net.Http.HttpResponseMessage response = client.GetAsync("Uev/GetConfigUev/?idUev=" + ctx.UevSet.FirstOrDefault().IdUev.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseWebApi = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<Result<UevResponse>>(responseWebApi);
                    result = Utils<UevResponse>.SetResult(result.Content, "True");
                }
                else
                {
                    result = Utils<UevResponse>.SetResult(null, response.StatusCode.ToString() + " - " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                result = Utils<UevResponse>.SetResult(null, ex.Message.ToString());
            }

            return result;

        }

        public Result<UevResponse> RegistrarDadosUeg()
        {
            Result<UevResponse> result = new Result<UevResponse>();

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(EnderecoBase);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(GetResultEleicao().Content), System.Text.Encoding.UTF8, "application/json");
                System.Net.Http.HttpResponseMessage response = client.PostAsync("Uev/SetResultEleicao", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseWebApi = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<Result<UevResponse>>(responseWebApi);
                }
                else
                {
                    result = Utils<UevResponse>.SetResult(null, response.StatusCode.ToString() + " - " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                result = Utils<UevResponse>.SetResult(null, ex.Message.ToString());
            }

            return result;
        }

        public Result<UevModel> IniciaVotacao()
        {
            Result<UevModel> result = new Result<UevModel>();

            try
            {
                Result<UevResponse> resultUeg = ReceberDadosUeg();

                Uev uev = ctx.UevSet.FirstOrDefault();
                uev.Inicio = resultUeg.Content.InicioEleicao;
                uev.Fim = resultUeg.Content.FimEleicao;

                if (GravaTiposCandidatos(resultUeg.Content.ListTipoCandidato))
                {
                    if (GravaCandidatos(resultUeg.Content.ListCandidato))
                    {
                        if (GravaEleitores(resultUeg.Content.ListEleitores))
                        {
                            result = Utils<UevModel>.SetResult(Converters.ConvertUevToUevModel(uev), "True");
                        }
                        else
                        {
                            result = Utils<UevModel>.SetResult(null, "Erro ao gravar Eleitores.");
                        }
                    }
                    else
                    {
                        result = Utils<UevModel>.SetResult(null, "Erro ao gravar candidatos.");
                    }
                }
                else
                {
                    result = Utils<UevModel>.SetResult(null, "Erro ao gravar Cargos.");
                }

                if (result.Content != null)
                    ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                result = Utils<UevModel>.SetResult(null, ex.Message.ToString());
            }

            return result;
        }

        public Result<CandidatoModel> ValidaVotacao(int idCandidato, int tipoCandidato)
        {
            Result<CandidatoModel> result = new Result<CandidatoModel>();

            if (ctx.UevSet.FirstOrDefault().Fim < DateTime.Now)
                result = Utils<CandidatoModel>.SetResult(null, "Votação encerrada.");
            else
            {
                if (idCandidato == -2)
                {
                    Candidato candidato;
                    if (tipoCandidato == 1)
                    {
                        candidato = ctx.CandidatoSet.Where(o => o.IdCandidato == 12).FirstOrDefault();
                    }else if(tipoCandidato == 2)
                    {
                        candidato = ctx.CandidatoSet.Where(o => o.IdCandidato == 13).FirstOrDefault();
                    }
                    else
                    {
                        candidato = ctx.CandidatoSet.Where(o => o.IdCandidato == 11).FirstOrDefault();
                    }

                    candidato.QuantidadeVotos = candidato.QuantidadeVotos + 1;
                    result = Utils<CandidatoModel>.SetResult(new CandidatoModel(), "Votação em branco com sucesso.");
                    ctx.SaveChanges();
                }
                else
                {
                    Candidato candidato = ctx.CandidatoSet.Where(o => o.IdCandidato == idCandidato).FirstOrDefault();
                    if (candidato != null && !(candidato.Nome.Contains("Nulo")) && !(candidato.Nome.Contains("Branco")) )
                    {
                        if (candidato.IdTipoCandidato != tipoCandidato)
                            result = Utils<CandidatoModel>.SetResult(null, "Candidato não correspondente com o cargo de votação.");
                        else
                        {
                            candidato.QuantidadeVotos = candidato.QuantidadeVotos + 1;
                            result = Utils<CandidatoModel>.SetResult(Converters.ConvertCandidatoToCandidatoModel(candidato), "Votação com sucesso.");
                            ctx.SaveChanges();
                        }
                    }
                    else
                    {
                        Candidato candidatoN;
                        if (tipoCandidato == 1)
                        {
                            candidatoN = ctx.CandidatoSet.Where(o => o.IdCandidato == 9).FirstOrDefault();
                        }
                        else if (tipoCandidato == 2)
                        {
                            candidatoN = ctx.CandidatoSet.Where(o => o.IdCandidato == 8).FirstOrDefault();
                        }
                        else
                        {
                            candidatoN = ctx.CandidatoSet.Where(o => o.IdCandidato == 10).FirstOrDefault();
                        }

                        candidatoN.QuantidadeVotos = candidatoN.QuantidadeVotos + 1;
                        result = Utils<CandidatoModel>.SetResult(new CandidatoModel(), "Votação em nulo com sucesso.");
                        ctx.SaveChanges();
                    }
                }                
            }

            return result;
        }

        public Result<EleitorModel> ValidaEleitor(int idEleitor)
        {
            Result<EleitorModel> result = new Result<EleitorModel>();

            try
            {
                Eleitor eleitor = ctx.EleitorSet.Where(o => o.IdEleitor == idEleitor).FirstOrDefault();
                if (eleitor == null)
                    result = Utils<EleitorModel>.SetResult(null, "Eleitor não cadastrado e/ou inexistente.");
                else
                    result = Utils<EleitorModel>.SetResult(Converters.ConvertEleitorToEleitorModel(eleitor), "True");
            }
            catch (Exception ex)
            {
                result = Utils<EleitorModel>.SetResult(null, ex.Message.ToString());
            }

            return result;
        }

        public Result<UevResponse> GetResultEleicao()
        {
            Result<UevResponse> result = new Result<UevResponse>();

            try
            {
                result.Content = new UevResponse();
                result.Content.ListCandidato = new List<CandidatoModel>();
                foreach (var item in ctx.CandidatoSet.ToList())
                {
                    result.Content.ListCandidato.Add(Converters.ConvertCandidatoToCandidatoModel(item));
                }

                result.Content.ListEleitores = new List<EleitorModel>();
                foreach (var item in ctx.EleitorSet.ToList())
                {
                    result.Content.ListEleitores.Add(Converters.ConvertEleitorToEleitorModel(item));
                }

                result.Content.IdUev = ctx.UevSet.FirstOrDefault().IdUev;

                result = Utils<UevResponse>.SetResult(result.Content, "True");
            }
            catch (Exception ex)
            {
                result = Utils<UevResponse>.SetResult(null, ex.Message.ToString());
            }

            return result;
        }

        public bool GravaCandidatos(List<CandidatoModel> list)
        {
            try
            {
                var path = "C:\\fotos_candidatos\\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var item in list)
                {

                    var auxPath = path + item.IdCandidato + "_" + item.Nome + ".jpeg";
                    if (!File.Exists(auxPath))
                    {
                        // Convert base 64 string to byte[]
                        byte[] imageBytes = Convert.FromBase64String(item.Foto);
                        // Convert byte[] to Image
                        using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                        {
                            Image image = Image.FromStream(ms, true);
                            image.Save(auxPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }

                    if (ctx.CandidatoSet.Where(o => o.IdCandidato == item.IdCandidato).FirstOrDefault() == null)
                    {
                        var candidato = item;
                        candidato.Foto = auxPath;
                        candidato.TipoCandidatoModel = null;
                        ctx.CandidatoSet.Add(Converters.ConvertCandidatoModelToCandidato(candidato));
                    }
                }
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GravaEleitores(List<EleitorModel> list)
        {
            try
            {

                foreach (var item in list)
                {
                    if (ctx.EleitorSet.Where(o => o.IdEleitor == item.IdEleitor).FirstOrDefault() == null)
                    {
                        ctx.EleitorSet.Add(Converters.ConvertEleitorModelToEleitor(item));
                    }
                }

                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GravaTiposCandidatos(List<TipoCandidatoModel> list)
        {
            try
            {
                foreach (var item in list)
                {
                    if (ctx.TipoCandidatoSet.Where(o => o.IdTipoCandidato == item.IdTipoCandidato).FirstOrDefault() == null)
                    {
                        ctx.TipoCandidatoSet.Add(Converters.ConvertTipoCandidatoModelToTipoCandidato(item));
                    }
                }
                ctx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        void IDisposable.Dispose()
        {
            if (ctx != null)
                ctx.Dispose();
        }
    }
}
