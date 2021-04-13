var intervalTimer;

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
        selectedThuThach: '',
        showTrigram: true,
        showIntroduce: false,
        showExam: false,
        timeLeft: '00:00',
        endTime: '0',
        cauHoiItems: [],
        result: [],
        e6: 1,
        steps: 1
    },
    async beforeCreate() {
    },

    watch: {
        steps(val) {
            if (this.e6 > val) {
                this.e6 = val
            }
        }
    },

    methods: {
        nextStep(n) {
            if (n === this.steps) {
                this.e6 = 1
            } else {
                this.e6 = n + 1
            }
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
                            that.showTrigram = false;
                            that.showIntroduce = true;
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

        async letStart() {
            var that = this;
            this.showIntroduce = false;
            let time = this.selectedThuThach.thoiGianLamBai * 60;
            this.steps = this.selectedThuThach.soCauHoi;

            await axios.get('/Challenge/GetCauHoiAsync?thuThachId=' + that.selectedThuThach.thuThachId)
                .then(function (response) {
                    that.cauHoiItems = response.data;
                    that.showExam = true;
                    that.setTime(time);
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        },

        setTime(seconds) {
            clearInterval(intervalTimer);
            this.timer(seconds);
        },
        timer(seconds) {
            const now = Date.now();
            const end = now + seconds * 1000;
            this.displayTimeLeft(seconds);

            this.selectedTime = seconds;
            this.displayEndTime(end);
            this.countdown(end);
        },
        countdown(end) {
            intervalTimer = setInterval(() => {
                const secondsLeft = Math.round((end - Date.now()) / 1000);

                if (secondsLeft === 0) {
                    this.endTime = 0;
                }

                if (secondsLeft < 0) {
                    clearInterval(intervalTimer);
                    return;
                }
                this.displayTimeLeft(secondsLeft)
            }, 1000);
        },

        displayTimeLeft(secondsLeft) {
            const minutes = Math.floor((secondsLeft % 3600) / 60);
            const seconds = secondsLeft % 60;

            this.timeLeft = `${zeroPadded(minutes)}:${zeroPadded(seconds)}`;
        },

        displayEndTime(timestamp) {
            const end = new Date(timestamp);
            const hour = end.getHours();
            const minutes = end.getMinutes();

            this.endTime = `${hourConvert(hour)}:${zeroPadded(minutes)}`
        }
    }
});

function zeroPadded(num) {
    // 4 --> 04
    return num < 10 ? `0${num}` : num;
}

function hourConvert(hour) {
    // 15 --> 3
    return (hour % 12) || 12;
}