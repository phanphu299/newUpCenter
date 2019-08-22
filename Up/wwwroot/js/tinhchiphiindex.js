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
            { text: 'Chi Phí', align: 'left', sortable: true, class: "red-header"},
            { text: 'Lương/Chi Phí', align: 'left', sortable: true },
            { text: 'Lương Giảng Dạy', align: 'left', sortable: true },
            { text: 'Lương Kèm', align: 'left', sortable: true },
            { text: 'Số Giờ Dạy', align: 'left', sortable: true },
            { text: 'Số Giờ Kèm', align: 'left', sortable: true },
            { text: 'Bonus', align: 'left', sortable: true },
            { text: 'Khoảng Trừ Khác', align: 'left', sortable: true },
            
        ]

    },

    computed: {
        total() {
            let sum = 0;
            for (let x of this.chiPhiList) {
                sum += (x.chiPhiMoi * 1.0);
            }
            return sum;
        }
    },

    methods: {
        async onTinhChiPhi(item) {
            if (!isNaN(item.soGioDay) && !isNaN(item.soGioKem) && !isNaN(item.bonus) && !isNaN(item.minus)) {
                item.chiPhiMoi = item.salary_Expense + (item.soGioKem * item.tutoringRate) + (item.soGioDay * item.teachingRate) + (item.bonus * 1) - (item.minus * 1);
            }
        },

        async onTinhTien() {
            let that = this;
            if (this.selectedNam !== '' && this.selectedThang !== '') {
                if (this.khuyenMai === "") {
                    this.khuyenMai = 0;
                }
                await axios.get('/ChiPhi/GetChiPhiAsync?month=' + this.selectedThang + '&year=' + this.selectedNam)
                    .then(function (response) {
                        that.chiPhiList = response.data.chiPhiList;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        },

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        async onLuuChiPhi() {
            let that = this;
            let chiPhiMoi = 0;
            for (let i = 0; i < this.chiPhiList.length; i++) {
                chiPhiMoi = chiPhiMoi + this.chiPhiList[i].chiPhiMoi;
                this.chiPhiList[i].year = this.selectedNam;
                this.chiPhiList[i].month = this.selectedThang;
            }

            await axios({
                method: 'post',
                url: '/ChiPhi/LuuChiPhiAsync',
                data: {
                    models: that.chiPhiList
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Lưu Chi Phí thành công !!!';
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
                    that.messageText = 'Lưu Chi Phí lỗi: ' + error;
                    that.color = 'error';
                });
        }
    }
});