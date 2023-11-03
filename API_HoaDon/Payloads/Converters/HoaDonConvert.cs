using API_HoaDon.Entities;
using API_HoaDon.Payloads.DTOs;
using API_HoaDon.Payloads.Requests;

namespace API_HoaDon.Payloads.Converters
{
    public class HoaDonConvert
    {
        private readonly AppDbContext dbContext;

        public HoaDonConvert()
        {
            dbContext = new AppDbContext();
        }
        public HoaDonDTO EntityToDTO(HoaDon hoaDon)
        {
            return new HoaDonDTO
            {
                HoTen = dbContext.KhachHang.SingleOrDefault(x => x.KhachHangId == hoaDon.KhachHangId).HoTen,
                TenHoaDon = hoaDon.TenHoaDon,
                MaGiaoDich = hoaDon.MaGiaoDich,
                ThoiGianTao = hoaDon.ThoiGianTao,
                ThoiGianCapNhat = hoaDon.ThoiGianCapNhat,
                GhiChu = hoaDon.GhiChu,
                TongTien = hoaDon.TongTien

            };
        }
    }
}
