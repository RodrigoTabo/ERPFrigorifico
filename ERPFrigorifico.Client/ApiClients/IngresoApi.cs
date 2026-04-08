using ERPFrigorifico.Client.Extensiones;
using ERPFrigorifico.Shared.DTOs.Ingresos;

namespace ERPFrigorifico.Client.ApiClients
{
    public class IngresoApi(IHttpClientFactory factory)
    {

        private readonly HttpClient _httpClient = factory.CreateClient("Api");

        public async Task<int> RegistrarIngreso(RegistrarIngresoRequest request)
        {
            var ingreso = await _httpClient.PostJsonOrThrowAsync
                <RegistrarIngresoRequest, CreatedIdResponse>
                ($"api/ingresos/ingreso", request);

            return ingreso.Id;
        }

        public async Task RegistrarSalida(string patente)
        {
           await _httpClient.DeleteOrThrowAsync($"api/ingresos/salida/{patente}");
        }

        public async Task<List<IngresoListadoResponse>> GetIngresosActivos()
            => await _httpClient.GetJsonOrThrowAsync<List<IngresoListadoResponse>>($"api/ingresos/ingresosactivos");

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }

    }
}
