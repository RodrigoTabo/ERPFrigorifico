using ERPFrigorifico.Client.Extensiones;
using ERPFrigorifico.Shared.DTOs.Faenas;

namespace ERPFrigorifico.Client.ApiClients
{
    public class FaenaApi(IHttpClientFactory factory)
    {

        private readonly HttpClient _httpClient = factory.CreateClient("Api");

        public async Task<List<FaenaResponse>> GetAllFaena()
            => await _httpClient.GetJsonOrThrowAsync<List<FaenaResponse>>($"api/faenas");

        public async Task<FaenaResponse> GetFaenaEnProceso()
            => await _httpClient.GetJsonOrThrowAsync<FaenaResponse>($"api/faenas/proceso");

        public async Task ProcesarFaena(int id)
          => await _httpClient.PostAsync($"api/faenas/{id}/procesar", null);

        public async Task TerminarFaena(int id)
          => await _httpClient.PostAsync($"api/faenas/{id}/terminar", null);




        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }

    }
}
