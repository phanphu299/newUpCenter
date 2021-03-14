var vue = new Vue({
    el: '#ExportHocVienIndex',
    data: {
        title: 'Export Học Viên'
    },

    methods: {
        forceFileDownload(response, name) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', name + '.xlsx'); //or any other extension
            document.body.appendChild(link);
            link.click();
        },

        async onExport() {
            let that = this;
            await axios
                ({
                    url: '/HocVien/Export',
                    method: 'get',
                    responseType: 'blob' // important
                })
                .then(function (response) {
                    that.forceFileDownload(response, 'DanhSachHocVien');
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        }
    }
});