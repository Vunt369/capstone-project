﻿using _2Sport_BE.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2Sport_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailController(ICustomerDetailService customerDetailService) : ControllerBase
    {
        private readonly ICustomerDetailService _customerDetailService = customerDetailService;

        [HttpGet]
        [Route("get-customer-detail-by-userId")]
        public async Task<IActionResult> GetCustomerDetailByUserId([FromQuery] int userId)
        {
            var response = await _customerDetailService.GetCustomerDetailByUserId(userId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
