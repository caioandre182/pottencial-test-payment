using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tech.Test.Payment.Application.DTO;
using Tech.Test.Payment.Application.Services.Interfaces;
using Tech.Test.Payment.Domain.Entities;

namespace Tech.Test.Payment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] SaleDTO saleDTO)
        {
            var result = await _saleService.CreateAsync(saleDTO);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _saleService.GetByIdAsync(id);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] SaleDTO saleDTO)
        {
            var result = await _saleService.UpdateAsync(saleDTO);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}