using libraries.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.repository.Configure
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>().HasData(
               new Book { Id = 1, Name = "Hamlet", Price = 200 },
                new Book { Id = 2, Name = "Othello", Price = 300 }
            );
        }
    }

}
