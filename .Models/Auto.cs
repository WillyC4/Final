using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Auto
    {
        [PrimaryKey]
        public string? id { get; set; }
        public string? make { get; set; }
        public string? model { get; set; }
        public string? year { get; set; }
        public string? vclass { get; set; }
        public string? trany { get; set; }
        public int? cylinders { get; set; }
        public string? drive { get; set; }
        public string? engid { get; set; }
        public string? fueltype { get; set; }
        public string? image { get; set; }
    }
}
