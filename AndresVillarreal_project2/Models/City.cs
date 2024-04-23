using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresVillarreal_project2.Models
{
    public class City
    {
        public int ID { get; set; }

        public string Summary
        {
            get
            {
                return Name + ", " + ProvinceID;
            }
        }

        public string Name { get; set; }


        public Byte[] RowVersion { get; set; }


        //Foreign Key
        public int ProvinceID { get; set; }
        public Province Province { get; set; }
    }
}
