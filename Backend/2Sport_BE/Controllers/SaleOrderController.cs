﻿using _2Sport_BE.DataContent;
using _2Sport_BE.Infrastructure.DTOs;
using _2Sport_BE.Infrastructure.Enums;
using _2Sport_BE.Infrastructure.Helpers;
using _2Sport_BE.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2Sport_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrderController : Controller
    {

        private readonly ISaleOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IMethodHelper _methodHelper;
        public SaleOrderController(ISaleOrderService orderService,
                                IPaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }
        [HttpGet]
        [Route("get-all-sale-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _orderService.GetAllSaleOrdersAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet]
        [Route("get-sale-order-detail")]
        public async Task<IActionResult> GetOrderByOrderId(int orderId)
        {
            var response = await _orderService.GetSaleOrderDetailByIdAsync(orderId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet]
        [Route("get-orders-of-user")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var response = await _orderService.GetSaleOrdersOfUserAsync(userId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet]
        [Route("get-orders-by-status")]
        public async Task<IActionResult> GetOrdersByStatus(int? orderStatus, int? paymentStatus)
        {
            var response = await _orderService.GetSaleOrdersByStatus(orderStatus, paymentStatus);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet]
        [Route("get-orders-by-code")]
        public async Task<IActionResult> GetOrdersByOrderCode(string orderCode)
        {
            var response = await _orderService.GetSaleOrderBySaleOrderCode(orderCode);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        /*        [HttpGet]
                [Route("get-orders-by-date-and-status")]
                public async Task<IActionResult> GetOrdersByOrderCode(DateTime startDate, DateTime endDate, int status)
                {
                    var response = await _orderService.GetOrdersByMonthAndStatus(startDate, endDate, status);
                    if (response.IsSuccess)
                    {
                        return Ok(response);
                    }
                    return BadRequest(response);
                }*/
        [HttpPut("update-order-status")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, int status)
        {
            var response = await _orderService.UpdateSaleOrderStatusAsync(orderId, status);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        /*        [HttpGet]
                [Route("revenue-summary")]
                public async Task<IActionResult> GetSalesOrdersByStatus([FromQuery] DateTime? startDate,
                                                                        [FromQuery] DateTime? endDate,
                                                                        [FromQuery] int? status)
                {
                    try
                    {
                        var query = await _orderService.GetAllSaleOrderQueryableAsync();
                        if (startDate.HasValue)
                        {
                            query = query.Where(o => o.CreatedAt >= startDate.Value);
                        }
                        if (endDate.HasValue)
                        {
                            query = query.Where(o => o.CreatedAt <= endDate.Value);
                        }

                        // Lọc theo trạng thái đơn hàng
                        if (status.HasValue)
                        {
                            query = query.Where(o => o.OrderStatus == status.Value);
                        }

                        var totalRevenue = await query.SumAsync(o => o.TotalAmount);
                        var totalOrders = await query.CountAsync();

                        decimal totalRevenueInMonth = (decimal)query.Sum(_ => _.TotalAmount);
                        int ordersInMonth = query.Count();
                        int ordersInLastMonth = 1;
                        bool isIncrease;
                        double orderGrowthRatio = PercentageChange(ordersInMonth, ordersInLastMonth, out isIncrease);

                        SaleOrdersSales ordersSales = new SaleOrdersSales
                        {
                            SaleOrderGrowthRatio = totalOrders,
                            TotalIntoMoney = totalRevenue,
                            TotalSaleOrders = 0,
                            IsIncrease = true
                        };

                        ResponseModel<SaleOrdersSales> response = new ResponseModel<SaleOrdersSales>
                        {
                            IsSuccess = true,
                            Message = "Query Successfully",
                            Data = ordersSales
                        };

                        return Ok(response);
                    }
                    catch (Exception ex)
                    {
                        ResponseModel<OrdersSales> response = new ResponseModel<OrdersSales>
                        {
                            IsSuccess = false,
                            Message = "Something went wrong: " + ex.Message,
                            Data = null
                        };
                        return BadRequest(response);
                    }
                }*/
        [HttpPost("create-sale-order")]
        public async Task<IActionResult> CreateOrder([FromBody] SaleOrderCM orderCM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var userId = GetCurrentUserIdFromToken();
            if (userId != orderCM.UserID || userId == 0 || orderCM.UserID == 0)
            {
                return Unauthorized();
            }
            //Tao order
            var response = await _orderService.CreatetSaleOrderAsync(orderCM);
            if (!response.IsSuccess)
            {
                return StatusCode(500, response);
            }
            //Tao link payment
            var paymentLink = orderCM.PaymentMethodID == (int)OrderMethods.PayOS
                                  ? await _paymentService.PaymentWithPayOs(response.Data.SaleOrderID)
                                  : "";
            if (paymentLink.Length == 0)
            {
                return BadRequest("Cannot create payment link");
            }
            response.Data.PaymentLink = paymentLink;
            return Ok(response);
        }

        [HttpPut("update-sale-order")]
        public async Task<IActionResult> UpdateSaleOrder([FromQuery] int orderId, [FromBody] SaleOrderUM orderUM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var response = await _orderService.UpdateSaleOrderAsync(orderId, orderUM);
            if (!response.IsSuccess)
            {
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpGet("cancel")]
        public async Task<IActionResult> HandleOrderCancel([FromQuery] PaymentResponse paymentResponse)
        {
            if (!ModelState.IsValid || _methodHelper.AreAnyStringsNullOrEmpty(paymentResponse))
            {
                return BadRequest(new ResponseModel<object>
                {
                    IsSuccess = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            }

            var result = await _orderService.ProcessCancelledSaleOrder(paymentResponse);
            if (result.IsSuccess)
            {
                var redirectUrl = "https://twosport.vercel.app/order_cancel";
                return Redirect(redirectUrl);
            }
            return BadRequest(result);
        }
        [HttpGet("return")]
        public async Task<IActionResult> HandleOrderReturn([FromQuery] PaymentResponse paymentResponse)
        {
            if (!ModelState.IsValid || _methodHelper.AreAnyStringsNullOrEmpty(paymentResponse))
            {
                return BadRequest(new ResponseModel<object>
                {
                    IsSuccess = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            }

            var result = await _orderService.ProcessCompletedSaleOrder(paymentResponse);
            if (result.IsSuccess)
            {
                var redirectUrl = "https://twosport.vercel.app/order_success";
                return Redirect(redirectUrl);
            }
            return BadRequest(result);
        }
        /*        }  
                [HttpGet]
                [Route("get-revenue")]
                public async Task<IActionResult> GetRevenue(
                   [FromQuery] int? branchId,
                   [FromQuery] int? orderType,
                   [FromQuery] DateTime? dateFrom,
                   [FromQuery] DateTime? dateTo,
                   [FromQuery] int? status)
                {
                    var response = await _orderService.GetOrdersRevenue(branchId, orderType, dateFrom, dateTo, status);
                    if (response.IsSuccess)
                    {
                        return Ok(response);
                    }
                    return BadRequest(response);
                }*/

        [NonAction]
        public int GetCurrentUserIdFromToken()
        {
            int UserId = 0;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string strUserId = identity.FindFirst("UserId").Value;
                        int.TryParse(strUserId, out UserId);

                    }
                }
                return UserId;
            }
            catch
            {
                return UserId;
            }
        }
    }
}
