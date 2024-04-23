using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresVillarreal_project2.Models
{
    public class Province
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Byte[] RowVersion { get; set; }


        public ICollection<City> Cities { get; set; }
    }
}
