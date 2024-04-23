using AndresVillarreal_project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresVillarreal_project2.Data
{
    public interface IProvinceRepository
    {
        Task<List<Province>> GetProvinces();
        Task<Province> GetProvince(int ID);
        Task AddProvince(Province provinceToAdd);
        Task UpdateProvince(Province provinceToUpdate);
        Task DeleteProvince(Province provinceToDelete);
    }
}
