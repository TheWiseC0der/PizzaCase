namespace client
{
    public class HttpClientCon : IClient
    {
        private HttpClient _httpClient;
        private readonly string _url;

        public void Start()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url);
        }

        public HttpClientCon(string? url)
        {
            _url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public HttpClientCon() : this("http://localhost:8080/")
        {
        }

        public void Stop() => _httpClient.Dispose();

        public string Read()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_url).Result;
                response.EnsureSuccessStatusCode();
                string responseData = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseData);
                return responseData;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("error: {0}", e.Message);
                return string.Empty;
            }
        }

        public void Write(string msg)
        {
            try
            {
                var response = _httpClient.PostAsync(_url, new StringContent(msg)).Result;
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine("error: {0}", e.Message);
            }
        }
    }
}