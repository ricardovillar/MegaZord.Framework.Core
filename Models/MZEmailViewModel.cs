using System.Collections.Generic;

namespace MegaZord.Framework.Models {
    public class MZEmailViewModel {
        public MZEmailViewModel(string body, string assunto, string emailorigem, bool ishtml) : this() {
            this.Assunto = assunto;
            this.ConteudoMensagem = body;
            this.EmailAutor = emailorigem;
            this.IsHtml = ishtml;
        }

        public MZEmailViewModel() {
            this.Dest = new List<string>();
            this.Cc = new List<string>();
            this.Bcc = new List<string>();
            this.Anexos = new List<MimeKit.MimeEntity>();
        }

        public string Assunto { get; set; }
        public IList<string> Dest { get; private set; }
        public IList<string> Cc { get; private set; }
        public IList<string> Bcc { get; private set; }
        public string EmailAutor { get; set; }
        public string ConteudoMensagem { get; set; }
        public bool IsHtml { get; set; }

        public IEnumerable<MimeKit.MimeEntity> Anexos { get; private set; }

    }
}
