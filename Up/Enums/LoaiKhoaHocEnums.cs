namespace Up.Enums
{
    using System;

    public enum LoaiKhoaHocEnums
    {
        GiaoTiep = 1,
        ThieuNhi
    }

    public static class LoaiKhoaHocFunctions
    {
        public static Guid ToId(this LoaiKhoaHocEnums value)
        {
            switch (value)
            {
                case LoaiKhoaHocEnums.GiaoTiep:
                    return Guid.Parse("1A86418F-C9FA-47D7-4C0F-08D699557C37");
                case LoaiKhoaHocEnums.ThieuNhi:
                    return Guid.Parse("32B1D9A4-EB39-440B-4C10-08D699557C37");
                default:
                    throw new System.IO.InvalidDataException();
            }
        }
    }
}
