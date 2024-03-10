

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheListener.Models;
using TheListener.Models.Dto;
using TheListener.Repository.IRepository;

namespace TheListener.Controllers
{
    [Route("api/ProductAPI")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProductRepository _product;
        private readonly ICategoryRepository _category;
        private readonly IMapper _mapper;
        public ProductAPIController(IProductRepository product,  IMapper mapper,
            ICategoryRepository category)
        {
            _product = product;
            _category = category;
            _mapper = mapper;
            _response = new();
            
        }




        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetProducts()
        {
            try
            {

                IEnumerable<Product> productList = await _product.GetAllAsync(includeProperties: "Category");
                _response.Result = _mapper.Map<List<ProductDto>>(productList);
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


        [HttpGet("{id:int}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProdutId(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var product = await _product.GetAsync(x => x.Id == id);
                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ProductDto>(product);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductCreateDto createDTO)
        {
            try
            {

                if (await _product.GetAsync(u => u.Id == createDTO.ProductId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "product already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _category.GetAsync(x=> x.CategoryId == createDTO.CategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Product product = _mapper.Map<Product>(createDTO);


                await _product.CreateAsync(product);
                _response.Result = _mapper.Map<Product>(product);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetProduct", new { id = product.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var product = await _product.GetAsync(u => u.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                await _product.RemoveAsync(product);
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

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateProduct(int id, [FromBody] ProductUpdateDto updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.ProductId)
                {
                    return BadRequest();
                }

                Product model = _mapper.Map<Product>(updateDTO);

                await _product.UpdateAsync(model);
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
