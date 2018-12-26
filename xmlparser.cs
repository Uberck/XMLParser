//Goldie Group XML parser 1.0
//Provides a UI with the following functionality:
//Selectable list for wireless providers that can be searched and modifed, and saved
//Upon selection of carrier, provide a request button that generates and sends an XML request based on selection to their Web API wrapped with an API key (done in XML)
//upon retrieval of the xml, parse the data and put it into a table to be displayed in... (HTML?)
 
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
 
//name used for original devolpment, rename to something else later
namespace ComboBoxSampleCSharp
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
 
        ComboBox Carrier = new ComboBox();
 
        //Button populates list of carriers
        private void CreateButton_Click(object sender, EventArgs e)
        {
 
            Carrier.Location = new System.Drawing.Point(10, 60);
            Carrier.Name = "Carrier";
            Carrier.Size = new System.Drawing.Size(150, 25);
            Carrier.BackColor = System.Drawing.Color.White;
            Carrier.ForeColor = System.Drawing.Color.Black;
 
            //Carrier.DropDownHeight = 50;
            //Carrier.DropDownWidth = 300;
            Carrier.Font = new Font("Georgia", 16);
           
 
            Carrier.Items.Add("Verizon");
            Carrier.Items.Add("T-Mobile");
            Carrier.Items.Add("Sprint");
            Carrier.Items.Add("ATT");
 
            //Carrier.DataSource = System.Enum.GetValues(typeof(ComboBoxStyle));
 
            //Commented out for now, this adds functionality for a confirmation box
            //Carrier.SelectedIndexChanged += new System.EventHandler(Carrier_SelectedIndexChanged);
 
            Controls.Add(Carrier);
        }
 
        private void Carrier_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Selected_Carrier");
        }
 
        //button used to send request to WebAPI
        private void GetItemsButton_Click(object sender, EventArgs e)
        {
 
            MessageBox.Show("Fetching data...");
 
            // XML request, receive, and parse utilizing API key funcitionality for security
 
            string xml = "Custom XML request string here";
            string url = "https://portal.goldiegroup.com/ACNAPI.fwx";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
 
 
            //string s = "id="+Server.UrlEncode(xml);
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(xml);
            req.Method = "POST";
            req.ContentType = "text/xml;charset=utf-8";
            req.ContentLength = requestBytes.Length;
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();
 
 
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
            string backstr = sr.ReadToEnd();
            //TextBox1.Text = backstr;
            sr.Close();
            res.Close();
 
        }
 
        //This button will be intended to allow the user to save the modified provider's list after they have configured it to their liking
        private void SelectedTextButton_Click(object sender, EventArgs e)
        {
 
            Carrier.Text = "Configuration saved!";
            string selectedItem = Carrier.Items[Carrier.SelectedIndex].ToString();
            MessageBox.Show(Carrier.Text);
 
        }
 
        //Search button searches within populated list field
        private void FindButton_Click(object sender, EventArgs e)
        {
            int index = Carrier.FindString(textBox1.Text);
            if (index < 0)
 
            {
                MessageBox.Show("Item not found.");
                textBox1.Text = String.Empty;
            }
            else
            {
                Carrier.SelectedIndex = index;
            }
        }
 
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
 
        }
    }
}
