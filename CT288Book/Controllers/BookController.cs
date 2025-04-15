using CT288Book.Data;
using CT288Book.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CT288Book.Controllers
{
    public class BookController : Controller
    {
        private readonly BookShopContext db;

        public BookController(BookShopContext context)
        {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            var books = db.Books.AsQueryable();
            if (loai.HasValue)
            {
                books = books.Where(p => p.MaLoai == loai.Value);
            }

            var result = books.Select(p => new BookVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai

            });

            return View(result);
        }
        public IActionResult Search(string query)
        {
            var books = db.Books.AsQueryable();
            if (query != null)
            {
                books = books.Where(p => p.TenHh.Contains(query));
            }

            var result = books.Select(p => new BookVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai

            });

            return View(result);
        }
        public IActionResult Detail(int id)
        {
            var data = db.Books
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var result = new ChiTietBookVM
            {
                MaHh = data.MaHh,
                TenHH = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10,//tính sau
                DiemDanhGia = 5,//check sau
            };
            return View(result);
        }
    }
}
