using MailContainerTest.Types;

namespace MailContainerTest.Services.Validation.MailTypeValidation
{
    public class LargeLetterValidator : IMailTypeValidator
    {
        public bool IsValid(MailContainer mailContainer, int numberOfMailItems)
        {
            if (mailContainer == null ||
                !mailContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter) ||
                mailContainer.Capacity < numberOfMailItems)
            {
                return false;
            }
            
            return true;
        }
    }
}
