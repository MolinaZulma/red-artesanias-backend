namespace RedArtesanias.Api.DTOs
{
    public record ProductoDto
    (int Id, string Nombre, decimal Precion,
    string? Descripcion, int ArtesanoId, List<string> Fotos);
    
}