using libraries.domain;
using library.services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IBookRepository : IDbRepository<Book>
    {
    
    }
}
