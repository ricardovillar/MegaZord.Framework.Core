using MegaZord.Framework.Helpers;
using System;

namespace MegaZord.Framework.MVC.Results
{



    public class MZBaseResponse
        {
            public bool Sucesso { get; private set; }
            public string Mensagem { get; private set; }

            public object Results { get; private set; }
            public MZBaseResponse()
                : this(true)
            {

            }
            public MZBaseResponse(bool sucesso)
                : this(sucesso, string.Empty)
            {

            }
            public MZBaseResponse(Exception ex)
                : this(false, MZHelperException.GetErrorMessage(ex))
            {

            }

            public MZBaseResponse(bool sucesso, Exception ex)
                : this(sucesso, MZHelperException.GetErrorMessage(ex))
            {

            }
            public MZBaseResponse(bool sucesso, string mensagem)
                : this(sucesso, mensagem, null)
            {
            }

            public MZBaseResponse(bool sucesso, string mensagem, object results)
            {
                Sucesso = sucesso;
                Mensagem = mensagem;
                Results = results;
            }
        }
    }
