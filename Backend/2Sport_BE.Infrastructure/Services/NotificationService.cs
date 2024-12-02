﻿using _2Sport_BE.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace _2Sport_BE.Infrastructure.Services
{
    public interface INotificationService
    {
        Task NotifyForCreatingNewOrderAsync(string orderCode, bool isRentalOrder, int? brachId = null);
        Task NotifyForRejectOrderAsync(string orderCode, int branchId);
        Task NotifyPaymentCancellation(string orderCode, bool isRentalOrder, int? branchId = null);
        Task NotifyPaymentPaid(string orderCode, bool isRentalOrder, int? branchId = null);
        Task SendRentalOrderExpirationNotificationAsync(string customerId, string orderId, DateTime rentalEndDate);
    }
    public class NotificationService : INotificationService
    {
        private readonly INotificationHub _notificationHub;
        public NotificationService(INotificationHub notificationHub)
        {
            _notificationHub = notificationHub;
        }
        public async Task NotifyForCreatingNewOrderAsync(string orderCode, bool isRentalOrder, int? branchId = null)
        {
            var message = "";

            if (isRentalOrder == false)
            {
                message = $"Đơn hàng có mã là S-{orderCode} vừa được tạo.";
            }
            else
            {
                message = $"Đơn hàng thuê có mã là T-{orderCode} vừa được tạo.";
            }

            if (branchId != null)
            {
                string toBranch = $"Branch_{branchId}";
                await _notificationHub.SendMessageToGroup(toBranch, message);
            }
            else
            {
                await _notificationHub.SendMessageToGroup("Coordinator", message);
            }
        }

        public async Task NotifyForRejectOrderAsync(string orderCode, int branchId)
        {
            var message = $"Đơn hàng {orderCode} đã bị từ chối tại chi nhánh {branchId}.";
            await _notificationHub.SendMessageToGroup("Coordinator", message);
        }

        public async Task NotifyPaymentCancellation(string orderCode, bool isRentalOrder, int? branchId = null)
        {
            string message = "";
            if(isRentalOrder == false) {
                 message = $"Thanh toán cho đơn hàng S-{orderCode} đã bị hủy.";
            }
            else
            {
                 message = $"Thanh toán cho đơn hàng thuê T-{orderCode} đã bị hủy.";
            }

            if (branchId != null)
            {
                var toBranch = $"Branch_{branchId}";
                await _notificationHub.SendMessageToGroup(toBranch, message);
            }
            else
            {
                await _notificationHub.SendMessageToGroup("Coordinator", message);
            }
        }

        public async Task NotifyPaymentPaid(string orderCode, bool isRentalOrder, int? branchId = null)
        {
            string message = "";
            if (isRentalOrder == false)
            {
                message = $"Thanh toán cho đơn hàng S-{orderCode} thành công.";
            }
            else
            {
                message = $"Thanh toán cho đơn hàng thuê T-{orderCode} thành công.";
            }

            if (branchId != null)
            {
                var toBranch = $"Branch_{branchId}";
                await _notificationHub.SendMessageToGroup(toBranch, message);
            }
            else
            {
                await _notificationHub.SendMessageToGroup("Coordinator", message);
            }
        }

        public async Task SendRentalOrderExpirationNotificationAsync(string customerId, string orderCode, DateTime rentalEndDate)
        {
            var message = $"Đơn thuê T-{orderCode} sẽ hết hạn vào ngày {rentalEndDate:dd/MM/yyyy}.";
            await _notificationHub.SendNotificationToCustomer(customerId, message);
        }
    }
}
