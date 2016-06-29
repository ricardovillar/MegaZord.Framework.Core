using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MegaZord.Framework.Interfaces;
using MegaZord.Framework.Common;
using MimeKit;

namespace MegaZord.Framework.DTO {
    public class EmailDTO {
        private readonly IMZParametrosRepository _parametrosRepository;

        public EmailDTO() {
            Dest = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
            Anexos = new List<MimeEntity>();
        }

        public EmailDTO(string preLoadedMail) : this() {
            PreLoadedMail = preLoadedMail;
        }

        public string Assunto { get; set; }
        public IList<string> Dest { get; private set; }
        public IList<string> Cc { get; private set; }
        public IList<string> Bcc { get; private set; }
        public string EmailAutor { get; set; }
        public string ConteudoMensagem { get; set; }
        public string ConteudoCabecalho { get; set; }
        public string ConteudoRodape { get; set; }
        public bool IsHtml { get; set; }
        public IList<MimeEntity> Anexos { get; private set; }

        private string PreLoadedMail { get; set; }


        private string Repace(string nomeTag, string valorTag, string texto) {

            string patter = string.Format("\\[\\<{0}\\>\\]", nomeTag);

            if (string.IsNullOrEmpty(valorTag))
                valorTag = "";

            return new Regex(patter, RegexOptions.IgnoreCase).Replace(texto, valorTag);
        }

    }
}
