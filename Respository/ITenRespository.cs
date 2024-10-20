using QLDienThoai.Models;
namespace QLDienThoai.Respository

{
	public interface ITenRespository
	{
		Category Add(Category ca);
		Category Update(Category ca);
		Category Delete(Category ca);
		Category GetLoaiSp(Category ca);
		IEnumerable<Category> GetAllLoaiSp();
	}
}
