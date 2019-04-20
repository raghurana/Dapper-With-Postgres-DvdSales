using System;

namespace DvdRentalPostgres.Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public DateTime LastUpdate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}