using Microsoft.EntityFrameworkCore;

namespace API_HoaDon.Entities
{
    public class AppDbContext:DbContext
    {
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server = ZIGA\\SQLEXPRESS; Database = QLHoaDon; Trusted_Connection = True; " +
                $"TrustServerCertificate=True");
        }
    }
}
