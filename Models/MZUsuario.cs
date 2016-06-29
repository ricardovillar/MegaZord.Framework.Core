using System.Collections.Generic;

namespace MegaZord.Framework.Models {
    public class LMRole {
        public LMRole(long roleId, string name) {
            this.RoleId = roleId;
            this.Name = name;
        }

        public long RoleId { get; private set; }
        public string Name { get; private set; }
    }

    public class MZUsuario {
        public MZUsuario() {
            this.Roles = new List<LMRole>();
        }

        public MZUsuario(long id, string nome, string login, long empresaid) : this() {
            this.Id = id;
            this.Nome = nome;
            this.Login = login;
            this.EmpresaId = empresaid;
        }

        public long Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public long EmpresaId { get; set; }
        public virtual ICollection<LMRole> Roles { get; private set; }
    }
}
