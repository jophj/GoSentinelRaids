using System.Linq;
using Google.Cloud.Vision.V1;
using ScreenshotAI.GoogleVision;
using Xunit;

namespace ScreeshotAITests.GoogleVision
{
    public class EggFeaturesRecognizionTests
    {
        private readonly ImageAnnotatorClient _client;

        public EggFeaturesRecognizionTests()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                @"C:\Users\jop\Documents\GoSentinel-50300a166648.json");
            _client = ImageAnnotatorClient.Create();
        }

        [Theory]
        [InlineData("./GoogleVision/Screenshots/d5f7a003-3efa-4850-b1a0-94a6ffab62e8.jpg", "Monumento Caduti in Guerra")]
        [InlineData("./GoogleVision/Screenshots/1adfad1c-a68c-4a05-8384-ffd736975b8c.jpg", "Chiesa Santa Caterina")]
        [InlineData("./GoogleVision/Screenshots/1e5ce2c7-bfb7-4771-b24d-1f12b6ffb5cc.jpg", "Porta Del Castello")]
        [InlineData("./GoogleVision/Screenshots/213ed098-d728-4a6c-9230-d808be275e15.jpg", "Chiesa Santa Maria Dell ' umilta - Chiesanuova")]
        [InlineData("./GoogleVision/Screenshots/29bf628a-5f5b-4b26-b59c-4ddeb1beb0ba.jpg", "Prato Borgonuovo")]
        [InlineData("./GoogleVision/Screenshots/b8c9a919-fff5-4290-b3db-7ed7dc6bf138.jpg", "Statuetta Madonna Con Bambino")]
        [InlineData("./GoogleVision/Screenshots/c39634c0-6a5a-40f4-8a2a-14fbd2eeeec3.jpg", "Scuola Calcio Tobbiana 1949")]
        [InlineData("./GoogleVision/Screenshots/ddf0a277-2f8c-4081-b5f5-ecddbecdeb43.jpg", "Croce Di Metallo")]
        public async void ExtractGymName_WithEggImage_ShouldRecognizeGymName(string screenshotPath, string expectedgymName)
        {
            var service = new EggFeaturesRecognizionService();
            var image = Image.FromFile(screenshotPath);
            TextAnnotation textAnnotation = await _client.DetectDocumentTextAsync(image);

            var extractedGymName = service.ExtractGymName(textAnnotation);

            Assert.Equal(expectedgymName, extractedGymName);
        }

        [Theory]
        [InlineData("./GoogleVision/Screenshots/d5f7a003-3efa-4850-b1a0-94a6ffab62e8.jpg", "00:32:14")]
        [InlineData("./GoogleVision/Screenshots/1adfad1c-a68c-4a05-8384-ffd736975b8c.jpg", "00:11:40")]
        [InlineData("./GoogleVision/Screenshots/1e5ce2c7-bfb7-4771-b24d-1f12b6ffb5cc.jpg", "00:29:41")]
        [InlineData("./GoogleVision/Screenshots/213ed098-d728-4a6c-9230-d808be275e15.jpg", "00:25:42")]
        [InlineData("./GoogleVision/Screenshots/29bf628a-5f5b-4b26-b59c-4ddeb1beb0ba.jpg", "00:50:44")]
        [InlineData("./GoogleVision/Screenshots/b8c9a919-fff5-4290-b3db-7ed7dc6bf138.jpg", "00:51:06")]
        [InlineData("./GoogleVision/Screenshots/c39634c0-6a5a-40f4-8a2a-14fbd2eeeec3.jpg", "00:45:21")]
        [InlineData("./GoogleVision/Screenshots/ddf0a277-2f8c-4081-b5f5-ecddbecdeb43.jpg", "00:54:46")]
        public async void ExtractCountdown_WithEggImage_ShouldRecognizeCountdown(string screenshotPath, string expectedCountdown)
        {
            var service = new EggFeaturesRecognizionService();
            var image = Image.FromFile(screenshotPath);
            TextAnnotation textAnnotation = await _client.DetectDocumentTextAsync(image);

            var extractedCountdown = service.ExtractCountDown(textAnnotation);

            Assert.Equal(expectedCountdown, extractedCountdown.ToString());
        }

        [Theory]
        [InlineData("./GoogleVision/Screenshots/d5f7a003-3efa-4850-b1a0-94a6ffab62e8.jpg", "19:44:00")]
        [InlineData("./GoogleVision/Screenshots/1adfad1c-a68c-4a05-8384-ffd736975b8c.jpg", "19:04:00")]
        [InlineData("./GoogleVision/Screenshots/1e5ce2c7-bfb7-4771-b24d-1f12b6ffb5cc.jpg", "16:05:00")]
        [InlineData("./GoogleVision/Screenshots/213ed098-d728-4a6c-9230-d808be275e15.jpg", "18:58:00")]
        [InlineData("./GoogleVision/Screenshots/29bf628a-5f5b-4b26-b59c-4ddeb1beb0ba.jpg", "16:13:00")]
        [InlineData("./GoogleVision/Screenshots/b8c9a919-fff5-4290-b3db-7ed7dc6bf138.jpg", "16:15:00")]
        [InlineData("./GoogleVision/Screenshots/c39634c0-6a5a-40f4-8a2a-14fbd2eeeec3.jpg", "10:32:00")]
        [InlineData("./GoogleVision/Screenshots/ddf0a277-2f8c-4081-b5f5-ecddbecdeb43.jpg", "16:31:00")]
        public async void ExtractTime_WithEggImage_ShouldRecognizeTime(string screenshotPath, string expectedTime)
        {
            var service = new EggFeaturesRecognizionService();
            var image = Image.FromFile(screenshotPath);
            TextAnnotation textAnnotation = await _client.DetectDocumentTextAsync(image);

            var extractTime = service.ExtractTime(textAnnotation);

            Assert.Equal(expectedTime, extractTime.ToString());
        }
    }
}
