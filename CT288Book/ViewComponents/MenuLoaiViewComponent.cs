using CT288Book.Data;
using CT288Book.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CT288Book.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly BookShopContext db;

        public MenuLoaiViewComponent(BookShopContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.Books.Count
            }).OrderBy(p =>p.TenLoai);
            return View(data);
        }
    }
}
