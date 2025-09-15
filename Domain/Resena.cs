public class Resena
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public String Producto { get; set; } = default!;
    public int Rating { get; set; } //1..5
    public String Comentario { get; set; }
    public DateTime Fecha { get; set; }= DateTime.UtcNow;
}