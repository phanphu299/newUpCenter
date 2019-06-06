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
        itemThang: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0],
        itemLopHoc: [],
        tongNgayHoc: 0
        //isShowDatePicker: false,
        
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
            await axios.get('/HocPhi/GetTongNgayHocAsync?LopHocId=' + this.selectedLopHoc + '&Month=' + 5 + '&Year=' + 2019)
                .then(function (response) {
                    that.tongNgayHoc = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
    }
});