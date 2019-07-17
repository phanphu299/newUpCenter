
namespace Up.Enums
{
    using System;

    public enum LoaiCheDoEnums
    {
        FullTime = 1,
        PartTime
    }
    public static class LoaiCheDoFunctions
    {
        public static Guid ToId(this LoaiCheDoEnums value)
        {
            switch (value)
            {
                case LoaiCheDoEnums.FullTime:
                    return Guid.Parse("2153D8B8-E8F4-4513-085E-08D70AC508CC");
                case LoaiCheDoEnums.PartTime:
                    return Guid.Parse("D59BB3EC-CFAD-4E17-085F-08D70AC508CC");
                default:
                    throw new System.IO.InvalidDataException();
            }
        }
    }
}
