using System;
using System.Threading.Tasks;
using PartlyNewsy.Models;
using Refit;

namespace PartlyNewsy.Core
{
    public class NewsService
    {
        readonly string FunctionUrl = "http://localhost:7071/api";
        readonly INewsFunctions newsFunctions;

        public NewsService()
        {
            newsFunctions = RestService.For<INewsFunctions>(FunctionUrl);
        }

        public async Task<Article> GetTopNews()
        {
            return await newsFunctions.GetTopNewsFromFunction();
        }
    }

    public interface INewsFunctions
    {
        [Get("/GetTopNews")]
        Task<Article> GetTopNewsFromFunction();
    }
}
