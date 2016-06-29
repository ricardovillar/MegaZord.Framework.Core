using MegaZord.Framework.Common;

namespace MegaZord.Framework.Helpers {
    public class MailServerConfiguration {
        public MailServerConfiguration(MZServerMailConfiguration config) { _config = config; }

        private readonly MZServerMailConfiguration _config;
        public string MZServer { get { return _config.Server; } }
        public string MZUserName { get { return _config.UserName; } }
        public string MZPassword { get { return _config.Password; } }
        public string MZFrom { get { return _config.From; } }

        public bool MZEnableSsl {
            get { return _config.EnableSsl; }
        }

        public int MZPort { get { return _config.Port; } }

    }
    public class FacebookConfiguration {
        private readonly MZFacebookElement _config;

        public FacebookConfiguration(MZFacebookElement config) {
            _config = config;
        }
        public string AppId { get { return _config.AppId; } }
        public string Page { get { return _config.Page; } }
        public string Id { get { return _config.Id; } }
        public string AccessToken { get { return _config.AccessToken; } }
    }

    public class TwitterConfiguration {
        private readonly MZTwitterElement _config;

        public TwitterConfiguration(MZTwitterElement config) {
            _config = config;
        }

        public string Page { get { return _config.Page; } }
        public string Id { get { return _config.Id; } }
        public string DataText { get { return _config.DataText; } }
        public string DataHashTags { get { return _config.DataHashTags; } }

    }

    public class GooglePlusConfiguration {
        private readonly MZGooglePlusElement _config;

        public GooglePlusConfiguration(MZGooglePlusElement config) {
            _config = config;
        }

        public string Page { get { return _config.Page; } }

    }

    public static class MZHelperConfiguration {

        private static MZConfiguration _config;
        private static MZConfiguration Config {
            get {//castro
                return _config ??
                       (_config = new MZConfiguration());
            }
        }

        public static class MZEmail {

            public static string MZDefaultReceiver {
                get { return Config.Email.DefaultReceiver; }
            }

            public static string MZDefaultDisplayName { get { return Config.Email.DefaultDisplayName; } }
            public static MailServerConfiguration MZReceive { get { return new MailServerConfiguration(Config.Email.ReceiveServer); } }

            public static MailServerConfiguration MZSpamSend { get { return new MailServerConfiguration(Config.Email.SpamSend); } }

            public static MailServerConfiguration MZNormalSend { get { return new MailServerConfiguration(Config.Email.NormalSend); } }

        }

        public static class MZSocialNetwork {
            public static FacebookConfiguration MZFacebook { get { return new FacebookConfiguration(Config.SocialNetwork.Facebook); } }
            public static TwitterConfiguration MZTwitter { get { return new TwitterConfiguration(Config.SocialNetwork.Twitter); } }

            public static GooglePlusConfiguration MZGooglePlus { get { return new GooglePlusConfiguration(Config.SocialNetwork.GooglePlus); } }


        }

        public static class MZLog {
            public static long MZFileLogSize {
                get { return Config.Log.FileSize; }
            }

            public static bool MZLogDebug {
                get { return Config.Log.LogDebug; }
            }
            public static bool MZLogError {
                get { return Config.Log.LogError; }
            }

            public static bool MZLogSQL {
                get { return Config.Log.LogSQL; }
            }
            public static bool MZLogAudit {
                get { return Config.Log.LogAudit; }
            }


        }

        public static string MZConnectionString {
            get {
                return Config.DefaultConnectionString;
            }
        }
        
        public static string MZAppName {
            get {
                return Config.AppName;
            }
        }
        
        public static string MZAppUrl {
            get {
                return Config.AppUrl;
            }
        }

        public static string MZPublicCryptoKey {
            get {
                return Config.PublicCryptoKey;
            }
        }

        public static long MZLengthPassowrdsAutomatic {
            get {
                return Config.LengthPassowrdsAutomatic;
            }
        }

        public static long MZCRUDPageSize {
            get {
                return Config.CRUDPageSize;
            }
        }

        public static long MZCacheTime {
            get {
                return Config.CacheTime;
            }
        }

    }
}


