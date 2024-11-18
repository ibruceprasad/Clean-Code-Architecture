using AutoMapper;
using libraries.domain;
using library.Services.Domain;
using library.Services.Domain.Dtos;
using Library.Services.Contracts;
using Library.Services.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace library.services.Services
{
    public interface IBookServices
    {
        Task<List<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<ServiceResult<BookDto>> AddBookAsync(BookDto bookDto);
        Task<ServiceResult<BookDto>> UpdateBookAsync(int id, BookDto bookDto);
        Task<ServiceResult<BookDto>> DeleteBookAsync(int id);
    }

    public class BookServices : IBookServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IDataValidaion _dataValidation;
        private readonly IMapper _mapper;
 
        public BookServices(IBookRepository bookRepository, IMapper mapper, IDataValidaion dataValidation) 
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _dataValidation = dataValidation;
        }
      
        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<ServiceResult<BookDto>> AddBookAsync(BookDto bookDto)
        {
            var validationResult = _dataValidation.ValidateData(bookDto);

            if (!string.IsNullOrEmpty(validationResult))
                return ServiceResult<BookDto>.SetFailureServiceResult(HttpStatusCode.BadRequest, validationResult);

            var book = _mapper.Map<Book>(bookDto);
            var status = await _bookRepository.AddAsync(book);
            var createdItem = await GetBookByIdAsync(book.Id);

            return  ServiceResult<BookDto>.SetSuccessServiceResult(HttpStatusCode.OK, createdItem);
        }

        public async Task<ServiceResult<BookDto>> UpdateBookAsync(int id, BookDto bookDto)
        {
            var validationResult = _dataValidation.ValidateData(bookDto);

            if (!string.IsNullOrEmpty(validationResult))
                return ServiceResult<BookDto>.SetFailureServiceResult(HttpStatusCode.BadRequest, validationResult);
            if(id != bookDto.Id)
                return ServiceResult<BookDto>.SetFailureServiceResult(HttpStatusCode.NotFound, $"Id:{id} is not maches with enity id:{bookDto.Id}");

            var book = _mapper.Map<Book>(bookDto);
            var updatedBook = await _bookRepository.UpdateAsync(id, book);
     
            return ServiceResult<BookDto>.SetSuccessServiceResult(HttpStatusCode.OK, bookDto);

            
        }

        public async Task<ServiceResult<BookDto>> DeleteBookAsync(int id)
        {
            var status = await _bookRepository.DeleteAsync(id);
            if (status)
                return ServiceResult<BookDto>.SetSuccessServiceResult(HttpStatusCode.OK, default);
            return ServiceResult<BookDto>.SetFailureServiceResult(HttpStatusCode.NotFound, default);
        }
    }
}
