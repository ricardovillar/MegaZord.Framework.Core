//http://weblog.west-wind.com/posts/2016/May/23/Strongly-Typed-Configuration-Settings-in-ASPNET-Core
namespace MegaZord.Framework.Common {
    public class MZConfiguration {
        public long CacheTime { get; set; } = 60;
        public string PublicCryptoKey { get; set; } = "LM";
        public long LengthPassowrdsAutomatic { get; set; } = 8;
        public long CRUDPageSize { get; set; } = 10;
        public string AppName { get; set; } = "LM";
        public string AppUrl { get; set; } = "LM";
        public string DefaultConnectionString { get; set; } = "";
        public MZEmailElement Email { get; set; } = new MZEmailElement();
        public LMSocialNetworkElement SocialNetwork { get; set; } = new LMSocialNetworkElement();
        public MZLogElement Log { get; set; } = new MZLogElement();
    }


    public class MZLogElement  {
        public long FileSize { get; set; } = 500000;
        public bool LogDebug { get; set; } = false;
        public bool LogError { get; set; } = true;
        public bool LogSQL { get; set; } = false;
        public bool LogAudit { get; set; } = true;
    }

    public class MZEmailElement  {

        public string DefaultReceiver { get; set; } = "";
        public string DefaultDisplayName { get; set; } = "";
        public MZServerMailConfiguration ReceiveServer { get; set; } = new MZServerMailConfiguration();
        public MZServerMailConfiguration SpamSend { get; set; } = new MZServerMailConfiguration();
        public MZServerMailConfiguration NormalSend { get; set; } = new MZServerMailConfiguration();
         
    }


    public class MZServerMailConfiguration  {
        public string Server { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string From { get; set; } = "";
        public bool EnableSsl { get; set; } =true;
        public int Port { get; set; } = 0;
    }

    public class LMSocialNetworkElement  {
        public MZFacebookElement Facebook { get; set; } = new MZFacebookElement();
        public MZTwitterElement Twitter { get; set; } = new MZTwitterElement();
        public MZGooglePlusElement GooglePlus { get; set; } = new MZGooglePlusElement();
    }


    public class MZBaseSocialNetworkElement  {
        public string Id { get; set; } = "";
        public string Page { get; set; } = "";
    }

    public class MZTwitterElement : MZBaseSocialNetworkElement {
        public string DataText { get; set; } = "";
        public string DataHashTags { get; set; } = "";
    }

    public class MZGooglePlusElement : MZBaseSocialNetworkElement {
    }

    public class MZFacebookElement : MZBaseSocialNetworkElement {

        public string AccessToken { get; set; } = "";
        public string AppId { get; set; } = "";

    }
}
