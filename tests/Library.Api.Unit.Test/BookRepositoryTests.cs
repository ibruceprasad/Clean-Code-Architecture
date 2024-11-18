using FluentAssertions;
using libraries.domain;
using library.repository;
using library.services.Services;
using Library.Repository.Repositories;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Api.Unit.Test
{
    public class BookRepositoryTests
    {

        private readonly LibraryDbContext _mockLibraryDbContext;
        private readonly BookRepository _mockBookRepository;
        private DbSet<Book> _mockBooks;

        public BookRepositoryTests()
        {


            _mockLibraryDbContext = Substitute.For<LibraryDbContext>();
            _mockBookRepository = new BookRepository(_mockLibraryDbContext);
            _mockBooks = Substitute.For<DbSet<Book>>();

        }

        public async Task GetAllAsync_ReturnAllAvailableBooks()
        {
            // Arrange
            var books = new List<Book>()
            {
                new Book { Id = 10, Name = "Capital", Price = 100 },
                new Book { Id = 11, Name = "Freedom", Price = 300 }
            };
            _mockBooks.AddRange(books);
            _mockLibraryDbContext.Books.Returns(_mockBooks);

            // Act
            var bookReturned = await _mockBookRepository.GetAllAsync();

            // Assert 
            bookReturned.Should().HaveCount(2);
            bookReturned.Should().BeEquivalentTo(books);

        }
    }
}
