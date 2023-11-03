namespace API_HoaDon.Entities
{
    public class LoaiSanPham
    {
        public int LoaiSanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public List<SanPham> SanPhams { get; set; }
    }
}
