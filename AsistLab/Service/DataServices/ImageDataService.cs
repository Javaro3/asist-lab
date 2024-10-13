using Common.Domains;
using Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repository.Repositories.Interfaces;

namespace Service.DataServices;

public class ImageDataService
{
    private readonly IImageRepository _imageRepository;
    private readonly LocalStorageOption _localStorageOption;

    public ImageDataService(
        IImageRepository imageRepository,
        IOptions<LocalStorageOption> localStorageOption)
    {
        _imageRepository = imageRepository;
        _localStorageOption = localStorageOption.Value;
    }

    public async Task<List<Image>> SaveImagesToLocalStorageAsync(IEnumerable<IFormFile> formImages)
    {
        var images = new List<Image>();
        
        foreach (var formImage in formImages)
        {
            if (formImage.Length > 0)
            {
                var imgName = Path.GetFileName(formImage.FileName);
                var imgPath = Path.Combine(_localStorageOption.Path, imgName);

                await using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    await formImage.CopyToAsync(stream);
                }

                var image = new Image { Url = imgName };
                images.Add(image);
            }
        }

        return images;
    }
    
    public async Task<byte[]> GetImageAsync(int id)
    {
        var image = await _imageRepository.GetByIdAsync(id);
        var filePath = Path.Combine(_localStorageOption.Path, image!.Url);
        var imageBytes = await File.ReadAllBytesAsync(filePath);
        return imageBytes;
    }
}