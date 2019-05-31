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
        
    }
});