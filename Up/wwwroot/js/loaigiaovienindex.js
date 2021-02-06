var vue = new Vue({
    el: '#LoaiGiaoVienIndex',
    data: {
        title: 'Quản Lý Loại Nhân Viên',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        alert: false,
        search: '',
        newItem: {
            name: '',
            order: 0
        },
        itemToDelete: {},
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Tên Loại Nhân Viên', value: 'name', align: 'left', sortable: false },
            { text: 'Thứ Tự', value: 'order', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetLoaiGiaoVienAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        async onUpdate(item) {
            let that = this;
            if (isNaN(this.newItem.order)) {
                this.snackbar = true;
                this.messageText = 'Thứ tự phải là số!!!';
                this.color = 'error';
            }
            else {
                await axios({
                    method: 'put',
                    url: '/category/UpdateLoaiGiaoVienAsync',
                    data: {
                        Name: item.name,
                        LoaiGiaoVienId: item.loaiGiaoVienId,
                        Order: item.order
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
                        console.log(error.response.data.Message);
                        that.snackbar = true;
                        that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                        that.color = 'error';
                    });
            }
        },

        async onSave(item) {
            if (this.newItem.name === '' || this.newItem.order === 0) {
                this.alert = true;
            }
            else if (isNaN(this.newItem.order)) {
                this.snackbar = true;
                this.messageText = 'Thứ tự phải là số!!!';
                this.color = 'error';
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/category/CreateLoaiGiaoVienAsync',
                    data: {
                        Name: that.newItem.name,
                        Order: that.newItem.order
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.khoaHocItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItem.name = '';
                            that.newItem.order = 0;
                        }
                        else {
                            that.snackbar = true;
                            that.messageText = response.data.message;
                            that.color = 'error';
                        }
                    })
                    .catch(function (error) {
                        console.log(error.response.data.Message);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới lỗi: ' + error.response.data.Message;
                        that.color = 'error';
                    });
            }
        },

        async onDelete(item) {
            let that = this;
            await axios({
                method: 'delete',
                url: '/category/DeleteLoaiGiaoVienAsync',
                data: {
                    LoaiGiaoVienId: item.loaiGiaoVienId
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
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Xóa lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        }
    }
});