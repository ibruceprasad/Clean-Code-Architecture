using AutoMapper;
using FluentAssertions;
using libraries.domain;
using library.services.Services;
using library.Services.Domain;
using library.Services.Domain.Dtos;
using library.Services.Helpers.MappingProfiles;
using Library.Api.Services.UnitTest.TestConfiguration;
using Library.Services.Contracts;
using Library.Services.Services;
using Moq;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Api.Services.UnitTest.TestSuites
{

    
    public class BookServiceTests : IClassFixture<BookServiceDataFixture>
    {
        public readonly BookServiceDataFixture _fixture;
        public BookServiceTests(BookServiceDataFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact] 
        public async Task GetAllBooksAsync_ReturnsAllBooks()
        {
            // Arrange
            var books = new List<Book>() {
                new Book { Id = 1, Name = "Hamlet", Price = 200 },
                new Book { Id = 2, Name = "Othello", Price = 300 }
            };
            var expected = _fixture.mapper.Map<List<BookDto>>(books);
            // Act
            _fixture.mockBookRepository.Setup(x=>x.GetAllAsync()).ReturnsAsync(books);
            List<BookDto> allBooksReturened = await _fixture.bookService.GetAllBooksAsync();

            // Assert
            allBooksReturened.Should().BeOfType<List<BookDto>>();
            allBooksReturened.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetBookByIdAsync_GivenExistingBookId_ReturnBookDetail()
        {
            // Arrange
            var book =
                new Book { Id = 1, Name = "Hamlet", Price = 200 };
               
            var expected = _fixture.mapper.Map<BookDto>(book);
            // Act
            _fixture.mockBookRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(book);
            BookDto booksReturened = await _fixture.bookService.GetBookByIdAsync(1);

            // Assert
            booksReturened.Should().BeOfType<BookDto>();
            booksReturened.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task AddBookAsync_GivenExistingBookId_ReturnBookCreated()
        {
            // Arrange
            var bookDto = new BookDto() { Id = 1, Name = "Hamlet", Price = 200 };
            var book = _fixture.mapper.Map<Book>(bookDto);
            var expected = _fixture.mapper.Map<BookDto>(book);
            // Act
            _fixture.mockBookRepository.Setup(x => x.AddAsync(book)).ReturnsAsync(book);
            _fixture.mockBookRepository.Setup(x => x.GetByIdAsync(book.Id)).ReturnsAsync(book);

            ServiceResult<BookDto> result = await _fixture.bookService.AddBookAsync(bookDto);
            // Assert
            result.Data.Should().BeEquivalentTo(expected);
            result.Status.Should().Be(System.Net.HttpStatusCode.OK);
            result.IsSuccess.Should().BeTrue();
            result.ErrorMessage.Should().Be(default);
          
        }

        [Fact]
        public async Task UpdateBookAsync_GivenExistingBook_ReturnUpdatedBook()
        {
            // Arrange
            var bookDto = new BookDto() { Id = 1, Name = "Hamlet", Price = 200 };
            var book = _fixture.mapper.Map<Book>(bookDto);
            var expected = _fixture.mapper.Map<BookDto>(book);
            // Act
            _fixture.mockBookRepository.Setup(x => x.UpdateAsync(1, book)).ReturnsAsync(book);

            ServiceResult<BookDto> result = await _fixture.bookService.UpdateBookAsync(1, bookDto);
            // Assert
            result.Data.Should().BeEquivalentTo(expected);
            result.Status.Should().Be(System.Net.HttpStatusCode.OK);
            result.IsSuccess.Should().BeTrue();
            result.ErrorMessage.Should().Be(default);

        }


    }
}