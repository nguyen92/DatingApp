using AutoMapper;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DatingApp.API.Helpers;
using DatingApp.API.DTOS;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Models;
using System.Linq;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
   
    public class PhotosController : ControllerBase
    {
    private readonly IMapper _mapper;
    private readonly IDatingAppRepository _repo;
    private Cloudinary _cloudinary;
    private readonly IOptions<CloudinarySettings> _cloudinaryConfig;

        public PhotosController(IMapper mapper, IDatingAppRepository repo, IOptions<CloudinarySettings> cloudinaryConfig, Cloudinary cloudinary)
        {
            _mapper = mapper;
            _repo = repo;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
              _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
             _cloudinary = new Cloudinary(acc);
        }
    [HttpGet("{id}", Name = "GetPhoto")]
    public async Task<IActionResult> GetPhoto(int id)
    {
        var photoFromRepo = await _repo.GetPhoto(id);
        var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);
        return Ok(photo);
    }
    [HttpPost]
    public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm]PhotoForCreationDto photoForCreationDto)
    {
        if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();
        var userFromRepo = await _repo.GetUser(userId);
        var file = photoForCreationDto.File;
        var uploadResult = new ImageUploadResult();
        if (file.Length >0)
        {
         using (var stream =  file.OpenReadStream())
          {   
            var uploadParams = new ImageUploadParams() {
                File = new FileDescription(file.Name, stream),
                 Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
            };
            uploadResult = _cloudinary.Upload(uploadParams);
          }
        }

        photoForCreationDto.PublicId = uploadResult.PublicId;
        photoForCreationDto.Url = uploadResult.Uri.ToString();

        var photo = _mapper.Map<Photo>(photoForCreationDto);
         if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;
        userFromRepo.Photos.Add(photo);
        if (await _repo.SaveAll())
        {
            var photoForReturn = _mapper.Map<PhotoForReturnDto>(photo);
            return CreatedAtRoute("GetPhoto", new {userId = userId, id = photo.Id}, photoForReturn);
        }
        return BadRequest("Could not add the photo");
    }

    }
}