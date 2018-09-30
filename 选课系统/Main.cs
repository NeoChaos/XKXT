using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 选课系统
{
    public partial class Form1 : Form
    {
        public Login login;
        public Form1(Login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";
            SqlConnection mycon = new SqlConnection(strCon);
            string strSel = "select * from T_Student";
            SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            dataGridView1.DataSource = dt;
            mycon.Close();

            MessageBox.Show(login.Lno.Text);
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }



    }
}
