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
    public class BookRepositoryReadTests : GenericSqliteInMemoryDbContext<LibraryDbContext>, IAsyncLifetime
    {
        private readonly BookRepository _bookRepository;
        public BookRepositoryReadTests()
        {
            _bookRepository = new BookRepository(sqliteInMemoryDbContext);
        }
        public async Task DisposeAsync()
        {
            base.Dispose();
        }

        [Fact]
        public async Task Check_IfDatabaseCreated()
        {
            sqliteInMemoryDbContext.Database.IsSqlite().Should().BeTrue();
            sqliteInMemoryDbContext.Database.CanConnect().Should().BeTrue();
            var connectionString = sqliteInMemoryDbContext.Database.GetConnectionString();
        }

  
        [Fact]
        public async Task GetAllAsync_ReturnsAllBooks()
        {
            //Act
            var existingBooks = await _bookRepository.GetAllAsync();
            // Assert
            existingBooks.Should().BeOfType<List<Book>>();
            existingBooks.Should().HaveCount(2);
            existingBooks[0].Id.Should().Be(1);
            existingBooks[1].Id.Should().Be(2);
        }

        public async Task InitializeAsync()
        {
            Console.WriteLine("Called");
        }
    }


}
 
