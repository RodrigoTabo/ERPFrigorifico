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

        public async Task<List<MovimientoAnimalByIdResponse>> GetAllMovimientosAnimales(int id)
            => await _httpClient.GetJsonOrThrowAsync<List<MovimientoAnimalByIdResponse>>($"api/movimientosanimales/{id}");


        public async Task EnviarAnimales(List<int> animalIds)
           => await _httpClient.PostOrThrowAsync($"api/movimientosanimales", animalIds);


        private class CreatedIdResponse
        {
            public int Id { get; set; }
        }
    }
}
