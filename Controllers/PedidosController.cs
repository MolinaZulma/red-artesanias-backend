using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("pedidos")]

public class PedidosController : ControllerBase
{
    private readonly AppDbContext _db;
    public PedidosController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<ActionResult<int>> Crear(CrearPedidoDto dto)
    {
        if (dto.Items is null || dto.Items.Count == 0) return
        BadRequest("Sin Items");
        var pedido = new Pedido
        {
            Items = dto.Items.Select(i => new PedidoItem
            {
                ProductoId = i.ProductoId,
                Nombre = i.Nombre,
                Precio = i.Precio,
                Cantidad = i.Cantidad
            }).ToList()
        };
        _db.Pedidos.Add(pedido);
        await _db.SaveChangesAsync();
        return Ok(pedido.Id);
    }
}