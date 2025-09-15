public class PedidoItem
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public String Nombre { get; set; } = default!;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
}