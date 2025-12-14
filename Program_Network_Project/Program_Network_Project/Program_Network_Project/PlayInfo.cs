using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Network_Project
{
    // Lưu thông tin về một nước đi.
    // Dùng để xây dựng lịch sử nước đi (stack) để có thể undo hoặc xác định nước đi cuối.
    public class PlayInfo
    {
        private Point point;
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }
        private int currentPlayer;
        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }
        // Khởi tạo với toạ độ trên bàn và người chơi đã thực hiện nước đi (0 hoặc1)
        public PlayInfo(Point point, int currentPlayer)
        {
            this.Point = point;
            this.CurrentPlayer = currentPlayer;
        }
    }
}
