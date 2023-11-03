namespace API_HoaDon.Payloads.DTOs
{
    public class HoaDonDTO
    {
        public string HoTen { get; set; }
        public string TenHoaDon { get; set; }
        public string? MaGiaoDich { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
        public string GhiChu { get; set; }
        public double? TongTien { get; set; }
    }
}
