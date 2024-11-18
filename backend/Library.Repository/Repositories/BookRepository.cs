using libraries.domain;
using library.repository;
using library.services.Contracts;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}