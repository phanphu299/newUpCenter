using OfficeOpenXml;
using System;
using Up.Models;

namespace Up.Converters
{
    public class Converter
    {
        public ResultModel ToResultModel(string message, bool isSuccess, object data = null)
        {
            return new ResultModel
            {
                Message = message,
                Status = isSuccess ? "OK" : "Failed",
                Result = data
            };
        }

        public DateTime ToDateTime(string value)
        {
            return Convert.ToDateTime(value, System.Globalization.CultureInfo.InvariantCulture);
        }

        public ImportHocVienInputModel ToImportHocVien(ExcelWorksheet worksheet, int row)
        {
            Guid? quanHe = null;
            return new ImportHocVienInputModel
            {
                FullName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                EnglishName = worksheet.Cells[row, 2].Value?.ToString().Trim() ?? string.Empty,
                Phone = worksheet.Cells[row, 3].Value?.ToString().Trim() ?? string.Empty,
                OtherPhone = worksheet.Cells[row, 4].Value?.ToString().Trim() ?? string.Empty,
                FacebookAccount = worksheet.Cells[row, 5].Value?.ToString().Trim() ?? string.Empty,
                ParentFullName = worksheet.Cells[row, 7].Value?.ToString().Trim() ?? string.Empty,
                ParentPhone = worksheet.Cells[row, 8].Value?.ToString().Trim() ?? string.Empty,
                QuanHeId = worksheet.Cells[row, 9].Value == null ? quanHe : new Guid(worksheet.Cells[row, 9].Value.ToString().Trim()),
                CMND = worksheet.Cells[row, 11].Value?.ToString().Trim() ?? string.Empty,
                DiaChi = worksheet.Cells[row, 12].Value?.ToString().Trim() ?? string.Empty,
                Notes = worksheet.Cells[row, 13].Value?.ToString().Trim() ?? string.Empty
            };
        }

        public DiemDanhHocVienInput ToDiemDanhHocVienInput(Guid lopHocId, Guid hocVienId, bool isOff, DateTime ngayDiemDanh)
        {
            return new DiemDanhHocVienInput
            {
                LopHocId = lopHocId,
                HocVienId = hocVienId,
                IsOff = isOff,
                NgayDiemDanh = ngayDiemDanh
            };
        }

        public CreateBienLaiInputModel ToCreateBienLai(ThongKe_DoanhThuHocPhiInputModel model, string maBienLai)
        {
            return new CreateBienLaiInputModel
            {
                HocPhi = model.HocPhi,
                HocVienId = model.HocVienId,
                LopHocId = model.LopHocId,
                MaBienLai = maBienLai,
                ThangHocPhi = $"{model.month}/{model.year}"
            };
        }
        
        public CreateBienLaiInputModel ToCreateBienLaiTronGoi(CreateBienLaiTronGoiInputModel model, string maBienLai)
        {
            return new CreateBienLaiInputModel
            {
                HocPhi = model.HocPhi,
                HocVienId = model.HocVienId,
                LopHocId = Guid.Empty,
                MaBienLai = maBienLai,
                ThangHocPhi = $"{model.FromDate} - {model.ToDate}"
            };
        }
    }
}
