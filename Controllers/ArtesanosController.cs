using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("artesanos")]
public class ArtesanosController : ControllerBase
{
    private readonly AppDbContext _db;
    public ArtesanosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IEnumerable<ArtesanoDto>> Listar() =>
        await _db.Artesanos.AsNoTracking()
            .Select(a => new ArtesanoDto(a.Id, a.Nombre, a.Bio))
            .ToListAsync();

}
