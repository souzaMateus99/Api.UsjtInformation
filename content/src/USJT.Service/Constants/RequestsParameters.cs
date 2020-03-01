using System.Collections.Generic;

namespace USJT.Service.Constants
{
    public static class RequestsParameters
    {
        public static Dictionary<string, string> LoginParameters = new Dictionary<string, string>()
        {
            {"loginEx[]", "1"},
            {"autologin", string.Empty},
            {"combo", "2"},
            {"matricula", string.Empty},
            {"senha", string.Empty},
            {"num_cpf", string.Empty},
            {"dat_nascimento", string.Empty},
            {"instituicao", "8"},
            {"codigo_marca", "3"},
            {"logar", "Entrar"},
            {"__ajax", "1"},
            {"opcao_acesso", "1"}
        };
    }
}