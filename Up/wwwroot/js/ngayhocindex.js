﻿var vue = new Vue({
    el: '#NgayHocIndex',
    data: {
        title: 'Quản Lý Ngày Học',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
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
            { text: 'Ngày Học', value: 'name', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetNgayHocAsync')
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
            await axios({
                method: 'put',
                url: '/category/UpdateNgayHocAsync',
                data: {
                    Name: item.name,
                    NgayHocId: item.ngayHocId
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
        },

        async onSave(item) {
            if (this.newItem === '') {
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/category/CreateNgayHocAsync',
                    data: {
                        Name: that.newItem
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
                url: '/category/DeleteNgayHocAsync',
                data: {
                    NgayHocId: item.ngayHocId
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