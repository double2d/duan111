using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Network_Project
{
    // Lớp lưu thông tin người chơi: tên và ảnh đánh dấu (X hoặc O)
    public class Player
    {
        private string name;
        // Tên người chơi hiển thị trên UI
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private Image mark;
        private string v;
        private Image image;

        //Ảnh đánh dấu (dùng làm BackgroundImage cho ô trên bàn cờ)
        public Image Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        // Khởi tạo Player với tên và ảnh đánh dấu
        public Player(string name, Image mark)
        {
            this.Name = name;
            this.Mark = mark;
        }

    }
}
