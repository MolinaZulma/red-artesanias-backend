using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Artesano> Artesanos => Set<Artesano>();
    public DbSet<Foto> Fotos => Set<Foto>();
    public DbSet<Resena> Resenas => Set<Resena>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoItem> PedidoItems => Set<PedidoItem>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Producto>().HasOne(p => p.Artesano).WithMany(a => a.Productos).HasForeignKey(p => p.ArtesanoId).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Producto>().HasMany(p => p.Fotos).WithOne().HasForeignKey(f => f.ProductoId);
        b.Entity<Pedido>().HasMany(p => p.Items).WithOne(i => i.Pedido!).HasForeignKey(i => i.PedidoId);
    }
}