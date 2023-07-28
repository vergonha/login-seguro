using Microsoft.EntityFrameworkCore;
using secure_api.Entities;

namespace secure_api.Data;

// Classe que vai representar o contexto do nosso banco de dados, que vai nos conectar com ele e vai permitir as interações.
public sealed class DataContext : DbContext
{

    // Fornece a API para inserir, remover, ler e atualizar no banco de dados.
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    // Representa a tabela no banco de dados e o formato que ela aceita.
    public DbSet<User> Users { get; set; }
}