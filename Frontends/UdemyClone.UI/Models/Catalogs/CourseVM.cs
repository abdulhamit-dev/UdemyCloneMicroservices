using System;
using UdemyClone.UI.Models.Catalogs;


namespace UdemyClone.UI.Models;

public class CourseVM
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string UserId { get; set; }
    public string Picture { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime Deneme { get; set; }

    public FeatureVM Feature { get; set; }

    public string CategoryId { get; set; }

    public CategoryVM Category { get; set; }
    public string StockPictureUrl { get; set; }
    public string ShortDescription
    {
        get => Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description;
    }
}