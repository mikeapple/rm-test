using MailContainerTest.Types;

namespace MailContainerTest.Services.Validation.MailTypeValidation
{
    public interface IMailTypeValidatorFactory
    {
        IMailTypeValidator GetInstance(MailType mailType);
    }
}