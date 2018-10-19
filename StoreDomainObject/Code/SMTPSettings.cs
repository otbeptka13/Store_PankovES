namespace StoreDomainObject.Code
{
    internal class SMTPSettings
    {
        private SMTPSettings() {}
        public string url { get; set; }
        public int port { get; set; }
        public bool ssl { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string alias { get; set; }
        public static SMTPSettings Get()
        {
            var settings = new SMTPSettings
            {
                url = Base.GetSettingValue("smtpUrl"),
                port = int.Parse(Base.GetSettingValue("smtpPort")),
                login = Base.GetSettingValue("smtpUser"),
                password = Base.GetSettingValue("smtpPassword"),
                alias = Base.GetSettingValue("smtpAlias"),
                ssl = Base.GetSettingValue("smtpSsl").Equals("true", System.StringComparison.InvariantCultureIgnoreCase)
            };
            return settings;
        }

    }
}