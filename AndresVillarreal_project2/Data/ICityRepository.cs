using AndresVillarreal_project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresVillarreal_project2.Data
{
    public interface ICityRepository
    {
        Task<List<City>> GetCities();
        Task<City> GetCity(int id);
        Task<List<City>> GetCitiesByProvince(int provinceID);
        Task AddCity(City cityToAdd);
        Task UpdateCity(City cityToUpdate);
        Task DeleteCity(City cityToDelete);
    }
}
