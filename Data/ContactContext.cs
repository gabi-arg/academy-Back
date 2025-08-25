using Microsoft.EntityFrameworkCore;
using ContactApi.Models;

namespace ContactApi.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options) {}

        public DbSet<ContactForm> ContactForms { get; set; }
    }
}
