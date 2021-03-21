using System.Text;

namespace Up
{
    public static class TemplateGenerator
    {
        public static string GetHocPhiTronGoiTemplate()
        {
            var sb = new StringBuilder();
            sb.Append(@"
            <html>
              <head>
              </head>
              <body style='background-image: url(https://upenglishvietnam.com/img/transparent-logo.png);
    background-size: 500px;
    background-position: center center;
    background-repeat: no-repeat;'>
                <div class='container'>
                  <div class='row mt-2' style='display: inline-flex;'>
                    <div>
                      <h6>CÔNG TY TNHH MTV GIÁO DỤC VÀ ĐÀO TẠO QUÂN NGUYỄN</h6>
                      <p style='font-style: italic; font-size: 14px'>
                        82, Ngô Chí Quốc, tổ 85, khu phố 13, phường Phú Cường, TP.TDM, Bình
                        Dương.
                      </p>
                    </div>
                    <div>
                      <h5>Số: {0}</h5>
                    </div>
                  </div>

                  <div class='row justify-content-center mt-3' style='text-align: center;'>
                    <h1>BIÊN LAI THU TIỀN</h1>
                  </div>

                  <div class='row justify-content-center' style='text-align: center;'>
                    <p style='font-style: italic'>Ngày {1} tháng {2} năm {3}</p>
                  </div>

                  <div class='row mt-3'>
                    <p>Họ và tên người nộp tiền: {4}</p>
                  </div>
                  <div class='row'>
                    <p>Ngày sinh: {5}</p>
                  </div>
                  <div class='row'>
                    <p>Địa chỉ: {6}</p>
                  </div>
                  <div class='row'>
                    <p>Số tiền thu: <b>{7}</b></p>
                  </div>
                  <div class='row'>
                    <p>Nội dung thu: Học phí trọn gói từ {8} đến {9}</p>
                  </div>

                  <div style='text-align: center; float: right; margin-right: 30px'>
                    <b>Người thu tiền</b>
                    <br/>
                    (Ký và ghi rõ họ tên)
                  </div>

                  <p style='font-style: italic; margin-top: 9rem'>
                      * Người nộp tiền vui lòng kiểm tra kỹ thông tin trước khi duỵệt mẫu,
                      giữ lại biên lai cẩn thận và xuất trình khi công ty yêu cầu
                    </p>
                </div>
              </body>
            </html>
            ");
            return sb.ToString();
        }

        public static string GetHocPhiTheoThangTemplate()
        {
            var sb = new StringBuilder();
            sb.Append(@"
            <html>
              <head>
              </head>
              <body style='background-image: url(https://upenglishvietnam.com/img/transparent-logo.png);
    background-size: 500px;
    background-position: center center;
    background-repeat: no-repeat;'>
                <div class='container'>
                  <div class='row mt-2' style='display: inline-flex;'>
                    <div>
                      <h6>CÔNG TY TNHH MTV GIÁO DỤC VÀ ĐÀO TẠO QUÂN NGUYỄN</h6>
                      <p style='font-style: italic; font-size: 14px'>
                        82, Ngô Chí Quốc, tổ 85, khu phố 13, phường Phú Cường, TP.TDM, Bình
                        Dương.
                      </p>
                    </div>
                    <div>
                      <h5>Số: {0}</h5>
                    </div>
                  </div>

                  <div class='row justify-content-center mt-3' style='text-align: center;'>
                    <h1>BIÊN LAI THU TIỀN</h1>
                  </div>

                  <div class='row justify-content-center' style='text-align: center;'>
                    <p style='font-style: italic'>Ngày {1} tháng {2} năm {3}</p>
                  </div>

                  <div class='row mt-3'>
                    <p>Họ và tên người nộp tiền: {4}</p>
                  </div>
                  <div class='row'>
                    <p>Ngày sinh: {5}</p>
                  </div>
                  <div class='row'>
                    <p>Địa chỉ: {6}</p>
                  </div>
                  <div class='row'>
                    <p>Số tiền thu: <b>{7}</b></p>
                  </div>
                  <div class='row'>
                    <p>Nội dung thu: Nộp học phí lớp tháng {8}</p>
                  </div>

                  <div style='text-align: center; float: right; margin-right: 30px'>
                    <b>Người thu tiền</b>
                    <br/>
                    (Ký và ghi rõ họ tên)
                  </div>

                  <p style='font-style: italic; margin-top: 9rem'>
                      * Người nộp tiền vui lòng kiểm tra kỹ thông tin trước khi duỵệt mẫu,
                      giữ lại biên lai cẩn thận và xuất trình khi công ty yêu cầu
                    </p>
                </div>
              </body>
            </html>
            ");
            return sb.ToString();
        }
    }
}
