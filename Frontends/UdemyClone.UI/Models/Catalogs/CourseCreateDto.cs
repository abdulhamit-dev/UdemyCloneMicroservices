using Microsoft.AspNetCore.Http;

namespace UdemyClone.UI.Models.Catalogs;

public class CourseCreateDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Picture { get; set; }

    public string UserId { get; set; }

    public FeatureVM Feature { get; set; }

    public string CategoryId { get; set; }
    public IFormFile PhotoFormFile { get; set; }
}