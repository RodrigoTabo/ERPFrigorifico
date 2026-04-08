using ERPFrigorifico.Client.Extensiones;
using ERPFrigorifico.Shared.DTOs.Proveedor;

namespace ERPFrigorifico.Client.ApiClients
{
    public class ProveedorApi(IHttpClientFactory factory)
    {

        private readonly HttpClient _httpClient = factory.CreateClient("Api");

        public async Task<List<ProveedorResponse>> GetAll()
            => await _httpClient.GetJsonOrThrowAsync<List<ProveedorResponse>>($"api/proveedores");

    }
}
