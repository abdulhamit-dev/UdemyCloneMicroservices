using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CatalogAPI.Services;
using Shared.Dtos;
using UdemyClone.UI.Helper;
using UdemyClone.UI.Models;
using UdemyClone.UI.Models.Catalogs;
using UdemyClone.UI.Services.Interfaces;
using CourseCreateDto = UdemyClone.UI.Models.Catalogs.CourseCreateDto;

namespace UdemyClone.UI.Services;

public class CatalogService:ICatalogService
{
    private readonly HttpClient _httpClient;
    private readonly IPhotoStockService _photoStockService;
    private readonly PhotoHelper _photoHelper;

    public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
    {
        _httpClient = httpClient;
        _photoStockService = photoStockService;
        _photoHelper=photoHelper;
    }

    public async Task<List<CourseVM>> GetAllCourseAsync()
    {
        var response = await _httpClient.GetAsync("courses");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess= await response.Content.ReadFromJsonAsync<Response<List<CourseVM>>>();

        responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

        return responseSuccess.Data;
    }

    public async Task<List<CategoryVM>> GetAllCategoryAsync()
    {
        var response = await _httpClient.GetAsync("categories");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryVM>>>();

        return responseSuccess.Data;
    }

    public async Task<List<CourseVM>> GetAllCourseByUserIdAsycn(string userId)
    {
        var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseVM>>>();

        responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

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

        responseSuccess.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);

        return responseSuccess.Data;
    }

    public async Task<bool> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateDto.PhotoFormFile);
        if (resultPhotoService != null)
        {
            courseCreateDto.Picture = resultPhotoService.Url;
        }

        var response = await _httpClient.PostAsJsonAsync("courses",courseCreateDto);

   
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCourseAsync(CourseUpdateDto courceUpdateDto)
    {
        var resultPhotoService = await _photoStockService.UploadPhoto(courceUpdateDto.PhotoFormFile);

        if (resultPhotoService != null)
        {
            await _photoStockService.DeletePhoto(courceUpdateDto.Picture);
            courceUpdateDto.Picture = resultPhotoService.Url;
        }

        var response = await _httpClient.PutAsJsonAsync("courses",courceUpdateDto);

   
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var response = await _httpClient.DeleteAsync($"courses/{courseId}");
   
        return response.IsSuccessStatusCode;
    }
}