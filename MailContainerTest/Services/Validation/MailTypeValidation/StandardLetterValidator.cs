using MailContainerTest.Types;

namespace MailContainerTest.Services.Validation.MailTypeValidation
{
    public class StandardLetterValidator : IMailTypeValidator
    {
        public bool IsValid(MailContainer mailContainer, int numberOfMailItems)
        {
            if (mailContainer == null || 
                !mailContainer.AllowedMailType.HasFlag(AllowedMailType.StandardLetter))
            {
                return false;
            }

            return true;
        }
    }
}
