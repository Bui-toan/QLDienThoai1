using QLDienThoai.Models;

namespace QLDienThoai.Respository
{
	public class TenRespository : ITenRespository
	{
		private readonly QldienThoaiContext _context;
		public TenRespository(QldienThoaiContext context)
		{
			_context = context;
		}
		public Category Add(Category ca)
		{
			_context.Categories.Add(ca);
			_context.SaveChanges();
			return ca;
		}

		public Category Delete(Category ca)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Category> GetAllLoaiSp()
		{

			return _context.Categories;


		}

		public Category GetLoaiSp(Category ca)
		{
			return _context.Categories.Find(ca);
		}

		public Category Update(Category ca)
		{
			_context.Update(ca);
			_context.SaveChanges();
			return ca;
		}
	}
}
