var vue = new Vue({
    el: '#DiemDanhIndex',
    data: {
        title: 'Điểm Danh Học Viên',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        selectedLopHoc: '',
        ngayDiemDanh: new Date().toISOString().substr(0, 10),
        itemLopHoc: [],
        isShowDatePicker: false,
        hocVienList: []
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
    },
    methods: {
        async GetHocVienByLopHoc() {
            let that = this;
            await axios.get('/DiemDanh/GetHocVienByLopHocAsync?LopHocId=' + this.selectedLopHoc)
                .then(function (response) {
                    that.hocVienList = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        async DiemDanhCaLop() {
            let that = this;
            await axios({
                method: 'post',
                url: '/DiemDanh/DiemDanhTatCaAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    NgayDiemDanh: this.ngayDiemDanh,
                    IsOff: false
                }
            })
            .then(function (response) {
                console.log(response);
                if (response.data.status === "OK") {
                    that.snackbar = true;
                    that.messageText = 'Điểm danh thành công !!!';
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
                that.messageText = 'Điểm danh lỗi: ' + error;
                that.color = 'error';
            });
        }
    }
});