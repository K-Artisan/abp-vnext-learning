using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Zto.BookStore.Books;

namespace Zto.BookStore.HttpApi.Client.ConsoleTestApp
{
    public class ClientDemoService : ITransientDependency
    {
        private readonly IBookAppService _bookAppService;

        public ClientDemoService(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task RunAsync()
        {
            var requstDto = new PagedAndSortedResultRequestDto
            {
                Sorting = "PublishDate desc"
            };

            PagedResultDto<BookDto> output = await _bookAppService.GetListAsync(requstDto);
            Console.WriteLine($"BookList:{JsonConvert.SerializeObject(output)}");
        }
    }
}
