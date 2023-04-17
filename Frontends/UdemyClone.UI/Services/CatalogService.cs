using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CourseVM>> GetAllCourseAsync()
    {
        var response = await _httpClient.GetAsync("courses");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess= await response.Content.ReadFromJsonAsync<Response<List<CourseVM>>>();
        return responseSuccess.Data;
    }

    public Task<List<CategoryVM>> GetAllCategoryAsync()
    {
        throw new System.NotImplementedException();
    }

    public async Task<List<CourseVM>> GetAllCourseByUserIdAsycn(string userId)
    {
        var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseVM>>>();
        return responseSuccess.Data;
    }

    public async Task<CourseVM> GetByCourseId(string courseId)
    {
        var response = await _httpClient.GetAsync($"courses/{courseId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseVM>>();


        return responseSuccess.Data;
    }

    public async Task<bool> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        var response = await _httpClient.PostAsJsonAsync("courses",courseCreateDto);

   
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(CourceUpdateDto courceUpdateDto)
    {
        var response = await _httpClient.PutAsJsonAsync("courses",courceUpdateDto);

   
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = await _httpClient.DeleteAsync($"courses/{courseId}");
   
        return response.IsSuccessStatusCode;
    }
}