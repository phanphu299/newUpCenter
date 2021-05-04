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
                    borderColor: '#1976d2',
                    pointBackgroundColor: '#1976d2',
                    borderWidth: 1,
                    pointBorderColor: '#1976d2',
                    backgroundColor: 'transparent'
                },
                {
                    label: 'Chi Phí',
                    data: [],
                    borderColor: '#f44336',
                    pointBackgroundColor: '#f44336',
                    borderWidth: 1,
                    pointBorderColor: '#f44336',
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
        },

        hocVienNghiNhieu: {},
        headers: [
            { text: 'Họ Tên', value: 'tenHocVien', align: 'left', sortable: true },
            { text: 'Lớp Học', value: 'tenLop', align: 'left', sortable: true },
            { text: 'Ngày Học Cuối', value: 'ngayHocCuoi', align: 'left', sortable: true },
        ],
        search: '',

        bienLai: {},
        headersBienLai: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Mã Biên Lai', value: 'maBienLai', align: 'left', sortable: true },
            { text: 'Tên Học Viên', value: 'fullName', align: 'left', sortable: true },
            { text: 'Lớp Học', value: 'tenLop', align: 'left', sortable: true },
            { text: 'Học Phí', value: 'hocPhi', align: 'left', sortable: true },
            { text: 'Tháng Học Phí', value: 'thangHocPhi', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
        ],
        searchBienLai: '',
        deleteBienLaiDialog: false,
        bienLaiToDelete: {},

        hocVienTheoDoi: [],
        headersTheoDoi: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Họ Tên', value: 'tenHocVien', align: 'left', sortable: true },
            { text: 'Trigram', value: 'trigram', align: 'left', sortable: true },
            { text: 'Ghi Chú', value: 'ghiChu', align: 'left', sortable: true },
        ],
        searchTheoDoi: '',
        loadingHocVien: false,
        itemsHocVien: [],
        itemToDeleteTheoDoi: {},
        dialogTheoDoi: false,
        deleteDialogTheoDoi: false,
        newItemTheoDoi: {
            hocVien: '',
            ghiChu: ''
        },
        alert: false,
        searchHocVien: null,

        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
    },

    watch: {
        searchHocVien(val) {
            val && val !== this.newItemTheoDoi.hocVien && this.querySelections(val)
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
                    console.log(error.response.data.Message);
                });

            await axios.get('/ThongKe/GetThongKeGiaoVienAsync')
                .then(function (response) {
                    that.chartdataGiaoVien.datasets[0].data = response.data.fullTime;
                    that.chartdataGiaoVien.datasets[1].data = response.data.partTime;
                    //that.chartdataGiaoVien.datasets[2].data = response.data.quocTe;
                    that.loadedGiaoVien = true;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });

            await axios.get('/ThongKe/GetTongGiaoVienVaHocVienAsync')
                .then(function (response) {
                    that.thongKeTong.hocVien = response.data.hocVien;
                    that.thongKeTong.giaoVien = response.data.giaoVien;
                    that.thongKeTong.doanhThu = response.data.doanhThu;
                    that.thongKeTong.chiPhi = response.data.chiPhi;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });

            await axios.get('/ThongKe/GetThongKeDoanhThu_HocPhiAsync')
                .then(function (response) {
                    that.chartdataDoanhThuHocPhi.datasets[0].data = response.data.doanhThu;
                    that.chartdataDoanhThuHocPhi.datasets[1].data = response.data.chiPhi;
                    that.loadedDoanhThuHocPhi = true;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });

            await axios.get('/ThongKe/GetNoAsync')
                .then(function (response) {
                    that.chartdataNo.datasets[0].data = response.data;
                    that.loadedNo= true;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });

            await axios.get('/ThongKe/GetHocVienNghiHon3NgayAsync')
                .then(function (response) {
                    that.hocVienNghiNhieu = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });

            await axios.get('/HocPhi/CheckHocPhiTronGoiAsync')
                .then(function (response) {
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });

            await axios.get('/BienLai/GetBienLaiAsync')
                .then(function (response) {
                    that.bienLai = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });

            await axios.get('/ThongKe/GetHocVienTheoDoiAsync')
                .then(function (response) {
                    that.hocVienTheoDoi = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        } catch (e) {
            console.error(e);
        }
    },

    methods: {
        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        async querySelections(v) {
            this.loadingHocVien = true;

            let that = this;
            await axios.get('/hocvien/GetHocVienByNameAsync?name=' + v)
                .then(function (response) {
                    that.itemsHocVien = response.data;
                    that.loadingHocVien = false;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        },

        async onSaveTheoDoi() {
            if (this.newItemTheoDoi.ghiChu === '') {
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/thongke/CreateHocVienTheoDoiAsync',
                    data: {
                        GhiChu: that.newItemTheoDoi.ghiChu,
                        HocVienId: that.newItemTheoDoi.hocVien.hocVienId
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.hocVienTheoDoi.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItemTheoDoi.ghiChu = '';
                            that.newItemTheoDoi.hocVien = '';
                            that.dialogTheoDoi = false;
                        }
                        else {
                            that.snackbar = true;
                            that.messageText = response.data.message;
                            that.color = 'error';
                        }
                    })
                    .catch(function (error) {
                        console.log(error.response.data.Message);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới lỗi: ' + error.response.data.Message;
                        that.color = 'error';
                    });
            }
        },

        async onDeleteTheoDoi(item) {
            let that = this;
            await axios({
                method: 'delete',
                url: '/thongke/DeleteHocVienTheoDoiAsync',
                data: {
                    NoteId: item.noteId
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.hocVienTheoDoi.splice(that.hocVienTheoDoi.indexOf(item), 1);
                        that.snackbar = true;
                        that.messageText = 'Xóa thành công !!!';
                        that.color = 'success';
                        that.deleteDialogTheoDoi = false;
                    }
                    else {
                        that.snackbar = true;
                        that.messageText = response.data.message;
                        that.color = 'error';
                    }
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Xóa lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onUpdateTheoDoi(item) {
            let that = this;
            await axios({
                method: 'put',
                url: '/thongke/UpdateHocVienTheoDoiAsync',
                data: {
                    GhiChu: item.ghiChu,
                    NoteId: item.noteId
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Cập nhật thành công !!!';
                        that.color = 'success';
                    }
                    else {
                        that.snackbar = true;
                        that.messageText = response.data.message;
                        that.color = 'error';
                    }
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onDeleteBienLai(item) {
            let that = this;
            await axios({
                method: 'delete',
                url: '/BienLai/DeleteBienLaiAsync',
                data: {
                    BienLaiId: item.bienLaiId
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.bienLai.splice(that.bienLai.indexOf(item), 1);
                        that.snackbar = true;
                        that.messageText = 'Xóa thành công !!!';
                        that.color = 'success';
                        that.deleteBienLaiDialog = false;
                    }
                    else {
                        that.snackbar = true;
                        that.messageText = response.data.message;
                        that.color = 'error';
                    }
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Xóa lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        }
    }
});