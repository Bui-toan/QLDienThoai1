using QLDienThoai.Models;
namespace QLDienThoai.Respository

{
	public interface ITenRespository
	{
		Categories Add(Categories ca);
		Categories Update(Categories ca);
		Categories Delete(Categories ca);
		Categories GetLoaiSp(Categories ca);
		IEnumerable<Categories> GetAllLoaiSp();
	}
}
