﻿var vue = new Vue({
    el: '#SachIndex',
    data: {
        title: 'Quản Lý Sách',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        alert: false,
        rules: [
            v => /^[0-9]*$/.test(v) || "Chỉ được nhập số"
        ],
        mustNumber: v => !isNaN(v) || 'Chỉ được nhập số',
        alertMessage:'',
        search: '',
        newItem: {
            name: "",
            gia: ""
        },
        itemToDelete: {},
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Tên Tài Liệu', value: 'name', align: 'left', sortable: false },
            { text: 'Giá', value: 'gia', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetSachAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
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
                url: '/category/UpdateSachAsync',
                data: {
                    Name: item.name,
                    Gia: item.gia,
                    SachId: item.sachId
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
            if (this.newItem.name === '') {
                this.alertMessage = "Không được bỏ trống";
                this.alert = true;
            }
            else if (isNaN(this.newItem.gia) || this.newItem.gia === '') {
                this.alertMessage = "Chỉ được nhập số";
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/category/CreateSachAsync',
                    data: {
                        Name: that.newItem.name,
                        Gia: that.newItem.gia
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
                        that.newItem.gia = '';
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
                url: '/category/DeleteSachAsync',
                data: {
                    SachId: item.sachId
                }
            })
            .then(function (response) {
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