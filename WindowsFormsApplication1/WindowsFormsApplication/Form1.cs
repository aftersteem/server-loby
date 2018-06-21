using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        static bool listening = false;
        static bool terminating = false;
        static bool accept = true;
        static Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static List<Socket> socketList = new List<Socket>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Listen_Click(object sender, EventArgs e)
        {
            int serverPort;
            Thread thrAccept;

            //this port will be used by clients to connect
            serverPort=Int32.Parse(textBox2.Text);

            try
            {
                server.Bind(new IPEndPoint(IPAddress.Any, serverPort));
                richTextBox1.AppendText("Started listening for incoming connections.");

                server.Listen(3); //the parameter here is maximum length of the pending connections queue
                thrAccept = new Thread(new ThreadStart(Accept));
                thrAccept.Start();
                listening = true;

            }
            catch
            {
                richTextBox1.AppendText("Cannot create a server with the specified port number\n Check the port number and try again.");
                richTextBox1.AppendText("terminating...");
            }




        }

        private void Accept()
        {
            while (accept)
            {
                try
                {
                    socketList.Add(server.Accept());
                    richTextBox1.AppendText("New client connected...\n");
                    Thread thrReceive;
                   // thrReceive = new Thread(new ThreadStart(Receive));
                   // thrReceive.Start();
                }
                catch
                {
                    if (terminating)
                        accept = false;
                    else
                        richTextBox1.AppendText("Listening socket has stopped working...\n");
                }
            }
        }




    }
}
