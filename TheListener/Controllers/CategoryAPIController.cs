using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheListener.Data;
using TheListener.Models;
using TheListener.Models.Dto;
using TheListener.Repository.IRepository;

namespace TheListener.Controllers
{
    [Route("api/CategoryAPI")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ApplicationDbContext _db;
        private readonly ICategoryRepository _category;
        private readonly IMapper _mapper;
        public CategoryAPIController(ApplicationDbContext db, ICategoryRepository category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
            _db = db;
            _response = new();
        }



        [HttpGet()]
        //[Authorize(Roles = "admin")]
       // [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<ActionResult<APIResponse>> GetCategories([FromQuery(Name = "Search")] string? search
            ,int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Category> AssetList;
             
                AssetList = await _category.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
             
                if (!string.IsNullOrEmpty(search))
                {
                    AssetList = AssetList.Where(x => x.CategoryName.ToLower().Contains(search)
                    || x.CategoryName.ToLower().Contains(search));
                }
                _response.Result = _mapper.Map<List<CategoryDto>>(AssetList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        //[ResponseCache(Location = ResponseCacheLocation.None,NoStore =true)]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _category.GetAsync(u => u.CategoryId == id);
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CategoryDto>(category);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CategoryCreateDto createDTO)
        {
            try
            {
                if (await _category.GetAsync(u => u.CategoryName.ToLower() == createDTO.CategoryName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Category category = _mapper.Map<Category>(createDTO);


                await _category.CreateAsync(category);
                _response.Result = _mapper.Map<CategoryDto>(category);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = category.CategoryId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteCategory")]
       
        public async Task<ActionResult<APIResponse>> DeleteCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var category = await _category.GetAsync(x => x.CategoryId == id);

                if(category == null)
                {
                    return NotFound();
                }
               
                await _category.RemoveAsync(category);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateCategory(int id, [FromBody] CategoryUpdateDto updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.CategoryId)
                {
                    return BadRequest();
                }

                Category model = _mapper.Map<Category>(updateDTO);

                await _category.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

       
    }
}
