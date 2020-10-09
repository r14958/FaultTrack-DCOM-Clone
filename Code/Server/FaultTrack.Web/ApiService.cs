namespace FaultTrack.Web
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;

    public abstract class ApiService : IDisposable
    {
        private bool disposed;
        protected readonly HttpClient Client;
        protected readonly Uri ServiceUri;

        protected ApiService(Uri serviceUri)
        {
            ServiceUri = serviceUri;

            Client = new HttpClient
                     {
                         BaseAddress = ServiceUri
                     };

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> Get<T>(string url)
        {
            HttpResponseMessage message;

            try
            {
                message = await Client.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                throw new ServiceException(HttpStatusCode.ServiceUnavailable, ex.Message, ex.Message);
            }

            return await GetResultAsync<T>(message);
        }

        protected async Task Post<T>(string url, T value)
        {
            HttpResponseMessage message;

            try
            {
                message = await Client.PostAsJsonAsync(url, value);
            }
            catch (HttpRequestException ex)
            {
                throw new ServiceException(HttpStatusCode.ServiceUnavailable, ex.Message, ex.Message);
            }

            await GetResultAsync(message);
        }

        protected async Task<U> Post<T, U>(string url, T value)
        {
            HttpResponseMessage message;

            try
            {
                message = await Client.PostAsJsonAsync(url, value);
            }
            catch (HttpRequestException ex)
            {
                throw new ServiceException(HttpStatusCode.ServiceUnavailable, ex.Message, ex.InnerException.Message);
            }

            return await GetResultAsync<U>(message);
        }

        protected async Task GetResultAsync(HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                HttpError result = await message.Content.ReadAsAsync<HttpError>();

                throw new ServiceException(message.StatusCode, message.ReasonPhrase, result.ExceptionMessage);
            }
        }

        protected async Task<T> GetResultAsync<T>(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                return await message.Content.ReadAsAsync<T>();
            }

            HttpError result = await message.Content.ReadAsAsync<HttpError>();

            throw new ServiceException(message.StatusCode, message.ReasonPhrase, result.ExceptionMessage);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing)
            {
                return;
            }

            Client.Dispose();

            disposed = true;
        }
    }
}