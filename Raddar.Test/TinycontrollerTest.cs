using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using PruebaRaddar.Controllers;
using Raddar.Api.Business;
using Raddar.Shared;
using Raddar.Api.Models;

namespace Raddar.Test 
{ 
    public class TinyControllerTests
    {
        private readonly TinyController _controller;
        private readonly Mock<IURLShortener> _mockUrlShortener;

        public TinyControllerTests()
        {
            _mockUrlShortener = new Mock<IURLShortener>();
            _controller = new TinyController(_mockUrlShortener.Object);
        }

        [Fact]
        public void Encode_ValidLongUrl_ReturnsShortUrl()
        {
            // Arrange
            var longUrl = new LongUrlRequestModel { Url = "https://raddarstudios.com/problems/design-tinyurl" };
            var shortUrl = $"{BasicConfigurations.BaseShortUrlResult}AwEjs2";
            _mockUrlShortener.Setup(us => us.Encode(longUrl.Url)).Returns(shortUrl);

            // Act
            var result = _controller.Encode(longUrl);
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(shortUrl, okResult.Value);
        }

        [Fact]
        public void Encode_InvalidUrl_ReturnsBadRequest()
        {
            // Arrange
            var longUrl = new LongUrlRequestModel { Url = "raddarstudios.com/problems/design-tinyurl" }; // Invalid URL
            _controller.ModelState.AddModelError("Url", "La URL corta no es una URL válida.");

            // Act
            var result = _controller.Encode(longUrl);
            var badResult = result.Result as BadRequestObjectResult;

            // Assert
            var errors = ((SerializableError)badResult.Value)["Url"] as string[];
            Assert.NotNull(errors);
            Assert.Contains("La URL corta no es una URL válida.", errors);

        }

        [Fact]
        public void Encode_InvalidProtocol_ReturnsBadRequest()
        {
            // Arrange
            var longUrl = new LongUrlRequestModel { Url = "raddarstudios.com/problems/design-tinyurl" }; // Invalid URL
            _controller.ModelState.AddModelError("Url", "La URL corta no es una URL válida.");

            // Act
            var result = _controller.Encode(longUrl);
            var badResult = result.Result as BadRequestObjectResult;

            // Assert
            var errors = ((SerializableError)badResult.Value)["Url"] as string[];
            Assert.NotNull(errors);
            Assert.Contains("La URL corta no es una URL válida.", errors);
        }
    }
}
