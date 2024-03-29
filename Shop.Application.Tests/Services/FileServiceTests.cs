﻿using Xunit;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.AspNetCore.Hosting;

namespace Shop.Application.Services.Tests
{
    public class FileServiceTests
    {
        public class FileUploadServiceTests
        {
            [Fact]
            public void UploadFile_WhenImageIsNull_ReturnsNull()
            {
                // Arrange
                var webHostEnvironment = new Mock<IWebHostEnvironment>();
                IFormFile nullImage = null;
                var fileUploadService = new FileService();

                // Act
                var result = fileUploadService.UploadFile(nullImage, webHostEnvironment.Object);

                // Assert
                Assert.Null(result);
            }
        }
    }
}