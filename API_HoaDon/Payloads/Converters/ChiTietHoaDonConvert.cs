using API_HoaDon.Entities;
using API_HoaDon.Payloads.DTOs;

namespace API_HoaDon.Payloads.Converters
{
    public class ChiTietHoaDonConvert
    {
        private readonly AppDbContext dbContext;

        public ChiTietHoaDonConvert()
        {
            dbContext = new AppDbContext();
        }
        public List<ChiTietHoaDonDTO> EntityToDTO(List<ChiTietHoaDon> lstChiTiet)
        {
            List<ChiTietHoaDonDTO> listDTO = new List<ChiTietHoaDonDTO>();
            foreach (var chiTietHoaDon in lstChiTiet)
            {
                ChiTietHoaDonDTO chiTietHoaDonDTO = new ChiTietHoaDonDTO();

                chiTietHoaDonDTO.TenHoaDon = dbContext.HoaDon.FirstOrDefault(x => x.HoaDonId == chiTietHoaDon.HoaDonId).TenHoaDon;
                chiTietHoaDonDTO.TenSanPham = dbContext.SanPham.FirstOrDefault(x => x.SanPhamId == chiTietHoaDon.SanPhamId).TenSanPham;
                chiTietHoaDonDTO.SoLuong = chiTietHoaDon.SoLuong;
                chiTietHoaDonDTO.DVT = chiTietHoaDon.DVT;
                chiTietHoaDonDTO.ThanhTien = chiTietHoaDon.ThanhTien;
                listDTO.Add(chiTietHoaDonDTO);
            }

            return listDTO;
        }
    }
}
