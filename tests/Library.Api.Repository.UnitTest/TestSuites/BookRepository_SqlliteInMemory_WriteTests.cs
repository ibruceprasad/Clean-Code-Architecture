using FluentAssertions;
using libraries.domain;
using library.repository;
using Library.Api.Unit.Test.TestConfiguration;
using Library.Repository.Repositories;
using Library.Services.Contracts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockQueryable.Moq;
using MockQueryable.NSubstitute;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MoqBuilder = MockQueryable.Moq;

namespace Library.Api.Unit.Test.TestSuites
{ 
    public class BookRepositoryWriteTests :  IClassFixture<GenericSqliteInMemoryDbContext<LibraryDbContext>>
    {
        private readonly BookRepository _bookRepository;
        public BookRepositoryWriteTests(GenericSqliteInMemoryDbContext<LibraryDbContext> librarySqlliteInMemory) 
        {
            _bookRepository = new BookRepository(librarySqlliteInMemory.sqliteInMemoryDbContext);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsUpdatedBookDetail_GivenExistingBookDetail()
        {
            var allBooks = await _bookRepository.GetAllAsync();
            //Arrange
            var book = new Book() { Name = "Journies", Price = 101 };
            var newBook = await _bookRepository.AddAsync(book);
            newBook.Price = 802;
            // Act
            var updatedBook = await _bookRepository.UpdateAsync(newBook.Id, newBook);
            //Assert
            var item= await _bookRepository.GetByIdAsync(updatedBook.Id);
            updatedBook.Price.Should().Be(802);
        }
               
        [Fact]
        public async Task AddAsync_ReturnsAddedBookDetail_GivenNewBookDetail()
        {
            //Arrange
            var newBook = new Book() { Name = "All Findings", Price = 201 };
            // Act
            var bookCreated = await _bookRepository.AddAsync(newBook);
            //Assert
            var allBooks = await _bookRepository.GetAllAsync();
            allBooks.Should().HaveCount(bookCreated.Id);
            allBooks[bookCreated.Id-1].Id.Should().Be(bookCreated.Id);
        }

    }
}
 
