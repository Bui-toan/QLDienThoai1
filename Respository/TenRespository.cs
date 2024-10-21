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
		public Categories Add(Categories ca)
		{
			_context.Categories.Add(ca);
			_context.SaveChanges();
			return ca;
		}

		public Categories Delete(Categories ca)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Categories> GetAllLoaiSp()
		{

			return _context.Categories;


		}

		public Categories GetLoaiSp(Categories ca)
		{
			return _context.Categories.Find(ca);
		}

		public Categories Update(Categories ca)
		{
			_context.Update(ca);
			_context.SaveChanges();
			return ca;
		}
	}
}
