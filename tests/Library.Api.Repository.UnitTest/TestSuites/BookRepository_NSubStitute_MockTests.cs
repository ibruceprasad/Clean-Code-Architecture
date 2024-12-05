using FluentAssertions;
using libraries.domain;
using library.repository;
using library.services.Services;
using library.Services.Domain.Dtos;
using Library.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MockQueryable.NSubstitute;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Diagnostics;

namespace Library.Api.Unit.Test.TestSuites
{
    // Note: Mocking of the Entry(entity) management is difficut using NSubStitute or Moq, hence go for inmemory test ???
    // _libraryDbContext.Set<T>().Entry(entity).State = EntityState.Modified; 

    public class BookRepository_NSubStitute_MockTests
    {
        private readonly LibraryDbContext _mockLibraryDbContext;
        private readonly DbSet<Book> _mockDBSetBooks;

        private readonly BookRepository _bookRepository;
        public BookRepository_NSubStitute_MockTests()
        {
            _mockLibraryDbContext = Substitute.For<LibraryDbContext>();
            _mockDBSetBooks = Substitute.For<DbSet<Book>>();
            _bookRepository = new BookRepository(_mockLibraryDbContext);
        }


        [Fact]
        public async Task GetAllAsync_ReturnsAllBooks()
        {
            //Arrange
            var books = new List<Book>()
            {
                 new Book() { Id=20, Name="Capital", Price = 201 },
                 new Book() { Id=21, Name="Capital", Price = 201 }
            };

            var mockDbSet = books.BuildMockDbSet();
            var bookSet = _mockLibraryDbContext.Set<Book>().Returns(mockDbSet);
            // Act
            var allBooks = await _bookRepository.GetAllAsync();
            // Assert
            allBooks.Should().BeEquivalentTo(books);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnsBookDetails_GivenBookId()
        {


            //Arrange
            var books = new List<Book>() {
                new Book() { Id = 20, Name = "Capital", Price = 201 },
                new Book(){ Id = 21, Name = "Findings", Price = 201 }
            };
            var mockDbSet = books.BuildMockDbSet();
            var bookSet = _mockLibraryDbContext.Set<Book>().Returns(mockDbSet);
            // Act
            var returnedBook = await _bookRepository.GetByIdAsync(20);
            // Assert
            returnedBook.Should().BeEquivalentTo(books[0]);
        }

        [Fact]
        public async Task AddAsync_ReturnsAddedBookDetail_GivenNewBookDetail()
        {
            //Arrange
            var books = new List<Book>() {
                new Book() { Id = 20, Name = "Capital", Price = 201 },
                new Book(){ Id = 21, Name = "Findings", Price = 201 }
            };
            var newBook = new Book() { Id = 23, Name = "Findings", Price = 201 };
            var mockDbSet = books.BuildMockDbSet();
            var configureDbSet = _mockLibraryDbContext.Set<Book>().Returns(mockDbSet);
            var configureSaveChanges = _mockLibraryDbContext.SaveChangesAsync().Returns(1);

            // Act
            var returnedBook = await _bookRepository.AddAsync(newBook);

            // Assert
            returnedBook.Should().BeEquivalentTo(newBook);
        }


        [Fact]
        public async Task AddAsync_ReturnsApplicationException_WhenFailedToSave()
        {
            //Arrange
            var books = new List<Book>() {
                new Book() { Id = 20, Name = "Capital", Price = 201 },
                new Book(){ Id = 21, Name = "Findings", Price = 201 }
            };
            var newBook = new Book() { Id = 23, Name = "Findings", Price = 201 };
            var mockDbSet = books.BuildMockDbSet();
            var configureDbSet = _mockLibraryDbContext.Set<Book>().Returns(mockDbSet);
            var configureSaveChanges = _mockLibraryDbContext.SaveChangesAsync().Returns(0);

            // Act + Assert
             await Assert.ThrowsAsync<ApplicationException>(async () => await _bookRepository.AddAsync(newBook));
        }



        [Fact]
        public async Task UpdateAsync_ReturnsUpdatedBookDetail_GivenExistingBookDetail()
        {

            // Note: Mocking of the Entry(entity) management is difficut using NSubStitute or Moq, hence go for inmemort test ???
            // _libraryDbContext.Set<T>().Entry(entity).State = EntityState.Modified; 

            ////Arrange
            //var books = new List<Book>() {
            //    new Book() { Id = 20, Name = "Capital", Price = 201 },
            //    new Book() { Id = 21, Name = "Findings", Price = 201 }
            //};
            //var updatedBook = new Book() { Id = 21, Name = "All Findings", Price = 201 };
            //var mockDbSet = books.BuildMockDbSet();

            //var configureDbSet = _mockLibraryDbContext.Set<Book>().Returns(mockDbSet);


            ////_mockLibraryDbContext.Entry(updatedBook).Returns(new EntityEntry<Book> (new InternalEntry()){ State = EntityState.Modified });
            ////_mockLibraryDbContext.SaveChangesAsync().Returns(Task.FromResult(1));
            //var updateDbSet = _bookRepository.UpdateAsync(21, updatedBook).Returns(updatedBook);

            ////var configureDbSet = _mockLibraryDbContext.Set<Book>().Returns(mockDbSet);

            ////var mockEntityEntry = new MockEntityEntry<Book>();
            ////_mockLibraryDbContext.Set<Book>().Entry(updatedBook).Returns(mockEntityEntry);


            ////var mockEntityEntry = Substitute.For<EntityEntry<Book>>();
            ////mockEntityEntry.State = EntityState.Modified;
            ////_mockLibraryDbContext.Set<Book>().Entry(updatedBook).Returns(mockEntityEntry);

            //// _mockLibraryDbContext.Set<Book>().Entry(updatedBook).State = EntityState.Modified;


            //// Act  
            //var updatedBookDetail = await _bookRepository.UpdateAsync(21, updatedBook);

            //// Assert
            //updatedBookDetail.Should().BeEquivalentTo(updatedBook); 
        }



    }




}