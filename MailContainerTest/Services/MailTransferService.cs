using MailContainerTest.Data;
using MailContainerTest.Services.Validation.MailTypeValidation;
using MailContainerTest.Types;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        private readonly IMailTypeValidatorFactory _mailTypeValidatorFactory;
        private readonly IMailContainerDataStore _mailContainerDataStore;

        public MailTransferService(
            IMailTypeValidatorFactory mailTypeValidatorFactory,
            IMailContainerDataStore mailContainerDataStore)
        {
            _mailTypeValidatorFactory = mailTypeValidatorFactory;
            _mailContainerDataStore = mailContainerDataStore;
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            MailContainer mailContainer = null;

            if (dataStoreType == "Backup")
            {
                var mailContainerDataStore = new BackupMailContainerDataStore();
                mailContainer = mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);

            } else
            {
                mailContainer = _mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);
            }

            var result = new MakeMailTransferResult();

            if (mailContainer == null)
            {
                result.Success = false;
                return result;
            }

            var mailTypeValidator = _mailTypeValidatorFactory.GetInstance(request.MailType);

            result.Success = mailTypeValidator.IsValid(mailContainer, request.NumberOfMailItems);

            if (result.Success)
            {
                mailContainer.Capacity -= request.NumberOfMailItems;

                if (dataStoreType == "Backup")
                {
                    var mailContainerDataStore = new BackupMailContainerDataStore();
                    mailContainerDataStore.UpdateMailContainer(mailContainer);

                }
                else
                {
                    var mailContainerDataStore = new MailContainerDataStore();
                    mailContainerDataStore.UpdateMailContainer(mailContainer);
                }
            }

            return result;
        }
    }
}
