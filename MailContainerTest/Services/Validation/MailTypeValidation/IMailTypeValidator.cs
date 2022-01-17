using MailContainerTest.Types;

namespace MailContainerTest.Services.Validation.MailTypeValidation
{
    public interface IMailTypeValidator
    {
        bool IsValid(MailContainer mailContainer, int numberOfMailItems);
    }
}
