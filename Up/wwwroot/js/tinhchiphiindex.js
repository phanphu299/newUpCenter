var vue = new Vue({
    el: '#ChiPhiIndex',
    data: {
        title: 'Tính Chi Phí',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        selectedThang: '',
        selectedNam: '',
        itemThang: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        itemKhuyenMai: ['5', '10', '15', '20', '25', '30', '35', '40', '45', '50'],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0],
        itemLopHoc: [],
        itemSach: [],
        tongNgayHoc: 0,
        tongNgayDuocNghi: 0,
        tongHocPhi: 0,
        hocPhiMoiNgay: 0,
        chiPhiList: [],
        headers: [
            { text: 'Tên', align: 'left', sortable: true },
            { text: 'Lương/Chi Phí', align: 'left', sortable: true },
            { text: 'Lương Giảng Dạy', align: 'left', sortable: true },
            { text: 'Lương Kèm', align: 'left', sortable: true },
            { text: 'Số Giờ Dạy', align: 'left', sortable: true },
            { text: 'Số Giờ Kèm', align: 'left', sortable: true },
            { text: 'Bonus', align: 'left', sortable: true },
            { text: 'Chi Phí', align: 'left', sortable: true },
            { text: 'Action', align: 'left', sortable: false }
        ]

    },
    methods: {
        async onTinhChiPhi(item) {
            if (!isNaN(item.soGioDay) && !isNaN(item.soGioKem) && !isNaN(item.bonus)) {
                item.chiPhiMoi = item.salary_Expense + (item.soGioKem * item.tutoringRate) + (item.soGioDay * item.teachingRate) + (item.bonus * 1);
            }
        },

        async onchangeKhuyenMai(value, item) {
            item.hocPhiMoi = item.hocPhiMoi - ((item.hocPhiFixed * value) / 100);
        },
        
        async onTinhTien() {
            let that = this;
            if (this.selectedNam !== '' && this.selectedThang !== '') {
                if (this.khuyenMai === "") {
                    this.khuyenMai = 0;
                }
                await axios.get('/ChiPhi/GetChiPhiAsync')
                    .then(function (response) {
                        that.chiPhiList = response.data.chiPhiList;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
            else {
                that.snackbar = true;
                that.messageText = "Phải chọn Tháng và Năm trước khi tính chi phí!!!";
                that.color = 'error';
                that.dialogEdit = false;
            }
        },

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        async onLuu(item) {
            let that = this;
            await axios({
                method: 'post',
                url: '/HocPhi/LuuDoanhThu_HocPhiAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    HocVienId: item.hocVienId,
                    HocPhi: item.hocPhiMoi,
                    month: this.selectedThang,
                    year: this.selectedNam
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Lưu Doanh Thu thành công !!!';
                        that.color = 'success';
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
        },

        async onNo(item) {
            let that = this;
            await axios({
                method: 'post',
                url: '/HocPhi/LuuNo_HocPhiAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
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
});