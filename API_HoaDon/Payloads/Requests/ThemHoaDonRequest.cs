using API_HoaDon.Entities;

namespace API_HoaDon.Payloads.Requests
{
    public class ThemHoaDonRequest
    {
        public int KhachHangId { get; set; }
        public string TenHoaDon { get; set; }
        public string GhiChu { get; set; }
    }
}
