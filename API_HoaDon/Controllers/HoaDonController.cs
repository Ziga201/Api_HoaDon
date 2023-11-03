using API_HoaDon.Entities;
using API_HoaDon.IServices;
using API_HoaDon.Payloads.Requests;
using API_HoaDon.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_HoaDon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonServices _services;
        public HoaDonController()
        {
            _services = new HoaDonServices();
        }
        [HttpPost("ThemHoaDon")]
        public IActionResult ThemHoaDon([FromForm] ThemHoaDonRequest request)
        {
            return Ok(_services.ThemHoaDon(request));
        }
        [HttpPut("SuaHoaDon")]
        public IActionResult SuaHoaDon([FromForm] SuaHoaDonRequest request)
        {
            return Ok(_services.SuaHoaDon(request));
        }
        [HttpDelete("XoaHoaDon")]
        public IActionResult XoaHoaDon([FromForm] XoaHoaDonRequest request)
        {
            return Ok(_services.XoaHoaDon(request));
        }
        //[HttpPost("themChiTietHoaDon")]
        //public IActionResult ThemCTHDs(int hoaDonId,[FromBody] List<ThemChiTietHoaDonRequest> requests)
        //{
        //    return Ok(_services.ThemCTHDs(hoaDonId, requests));
        //}

        [HttpPost("ThemChiTietHoaDon")]
        public IActionResult ThemChiTietHoaDon(int hoaDonId, List<ThemChiTietHoaDonRequest> lstRequest)
        {
            return Ok(_services.ThemChiTietHoaDon(hoaDonId, lstRequest));
        }
        [HttpGet("LayDSHoaDon")]
        public IActionResult LayDSHoaDon([FromQuery] Pagination pagination, string? key, int? year = null, int? month = null, DateTime? firstDay = null,
            DateTime? lastDay = null, double? minTotal = null, double? maxTotal = null)
        {
            return Ok(_services.DSHoaDon(pagination, key, year, month, firstDay, lastDay, minTotal, maxTotal));
        }

    }
}
