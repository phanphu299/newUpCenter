var vue = new Vue({
    el: '#AccountIndex',
    data: {
        title: 'Quản Lý Tài Khoản',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        searchAdmin: '',
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
        adminItems: [],
        userItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/Setting/GetAccountAsync')
            .then(function (response) {
                that.adminItems = response.data.administrators;
                that.userItems = response.data.everyone;
            })
            .catch(function (error) {
                console.log(error);
            });
    },
    methods: {
        async ResetMatKhau(id) {
            let that = this;
            await axios.get('/Setting/ResetMatKhauAsync?Id=' + id)
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Reset thành công !!!';
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
                    that.messageText = 'Reset lỗi: ' + error;
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
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Kích hoạt tài khoản lỗi: ' + error;
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
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Khóa tài khoản lỗi: ' + error;
                    that.color = 'error';
                });
        }
    }
});