using ERPFrigorifico.Client.Extensiones;
using ERPFrigorifico.Shared.DTOs;
using ERPFrigorifico.Shared.DTOs.MovimientosAnimales;
using ERPFrigorifico.Shared.Enums;

namespace ERPFrigorifico.Client.ApiClients
{
    public class MovimientoAnimalApi(IHttpClientFactory factory)
    {

        private readonly HttpClient _httpClient = factory.CreateClient("Api");

        public async Task<PagedResult<MovimientoAnimalResponse>> GetAllMovimientosAnimales(int pageIndex, int pageSize, TipoMovimiento? tipoMovimiento)
        {

            var url = $"api/movimientosanimales?pageIndex={pageIndex}&pageSize={pageSize}";

            if (tipoMovimiento.HasValue)
            {
                url += $"&tipoMovimiento={(int)tipoMovimiento.Value}";
            }

            return await _httpClient.GetJsonOrThrowAsync<PagedResult<MovimientoAnimalResponse>>(url);
        }

        public async Task<int> EnviarAnimales(List<int> animalIds)
        {
            var enviar = await _httpClient.PostJsonOrThrowAsync
                 <List<int>, CreatedIdResponse>
                 ($"api/movimientosanimales", animalIds);

            return enviar.Id;
        }

        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
