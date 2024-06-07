using Microsoft.EntityFrameworkCore;
using Edumin.Models;

namespace Edumin.Context
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<School> Schools { get; set; }
		// Adicione outras propriedades DbSet aqui
	}
}
