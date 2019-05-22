namespace Up.Enums
{
    using System;

    public enum LoaiGiaoVienEnums
    {
        FullTime = 1,
        PartTime,
        GVNN
    }
    public static class LoaiGiaoVienFunctions
    {
        public static Guid ToId(this LoaiGiaoVienEnums value)
        {
            switch (value)
            {
                case LoaiGiaoVienEnums.FullTime:
                    return Guid.Parse("19F5C9FA-9601-4D23-1730-08D6D6F43666");
                case LoaiGiaoVienEnums.PartTime:
                    return Guid.Parse("ADC1DCD8-ED5C-4781-1731-08D6D6F43666");
                case LoaiGiaoVienEnums.GVNN:
                    return Guid.Parse("1D46067D-07D6-4A9A-1732-08D6D6F43666");
                default:
                    throw new System.IO.InvalidDataException();
            }
        }
    }
}
