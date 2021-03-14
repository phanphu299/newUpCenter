var vue = new Vue({
    el: '#AccountIndex',
    data: {
        title: 'Quản Lý Tài Khoản',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        searchUser: '',
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Email', value: 'email', align: 'left', sortable: true },
            { text: 'Roles', value: 'roles', align: 'left', sortable: true }
        ],
        userItems: [],
        dialogEditRole: false,
        itemToEditRole: {},
        editedIndex: -1,
        itemRoles: [],
        dialog: false,
        alert: false,
        newItem: "",
        rules: [
            v => /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(v) || "Chỉ được nhập email"
        ]
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/Setting/GetAccountAsync')
            .then(function (response) {
                that.userItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
        
        await axios.get('/Setting/GetRolesAsync')
            .then(function (response) {
                that.itemRoles = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        mappingEditRoleItem(item) {
            this.editedIndex = this.userItems.indexOf(item);
            this.itemToEditRole = Object.assign({}, item);
        },

        async onSave() {
            this.dialog = false;
            if (this.newItem !== "") {
                let that = this;
                await axios.get('/Setting/CreateNewUserAsync?Email=' + this.newItem)
                    .then(function (response) {
                        if (response.data.status === "OK") {
                            that.userItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Tạo tài khoản thành công !!!';
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
                        that.messageText = 'Tạo tài khoản lỗi: ' + error.response.data.Message;
                        that.color = 'error';
                    });
            }
            else {
                this.alert = true;
            }
        },

        async ResetMatKhau(id) {
            let that = this;
            await axios.get('/Setting/ResetMatKhauAsync?Id=' + id)
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Reset thành công, Mật khẩu : M@tkhau@123 !!!';
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
                    that.messageText = 'Reset lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async KichHoat(id) {
            let that = this;
            await axios.get('/Setting/KichHoatTaiKhoanAsync?Id=' + id)
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Kích hoạt tài khoản thành công !!!';
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
                    that.messageText = 'Kích hoạt tài khoản lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async Khoa(id) {
            let that = this;
            await axios.get('/Setting/KhoaTaiKhoanAsync?Id=' + id)
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Khóa tài khoản thành công !!!';
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
                    that.messageText = 'Khóa tài khoản lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async Xoa(item) {
            let that = this;
            await axios.get('/Setting/RemoveUserAsync?Id=' + item.id)
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.userItems.splice(that.userItems.indexOf(item), 1);
                        that.snackbar = true;
                        that.messageText = 'Xóa tài khoản thành công !!!';
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
                    that.messageText = 'Xóa tài khoản lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onUpdateRole(item) {
            var that = this;
            await axios({
                method: 'put',
                url: '/Setting/UpdateRoleAsync',
                data: {
                    Id: item.id,
                    RoleIds: item.roleIds
                }
            })
            .then(function (response) {
                if (response.data.status === "OK") {
                    Object.assign(that.userItems[that.editedIndex], response.data.result);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật thành công !!!';
                    that.color = 'success';
                    that.dialogEditRole = false;
                }
                else {
                    that.snackbar = true;
                    that.messageText = response.data.message;
                    that.color = 'error';
                    that.dialogEditRole = false;
                }
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
                that.snackbar = true;
                that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                that.color = 'error';
                that.dialogEditRole = false;
            });
        }
    }
});