using Library.Repository.Repositories;
using library.repository;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Unit.Test.TestConfiguration
{
    public  class GenericSqliteInMemoryDbContext<TDbContext> where TDbContext : DbContext, IDisposable
    {
        public readonly TDbContext sqliteInMemoryDbContext;

        private SqliteConnection connection = new SqliteConnection("Filename=:memory:");

        public GenericSqliteInMemoryDbContext()
        {
            connection.Open();
            var contextOptions = new DbContextOptionsBuilder<TDbContext>()
                                .UseSqlite(connection)
                                .Options;
            sqliteInMemoryDbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext), contextOptions);

            sqliteInMemoryDbContext.Database.EnsureCreated();

        }
        public void Dispose()
        {
            connection.Close();
        }
    }


}
