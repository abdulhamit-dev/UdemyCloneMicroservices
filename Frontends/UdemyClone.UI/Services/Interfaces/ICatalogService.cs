using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyClone.UI.Models;
using UdemyClone.UI.Models.Catalog;

namespace UdemyClone.UI.Services.Interfaces;

public interface ICatalogService
{
    Task<List<CourseVM>> GetAllCourseAsync();
    Task<List<CategoryVM>> GetAllCategoryAsync();
    Task<List<CourseVM>> GetAllCourseByUserIdAsycn(string userId);
    Task<CourseVM> GetByCourseId(string courseId);
    Task<bool> CreateCourseAsync(CourseCreateDto courseCreateDto);
    Task<bool> UpdateCourseAsync(CourceUpdateDto courceUpdateDto);
    Task<bool> DeleteCourseAsync(string courseId);
    
}