﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService
                              ?? throw new NullReferenceException(nameof(productService));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByPage([FromQuery] PaginationDto byPageDto)
        {
            var productsDto = await _productService
                .GetProductsByPageAsync(byPageDto, CancellationToken.None);

            return Ok(productsDto);

        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetProductsByCategory([FromRoute] int categoryId)
        {
            var productsByCategory = await _productService
                .GetProductsByCategoryAsync(categoryId, CancellationToken.None);

            return Ok(productsByCategory);
        }
    }
}
