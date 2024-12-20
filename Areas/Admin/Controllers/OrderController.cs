﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

namespace QLDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Order")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly QldienThoaiContext _context = new QldienThoaiContext();
        public OrderController(QldienThoaiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {

            return View(await _context.Orders.OrderByDescending(x => x.OrderId).ToListAsync());
        }
        [Route("ViewOrder")]
        public async Task<ActionResult> ViewOrder(string ordercode)
        {
            var detail = await _context.OrderDetails.Include(X => X.Product).Where(x => x.OrderCode == ordercode).ToListAsync();
            return View(detail);
        }
        [HttpPost]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(string ordercode, int status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderCode == ordercode);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = status;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Order status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Route("Delete")]
        public async Task<IActionResult> DeleteOrder(string ordercode)
        {
            // Tìm đơn hàng theo mã đơn hàng
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderCode == ordercode);
            if (order == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy đơn hàng!" });
            }


            // Xóa các chi tiết đơn hàng liên quan trước
            var orderDetails = _context.OrderDetails.Where(x => x.OrderCode == ordercode);
            if (orderDetails.Any())
            {
                _context.OrderDetails.RemoveRange(orderDetails);
            }

            // Xóa đơn hàng
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Order");


        }

    }
}
