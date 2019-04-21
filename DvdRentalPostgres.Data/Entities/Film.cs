namespace DvdRentalPostgres.Data.Entities
{
    public class Film
    {
        public int FilmId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Film()
        {}

        public Film(Film f)
        {
            FilmId      = f.FilmId;
            Title       = f.Title;
            Description = f.Description;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}