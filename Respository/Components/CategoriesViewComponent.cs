using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

namespace QLDienThoai.Respository.Components
{
	public class CategoriesViewComponent : ViewComponent
	{
		private readonly QldienThoaiContext _dataContext;
		public CategoriesViewComponent(QldienThoaiContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(_dataContext.Categories.ToListAsync());
	}
}
