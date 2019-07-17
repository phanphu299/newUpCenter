namespace Up.Enums
{
    using System;
    public enum LoaiNhanVienEnums
    {
        GiaoVien = 1
    }
    public static class LoaiNhanVienFunctions
    {
        public static Guid ToId(this LoaiNhanVienEnums value)
        {
            switch (value)
            {
                case LoaiNhanVienEnums.GiaoVien:
                    return Guid.Parse("01F57758-F1BC-4E52-0DB6-08D70AC5460B");
                default:
                    throw new System.IO.InvalidDataException();
            }
        }
    }
}
