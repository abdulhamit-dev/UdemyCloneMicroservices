using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogAPI.Dtos;
using CatalogAPI.Services;
using Shared.Dtos;
using UdemyClone.UI.Models;
using UdemyClone.UI.Models.Catalog;
using UdemyClone.UI.Services.Interfaces;
using CourseCreateDto = UdemyClone.UI.Models.Catalog.CourseCreateDto;

namespace UdemyClone.UI.Services;

public class CatalogService:ICatalogService
{
    
    public Task<List<CourseVM>> GetAllCourseAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<List<CategoryVM>> GetAllCategoryAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<List<CourseVM>> GetAllCourseByUserIdAsycn()
    {
        throw new System.NotImplementedException();
    }

    public Task<CourseVM> GetByCourseId(string courseId)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> UpdateCourseAsync(CourceUpdateDto courceUpdateDto)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteCourseAsync(string courseId)
    {
        throw new System.NotImplementedException();
    }
}