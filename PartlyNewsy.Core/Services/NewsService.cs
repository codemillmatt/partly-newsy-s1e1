using System;
using System.Threading.Tasks;
using PartlyNewsy.Models;
using Refit;
using Xamarin.Essentials;

namespace PartlyNewsy.Core
{
    public class NewsService
    {
        readonly string FunctionUrl = "http://localhost:7071/api";
        readonly INewsFunctions newsFunctions;

        public NewsService()
        {
            // this is to account for Android emulator pecularities in local networking
            // https://developer.android.com/studio/run/emulator-networking.html
            if (DeviceInfo.DeviceType == DeviceType.Virtual && DeviceInfo.Platform == DevicePlatform.Android)
                FunctionUrl = "http://10.0.2.2:7071/api";

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
