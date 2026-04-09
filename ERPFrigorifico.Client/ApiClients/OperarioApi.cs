using ERPFrigorifico.Client.Extensiones;
using ERPFrigorifico.Shared.DTOs.Operario;

namespace ERPFrigorifico.Client.ApiClients
{
    public class OperarioApi(IHttpClientFactory factory)
    {

        private readonly HttpClient _httpClient = factory.CreateClient("Api");

        public async Task<List<OperarioResponse>> GetAllOperariosHabilitados()
            => await _httpClient.GetJsonOrThrowAsync<List<OperarioResponse>>($"api/operarios");

    }
}
