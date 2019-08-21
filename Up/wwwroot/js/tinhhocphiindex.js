var vue = new Vue({
    el: '#HocPhiIndex',
    data: {
        title: 'Tính Học Phí',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        selectedLopHoc: '',
        selectedThang: '',
        selectedNam: '',
        itemThang: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        itemKhuyenMai: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0],
        itemLopHoc: [],
        itemSach: [],
        tongNgayHoc: 0,
        tongNgayDuocNghi: 0,
        tongHocPhi: 0,
        hocPhiMoiNgay: 0,
        hocVienList: [],
        headers: [
            { text: 'Đã Đóng?', align: 'left', sortable: true },
            { text: 'Tên Học Viên', align: 'left', sortable: true },
            { text: 'Học Phí Tháng Này', align: 'left', sortable: true, class: "red-header"},
            { text: 'Nợ', align: 'left', sortable: true },
            { text: 'Tài Liệu', align: 'left', sortable: true },
            { text: 'Khuyến Mãi', align: 'left', sortable: true },
            { text: 'Bonus', align: 'left', sortable: true },
            { text: 'Khoảng Trừ Khác', align: 'left', sortable: true },
            { text: 'Ghi Chú', align: 'left', sortable: true },
            { text: 'Action', align: 'left', sortable: false }
        ],
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/LopHoc/GetAvailableLopHocAsync')
            .then(function (response) {
                that.itemLopHoc = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/Category/GetSachAsync')
            .then(function (response) {
                that.itemSach = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    },

    computed: {
        total() {
            let sum = 0;
            for (let x of this.hocVienList) {
                sum += (x.hocPhiMoi * 1.0);
            }
            return sum;
        }
    },

    methods: {
        async onchangeKhuyenMai(value, item) {
            if (value === undefined) {
                value = 0;
            }
            item.hocPhiMoi = item.hocPhiFixed - ((item.hocPhiFixed * value) / 100);
            item.hocPhiMoi = (Math.ceil(item.hocPhiMoi / 10000) * 10000);
            if (item.lastGiaSach !== null) {
                for (let i = 0; i < item.lastGiaSach.length; i++) {
                    item.hocPhiMoi = item.hocPhiMoi + (item.lastGiaSach[i].gia * 1.0);
                }
            }

            if (item.lastBonus > 0) {
                item.hocPhiMoi = item.hocPhiMoi + (item.lastBonus * 1.0);
            }

            if (item.lastMinus > 0) {
                item.hocPhiMoi = item.hocPhiMoi - (item.lastMinus * 1.0);
            }
        },

        async onBonusHocPhi(value, item) {
            if (item.lastBonus > 0)
                item.hocPhiMoi = item.hocPhiMoi - (item.lastBonus * 1);
            item.hocPhiMoi = item.hocPhiMoi + (value * 1);
            item.lastBonus = value;
        },

        async onMinusHocPhi(value, item) {
            if (item.lastMinus > 0)
                item.hocPhiMoi = item.hocPhiMoi + (item.lastMinus * 1);
            item.hocPhiMoi = item.hocPhiMoi - (value * 1);
            item.lastMinus = value;
        },

        async onchangeSach(value, item) {
            if (value.length > 0) {
                if (item.lastGiaSach !== null) {
                    for (let i = 0; i < item.lastGiaSach.length; i++) {
                        item.hocPhiMoi = item.hocPhiMoi - (item.lastGiaSach[i].gia * 1.0);
                    }
                }
                item.lastGiaSach = value;
                for (let i = 0; i < value.length; i++) {
                    item.hocPhiMoi = item.hocPhiMoi + (value[i].gia * 1.0);
                }
            }
            else {
                for (let i = 0; i < item.lastGiaSach.length; i++) {
                    item.hocPhiMoi = item.hocPhiMoi - (item.lastGiaSach[i].gia * 1.0);
                }
                item.lastGiaSach = value;
            }
        },

        async onTinhTien() {
            let that = this;
            if (this.selectedLopHoc !== '' && this.selectedNam !== '' && this.selectedThang !== '') {
                if (this.khuyenMai === "") {
                    this.khuyenMai = 0;
                }
                await axios.get('/HocPhi/GetTinhHocPhiAsync?LopHocId=' + this.selectedLopHoc.lopHocId + '&Month=' + this.selectedThang + '&Year=' + this.selectedNam)
                    .then(function (response) {
                        that.tongNgayHoc = response.data.soNgayHoc;
                        that.tongNgayDuocNghi = response.data.soNgayDuocNghi;
                        that.tongHocPhi = response.data.hocPhi;
                        that.hocVienList = response.data.hocVienList;
                        that.hocPhiMoiNgay = response.data.hocPhiMoiNgay;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        },

        forceFileDownload(response) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'hocphi_' + this.selectedLopHoc.name + '_T' + this.selectedThang + '-' + this.selectedNam + '.xlsx'); //or any other extension
            document.body.appendChild(link);
            link.click();
        },

        async onExport() {
            let that = this;
            await axios
                ({
                    url: '/HocPhi/Export',
                    method: 'put',
                    responseType: 'blob', // important
                    data: {
                        HocVienList: that.hocVienList,
                        LopHocId: that.selectedLopHoc.lopHocId,
                        month: that.selectedThang,
                        year: that.selectedNam
                    }
                })
                .then(function (response) {
                    that.forceFileDownload(response);
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        async onLuu(item) {
            let that = this;
            if (isNaN(item.hocPhiMoi)) {
                this.messageText = "Học phí chỉ được nhập số";
                this.snackbar = true;
                this.color = 'error';
            }
            else {
                let Sachs = item.giaSach.map(m => m.sachId);
                await axios({
                    method: 'post',
                    url: '/HocPhi/LuuDoanhThu_HocPhiAsync',
                    data: {
                        LopHocId: this.selectedLopHoc.lopHocId,
                        HocVienId: item.hocVienId,
                        HocPhi: item.hocPhiMoi,
                        month: this.selectedThang,
                        year: this.selectedNam,
                        Bonus: item.bonus,
                        Minus: item.minus,
                        KhuyenMai: item.khuyenMai,
                        GhiChu: item.ghiChu,
                        SachIds: Sachs
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.snackbar = true;
                            that.messageText = 'Lưu Doanh Thu thành công !!!';
                            that.color = 'success';
                            item.daDongHocPhi = true;
                        }
                        else {
                            that.snackbar = true;
                            that.messageText = response.data.message;
                            that.color = 'error';
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                        that.snackbar = true;
                        that.messageText = 'Lưu Doanh Thu lỗi: ' + error;
                        that.color = 'error';
                    });
            }
        },

        async onNo(item) {
            let that = this;
            if (isNaN(item.hocPhiMoi)) {
                this.messageText = "Học phí chỉ được nhập số";
                this.snackbar = true;
                this.color = 'error';
            }
            else {
                await axios({
                    method: 'post',
                    url: '/HocPhi/LuuNo_HocPhiAsync',
                    data: {
                        LopHocId: this.selectedLopHoc.lopHocId,
                        HocVienId: item.hocVienId,
                        TienNo: item.hocPhiMoi,
                        month: this.selectedThang,
                        year: this.selectedNam
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.snackbar = true;
                            that.messageText = 'Lưu Nợ thành công !!!';
                            that.color = 'success';
                            item.daDongHocPhi = false;
                        }
                        else {
                            that.snackbar = true;
                            that.messageText = response.data.message;
                            that.color = 'error';
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                        that.snackbar = true;
                        that.messageText = 'Lưu Nợ lỗi: ' + error;
                        that.color = 'error';
                    });
            }
        }
    }
});