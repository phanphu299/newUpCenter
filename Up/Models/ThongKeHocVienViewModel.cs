namespace Up.Models
{
    using System;
    using System.Collections.Generic;

    public class ThongKeHocVienViewModel
    {
        public List<double> GiaoTiep { get; set; }
        public List<double> ThieuNhi { get; set; }
        public List<double> QuocTe { get; set; }
    }

    public class ThongKeModel
    {
        public string Label { get; set; }
        public Guid HocVienId { get; set; }
        public double Data { get; set; }
        public List<ThongKeDiemDanhModel> ThongKeDiemDanh { get; set; }

        public int NgayBatDau_Day { get; set; }
        public int NgayBatDau_Month { get; set; }
        public int NgayBatDau_Year { get; set; }

        public int NgayKetThuc_Day { get; set; }
        public int NgayKetThuc_Month { get; set; }
        public int NgayKetThuc_Year { get; set; }
        public DateTime Date { get; set; }
    }

    public class ThongKeDiemDanhModel
    {
        public bool IsOff { get; set; }
        public bool? DuocNghi { get; set; }
        public DateTime Dates { get; set; }
        public int Day { get; set; }
    }
}
