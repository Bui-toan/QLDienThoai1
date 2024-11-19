using System;
using System.Collections.Generic;

namespace QLDienThoai.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreateDate { get; set; }
	
	public string UserName { get; set; }
	public string OrderCode { get; set; }

	public int Status { get; set; }



	public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}
