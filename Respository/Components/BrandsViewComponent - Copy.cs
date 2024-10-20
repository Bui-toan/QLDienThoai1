using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

namespace QLDienThoai.Respository.Components
{
	public class BrandsViewComponent : ViewComponent
	{
		/*private readonly ITenRespository _ca;
		public CategoriesViewComponent(ITenRespository ca)
		{
			_ca = ca;
		}
		public IViewComponentResult Invoke()
		{
			var ca = _ca.GetAllLoaiSp().OrderBy(x => x.Name);
			return View(ca);
		}*/
		private readonly QldienThoaiContext _dataContext;
		public BrandsViewComponent(QldienThoaiContext context)
		{
			_dataContext = context;
		}

		public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Brands.ToListAsync());
	}
}
