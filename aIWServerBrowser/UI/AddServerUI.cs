using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace aIWServerBrowser
{
    public delegate void addServerCallback(Server e);
    class AddServerUI : Form
    {
        private Label label1;
        private TextBox ipInput;
        private Button button1;
        private Button addBtn;
        private addServerCallback _callback;
        public AddServerUI(addServerCallback e)
        {
            _callback = e;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ipInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the IP address of the server. (IP:Port format)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ipInput
            // 
            this.ipInput.Location = new System.Drawing.Point(15, 31);
            this.ipInput.Name = "ipInput";
            this.ipInput.Size = new System.Drawing.Size(264, 20);
            this.ipInput.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(123, 60);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // AddServerUI
            // 
            this.AcceptButton = this.addBtn;
            this.ClientSize = new System.Drawing.Size(291, 91);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ipInput);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddServerUI";
            this.Text = "Enter Server Address";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string[] split = new string[] { "..." };
            try
            {
                split = ipInput.Text.Split(':');
                int port;
                IPAddress addr;
                if (!IPAddress.TryParse(split[0], out addr))
                    addr = Dns.GetHostAddresses(split[0])[0];
                if (split.Length >= 2)
                {
                    if (!int.TryParse(split[1], out port))
                        port = 28960;
                }
                else
                    port = 28960;
                _callback(new Server(new IPEndPoint(addr, port)));
                this.Close();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Address format incorrect! Format is IP[:Port]", "Format incorrect!");
            }
            catch (SocketException)
            {
                MessageBox.Show("Could not resolve the hostname: " + split[0], "DNS resolve error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WTFException?!?!");
            }
        }
    }
}
