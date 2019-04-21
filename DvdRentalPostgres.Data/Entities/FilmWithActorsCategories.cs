using System.Collections.Generic;

namespace DvdRentalPostgres.Data.Entities
{
    public class FilmWithActorsCategories : Film
    {
        public HashSet<Actor> Actors { get; }

        public HashSet<Category> Categories { get; }

        public FilmWithActorsCategories(Film f)
            : base(f)
        {
            Actors      = new HashSet<Actor>();
            Categories  = new HashSet<Category>();
        }
    }
}