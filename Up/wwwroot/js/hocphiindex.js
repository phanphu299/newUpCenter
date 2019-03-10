var vue = new Vue({
    el: '#HocPhiIndex',
    data: {
        title: 'Quản Lý Học Phí',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        alertMessage: '',
        rules: [
            v => /^[0-9]*$/.test(v) || "Chỉ được nhập số"
        ],
        mustNumber: v => !isNaN(v) || 'Chỉ được nhập số',
        dialog: false,
        alert: false,
        search: '',
        newItem: '',
        itemToDelete: {},
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Học Phí', value: 'gia', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: []
    },

    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetHocPhiAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    },
    methods: {

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        async onUpdate(item) {
            let that = this;
            await axios({
                method: 'put',
                url: '/category/UpdateHocPhiAsync',
                data: {
                    Gia: item.gia,
                    HocPhiId: item.hocPhiId
                }
            })
            .then(function (response) {
                console.log(response);
                if (response.data.status === "OK") {
                    that.snackbar = true;
                    that.messageText = 'Cập nhật thành công !!!';
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
                that.messageText = 'Cập nhật lỗi: ' + error;
                that.color = 'error';
            });
        },

        async onSave(item) {
            if (this.newItem === '') {
                this.alertMessage = "Không được bỏ trống";
                this.alert = true;
            }
            else if (isNaN(this.newItem)) {
                this.alertMessage = "Chỉ được nhập số";
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/category/CreateHocPhiAsync',
                    data: {
                        Gia: that.newItem
                    }
                })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.khoaHocItems.splice(0, 0, response.data.result);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới thành công !!!';
                        that.color = 'success';
                        that.newItem = '';
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
                    that.messageText = 'Thêm mới lỗi: ' + error;
                    that.color = 'error';
                });
            }
        },

        async onDelete(item) {
            let that = this;
            await axios({
                method: 'delete',
                url: '/category/DeleteHocPhiAsync',
                data: {
                    HocPhiId: item.hocPhiId
                }
            })
            .then(function (response) {
                console.log(response);
                if (response.data.status === "OK") {
                    that.khoaHocItems.splice(that.khoaHocItems.indexOf(item), 1);
                    that.snackbar = true;
                    that.messageText = 'Xóa thành công !!!';
                    that.color = 'success';
                    that.deleteDialog = false;
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
                that.messageText = 'Xóa lỗi: ' + error;
                that.color = 'error';
            });
        }
    }
});