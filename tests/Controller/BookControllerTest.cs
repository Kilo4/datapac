using AutoMapper;
using datapac_interview.Dto.Book.request;
using datapac_interview.Models;
using datapac_interview.Presistance;
using datapac_interview.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace datapac_interview.Controllers;

public class BookControllerTest
{
[Fact]
    public async Task CreateBook_ReturnsCreatedResult_WhenBookIsCreated()
    {
        // Arrange
        var mockRepository = new Mock<IBookRepository>();
        var mockMapper = new Mock<IMapper>();
        var controller = new BookController(null, mockMapper.Object, mockRepository.Object);

        var createBookRequest = new CreateBookRequest
        {
            Title = "Test Book",
            Author = "Test Author"
            // Add other properties as needed
        };

        var createdBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = createBookRequest.Title,
            Author = createBookRequest.Author
            // Set other properties as needed
        };

        mockRepository.Setup(repo => repo.CheckByTitleAndAuthorAsync(It.IsAny<string>(), It.IsAny<string>()))
                      .ReturnsAsync(false); // Assume the book doesn't exist

        mockMapper.Setup(mapper => mapper.Map<CreateBookRequest, Book>(createBookRequest))
                   .Returns(createdBook);

        // Act
        var result = await controller.CreateBook(createBookRequest);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var model = Assert.IsType<Book>(createdAtActionResult.Value);
        Assert.Equal(createdBook.Id, model.Id);
        Assert.Equal(createdBook.Title, model.Title);
        Assert.Equal(createdBook.Author, model.Author);
    }

    // Add similar tests for other methods in BookController...

    [Fact]
    public async Task UpdateBook_ReturnsCreatedResult_WhenBookIsUpdated()
    {
        // Arrange
        var mockRepository = new Mock<IBookRepository>();
        var mockMapper = new Mock<IMapper>();
        var controller = new BookController(null, mockMapper.Object, mockRepository.Object);

        var updateBookRequest = new UpdateBookRequest
        {
            Title = "Updated Test Book",
            Author = "Updated Test Author"
            // Add other properties as needed
        };

        var existingBookId = Guid.NewGuid();
        var existingBook = new Book
        {
            Id = existingBookId,
            Title = "Test Book",
            Author = "Test Author"
            // Set other properties as needed
        };

        mockRepository.Setup(repo => repo.GetByIdAsync(existingBookId))
                      .ReturnsAsync(existingBook);

        mockRepository.Setup(repo => repo.CheckByTitleAndAuthorAsync(It.IsAny<string>(), It.IsAny<string>()))
                      .ReturnsAsync(false); // Assume the updated book doesn't exist

        // Act
        var result = await controller.UpdateBook(updateBookRequest, existingBookId);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var model = Assert.IsType<Book>(createdAtActionResult.Value);
        Assert.Equal(existingBookId, model.Id);
        Assert.Equal(updateBookRequest.Title, model.Title);
        Assert.Equal(updateBookRequest.Author, model.Author);
    }
}