﻿
namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IThongKeService
    {
        Task<List<HocVienViewModel>> GetHocVienGiaoTiepAsync();
        Task<List<HocVienViewModel>> GetHocVienThieuNhiAsync();
        Task<List<HocVienViewModel>> GetHocVienCCQuocTeAsync();
    }
}
