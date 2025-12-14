using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program_Network_Project
{
    // Main form that coordinates UI, socket communication and game logic
    public partial class Form1 : Form
    {
        #region Properties
        ChessBoardManager ChessBoard;
        SocketManager socket;
        #endregion

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            // Initialize the chess board manager with references to UI elements
            ChessBoard = new ChessBoardManager(plnChessBoard, txbPlayerName, pctbMark);

            // Subscribe events to react when player marks or game ends
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;

            // Configure progress bar and timer
            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;
            tmCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;

            // Socket manager handles network communication (client/server)
            socket = new SocketManager();

            // Wire up player name editing handlers (UI convenience)
            txbPlayerName.Leave += TxbPlayerName_Leave;
            txbPlayerName.KeyDown += TxbPlayerName_KeyDown;

            NewGame();
        }

        // Update local player name when textbox loses focus or on Enter
        private void TxbPlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Commit the name change when pressing Enter
                TxbPlayerName_Leave(sender, EventArgs.Empty);
                // prevent ding sound
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxbPlayerName_Leave(object sender, EventArgs e)
        {
            if (ChessBoard != null && ChessBoard.Player != null && ChessBoard.Player.Count > 0)
            {
                // Update local player (player0) name from textbox
                ChessBoard.Player[0].Name = txbPlayerName.Text;
                // If it's currently player0's turn, refresh displayed name
                if (ChessBoard.CurrentPlayer == 0)
                {
                    ChessBoard.PlayerName.Text = ChessBoard.Player[0].Name;
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        // EndGame stops timers and disables board UI
        void EndGame()
        {
            tmCoolDown.Stop();
            plnChessBoard.Enabled = false;
        }
        // Reset the game board and timer
        void NewGame()
        {
            prcbCoolDown.Value = 0;
            tmCoolDown.Stop();

            ChessBoard.DrawChessBoard();
        }
        // Quit application
        void Quit()
        {
            Application.Exit();
        }
        // Called when local player marks a cell: send move to opponent
        void ChessBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start();
            plnChessBoard.Enabled = false;
            prcbCoolDown.Value = 0;
            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));
            Listen();

        }
        // Called when the chess board detects the end of the game
        void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            // Determine who made the last move (the winner)
            if (ChessBoard.PlayTimeLine == null || ChessBoard.PlayTimeLine.Count == 0)
            {
                EndGame();
                return;
            }

            PlayInfo last = ChessBoard.PlayTimeLine.Peek();
            int winnerIndex = last.CurrentPlayer; // index of player who played last (0 or1)

            // Determine local player index: server is player0, client is player1
            int localIndex = socket != null && socket.isServer ? 0 : 1;

            // Safe get names
            string localName = (ChessBoard.Player != null && ChessBoard.Player.Count > 0) ? ChessBoard.Player[localIndex].Name : "Bạn";
            string opponentName = (ChessBoard.Player != null && ChessBoard.Player.Count > 1) ? ChessBoard.Player[1 - localIndex].Name : "Đối thủ";

            if (winnerIndex == localIndex)
            {
                // Local player won -> congratulate local and send consolation to opponent
                MessageBox.Show($"Chúc mừng {localName} đã chiến thắng!");
                try
                {
                    string msgToOpponent = $"Rất tiếc, bạn đã thua. {localName} đã chiến thắng!";
                    socket.Send(new SocketData((int)SocketCommand.END_GAME, msgToOpponent, new Point()));
                }
                catch { }
            }
            else
            {
                // Local player lost -> console local and send congrats to opponent
                MessageBox.Show($"Rất tiếc, bạn đã thua. {opponentName} đã chiến thắng!");
                try
                {
                    string msgToOpponent = $"Chúc mừng {opponentName} đã chiến thắng!";
                    socket.Send(new SocketData((int)SocketCommand.END_GAME, msgToOpponent, new Point()));
                }
                catch { }
            }

            EndGame();
        }

        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();
            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                EndGame();
            }
        }

        private void vánMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void thoátTròChơiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
        }

        // Set up or join a LAN game: if connect fails, become server
        private void btnLAN_Click(object sender, EventArgs e)
        {
            socket.IP = txbIP.Text;

            if (!socket.ConnectServer())
            {
                // FORM LÀM SERVER
                socket.isServer = true;

                plnChessBoard.Enabled = true;   // server được đánh trước

                socket.CreateServer();
                MessageBox.Show("Đang chờ người chơi khác kết nối...");

                // PHẢI LISTEN Ở SERVER
                Listen();
            }
            else
            {
                // FORM LÀM CLIENT
                socket.isServer = false;

                plnChessBoard.Enabled = false;  // client không đánh trước

                Listen();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txbIP.Text))
            {
                txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }
        // Start background thread to listen for incoming socket data
        void Listen()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                    SocketData data = (SocketData)socket.Receive();
                    ProcessData(data);
                }
                catch (Exception e)
                {
                }
            });
            listenThread.IsBackground = true;
            listenThread.Start();
        }
        // Process incoming socket messages and act accordingly
        private void ProcessData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    break;
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        prcbCoolDown.Value = 0;
                        plnChessBoard.Enabled = true;
                        tmCoolDown.Start();
                        ChessBoard.OtherPlayerMark(data.Point);
                    }));
                    break;

                case (int)SocketCommand.END_GAME:
                    // Show win/lose message sent by opponent and end game locally
                    this.Invoke((MethodInvoker)(() =>
                    {
                        try { MessageBox.Show(data.Message); } catch { }
                        EndGame();
                    }));
                    break;
                case (int)SocketCommand.QUIT:
                    break;
                default:
                    break;
            }
            Listen();
        }
    }
}
