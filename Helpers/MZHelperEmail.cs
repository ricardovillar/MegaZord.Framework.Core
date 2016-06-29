using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading;
using System;
using System.Text.RegularExpressions;
using MegaZord.Framework.DTO;

namespace MegaZord.Framework.Helpers {
    public static class MZHelperEmail {
        private static SmtpClient CreateSMTPClient(bool usingSpan) {
            var username = usingSpan ? MZHelperConfiguration.MZEmail.MZSpamSend.MZUserName : MZHelperConfiguration.MZEmail.MZNormalSend.MZUserName;
            var password = usingSpan ? MZHelperConfiguration.MZEmail.MZSpamSend.MZPassword : MZHelperConfiguration.MZEmail.MZNormalSend.MZPassword;
            var server = usingSpan ? MZHelperConfiguration.MZEmail.MZSpamSend.MZServer : MZHelperConfiguration.MZEmail.MZNormalSend.MZServer;
            var port = usingSpan ? MZHelperConfiguration.MZEmail.MZSpamSend.MZPort : MZHelperConfiguration.MZEmail.MZNormalSend.MZPort;
            var enablessl = usingSpan ? MZHelperConfiguration.MZEmail.MZSpamSend.MZEnableSsl : MZHelperConfiguration.MZEmail.MZNormalSend.MZEnableSsl;

            var Credentials = new System.Net.NetworkCredential(username, password);
            var client = new SmtpClient();
            client.Connect(server, port, enablessl);
            client.Authenticate(Credentials);

            return client;
        }

        private static void AddAttachmentsCollection(IList<MimeEntity> collection, IEnumerable<MimeEntity> attachments) {
            foreach (var attachment in attachments) {
                collection.Add(attachment);
            }
        }

        private static void AddMailAddressCollection(InternetAddressList collection, IEnumerable<string> emails) {
            foreach (var email in emails) {
                var splitedEmails = email.Split(';');
                foreach (var m in splitedEmails) {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(m);
                    if (match.Success) {
                        collection.Add(new MailboxAddress(m, m));
                    }
                }
            }
        }

        private static MimeMessage CreateMailMessage(EmailDTO email, bool sendBccDefault) {

            var builderMessageBody = new BodyBuilder();
            if (email.IsHtml)
                builderMessageBody.HtmlBody = email.ConteudoMensagem;
            else
                builderMessageBody.TextBody = email.ConteudoMensagem;

            var message = new MimeMessage();
            message.Subject = email.Assunto;
            message.Priority = MessagePriority.Urgent;
            message.XPriority = XMessagePriority.Highest;
            message.Body = builderMessageBody.ToMessageBody();
            message.From.Add(new MailboxAddress(MZHelperConfiguration.MZEmail.MZDefaultDisplayName, email.EmailAutor));

            AddMailAddressCollection(message.To, email.Dest);
            AddMailAddressCollection(message.Cc, email.Cc);
            AddMailAddressCollection(message.Bcc, email.Bcc);
            AddAttachmentsCollection(message.Attachments.ToList(), email.Anexos);

            if (sendBccDefault) {
                var listDefaultReceiver = new List<string>();
                if (MZHelperConfiguration.MZEmail.MZDefaultReceiver.IndexOf(';') > -1) {
                    listDefaultReceiver = MZHelperConfiguration.MZEmail.MZDefaultReceiver.Split(';').ToList();
                }
                else {
                    listDefaultReceiver.Add(MZHelperConfiguration.MZEmail.MZDefaultReceiver);
                }
                AddMailAddressCollection(message.Bcc, listDefaultReceiver);
            }

            return message;
        }

        public static void Send(EmailDTO email, bool sendBccDefault = false, bool sendUsingSpam = false) {
            InternalSend(email, sendBccDefault, sendUsingSpam);
        }

        public static void SendAsync(EmailDTO email, bool sendBccDefault = false, bool sendUsingSpam = false) {
            var ts = new ThreadStart(() => InternalSend(email, sendBccDefault, sendUsingSpam));
            var thread = new Thread(ts) { IsBackground = true };
            thread.Start();
        }


        private static void InternalSend(EmailDTO email, bool sendBccDefault, bool sendUsingSpam) {
            using (var smtp = CreateSMTPClient(sendUsingSpam)) {
                var mailMessage = CreateMailMessage(email, sendBccDefault);
                try {
                    smtp.Send(mailMessage);
                }
                catch (Exception e) {
                    if (e.Message == string.Empty)
                        throw new Exception("Sem texto definido");
                }
                finally {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
        }


    }
}


