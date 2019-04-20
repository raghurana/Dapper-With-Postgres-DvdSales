using System;

namespace DvdRentalPostgres.Data.Entities
{
    public class Actor
    {
        public int ActorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime LastUpdate { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}