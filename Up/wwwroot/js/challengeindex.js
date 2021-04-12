var vue = new Vue({
    el: '#ChallengeIndex',
    data: {
        title: 'Wellcome to UP Challenges',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        alert: false,
        message: '',
        trigram: '',
        hocVien: '',
        thuThachItems: [],
        selectedThuThach: ''
    },
    async beforeCreate() {
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

        async mappingEditCauHoiItem(item) {
            this.editedIndex = this.thuThachItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);

            var that = this;
            await axios.get('/CauHoi/GetCauHoiByThuThachAsync?thuThachId=' + item.thuThachId + '&stt=1')
                .then(function (response) {
                    that.cauHoiItems = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        },

        async onSubmitTrigram(item) {
            if (item === '') {
                that.snackbar = true;
                that.messageText = 'ID is required';
                that.color = 'error';
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/Challenge/GetHocVienAsync?trigram=' + item,
                    data: {
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.hocVien = response.data.result;
                            axios.get('/Challenge/GetThuThachHocVienAsync?hocVienId=' + that.hocVien.hocVienId)
                                .then(function (response) {
                                    that.thuThachItems = response.data;
                                })
                                .catch(function (error) {
                                    console.log(error.response.data.Message);
                                });
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
                        that.messageText = 'Lỗi: ' + error.response.data.Message;
                        that.color = 'error';
                    });
            }
        },

        async changeCauHoiSo(item) {
            var that = this;
            await axios.get('/CauHoi/GetCauHoiByThuThachAsync?thuThachId=' + item.thuThachId + '&stt=' + this.cauHoiSo)
                .then(function (response) {
                    that.cauHoiItems = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
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
        },

        async onDeleteCauHoi(item) {
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
                        that.deleteDialogCauHoi = false;
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