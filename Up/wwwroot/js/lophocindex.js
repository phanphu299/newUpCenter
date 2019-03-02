var vue = new Vue({
    el: '#LopHocIndex',
    data: {
        title: 'Quản Lý Lớp Học',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        dialogEdit: false,
        alert: false,
        alertEdit: false,
        isShowDatePicker: false,
        isShowDatePicker2: false,
        isShowDatePicker3: false,
        search: '',
        newItem: {
            name: "",
            khoaHoc: "",
            ngayHoc: "",
            gioHoc: "",
            ngayKhaiGiang: new Date().toISOString().substr(0, 10)
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
            { text: 'Tên Lớp Học', value: 'name', align: 'left', sortable: false },
            { text: 'Khóa Học', value: 'khoaHoc', align: 'left', sortable: false },
            { text: 'Ngày Học', value: 'ngayHoc', align: 'left', sortable: false },
            { text: 'Giờ Học', value: 'gioHoc', align: 'left', sortable: false },
            { text: 'Hủy Lớp', value: 'isCanceled', align: 'left', sortable: false },
            { text: 'Tốt Nghiệp', value: 'isGraduated', align: 'left', sortable: false },
            { text: 'Ngày Khai Giảng', value: 'ngayKhaiGiang', align: 'left', sortable: false },
            { text: 'Ngày Kết Thúc', value: 'ngayKetThuc', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: [],
        itemKhoaHoc: [],
        itemGioHoc: [],
        itemNgayHoc: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/LopHoc/GetLopHocAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetKhoaHocAsync')
            .then(function (response) {
                that.itemKhoaHoc = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetNgayHocAsync')
            .then(function (response) {
                that.itemNgayHoc = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetGioHocAsync')
            .then(function (response) {
                that.itemGioHoc = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    },

    methods: {
        async onUpdate(item) {
            let that = this;
            if (item.name === '' || item.khoaHocId === '' || item.ngayHocId === '' || item.gioHocId === '') {
                this.alertEdit = true;
            }
            else {
                await axios({
                    method: 'put',
                    url: '/LopHoc/UpdateLopHocAsync',
                    data: {
                        Name: item.name,
                        LopHocId: item.lopHocId,
                        KhoaHocId: item.khoaHocId,
                        GioHocId: item.gioHocId,
                        NgayHocId: item.ngayHocId,
                        NgayKhaiGiang: item.ngayKhaiGiang,
                        NgayKetThuc: item.ngayKetThuc,
                        IsCanceled: item.isCanceled,
                        IsGraduated: item.isGraduated
                    }
                })
                    .then(function (response) {
                        Object.assign(that.khoaHocItems[that.editedIndex], response.data);
                        that.snackbar = true;
                        that.messageText = 'Cập nhật thành công !!!';
                        that.color = 'success';
                        that.dialogEdit = false;
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

        mappingEditItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);

            let [dayKG, monthKG, yearKG] = this.itemToEdit.ngayKhaiGiang.split('/');
            this.itemToEdit.ngayKhaiGiang = yearKG + '-' + monthKG + '-' + dayKG;


            if (this.itemToEdit.ngayKetThuc !== "") {
                let [dayKT, monthKT, yearKT] = this.itemToEdit.ngayKetThuc.split('/');
                this.itemToEdit.ngayKetThuc = yearKT + '-' + monthKT + '-' + dayKT;
            }
        },

        async onSave(item) {
            if (this.newItem.name === '' || this.newItem.khoaHoc === '' || this.newItem.ngayHoc === '' || this.newItem.gioHoc === '') {
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/LopHoc/CreateLopHocAsync',
                    data: {
                        Name: that.newItem.name,
                        KhoaHocId: that.newItem.khoaHoc,
                        GioHocId: that.newItem.gioHoc,
                        NgayHocId: that.newItem.ngayHoc,
                        NgayKhaiGiang: that.newItem.ngayKhaiGiang
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        that.khoaHocItems.splice(0, 0, response.data);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới thành công !!!';
                        that.color = 'success';
                        that.newItem.name = '';
                        that.newItem.khoaHoc = '';
                        that.newItem.gioHoc = '';
                        that.newItem.ngayHoc = '';
                        that.newItem.ngayKhaiGiang = new Date().toISOString().substr(0, 10);
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
                url: '/LopHoc/DeleteLopHocAsync',
                data: {
                    LopHocId: item.lopHocId
                }
            })
                .then(function (response) {
                    console.log(response);
                    that.khoaHocItems.splice(that.khoaHocItems.indexOf(item), 1);
                    that.snackbar = true;
                    that.messageText = 'Xóa thành công !!!';
                    that.color = 'success';
                    that.deleteDialog = false;
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