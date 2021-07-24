var vue = new Vue({
    el: '#KetQuaIndex',
    data: {
        title: 'Kiểm Tra Kết Quả',
        search: '',
        headers: [
            { text: 'STT', value: 'stt', align: 'left', sortable: false },
            { text: 'Tên Học Viên', value: 'tenHocVien', align: 'left', sortable: false },
            { text: 'Trigram', value: 'trigram', align: 'left', sortable: false },
            { text: 'Thử Thách Đã Đạt', value: 'thoiGianLamBai', align: 'left', sortable: false }
        ],
        lopHocItems: [],
        selectedLopHoc: '',
        hocVienItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/LopHoc/GetAvailableLopHocAsync')
            .then(function (response) {
                that.lopHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        async onGetKetQua() {
            let that = this;
            await axios.get('/ThuThach/GetKetQuaAsync?LopHocId=' + this.selectedLopHoc.lopHocId)
                .then(function (response) {
                    that.hocVienItems = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        }
    }
});