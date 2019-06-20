﻿var vue = new Vue({
    el: '#NhanVienKhacIndex',
    data: {
        title: 'Quản Lý Nhân Viên',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialogEdit: false,
        dialog: false,
        alert: false,
        alertEdit: false,
        search: '',
        newItem: {
            name: '',
            phone: '',
            basicSalary: 0,
            facebookAccount: '',
            diaChi: '',
            cmnd: '',
        },
        itemToDelete: {},
        itemToEdit: {},
        editedIndex: -1,
        alertMessage: '',
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Tên Nhân Viên', value: 'name', align: 'left', sortable: true },
            { text: 'Số Điện Thoại', value: 'phone', align: 'left', sortable: true },
            { text: 'Lương Cơ Bản', value: 'basicSalary', align: 'left', sortable: true },
            { text: 'Facebook', value: 'facebookAccount', align: 'left', sortable: true },
            { text: 'Địa Chỉ', value: 'diaChi', align: 'left', sortable: true },
            { text: 'CMND', value: 'cmnd', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: true },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: true }
        ],
        khoaHocItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/NhanVienKhac/GetNhanVienAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    },
    methods: {
        mappingEditItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);
        },

        async onUpdate(item) {
            if (item.name === '' || item.phone === '' || item.facebookAccount === '' || item.diaChi === '' || item.cmnd === '') {
                this.alertMessage = "Không được bỏ trống";
                this.alertEdit = true;
            }
            else if (isNaN(item.basicSalary)) {
                this.alertMessage = "Basic Salary chỉ được nhập số";
                this.alertEdit = true;
            }
            else {
                let that = this;
                await axios({
                    method: 'put',
                    url: '/nhanvienkhac/UpdateNhanVienAsync',
                    data: {
                        NhanVienKhacId: item.nhanVienKhacId,
                        Name: item.name,
                        Phone: item.phone,
                        BasicSalary: item.basicSalary,
                        FacebookAccount: item.facebookAccount,
                        DiaChi: item.diaChi,
                        CMND: item.cmnd
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            Object.assign(that.khoaHocItems[that.editedIndex], response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Cập nhật thành công !!!';
                            that.color = 'success';
                            that.dialogEdit = false;
                        }
                        else {
                            that.snackbar = true;
                            that.messageText = response.data.message;
                            that.color = 'error';
                            that.dialogEdit = false;
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                        that.snackbar = true;
                        that.messageText = 'Cập nhật lỗi: ' + error;
                        that.color = 'error';
                        that.dialogEdit = false;
                    });
            }
        },

        async onSave(item) {
            if (this.newItem.name === '' || this.newItem.phone === '' || this.newItem.facebookAccount === '' || this.newItem.diaChi === '' || this.newItem.cmnd === '') {
                this.alertMessage = "Không được bỏ trống";
                this.alert = true;
            }
            else if (isNaN(this.newItem.basicSalary)) {
                this.alertMessage = "Basic Salary chỉ được nhập số";
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/nhanvienkhac/CreateNhanVienAsync',
                    data: {
                        Name: that.newItem.name,
                        Phone: that.newItem.phone,
                        BasicSalary: that.newItem.basicSalary,
                        FacebookAccount: that.newItem.facebookAccount,
                        DiaChi: that.newItem.diaChi,
                        CMND: that.newItem.cmnd,
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
                            that.newItem.phone = '';
                            that.newItem.basicSalary = 0;
                            that.newItem.facebookAccount = '';
                            that.newItem.diaChi = '';
                            that.newItem.cmnd = '';
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
                url: '/nhanvienkhac/DeleteNhanVienAsync',
                data: {
                    NhanVienKhacId: item.nhanVienKhacId
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