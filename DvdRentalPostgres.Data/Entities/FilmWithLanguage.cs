namespace DvdRentalPostgres.Data.Entities
{
    public class FilmWithLanguage : Film
    {
        public Language Lang { get; set;  }

        public FilmWithLanguage(Film f)
            : base(f)
        {
            Lang = new Language();
        }
    }
}