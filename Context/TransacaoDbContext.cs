using Microsoft.EntityFrameworkCore;
using ApiDtMoney.Models;

public class TransacaoDbContext : DbContext
{
    public TransacaoDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Transacao> transacoes { get; set; }

}