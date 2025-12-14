using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Network_Project
{
    // Dữ liệu gửi qua socket giữa hai client.
    // Đánh dấu [Serializable] để có thể tuần tự hoá thành mảng byte khi truyền.
    [Serializable]
    public class SocketData
    {
        // Loại lệnh (sử dụng enum SocketCommand) để chỉ loại payload
        private int command;
        public int Command
        {
            get { return command; }
            set { command = value; }
        }

        // Toạ độ nước đi (x, y) nếu gửi nước đi
        private Point point;
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        // Thông điệp văn bản tuỳ chọn (thông báo, kết thúc ván, v.v.)
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        // Tạo mới SocketData với command, message và point
        public SocketData(int command, string message, Point point)
        {
            this.Command = command;
            this.Point = point;
            this.Message = message;
        }
    }

    // Các lệnh sử dụng trong SocketData.Command
    public enum SocketCommand
    {
        SEND_POINT, // gửi nước đi (Point)
        NOTIFY, // thông báo văn bản đơn giản
        NEW_GAME, // bắt đầu ván mới
        
        END_GAME, // kết thúc ván và kèm thông điệp
        QUIT // thoát/ ngắt kết nối
    }
}
