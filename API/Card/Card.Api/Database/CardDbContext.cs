using Microsoft.EntityFrameworkCore;
using Card.Api.Model;


namespace Card.Api.Database
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(DbContextOptions<CardDbContext> options) : base(options) { }


        //Dbset
        public DbSet<Cards> Cards { get; set; }
    }
}
