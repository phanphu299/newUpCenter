var vue = new Vue({
    el: '#NoIndex',
    data: {
        title: 'Học Viên Nợ',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        selectedLopHoc: '',
        itemLopHoc: [],
        hocVienList: [],
        search: '',
        headers: [
            { text: 'Học Viên', value: 'hocVien', align: 'left', sortable: true },
            { text: 'Lớp Học', value: 'lopHoc', align: 'left', sortable: true },
            { text: 'Ngày Nợ', value: 'ngayNo', align: 'left', sortable: true },
            { text: 'Tiền Nợ', value: 'tienNo', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true }
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
    },
    methods: {
        async GetHocVienNoByLopHoc() {
            let that = this;
            await axios.get('/No/GetNoByLopHocAsync?LopHocId=' + this.selectedLopHoc)
                .then(function (response) {
                    that.hocVienList = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },
    }
});