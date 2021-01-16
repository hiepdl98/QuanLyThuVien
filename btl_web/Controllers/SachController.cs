using Framework.EF;
using Framework.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace btl_web.Controllers
{
    public class combo
    {
        public string ngonngu { get; set; }
        public string nhaxuatban { get; set; }
        public string tacgia { get; set; }
        public string loai { get; set; }
        public string vitri { get; set; }
        public string kho { get; set; }

    }
    public class SachController : Controller
    {
        private readonly Model1 data = new Model1();
        // GET: Sach
        public ActionResult SachIndex()
        {
            ViewBag.nhaxuatban = data.NXBs.ToList();
            ViewBag.tacgia = data.TacGias.ToList();
            ViewBag.vitri = data.ViTris.ToList();
            ViewBag.loai = data.LoaiSaches.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult ListSach()
        {
            var listSach = new List<Sach>();
            var list = data.Saches.ToList();
            foreach ( var item in list)
            {
                var model = new Sach();
                model.Masach = item.Masach;
                model.Tensach = item.Tensach;
                model.Namxuatban = item.Namxuatban;
                model.Soluong = item.Soluong;
                model.Manhaxuatban = item.Manhaxuatban;
                model.Matacgia = item.Matacgia;
                model.Maloai = item.Maloai;
                model.Mavitri = item.Mavitri;
                listSach.Add(model);
            }
            return Json(listSach, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveChange(Sach model, combo cb,int id)
        {
            try
            {

                if (id > 0)
                {
                    var update = data.Saches.FirstOrDefault(x => x.Masach == model.Masach);
                    update.Tensach = model.Tensach;
                    update.Namxuatban = model.Namxuatban;
                    update.Soluong = model.Soluong;
                    update.Manhaxuatban = model.Manhaxuatban;
                    update.Matacgia = model.Matacgia;
                    update.Maloai = model.Maloai;
                    update.Mavitri = model.Mavitri;

                    data.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }

                var checkSach = data.Saches.FirstOrDefault(x => x.Masach == model.Masach);
                if (checkSach != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại!" });
                }
                
                model.Manhaxuatban = data.NXBs.FirstOrDefault(x => x.Tennhaxuatban == cb.nhaxuatban).Manhaxuatban;
                model.Maloai = data.LoaiSaches.FirstOrDefault(x => x.Tenloai == cb.loai).Maloai;
                model.Matacgia = data.TacGias.FirstOrDefault(x => x.Tentacgia == cb.tacgia).Matacgia;
                model.Mavitri = data.ViTris.FirstOrDefault(x => x.Khu == cb.vitri).Mavitri;

                data.Saches.Add(model);
                data.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công!" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Có lỗi xảy ra!" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> getList()
        {
            var listsv = await data.SinhViens.ToListAsync();
            return Json(listsv, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult timkiem(string tuKhoa)
        {
            //Lấy danh sách kho
            IQueryable<Sach> lst = data.Saches;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Masach.Contains(tuKhoa) || p.Tensach.Contains(tuKhoa) ||
                p.Namxuatban.Contains(tuKhoa) || p.Namxuatban.Contains(tuKhoa) || p.Mavitri.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Masach));
        }
        [HttpDelete]
        public JsonResult Delete(string Masach)
        {
            try
            {
                var delete = data.Saches.FirstOrDefault(x => x.Masach == Masach);
                if (delete != null)
                {

                    data.Saches.Remove(delete);
                    data.SaveChanges();
                    return Json(new JMessage { Error = false, Title = "Xoa thanh cong!" });
                }
                else
                {
                    return Json(new JMessage { Error = true, Title = "Khong tim thay ban ghi!" });
                }
            }
            catch
            {
                return Json(new JMessage { Error = true, Title = "Đã cõ lỗi xảy ra trong quá trình thực hiện!" });
            }
        }
        [HttpPost]
        public JsonResult getInfoId(string Masach)
        {
            if (Masach != null)
            {
                var sach = data.Saches.FirstOrDefault(x => x.Masach == Masach);
                return Json(new Sach() { Masach = sach.Masach, Tensach = sach.Tensach, Namxuatban = sach.Namxuatban, Soluong = sach.Soluong, Manhaxuatban = sach.Manhaxuatban, Matacgia = sach.Matacgia, Maloai=sach.Maloai, Mavitri = sach.Mavitri }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }


        public void ToExcel()
        {
            var listSach = new List<Sach>();
            var list = data.Saches.ToList();
            foreach (var item in list)
            {
                var model = new Sach();
                model.Masach = item.Masach;
                model.Tensach = item.Tensach;
                model.Namxuatban = item.Namxuatban;
                model.Soluong = item.Soluong;
                model.Manhaxuatban = item.Manhaxuatban;
                model.Matacgia = item.Matacgia;
                model.Maloai = item.Maloai;
                model.Mavitri = item.Mavitri;
                listSach.Add(model);
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Bảng :";
            ws.Cells["B1"].Value = "Sách";

            ws.Cells["A2"].Value = "Báo Cáo";
            ws.Cells["B2"].Value = "Báo cáo về sách";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Mã Sách";
            ws.Cells["B6"].Value = "Tên Sách";
            ws.Cells["C6"].Value = "Năm Xuất Bản";
            ws.Cells["D6"].Value = "Số Lượng";
            ws.Cells["E6"].Value = "Mã Nhà Xuất Bản";
            ws.Cells["F6"].Value = "Mã Tác Giả";
            ws.Cells["G6"].Value = "Mã Loại";
            ws.Cells["H6"].Value = "Mã Vị Trí";

            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Masach.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Masach;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Tensach;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Namxuatban;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Soluong;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Manhaxuatban;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Matacgia;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Maloai;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Mavitri;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
    }
}