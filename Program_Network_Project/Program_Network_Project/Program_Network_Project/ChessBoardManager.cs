using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program_Network_Project
{
    public class ChessBoardManager
    {
        #region Properties
        // Panel chứa các control Button đại diện cho các ô trên bàn cờ
        private Panel chessBoard;
        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }

        // Hai người chơi (player0 và player1). Mỗi người có tên và ảnh đánh dấu.
        private List<Player> player;
        public List<Player> Player
        {
            get { return player; }
            set { player = value; }
        }

        // Chỉ số người chơi hiện tại (0 hoặc1)
        private int currentPlayer;
        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        // Tham chiếu tới TextBox hiển thị tên người chơi hiện tại
        private TextBox playerName;
        public TextBox PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        // Tham chiếu tới PictureBox hiển thị hình đánh dấu của người chơi hiện tại
        private PictureBox playerMark;
        public PictureBox PlayerMark
        {
            get { return playerMark; }
            set { playerMark = value; }
        }

        // Ma trận2D chứa các Button tạo bàn cờ
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        // Sự kiện: khi người chơi đánh và khi kết thúc ván
        private event EventHandler<ButtonClickEvent> playerMarked;
        public event EventHandler<ButtonClickEvent> PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }
        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }

        // Stack lưu lịch sử nước đi để có thể undo hoặc biết nước đi cuối
        private Stack<PlayInfo> playTimeLine;
        public Stack<PlayInfo> PlayTimeLine
        {
            get { return playTimeLine; }
            set { playTimeLine = value; }
        }
        #endregion

        #region Initialize
        // Constructor: gán tham chiếu UI và khởi tạo hai người chơi mặc định
        public ChessBoardManager(Panel chessBoard, TextBox playerName, PictureBox mark)
        {
            this.ChessBoard = chessBoard;
            this.PlayerName = playerName;
            this.PlayerMark = mark;
            this.Player = new List<Player>()
             {
             new Player("Tấn_Kiên", Image.FromFile(Application.StartupPath + "\\Resources\\logoO.png")),
             new Player("Trùm_cúi:)", Image.FromFile(Application.StartupPath + "\\Resources\\logox.png"))
             };
           
        }

        #endregion

        #region Methods
        // Vẽ bàn cờ bằng cách tạo một lưới Button trong Panel cung cấp.
        // Mỗi Button đại diện cho một ô trên bàn cờ.
        public void DrawChessBoard()
        {
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();
            PlayTimeLine = new Stack<PlayInfo>();
            CurrentPlayer =0; // player0 đánh trước
            ChangePlayer(); // cập nhật tên và ảnh hiển thị
            Matrix = new List<List<Button>>();
            Button oldButton = new Button() { Width =0, Location = new Point(0,0) };
            for (int i =0;i<Cons.CHESS_BOARD_HEIGHT;i++)
            {
                Matrix.Add(new List<Button>());
                for (int j =0;j<Cons.CHESS_BOARD_WIDTH;j++)
                {
                    // Tạo Button cho ô
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString() // lưu chỉ số hàng vào Tag để dễ truy xuất
                    };
                    btn.Click += btn_Click;
                    ChessBoard.Controls.Add(btn);
                    Matrix[i].Add(btn);
                    oldButton = btn;

                }
                // Chuyển xuống dòng tiếp theo
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                oldButton.Width =0; oldButton.Height =0;
            }
            
        }

        // Xử lý khi người dùng click vào ô (đánh nước đi)
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null) // nếu đã đánh rồi
                return;
            Mark(btn); // đặt ảnh đánh
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer));

            // đổi lượt người chơi
            CurrentPlayer = CurrentPlayer ==1 ?0 :1;

            // thông báo cho Form1 để gửi nước đi qua mạng
            if (playerMarked != null)
                playerMarked(this, new ButtonClickEvent(GetChessPoint(btn)));
            if (isEndGame(btn))
            {
                EndGame();
            }
        }

        // Đặt nước đi của đối thủ (nhận từ mạng)
        public void OtherPlayerMark(Point point)
        {
            Button btn = Matrix[point.Y][point.X];
            if (btn.BackgroundImage != null)
                return;
            Mark(btn);
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer));

            CurrentPlayer = CurrentPlayer ==1 ?0:1;
            ChangePlayer(); // cập nhật tên và ảnh hiển thị
            if (isEndGame(btn))
            {
                EndGame();
            }
        }

        // Kích hoạt sự kiện kết thúc ván
        public void EndGame()
        {
            if (endedGame != null)
                endedGame(this, new EventArgs());
        }

        // Kiểm tra điều kiện thắng (ngang/dọc/chéo)
        private bool isEndGame(Button btn)
        {
            return isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimary(btn) || isEndSub(btn);
        }

        // Chuyển Button thành toạ độ bàn cờ
        private Point GetChessPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);
            Point point = new Point(horizontal, vertical);
            return point;
        }

        private bool isEndHorizontal(Button btn)
        {
            Point point = GetChessPoint(btn);
            int countLeft =0;
            for (int i = point.X; i >=0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else break;
            }
            int countRight =0;
            for (int i = point.X +1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else break;
            }
            return countLeft + countRight ==5; //5 ô liên tiếp là chiến thắng
        }

        private bool isEndVertical(Button btn)
        {
            Point point = GetChessPoint(btn);
            int countTop =0;
            for (int i = point.Y;i>=0;i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else break;
            }
            int countBottom =0;
            for (int i = point.Y +1;i<Cons.CHESS_BOARD_HEIGHT;i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else break;
            }
            return countTop + countBottom ==5;
        }

        private bool isEndPrimary(Button btn)
        {
            // chéo chính (từ trên trái xuống dưới phải)
            Point point = GetChessPoint(btn);
            int countTop =0;
            for (int i =0;i<=point.X;i++)
            {
                if (point.X - i <0 || point.Y - i <0) break;
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else break;
            }
            int countBottom =0;
            for (int i =1;i<=Cons.CHESS_BOARD_WIDTH - point.X;i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i >= Cons.CHESS_BOARD_WIDTH) break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else break;
            }
            return countTop + countBottom ==5;
        }

        private bool isEndSub(Button btn)
        {
            // chéo phụ (từ trên phải xuống dưới trái)
            Point point = GetChessPoint(btn);
            int countTop =0;
            for (int i =0;i<=point.X;i++)
            {
                if (point.X + i > Cons.CHESS_BOARD_WIDTH || point.Y - i <0) break;
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else break;
            }
            int countBottom =0;
            for (int i =1;i<=Cons.CHESS_BOARD_WIDTH - point.X;i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X - i <0) break;
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else break;
            }
            return countTop + countBottom ==5;
        }

        // Đặt ảnh đánh của người chơi hiện tại lên ô
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
        }

        // Cập nhật UI để hiển thị tên và ảnh của người chơi hiện tại
        private void ChangePlayer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;
            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }
        #endregion
    }
    public class ButtonClickEvent : EventArgs
    {
        private Point clickedPoint;
        public Point ClickedPoint
        {
            get { return clickedPoint; }
            set { clickedPoint = value; }
        }
        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
}
