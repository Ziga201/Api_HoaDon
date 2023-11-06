using API_HoaDon.Entities;
using API_HoaDon.IServices;
using API_HoaDon.Payloads.Converters;
using API_HoaDon.Payloads.DTOs;
using API_HoaDon.Payloads.Requests;
using API_HoaDon.Payloads.Responses;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace API_HoaDon.Services
{
    public class HoaDonServices : IHoaDonServices
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<HoaDonDTO> responseObject;
        private readonly ResponseObject<List<ChiTietHoaDonDTO>> responseObjectChiTiet;
        private readonly HoaDonConvert hoaDonConvert;
        private readonly ChiTietHoaDonConvert chiTietHoaDonConvert;

        public HoaDonServices()
        {
            dbContext = new AppDbContext();
            responseObject = new ResponseObject<HoaDonDTO>();
            responseObjectChiTiet = new ResponseObject<List<ChiTietHoaDonDTO>>();
            hoaDonConvert = new HoaDonConvert();
            chiTietHoaDonConvert = new ChiTietHoaDonConvert();
        }


        public ResponseObject<HoaDonDTO> ThemHoaDon(ThemHoaDonRequest request)
        {
            if (!dbContext.KhachHang.Any(x => x.KhachHangId == request.KhachHangId))
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy khách hàng", null);
            }
            //var hoaDonThem = hoaDonConvert.ThemHoaDon(request);
            //DateTime dt = hoaDonThem.ThoiGianTao;
            HoaDon hoaDon = new HoaDon();
            DateTime dt = DateTime.Now;
            hoaDon.KhachHangId = request.KhachHangId;
            hoaDon.TenHoaDon = request.TenHoaDon;
            //hoaDon.MaGiaoDich = $"{dt.Year}{dt.Month:MM}{dt.Day.ToString("dd")}_{MaGiaoDich() +1}";
            hoaDon.MaGiaoDich = $"{dt.ToString("yyyyMMdd")}_{(MaGiaoDich() + 1).ToString("D3")}";
            hoaDon.ThoiGianTao = dt;
            hoaDon.ThoiGianCapNhat = dt;
            hoaDon.GhiChu = request.GhiChu;
            hoaDon.TongTien = 0;
            dbContext.HoaDon.Add(hoaDon);
            dbContext.SaveChanges();
            //hoaDon.ChiTietHoaDons = ThemCTHDs(hoaDon.HoaDonId);
            return responseObject.ResponseSucess("Thêm hoá đơn thành công", hoaDonConvert.EntityToDTO(hoaDon));
        }
        public int MaGiaoDich()
        {
            var lastRow = dbContext.HoaDon.OrderByDescending(x => x.HoaDonId).FirstOrDefault();
            if (lastRow == null) return 0;
            if (DateTime.Now.ToShortDateString() == lastRow.ThoiGianTao.ToShortDateString())
            {
                string maGDCuoi = lastRow.MaGiaoDich;
                int soHoaDon = int.Parse(maGDCuoi.Substring(maGDCuoi.IndexOf("_") + 1));
                return soHoaDon;
            }
            else return 0;
        }
        public ResponseObject<HoaDonDTO> SuaHoaDon(SuaHoaDonRequest request)
        {
            var hoaDonCanSua = dbContext.HoaDon.FirstOrDefault(x => x.HoaDonId == request.HoaDonId);
            if (hoaDonCanSua == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy hoá đơn", null);
            }
            if (!dbContext.KhachHang.Any(x => x.KhachHangId == request.KhachHangId))
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy khách hàng", null);
            }
            hoaDonCanSua.KhachHangId = request.KhachHangId;
            hoaDonCanSua.TenHoaDon = request.TenHoaDon;
            hoaDonCanSua.ThoiGianCapNhat = DateTime.Now;
            hoaDonCanSua.GhiChu = request.GhiChu;
            dbContext.Update(hoaDonCanSua);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Sửa hoá đơn thành công", hoaDonConvert.EntityToDTO(hoaDonCanSua));
        }

        public ResponseObject<HoaDonDTO> XoaHoaDon(XoaHoaDonRequest request)
        {
            var hoaDonCanXoa = dbContext.HoaDon.FirstOrDefault(x => x.HoaDonId.Equals(request.HoaDonId));
            if (hoaDonCanXoa == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy hoá đơn", null);
            }
            dbContext.Remove(hoaDonCanXoa);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xoá thành công", hoaDonConvert.EntityToDTO(hoaDonCanXoa));

        }

        public ResponseObject<List<ChiTietHoaDonDTO>> ThemChiTietHoaDon(int hoaDonId, List<ThemChiTietHoaDonRequest> lstRequest)
        {
            var hoaDon = dbContext.HoaDon.FirstOrDefault(x => x.HoaDonId == hoaDonId);
            if (hoaDon == null)
            {
                return responseObjectChiTiet.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy hoá đơn", null);
            }

            List<ChiTietHoaDon> lstChiTiet = new List<ChiTietHoaDon>();


            foreach (var request in lstRequest)
            {
                var sanPham = dbContext.SanPham.FirstOrDefault(x => x.SanPhamId == request.SanPhamId);
                
                if (sanPham == null)
                {
                    dbContext.HoaDon.Remove(hoaDon);
                    dbContext.SaveChanges();
                    return responseObjectChiTiet.ResponseError(StatusCodes.Status404NotFound, "Sản phẩm chưa tồn tại. Vui lòng kiểm tra lại!", null);
                }

                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                chiTietHoaDon.HoaDonId = hoaDonId;
                chiTietHoaDon.SanPhamId = request.SanPhamId;
                chiTietHoaDon.SoLuong = request.SoLuong;
                chiTietHoaDon.DVT = request.DVT;
                chiTietHoaDon.ThanhTien = sanPham.GiaThanh * request.SoLuong;

                lstChiTiet.Add(chiTietHoaDon);

                dbContext.ChiTietHoaDon.Add(chiTietHoaDon);
                dbContext.SaveChanges();

            }
            hoaDon.ChiTietHoaDons = lstChiTiet;
            hoaDon.TongTien = dbContext.ChiTietHoaDon.Sum(x => x.HoaDonId == hoaDonId ? x.ThanhTien : 0);
            hoaDon.ThoiGianCapNhat = DateTime.Now;
            dbContext.HoaDon.Update(hoaDon);
            dbContext.SaveChanges();
            return responseObjectChiTiet.ResponseSucess("Thêm chi tiết hoá đơn thành công", chiTietHoaDonConvert.EntityToDTO(lstChiTiet));
        }
        public PageResult<HoaDon> DSHoaDon(Pagination pagination, string? key, int? year = null, int? month = null, DateTime? firstDay = null, DateTime? lastDay = null, 
            double? minTotal = null, double? maxTotal = null)
        {
            var dsHoaDon = dbContext.HoaDon.Include(x => x.ChiTietHoaDons).OrderByDescending(x => x.ThoiGianTao).ToList();
            if (year.HasValue)
                dsHoaDon = dbContext.HoaDon.Where(x => x.ThoiGianTao.Year == year).ToList();
            if (month.HasValue)
                dsHoaDon = dbContext.HoaDon.Where(x => x.ThoiGianTao.Month == month).ToList();
            if (firstDay.HasValue && lastDay.HasValue)
                dsHoaDon = dbContext.HoaDon.Where(x => x.ThoiGianTao.Date >= firstDay.Value.Date && x.ThoiGianTao.Date <= lastDay.Value.Date).ToList();
            if(minTotal.HasValue &&  maxTotal.HasValue)
                dsHoaDon = dbContext.HoaDon.Where(x => x.TongTien >= minTotal && x.TongTien <= maxTotal).ToList();
            if(!string.IsNullOrWhiteSpace(key))
                dsHoaDon = dbContext.HoaDon.Where(x => x.MaGiaoDich.ToLower().Contains(key.ToLower()) 
                                                || x.TenHoaDon.ToLower().Contains(key.ToLower())).ToList();


            var result = PageResult<HoaDon>.ToPageResult(pagination, dsHoaDon);
            pagination.TotalCount = dsHoaDon.Count();
            return new PageResult<HoaDon>(pagination, result);
        }


        //private List<ChiTietHoaDon> ThemChiTietHoaDons(int hoaDonId, List<ThemChiTietHoaDonRequest> lstRequest)
        //{

        //    List<ChiTietHoaDon> lstChiTiet = new List<ChiTietHoaDon>();

        //    foreach (var request in lstRequest)
        //    {
        //        var sanPham = dbContext.SanPham.FirstOrDefault(x => x.SanPhamId == request.SanPhamId);

        //        ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
        //        chiTietHoaDon.HoaDonId = hoaDonId;
        //        chiTietHoaDon.SanPhamId = request.SanPhamId;
        //        chiTietHoaDon.SoLuong = request.SoLuong;
        //        chiTietHoaDon.DVT = request.DVT;
        //        chiTietHoaDon.ThanhTien = sanPham.GiaThanh * request.SoLuong;

        //        lstChiTiet.Add(chiTietHoaDon);

        //        dbContext.ChiTietHoaDon.Add(chiTietHoaDon);
        //        dbContext.SaveChanges();
        //    }


        //    return lstChiTiet;
        //}

        //public List<ChiTietHoaDon> ThemCTHDs(int hoaDonId, List<ThemChiTietHoaDonRequest> requests)
        //{
        //    var hoaDon = dbContext.HoaDon.SingleOrDefault(x => x.HoaDonId == hoaDonId);
        //    if (hoaDon == null)
        //    {
        //        return null;
        //    }
        //    List<ChiTietHoaDon> chiTiets = new List<ChiTietHoaDon>();
        //    foreach (var chiTiet in requests)
        //    {

        //        var chiTietThem = ThemChiTiet(chiTiet);
        //        chiTietThem.HoaDonId = hoaDonId;
        //        var sanPham = dbContext.SanPham.SingleOrDefault(x => x.SanPhamId == chiTietThem.SanPhamId);
        //        if(sanPham == null)
        //        {
        //            return null;
        //        }
        //        chiTietThem.ThanhTien = sanPham.GiaThanh * chiTietThem.SoLuong;
        //        chiTiets.Add(chiTietThem);

        //    }
        //    dbContext.ChiTietHoaDon.AddRange(chiTiets);
        //    return chiTiets;
        //}
        //private ChiTietHoaDon ThemChiTiet(ThemChiTietHoaDonRequest request)
        //{
        //    return new ChiTietHoaDon()
        //    {
        //        SanPhamId = request.SanPhamId,
        //        SoLuong = request.SoLuong,
        //        DVT = request.DVT
        //    };
        //}




    }
}
