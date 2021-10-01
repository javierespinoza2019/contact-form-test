namespace Ectotec.Entities.Common.Mailer
{
    public class MailConfigurationModel
    {       
        public string EmailManager { get; set; }
        public string PasswordManager { get; set; }
        public int SMTPPort { get; set; }
        public bool EnabledSSL { get; set; }
        public string SMTPHost { get; set; }
        public string BodyFile { get; set; }
        public string EmailSubject { get; set; }
    }
}
