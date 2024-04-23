using AndresVillarreal_project2.Models;
using AndresVillarreal_project2.Utilities;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AndresVillarreal_project2.Data
{
    public class CityRepository : ICityRepository
    {
        private readonly HttpClient client = new HttpClient();

        public CityRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<City>> GetCities()
        {
            HttpResponseMessage response = await client.GetAsync("api/City");
            if (response.IsSuccessStatusCode)
            {
                List<City> Cities = await response.Content.ReadAsAsync<List<City>>();
                return Cities;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<List<City>> GetCitiesByProvince(int ProvinceID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/City/ByProvince/{ProvinceID}");
            if (response.IsSuccessStatusCode)
            {
                List<City> Cities = await response.Content.ReadAsAsync<List<City>>();
                return Cities;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<City> GetCity(int ID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/City/{ID}");
            if (response.IsSuccessStatusCode)
            {
                City City = await response.Content.ReadAsAsync<City>();
                return City;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task AddCity(City cityToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/City", cityToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateCity(City cityToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/City/{cityToUpdate.ID}", cityToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteCity(City cityToDelete)
        {
            var response = await client.DeleteAsync($"/api/City/{cityToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
