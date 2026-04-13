using ERPFrigorifico.Client.Extensiones;
using ERPFrigorifico.Shared.DTOs.MovimientosAnimales;

namespace ERPFrigorifico.Client.ApiClients
{
    public class CorralApi(IHttpClientFactory factory)
    {
        private readonly HttpClient _httpClient = factory.CreateClient("Api");


        public async Task<int> EnviarAnimalesACorral(List<MovimientoAnimalResponse> request)
        {
            var url = _httpClient.PostJsonOrThrowAsync
                <List<MovimientoAnimalResponse>, CreatedIdResponse>
                ($"api/corrales", request);

            return url.Id;
        }


        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }

    }
}
