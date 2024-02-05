using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Model.Data;
using NZWalks.Api.Model.Domain;
using NZWalks.Api.Model.DTO;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        public RegionsController(NZWalksDbContext _context)
        {
            this._context = _context;
        }

        //GET ALL REGIONS
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from Database - Domain Model
            var regions = _context.Regions.ToList();

            //Map Domain Model to DTO
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        //GET SINGLE REGION (Get Region By ID)
        [HttpGet]
        [Route("(id:Guid)")]
        public IActionResult GetById(Guid id)
        {
            //GEt region Domain Modle from Db
            var region = _context.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }
            //Map the Region Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDto);
        }
        //POST  To Create New Region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            _context.Regions.Add(regionDomainModel);
            _context.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDomainModel);
        }
    }
}
