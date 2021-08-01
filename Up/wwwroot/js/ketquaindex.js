var vue = new Vue({
    el: '#KetQuaIndex',
    data: {
        title: 'Kiểm Tra Kết Quả',
        search: '',
        headers: [
            { text: 'STT', value: 'stt', align: 'left', sortable: false },
            { text: 'Tên Học Viên', value: 'fullName', align: 'left', sortable: false },
            { text: 'Trigram', value: 'trigram', align: 'left', sortable: false },
            { text: 'Thử Thách Đã Đạt', align: 'left', sortable: false }
        ],
        lopHocItems: [],
        itemThang: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0, parseInt(new Date().toISOString().substr(0, 4)) + 1],
        selectedLopHoc: '',
        selectedThang: '',
        selectedNam: '',
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
            if (this.selectedLopHoc !== '' && this.selectedNam !== '' && this.selectedThang !== '') {
                await axios.get('/ThuThach/GetKetQuaAsync?LopHocId=' + this.selectedLopHoc.lopHocId + '&Month=' + this.selectedThang + '&Year=' + this.selectedNam)
                    .then(function (response) {
                        that.hocVienItems = response.data;
                    })
                    .catch(function (error) {
                        console.log(error.response.data.Message);
                    });
            }
        }
    }
});