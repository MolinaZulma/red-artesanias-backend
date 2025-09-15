using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

[ApiController]
[Route("files")]
public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public FilesController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost]
    [RequestSizeLimit(10_000_000)]
    public async Task<ActionResult<IEnumerable<string>>> Upload([FromForm] List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return BadRequest("Sin archives");
        var uploadsPath = Path.Combine(_env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot"), "uploads");

        if (!Directory.Exists(uploadsPath))
        {
            Directory.CreateDirectory(uploadsPath);
        }

        var urls = new List<string>();
        foreach (var f in files)
        {
            
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(f.FileName)}";
            var full = Path.Combine(uploadsPath, fileName);

            using var stream = System.IO.File.Create(full);
            await f.CopyToAsync(stream);

            var url = $"/uploads/{fileName}";
            urls.Add(url);
        }

        return Ok(urls);
    }
}


 