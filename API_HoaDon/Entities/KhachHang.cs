namespace API_HoaDon.Entities
{
    public class KhachHang
    {
        public int KhachHangId { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Sdt { get; set; }
        public List<HoaDon> HoaDones { get; set; }
    }
}
