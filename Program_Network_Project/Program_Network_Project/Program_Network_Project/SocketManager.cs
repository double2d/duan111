using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program_Network_Project
{
    public class SocketManager
    {
        // ================= CLIENT ==================
        Socket client;

        // Thử kết nối tới server (dùng khi app chạy ở chế độ client)
        public bool ConnectServer()
        {
            try
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), PORT);

                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(iep);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // ================= SERVER ==================
        Socket server;

        // Tạo server socket và chạy thread background để accept client
        public void CreateServer()
        {
            try
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, PORT);

                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Cho phép reuse port để tránh lỗi Bind khi khởi động lại nhanh
                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                server.Bind(iep);
                server.Listen(10);

                Thread acceptClient = new Thread(() =>
                {
                    // Accept là blocking nên chạy trên background thread
                    client = server.Accept();
                });

                acceptClient.IsBackground = true;
                acceptClient.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo server: " + ex.Message);
            }
        }


        // ================= GENERAL ==================
        // Các cấu hình công khai
        public string IP = "127.0.0.1";
        public int PORT = 9999;
        public const int BUFFER = 4096;

        public bool isServer = true;

        // Gửi đối tượng (đã tuần tự hoá) tới đối phương
        public bool Send(object data)
        {
            if (client == null) return false;

            byte[] sendData = SerializeData(data);
            client.Send(sendData);
            return true;
        }

        // Nhận một đối tượng (blocking) từ đối phương
        public object Receive()
        {
            if (client == null) return null;

            byte[] buffer = new byte[BUFFER];
            int received = client.Receive(buffer);

            if (received <= 0) return null;

            return DeserializeData(buffer.Take(received).ToArray());
        }

        // Tuần tự hoá / giải tuần tự hoá bằng BinaryFormatter (đơn giản cho ví dụ)
        public byte[] SerializeData(object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }

        public object DeserializeData(byte[] array)
        {
            MemoryStream ms = new MemoryStream(array);
            BinaryFormatter bf = new BinaryFormatter();
            ms.Position = 0;
            return bf.Deserialize(ms);
        }

        // Đóng socket an toàn
        public void Close()
        {
            try { client?.Close(); } catch { }
            try { server?.Close(); } catch { }
        }

        // Helper để lấy IPv4 local cho UI
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
    }
}
