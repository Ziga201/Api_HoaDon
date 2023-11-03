using API_HoaDon.Entities;
using API_HoaDon.Payloads.DTOs;
using API_HoaDon.Payloads.Requests;
using API_HoaDon.Payloads.Responses;

namespace API_HoaDon.IServices
{
    public interface IHoaDonServices
    {
        ResponseObject<HoaDonDTO> ThemHoaDon(ThemHoaDonRequest request);
        ResponseObject<HoaDonDTO> SuaHoaDon(SuaHoaDonRequest request);
        ResponseObject<HoaDonDTO> XoaHoaDon(XoaHoaDonRequest request);
        ResponseObject<List<ChiTietHoaDonDTO>> ThemChiTietHoaDon(int hoaDonId, List<ThemChiTietHoaDonRequest> lstRequest);
        //List<ChiTietHoaDon> ThemCTHDs(int hoaDonId, List<ThemChiTietHoaDonRequest> requests);

        PageResult<HoaDon> DSHoaDon(Pagination pagination, string? key, int? year =null, int? month = null,DateTime? firstDay = null, 
            DateTime? lastDay = null, double? minTotal = null, double? maxTotal = null);

    }
}
