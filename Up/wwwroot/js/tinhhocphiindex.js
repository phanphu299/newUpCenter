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
        selectedSach: '',
        selectedThang: '',
        selectedNam: '',
        itemThang: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0],
        itemLopHoc: [],
        itemSach: [],
        tongNgayHoc: 0,
        tongNgayDuocNghi: 0,
        tongHocPhi: 0,
        khuyenMai: 0,
        hocVienList: [],
        headers: [
            //{
            //    text: 'Action',
            //    align: 'left',
            //    sortable: false,
            //    value: ''
            //},
            { text: 'Tên Học Viên', value: 'fullName', align: 'left', sortable: true },
            { text: 'Nợ Tháng Trước', value: 'tienNo', align: 'left', sortable: true },
            { text: 'Học Phí Tháng Này', value: '', align: 'left', sortable: true },
            { text: 'Action', value: '', align: 'left', sortable: false }
        ]

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
    methods: {
        async onTinhTien() {
            let that = this;
            if (this.selectedLopHoc !== '' && this.selectedNam !== '' && this.selectedThang !== '') {
                if (this.khuyenMai === "") {
                    this.khuyenMai = 0;
                }
                await axios.get('/HocPhi/GetTinhHocPhiAsync?LopHocId=' + this.selectedLopHoc + '&Month=' + this.selectedThang + '&Year=' + this.selectedNam + '&KhuyenMai=' + this.khuyenMai + '&GiaSachList=' + this.selectedSach)
                    .then(function (response) {
                        that.tongNgayHoc = response.data.soNgayHoc;
                        that.tongNgayDuocNghi = response.data.soNgayDuocNghi;
                        that.tongHocPhi = response.data.hocPhi;
                        that.hocVienList = response.data.hocVienList;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
            else {
                that.snackbar = true;
                that.messageText = "Phải chọn Lớp Học, Tháng và Năm trước khi tính tiền!!!";
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
                    HocPhi: item.hocPhiMoi
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
    }
});