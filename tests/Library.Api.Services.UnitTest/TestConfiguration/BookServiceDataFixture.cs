using AutoMapper;
using Library.Services.Contracts;
using library.Services.Helpers.MappingProfiles;
using library.services.Services;
using Library.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Api.Services.UnitTest.TestConfiguration
{
    public class BookServiceDataFixture
    {
        public readonly IBookServices bookService;
        public readonly Mock<IBookRepository> mockBookRepository;
        private readonly IDataValidaion _dataValidaion;
        public Mapper mapper;

        public BookServiceDataFixture()
        {
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            mapper = new Mapper(configuration);
            _dataValidaion = new DataValidaion();
            mockBookRepository = new Mock<IBookRepository>();
            bookService = new BookServices(mockBookRepository.Object, mapper, _dataValidaion);
        }
    }
}
