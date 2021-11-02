using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VINNamespace
{
    /// <summary>
    /// Process and model data in the VINS.csv file
    /// </summary>
    public class VIN
    {
        public string licensePlate { get; set; }
        public string location { get; set; }
        public string area { get; set; }
        public string manu { get; set; }
        public string model { get; set; }
        public string  modelYear { get; set; }
        public string vin { get; set; }
        public string mileage { get; set; }
        public string comments { get; set; }

        public static VIN ParseRow(string row)
        {
            var columns = row.Split(',');
            return new VIN()
            {
                // There's a bug here...
                licensePlate = columns[0],
                location = columns[1],
                area = columns[2],
                manu = columns[3],
                model = columns[4],
                vin = columns[5],
                mileage = columns[6]
            };
        }
    }
}
