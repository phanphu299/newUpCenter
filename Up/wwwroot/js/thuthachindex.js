var vue = new Vue({
    el: '#ThuThachIndex',
    data: {
        title: 'Quản Lý Thử Thách',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialogEdit: false,
        dialog: false,
        alert: false,
        search: '',
        newItem: {
            name: '',
            soCauHoi: 0,
            thoiGianLamBai: 0,
            minGrade: 0,
            khoaHoc: ''
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
            { text: 'Tên Khóa Học', value: 'tenKhoaHoc', align: 'left', sortable: false },
            { text: 'Số Câu Hỏi', value: 'soCauHoi', align: 'left', sortable: false },
            { text: 'Thời Gian Làm Bài (phút)', value: 'thoiGianLamBai', align: 'left', sortable: false },
            { text: 'Số Điểm Cần Đạt', value: 'minGrade', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        thuThachItems: [],
        itemKhoaHoc: [],
        message: ''
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/ThuThach/GetThuThachAsync')
            .then(function (response) {
                that.thuThachItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });

        await axios.get('/category/GetKhoaHocAsync')
            .then(function (response) {
                that.itemKhoaHoc = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        async onUpdate(item) {
            let that = this;
            if (item.name === '' || item.khoaHoc == '') {
                this.alert = true;
                this.message = 'Không được bỏ trống';
            }
            else if (item.soCauHoi <= 0 || item.thoiGianLamBai <= 0 || item.minGrade <= 0) {
                this.alert = true;
                this.message = 'Số câu hỏi, Thời gian làm bài, Số điểm cần đạt phải lớn hơn 0';
            }
            else if (isNaN(item.soCauHoi) || isNaN(item.thoiGianLamBai) || isNaN(item.minGrade)) {
                this.message = "Số câu hỏi, Thời gian làm bài, Số điểm cần đạt chỉ được nhập số";
                this.alert = true;
            }
            else {
                await axios({
                    method: 'put',
                    url: '/ThuThach/UpdateThuThachAsync',
                    data: {
                        ThuThachId: item.thuThachId,
                        Name: item.name,
                        KhoaHocId: item.khoaHocId,
                        SoCauHoi: item.soCauHoi,
                        ThoiGianLamBai: item.thoiGianLamBai,
                        MinGrade: item.minGrade
                    }
                })
                    .then(function (response) {
                        if (response.data.status === "OK") {
                            Object.assign(that.thuThachItems[that.editedIndex], response.data.result);
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
                        console.log(error.response.data.Message);
                        that.snackbar = true;
                        that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                        that.color = 'error';
                        that.dialogEdit = false;
                    });
            }
        },

        mappingEditItem(item) {
            this.editedIndex = this.thuThachItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);
        },

        async onSave(item) {
            if (item.name === '' || item.khoaHoc == '') {
                this.alert = true;
                this.message = 'Không được bỏ trống';
            }
            else if (item.soCauHoi <= 0 || item.thoiGianLamBai <= 0 || item.minGrade <= 0 ) {
                this.alert = true;
                this.message = 'Số câu hỏi, Thời gian làm bài, Số điểm cần đạt phải lớn hơn 0';
            }
            else if (isNaN(item.soCauHoi) || isNaN(item.thoiGianLamBai) || isNaN(item.minGrade)) {
                this.message = "Số câu hỏi, Thời gian làm bài, Số điểm cần đạt chỉ được nhập số";
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/ThuThach/CreateThuThachAsync',
                    data: {
                        Name: item.name,
                        KhoaHocId: item.khoaHoc,
                        SoCauHoi: item.soCauHoi,
                        ThoiGianLamBai: item.thoiGianLamBai,
                        MinGrade: item.minGrade
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.thuThachItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItem.name = '';
                            that.newItem.khoaHoc = '';
                            that.newItem.soCauHoi = 0;
                            that.newItem.thoiGianLamBai = 0;
                            that.newItem.minGrade = 0;
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
                url: '/ThuThach/DeleteThuThachAsync',
                data: {
                    ThuThachId: item.thuThachId
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.thuThachItems.splice(that.thuThachItems.indexOf(item), 1);
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