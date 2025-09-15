public class Artesano
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Bio { get; set; }
    public List<Producto> Productos { get; set; } = new();
}