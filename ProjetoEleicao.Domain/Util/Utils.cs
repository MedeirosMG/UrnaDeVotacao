using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEleicao.Domain.Util
{
    public static class Utils<T>
    {
        public static Result<T> SetResult(T instance, string message)
        {
            if (instance == null)
                return null;
            else
                if (instance != null)
                return new Result<T>()
                {
                    Content = instance,
                    Message = message,
                    Success = true,
                };
            else
                return new Result<T>()
                {
                    Content = instance,
                    Message = message,
                    Success = false,
                };
        }
    }
}
