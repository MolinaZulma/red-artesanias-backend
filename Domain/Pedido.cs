public class Pedido
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public List<PedidoItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.Precio * i.Cantidad);
}