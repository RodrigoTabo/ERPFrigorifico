namespace ERPFrigorifico.Client.Extensiones
{
    public static class HttpClientExtension
    {

        public static async Task<T> GetJsonOrThrowAsync<T>(this HttpClient http, string url)
        {
            var resp = await http.GetAsync(url);

            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<T>();

                if (data is null)
                    throw new HttpApiException("La API devolvió una respuesta vacía.");

                return data;
            }

            throw await HttpApiException.FromHttpResponse(resp);
        }

        public static async Task<TResp> PostJsonOrThrowAsync<TReq, TResp>(this HttpClient http, string url, TReq body)
        {
            var resp = await http.PostAsJsonAsync(url, body);

            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<TResp>();
                if (data is null)
                    throw new HttpApiException("La API devolvió una respuesta vacía.");

                return data;
            }
            throw await HttpApiException.FromHttpResponse(resp);
        }

        public static async Task<TResp> PutJsonOrThrowAsync<TReq, TResp>(this HttpClient http, string url, TReq body)
        {
            var resp = await http.PutAsJsonAsync(url, body);

            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<TResp>();

                if (data is null)
                    throw new HttpApiException("La API devolvió una respuesta vacía.");

                return data;

            }

            throw await HttpApiException.FromHttpResponse(resp);
        }

        public static async Task DeleteOrThrowAsync(this HttpClient http, string url)
        {
            var resp = await http.DeleteAsync(url);

            if (resp.IsSuccessStatusCode)
                return;

            throw await HttpApiException.FromHttpResponse(resp);
        }


    }
}
