﻿var vue = new Vue({
    el: '#RoleIndex',
    data: {
        title: 'Quản Lý Role',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        search: '',
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Role', value: 'role', align: 'left', sortable: true },
        ],
        roleItems: [],
        dialogEditRole: false,
        dialogQuyen: false,
        dialogUser: false,
        itemToEdit: {},
        editedIndex: -1,
        itemQuyens: [],
        itemUsers: [],
        dialog: false,
        alert: false,
        newItem: "",
        rules: [
            v => /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(v) || "Chỉ được nhập email"
        ]
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/Setting/GetRolesAsync')
            .then(function (response) {
                that.roleItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        async mappingUserRoleItem(item) {
            let that = this;
            this.editedIndex = this.roleItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);

            await axios.get('/Setting/GetQuyenByRoleIdAsync?RoleId=' + item.id)
                .then(function (response) {
                    that.itemQuyens = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        },

        async mappingQuyenRoleItem(item) {
            let that = this;
            this.editedIndex = this.roleItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);

            await axios.get('/Setting/GetAccountByRoleNameAsync?RoleName=' + item.role)
                .then(function (response) {
                    that.itemUsers = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        },

        async onUpdate(id, name) {
            let that = this;
            await axios.get('/Setting/EditRoleAsync?RoleId=' + id + '&Name=' + name)
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

        async onSave() {
            if (this.newItem !== "") {
                this.dialog = false;
                let that = this;
                await axios.get('/Setting/CreateNewRoleAsync?Name=' + this.newItem)
                    .then(function (response) {
                        if (response.data.status === "OK") {
                            that.roleItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Tạo role thành công !!!';
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
                        that.messageText = 'Tạo role lỗi: ' + error.response.data.Message;
                        that.color = 'error';
                    });
            }
            else {
                this.alert = true;
            }
        },
        
        async onUpdateQuyenRole(item) {
            var that = this;
            await axios({
                method: 'put',
                url: '/Setting/AddQuyenToRoleAsync',
                data: {
                    QuyenList: that.itemQuyens,
                    RoleId: item.id
                }
            })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Cập nhật thành công !!!';
                        that.color = 'success';
                        that.dialogQuyen = false;
                    }
                    else {
                        that.snackbar = true;
                        that.messageText = response.data.message;
                        that.color = 'error';
                        that.dialogQuyen = false;
                    }
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                    that.dialogQuyen = false;
                });
        }
    }
});