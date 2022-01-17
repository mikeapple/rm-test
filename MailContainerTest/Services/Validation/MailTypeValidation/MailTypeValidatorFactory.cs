using MailContainerTest.Types;

namespace MailContainerTest.Services.Validation.MailTypeValidation
{
    public class MailTypeValidatorFactory : IMailTypeValidatorFactory
    {
        private readonly IMailTypeValidator _standardLetterValidator;

        private readonly IMailTypeValidator _largeLetterValidator;

        private readonly IMailTypeValidator _smallParcelValidator;

        public MailTypeValidatorFactory(
            IMailTypeValidator standardLetterValidator,
            IMailTypeValidator largeLetterValidator,
            IMailTypeValidator smallParcelValidator)
        {
            _standardLetterValidator = standardLetterValidator;
            _largeLetterValidator = largeLetterValidator;
            _smallParcelValidator = smallParcelValidator;
        }

        public IMailTypeValidator GetInstance(MailType mailType)
        {
            switch (mailType)
            {
                case MailType.StandardLetter:
                    return _standardLetterValidator;
                case MailType.LargeLetter:
                    return _largeLetterValidator;
                case MailType.SmallParcel:
                    return _smallParcelValidator;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
