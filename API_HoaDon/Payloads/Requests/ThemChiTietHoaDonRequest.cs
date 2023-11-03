using API_HoaDon.Entities;

namespace API_HoaDon.Payloads.Requests
{
    public class ThemChiTietHoaDonRequest
    {
        //public int HoaDonId { get; set; }
        public int SanPhamId { get; set; }
        public int SoLuong { get; set; }
        public string DVT { get; set; }
    }
}
