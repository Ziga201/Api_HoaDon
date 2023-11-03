namespace API_HoaDon.Entities
{
    public class HoaDon
    {

        public int HoaDonId { get; set; }
        public int KhachHangId { get; set; }
        public string TenHoaDon { get; set; }
        public string? MaGiaoDich { get; set; }
        public DateTime ThoiGianTao { get; set; } = DateTime.Now;
        public DateTime? ThoiGianCapNhat { get; set; } = DateTime.Now;
        public string GhiChu { get; set; }
        public double? TongTien { get; set; }
        public KhachHang KhachHang { get; set; }
        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }


    }
}
