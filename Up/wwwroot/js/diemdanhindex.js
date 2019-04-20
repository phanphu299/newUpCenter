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
        }
    }
});