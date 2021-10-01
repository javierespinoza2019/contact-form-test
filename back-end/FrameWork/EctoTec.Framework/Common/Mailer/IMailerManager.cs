namespace EctoTec.Framework.Common.Mailer
{
    public interface IMailerManager<I>
    {
        bool SendMail(I item);
    }
}
