using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IKhoaHocService
    {
        Task<List<KhoaHocViewModel>> GetKhoaHocAsync();
    }
}
