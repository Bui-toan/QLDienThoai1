using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDienThoai.Models;

public partial class OrderDetails
{
    public int OrderDetailId { get; set; }

	public int OrderId { get; set; }

	public string? OrderCode { get; set; }
    public string Username {  get; set; }
	public int? ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public virtual Order? Order { get; set; }
    [ForeignKey("ProductId")]
    public virtual SanPham? Product { get; set; }
}
