using System.Security.Principal;
using System.Linq;
using Newtonsoft.Json;

namespace MegaZord.Framework.Security {
    public class LMPrincipalSerializeModel {
        public LMPrincipalSerializeModel() { }

        public LMPrincipalSerializeModel(long userid, string nome, long empresaId, string login, string[] roles) {
            this.UserId = userid;
            this.Nome = nome;
            this.Login = login;
            this.EmpresaId = empresaId;
            this.roles = roles;

        }
        public long EmpresaId { get; protected set; }
        public long UserId { get; protected set; }
        public string Nome { get; protected set; }
        public string Login { get; protected set; }
        public string[] roles { get; protected set; }
    }



    public class LMCustomPrincipal : LMPrincipalSerializeModel, IPrincipal {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) {
            return roles.Any(r => role.Equals(r));
        }

        public LMCustomPrincipal() { }

        public LMCustomPrincipal(string Username) : this() {
            if (!string.IsNullOrEmpty(Username))
                this.Identity = new GenericIdentity(Username);
        }

        public LMCustomPrincipal(string Username, LMPrincipalSerializeModel modelSerial) : this(modelSerial.UserId, modelSerial.Nome, modelSerial.EmpresaId, modelSerial.Login, modelSerial.roles) {

        }


        [JsonConstructor]
        public LMCustomPrincipal(long UserId, string Nome, long empresaid, string Login, string[] roles) : this(Nome) {
            this.UserId = UserId;
            this.Nome = Nome;
            this.EmpresaId = empresaid;
            this.Login = Login;
            this.roles = roles;
        }
    }
}
