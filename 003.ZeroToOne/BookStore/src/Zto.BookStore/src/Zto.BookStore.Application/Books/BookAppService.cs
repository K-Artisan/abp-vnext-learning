using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Zto.BookStore.Books
{
    public class BookAppService :
            CrudAppService<
                Book,                //The Book entity
                BookDto,             //Used to show books
                Guid,                //Primary key of the book entity
                PagedAndSortedResultRequestDto, //Used for paging/sorting
                CreateUpdateBookDto>,           //Used to create/update a book
            IBookAppService                     //implement the IBookAppService
    {

        public BookAppService(IRepository<Book, Guid> repository)
           : base(repository)
        {
        }

        //public override async Task<BookDto> GetAsync(Guid id)
        //{
        //    var query = from book in Repository
        //                select new { book };

        //    var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
        //    bookDto.AuthorName = queryResult.author.Name;
        //    return bookDto;
        //}



    }
}



