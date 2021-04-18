var vue = new Vue({
    el: '#ExportThuThachIndex',
    data: {
        title: 'Export Thử Thách',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        selectedThuThach: '',
        itemThuThach: [],

    },
    async beforeCreate() {
        let that = this;
        await axios.get('/ThuThach/GetThuThachAsync')
            .then(function (response) {
                that.itemThuThach = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },

    methods: {

        forceFileDownload(response) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'thuThach_' + this.selectedThuThach.name + '.xlsx'); //or any other extension
            document.body.appendChild(link);
            link.click();
        },

        async onExport() {
            let that = this;
            if (this.selectedThuThach !== '') {
                await axios
                    ({
                        url: '/ThuThach/ExportThuThach?ThuThachId=' + that.selectedThuThach.thuThachId,
                        method: 'get',
                        responseType: 'blob' // important
                    })
                    .then(function (response) {
                        that.forceFileDownload(response);
                    })
                    .catch(function (error) {
                        console.log(error.response.data.Message);
                    });
            }
            else {
                that.snackbar = true;
                that.messageText = "Phải chọn Thử Thách trước khi export!!!";
                that.color = 'error';
                that.dialogEdit = false;
            }
        }
    }
});