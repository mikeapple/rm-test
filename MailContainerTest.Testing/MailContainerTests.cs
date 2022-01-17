using MailContainerTest.Services;
using MailContainerTest.Services.Validation.MailTypeValidation;
using MailContainerTest.Types;
using Xunit;
using Moq;
using MailContainerTest.Data;

namespace MailContainerTest.Tests
{
    public class MailContainerTests
    {
        private readonly IMailTransferService _mailTransferService;

        private Mock<IMailContainerDataStore> _dataStore = new Mock<IMailContainerDataStore>();

        public MailContainerTests()
        {
            _mailTransferService = new MailTransferService(
                new MailTypeValidatorFactory(
                    new StandardLetterValidator(),
                    new LargeLetterValidator(),
                    new SmallParcelValidator()),
                    _dataStore.Object);
        }

        [Theory]
        [InlineData(MailType.SmallParcel)]
        [InlineData(MailType.LargeLetter)]
        [InlineData(MailType.StandardLetter)]
        public void MailTypeIsChecked(MailType mailType)
        {
            // Arrange
            _dataStore.Setup(s => s.GetMailContainer(It.IsAny<string>()))
                .Returns(new MailContainer() 
                { 
                    AllowedMailType = (AllowedMailType)mailType,
                    Status = MailContainerStatus.Operational, 
                });

            var request = new MakeMailTransferRequest()
            {
                MailType = mailType,
            };

            // Act
            var result = _mailTransferService.MakeMailTransfer(request);

            // Assert
            Assert.True(result.Success);
        }
    }
}
