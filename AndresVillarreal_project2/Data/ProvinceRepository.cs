using AndresVillarreal_project2.Models;
using AndresVillarreal_project2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AndresVillarreal_project2.Data
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly HttpClient client = new HttpClient();

        public ProvinceRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Province>> GetProvinces()
        {
            HttpResponseMessage response = await client.GetAsync("api/Province");
            if (response.IsSuccessStatusCode)
            {
                List<Province> provinces = await response.Content.ReadAsAsync<List<Province>>();
                return provinces;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<Province> GetProvince(int ProvinceID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Province/{ProvinceID}");
            if (response.IsSuccessStatusCode)
            {
                Province province = await response.Content.ReadAsAsync<Province>();
                return province;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddProvince(Province provinceToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/Province", provinceToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateProvince(Province provinceToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/Province/{provinceToUpdate.ID}", provinceToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteProvince(Province provinceToDelete)
        {
            var response = await client.DeleteAsync($"/api/Province/{provinceToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

    }
}
