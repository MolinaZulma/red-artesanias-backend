public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public decimal Precio { get; set; }
    public String? Descripcion { get; set; }
    public int ArtesanoId { get; set; }
    public Artesano? Artesano { get; set; }
    public List<Foto> Fotos { get; set; } = new();
    public List<Resena> Resenas { get; set; } = new();
}