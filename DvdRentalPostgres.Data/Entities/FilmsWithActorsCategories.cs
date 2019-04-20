using System.Collections.Generic;

namespace DvdRentalPostgres.Data.Entities
{
    public class FilmsWithActorsCategories : Film
    {
        public List<Actor> Actors { get; set; }

        public List<Category> Categories { get; set; }
    }
}