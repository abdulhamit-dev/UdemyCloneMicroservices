using CatalogAPI.Dtos;
using FluentValidation;

namespace UdemyClone.UI.Validators;

public class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
{
    public CourseCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("isim alanı boş olamaz");
        RuleFor(x => x.Description).NotEmpty().WithMessage("açıklama alanı boş olamaz");
        RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("süre alanı boş olamaz");
        RuleFor(x => x.Price).NotEmpty().WithMessage("fiyat alanı boş olamaz").ScalePrecision(2, 6).WithMessage("hatalı para formatı");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("kategori alanı seçiniz");
    }
}
