using AutoMapper;
using datapac_interview.Controllers;
using datapac_interview.Dto.Book.request;
using datapac_interview.Models;
using datapac_interview.Presistance;
using datapac_interview.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

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
            Author = "Test Author",
            Price = 5,
        };

        var createdBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = createBookRequest.Title,
            Author = createBookRequest.Author,
            Available = true,
            Price = 5
        };

        mockRepository.Setup(repo => repo.CheckByTitleAndAuthorAsync(It.IsAny<string>(), It.IsAny<string>()))
                      .ReturnsAsync(false); // Assume the book doesn't exist

        mockMapper.Setup(mapper => mapper.Map<CreateBookRequest, Book>(createBookRequest))
                   .Returns(createdBook);

        // Act
        var result = await controller.CreateBook(createBookRequest);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var model = Assert.IsType<Book>(createdAtActionResult.Value);
        Assert.Equal(createdBook.Id, model.Id);
        Assert.Equal(createdBook.Title, model.Title);
        Assert.Equal(createdBook.Author, model.Author);
    }

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
        };

        var existingBookId = Guid.NewGuid();
        var existingBook = new Book
        {
            Id = existingBookId,
            Title = "Test Book",
            Author = "Test Author"
        };

        mockRepository.Setup(repo => repo.GetByIdAsync(existingBookId))
                      .ReturnsAsync(existingBook);

        mockRepository.Setup(repo => repo.CheckByTitleAndAuthorAsync(It.IsAny<string>(), It.IsAny<string>()))
                      .ReturnsAsync(false);

        mockRepository.Setup(repo => repo.UpdateAsync(existingBook, updateBookRequest))
            .ReturnsAsync(new Book
            {
                Id = existingBookId,
                Title = "Updated Test Book",
                Author = "Updated Test Author"
            });

        // Act
        var result = await controller.UpdateBook(updateBookRequest, existingBookId);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var model = Assert.IsType<Book>(createdAtActionResult.Value);
        Assert.Equal(existingBookId, model.Id);
        Assert.Equal(updateBookRequest.Title, model.Title);
        Assert.Equal(updateBookRequest.Author, model.Author);
    }
}