namespace API_HoaDon.Payloads.Requests
{
    public class SuaHoaDonRequest
    {
        public int HoaDonId { get; set; }
        public int KhachHangId { get; set; }
        public string TenHoaDon { get; set; }
        public string GhiChu { get; set; }
    }
}
