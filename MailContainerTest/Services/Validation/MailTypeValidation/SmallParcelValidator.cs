using MailContainerTest.Types;

namespace MailContainerTest.Services.Validation.MailTypeValidation
{
    public class SmallParcelValidator : IMailTypeValidator
    {
        public bool IsValid(MailContainer mailContainer, int numberOfMailItems)
        {
            if (mailContainer == null ||
                !mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel) ||
                mailContainer.Status != MailContainerStatus.Operational)
            {
                return false;
            }

            return true;
        }
    }
}
