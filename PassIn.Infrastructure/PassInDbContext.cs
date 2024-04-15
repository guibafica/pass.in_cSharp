using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure;

public class PassInDbContext: DbContext
{
    public DbSet<Event> Events { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/home/guilherme/Área de Trabalho/NLW Unite (24\u204404\u204401)/C#/PassInDb.db");
    }
}
