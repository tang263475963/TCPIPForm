using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Policy;
using System.Net.Sockets;
using System.Net;

namespace ClientApp
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ConnectAsClint));
            t.Start();

        }

        private void ConnectAsClint()
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 5000);
            updateUI("เชื่อมต่อสำเร็จ");
            NetworkStream stream = client.GetStream();
            String str = "Helloworld";
            byte[] message = Encoding.ASCII.GetBytes(str);
            stream.Write(message, 0, message.Length);
            updateUI(str + "Send Complete");
            stream.Close();
            client.Close();

        }


        private void updateUI(string v)
        {
            Func<int> del = delegate ()
            {
                textBox1.AppendText(v + System.Environment.NewLine);
                return 0;
            };
            Invoke(del);
        }
    }
}
