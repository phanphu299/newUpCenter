var vue = new Vue({
    el: '#ExportDiemDanhIndex',
    data: {
        title: 'Export Điểm Danh',
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
        itemKhuyenMai: ['5', '10', '15', '20', '25', '30', '35', '40', '45', '50'],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0],
        itemLopHoc: [],

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
        
        forceFileDownload(response) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'diemdanh_T' + this.selectedThang + '-' + this.selectedNam + '.xlsx'); //or any other extension
            document.body.appendChild(link);
            link.click();
        },

        async onExport() {
            let that = this;
            if (this.selectedLopHoc !== '' && this.selectedNam !== '' && this.selectedThang !== '') {
                await axios
                    ({
                        url: '/DiemDanh/ExportDiemDanh?LopHocId=' + that.selectedLopHoc + '&month=' + that.selectedThang + '&year=' + that.selectedNam,
                        method: 'get',
                        responseType: 'blob' // important
                    })
                    .then(function (response) {
                        that.forceFileDownload(response);
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
            else {
                that.snackbar = true;
                that.messageText = "Phải chọn Lớp Học, Tháng và Năm trước khi export!!!";
                that.color = 'error';
                that.dialogEdit = false;
            }
        },

    }
});