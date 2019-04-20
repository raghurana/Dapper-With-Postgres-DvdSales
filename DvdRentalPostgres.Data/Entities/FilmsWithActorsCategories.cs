using System.Collections.Generic;

namespace DvdRentalPostgres.Data.Entities
{
    public class FilmsWithActorsCategories : Film
    {
        public HashSet<Actor> Actors { get; }

        public HashSet<Category> Categories { get; }

        public FilmsWithActorsCategories(Film f)
        {
            FilmId = f.FilmId;
            Title = f.Title;
            Description = f.Description;
            Actors = new HashSet<Actor>();
            Categories = new HashSet<Category>();
        }
    }
}