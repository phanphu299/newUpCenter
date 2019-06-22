Vue.component('line-chart', {
    extends: VueChartJs.Line,
    props: {
        chartdata: {
            type: Object,
            default: null
        },
        options: {
            type: Object,
            default: null
        }
    },
    mounted() {
        this.renderChart(this.chartdata, { responsive: true, maintainAspectRatio: false });
    }

});

var vue = new Vue({
    el: '#HomeIndex',
    data: {
        tab: null,
        loadedHocVien: false,
        loadedGiaoVien: false,
        loadedDoanhThuHocPhi: false,
        loadedNo: false,
        loadedChiPhi: false,
        chartdataHocVien: {
            labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
            datasets: [
                {
                    label: 'Giao Tiếp',
                    data: [],
                    borderColor: '#4caf50',
                    pointBackgroundColor: '#4caf50',
                    borderWidth: 1,
                    pointBorderColor: '#4caf50',
                    backgroundColor: 'transparent'
                },
                {
                    label: 'Thiếu Nhi',
                    data: [],
                    borderColor: '#1976d2',
                    pointBackgroundColor: '#1976d2',
                    borderWidth: 1,
                    pointBorderColor: '#1976d2',
                    backgroundColor: 'transparent'
                },
                {
                    label: 'Chứng Chỉ Quốc Tế',
                    data: [],
                    borderColor: '#f44336',
                    pointBackgroundColor: '#f44336',
                    borderWidth: 1,
                    pointBorderColor: '#f44336',
                    backgroundColor: 'transparent'
                }
            ]
        },
        chartdataGiaoVien: {
            labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
            datasets: [
                {
                    label: 'Full Time',
                    data: [],
                    borderColor: '#4caf50',
                    pointBackgroundColor: '#4caf50',
                    borderWidth: 1,
                    pointBorderColor: '#4caf50',
                    backgroundColor: 'transparent'
                },
                {
                    label: 'Part Time',
                    data: [],
                    borderColor: '#1976d2',
                    pointBackgroundColor: '#1976d2',
                    borderWidth: 1,
                    pointBorderColor: '#1976d2',
                    backgroundColor: 'transparent'
                },
                {
                    label: 'Giáo Viên Nước Ngoài',
                    data: [],
                    borderColor: '#f44336',
                    pointBackgroundColor: '#f44336',
                    borderWidth: 1,
                    pointBorderColor: '#f44336',
                    backgroundColor: 'transparent'
                }
            ]
        },

        thongKeTong: {
            hocVien: 0,
            giaoVien: 0,
            doanhThu: 0,
            chiPhi: 0
        },

        chartdataDoanhThuHocPhi: {
            labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
            datasets: [
                {
                    label: 'Doanh Thu Học Phí',
                    data: [],
                    borderColor: '#4caf50',
                    pointBackgroundColor: '#4caf50',
                    borderWidth: 1,
                    pointBorderColor: '#4caf50',
                    backgroundColor: 'transparent'
                }
            ]
        },

        chartdataChiPhi: {
            labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
            datasets: [
                {
                    label: 'Chi Phí',
                    data: [],
                    borderColor: '#1976d2',
                    pointBackgroundColor: '#1976d2',
                    borderWidth: 1,
                    pointBorderColor: '#1976d2',
                    backgroundColor: 'transparent'
                }
            ]
        },

        chartdataNo: {
            labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
            datasets: [
                {
                    label: 'Học Viên Nợ',
                    data: [],
                    borderColor: '#f44336',
                    pointBackgroundColor: '#f44336',
                    borderWidth: 1,
                    pointBorderColor: '#f44336',
                    backgroundColor: 'transparent'
                }
            ]
        }

    },

    async mounted() {
        var that = this;
        try {
            await axios.get('/ThongKe/GetThongKeHocVienAsync')
                .then(function (response) {
                    that.chartdataHocVien.datasets[0].data = response.data.giaoTiep;
                    that.chartdataHocVien.datasets[1].data = response.data.thieuNhi;
                    that.chartdataHocVien.datasets[2].data = response.data.quocTe;
                    that.loadedHocVien = true;
                })
                .catch(function (error) {
                    console.log(error);
                });

            await axios.get('/ThongKe/GetThongKeGiaoVienAsync')
                .then(function (response) {
                    that.chartdataGiaoVien.datasets[0].data = response.data.fullTime;
                    that.chartdataGiaoVien.datasets[1].data = response.data.partTime;
                    that.chartdataGiaoVien.datasets[2].data = response.data.quocTe;
                    that.loadedGiaoVien = true;
                })
                .catch(function (error) {
                    console.log(error);
                });

            await axios.get('/ThongKe/GetTongGiaoVienVaHocVienAsync')
                .then(function (response) {
                    that.thongKeTong.hocVien = response.data.hocVien;
                    that.thongKeTong.giaoVien = response.data.giaoVien;
                    that.thongKeTong.doanhThu = response.data.doanhThu;
                    that.thongKeTong.chiPhi = response.data.chiPhi;
                })
                .catch(function (error) {
                    console.log(error);
                });

            await axios.get('/ThongKe/GetThongKeDoanhThu_HocPhiAsync')
                .then(function (response) {
                    that.chartdataDoanhThuHocPhi.datasets[0].data = response.data;
                    that.loadedDoanhThuHocPhi = true;
                })
                .catch(function (error) {
                    console.log(error);
                });

            await axios.get('/ThongKe/GetThongKeChiPhiAsync')
                .then(function (response) {
                    that.chartdataChiPhi.datasets[0].data = response.data;
                    that.loadedChiPhi = true;
                })
                .catch(function (error) {
                    console.log(error);
                });

            await axios.get('/ThongKe/GetNoAsync')
                .then(function (response) {
                    that.chartdataNo.datasets[0].data = response.data;
                    that.loadedNo= true;
                })
                .catch(function (error) {
                    console.log(error);
                });
        } catch (e) {
            console.error(e);
        }
    },

    methods: {
        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },
    }
});