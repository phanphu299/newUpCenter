var vue = new Vue({
    el: '#CauHoiIndex',
    data: {
        title: 'Quản Lý Câu Hỏi',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialogDapAn: false,
        dialog: false,
        alert: false,
        search: '',
        newItem: {
            stt: 0,
            name: '',
            thuThach: '',
            dapAns: [],
            dapAnDung: ''
        },
        itemToDelete: {},
        itemToEdit: {},
        editedIndex: -1,
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Tên Thử Thách', value: 'name', align: 'left', sortable: false },
            { text: 'Câu Hỏi Số', value: 'stt', align: 'left', sortable: false },
            { text: 'Tên Câu Hỏi', value: 'tenThuThach', align: 'left', sortable: false },
        ],
        cauHoiItems: [],
        itemThuThach: [],
        itemSoDapAn: [
            { prefix: 'a', value: 1 },
            { prefix: 'b', value: 2 },
            { prefix: 'c', value: 3 },
            { prefix: 'd', value: 4 },
            { prefix: 'e', value: 5 },
            { prefix: 'f', value: 6 },
        ],
        message: '',
        soDapAn: 3,
        expanded: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/CauHoi/GetCauHoiAsync')
            .then(function (response) {
                that.cauHoiItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });

        await axios.get('/ThuThach/GetThuThachAsync')
            .then(function (response) {
                that.itemThuThach = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        calculateSoCauHoi(item) {
            return [...Array(item).keys()].map(x => ++x);;
        },

        mappingEditItem(item) {
            this.editedIndex = this.cauHoiItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);
        },

        validateDapAns(items) {
            var found = items.find(x => x == '');
            return found === undefined;
        },

        formatDapAns(item) {
            return item.dapAns.map((x, index) => {
                return {
                    name: x,
                    isTrue: index === (item.dapAnDung - 1)
                }
            });
        },

        async onSave(item) {
            if (item.name === '' || item.thuThach === '' || item.stt === 0 || item.dapAnDung === '' || !this.validateDapAns(item.dapAns)) {
                this.alert = true;
                this.message = 'Không được bỏ trống';
            }
            else {
                var dapAnsFormatted = this.formatDapAns(item);
                let that = this;
                this.dialog = false;
                await axios({
                    method: 'post',
                    url: '/CauHoi/CreateCauHoiAsync',
                    data: {
                        Name: item.name,
                        STT: item.stt,
                        ThuThachId: item.thuThach.thuThachId,
                        DapAns: dapAnsFormatted
                    }
                })
                    .then(function (response) {
                        if (response.data.status === "OK") {
                            that.cauHoiItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItem.name = '';
                            that.newItem.thuThach = '';
                            that.newItem.dapAnDung = '';
                            that.newItem.stt = 0;
                            that.newItem.dapAns = [];
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
                url: '/CauHoi/DeleteCauHoiAsync',
                data: {
                    CauHoiId: item.cauHoiId
                }
            })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.cauHoiItems.splice(that.cauHoiItems.indexOf(item), 1);
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