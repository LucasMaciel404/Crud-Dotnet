using Crud.Model;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> opts) : base(opts) {
    
    }
    public DbSet<UserModel> Users { get; set; }
}
