var vue = new Vue({
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
        itemToEdit: {},
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
        await axios.get('/Setting/GetRolesAsync')
            .then(function (response) {
                that.roleItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    },
    methods: {
        mappingEditRoleItem(item) {
            this.editedIndex = this.userItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);
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
                        console.log(error);
                        that.snackbar = true;
                        that.messageText = 'Tạo role lỗi: ' + error;
                        that.color = 'error';
                    });
            }
            else {
                this.alert = true;
            }
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
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error;
                    that.color = 'error';
                    that.dialogEditRole = false;
                });
        }
    }
});