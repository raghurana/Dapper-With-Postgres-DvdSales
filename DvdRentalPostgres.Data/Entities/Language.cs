using System;

namespace DvdRentalPostgres.Data.Entities
{
    public class Language
    {
        public int LanguageId { get; set; }

        public string Name { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}