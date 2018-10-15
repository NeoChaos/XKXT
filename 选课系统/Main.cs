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
            label1.Text = "学号：" + Login.Uno;
            //显示未选课程
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";
            SqlConnection mycon = new SqlConnection(strCon);
            string strSel = "select * from T_Course";
            SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            dataGridView1.DataSource = dt;
            mycon.Close();

            //显示已选课程
            strSel = "select Cno,Cname,Ccode from V_SC where Sno='"+Login.Uno+"'order by Cno";
            SqlDataAdapter myda2 = new SqlDataAdapter(strSel, mycon);
            DataTable dt2 = new DataTable();
            myda2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            mycon.Close();

            dataGridView1.Columns["Cno"].HeaderText = "课程号";
            dataGridView1.Columns["Cname"].HeaderText = "课程名称";
            dataGridView1.Columns["Ccode"].HeaderText = "学分";

            dataGridView2.Columns["Cno"].HeaderText = "课程号";
            dataGridView2.Columns["Cname"].HeaderText = "课程名称";
            dataGridView2.Columns["Ccode"].HeaderText = "学分";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            string add = dataGridView1.Rows[index].Cells["Cno"].Value.ToString();
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";

            string strCheck = "select count(*) from T_SC where Cno=" + add +"and Sno="+Login.Uno;
            

            SqlConnection mycon = new SqlConnection(strCon);
            mycon.Open();

            SqlCommand mychk = new SqlCommand(strCheck, mycon);
            if ((int)mychk.ExecuteScalar() == 0)
            {
                string strAdd = "INSERT INTO[dbo].[T_SC]([Cno], [Sno]) VALUES ('" + add + "','" + Login.Uno + "')";
                SqlCommand mycmd = new SqlCommand(strAdd, mycon);
                mycmd.ExecuteNonQuery();
            }
                

            string strSel = "select Cno,Cname,Ccode from V_SC where Sno='" + Login.Uno + "'order by Cno";
            SqlDataAdapter myda2 = new SqlDataAdapter(strSel, mycon);
            DataTable dt2 = new DataTable();
            myda2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            mycon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentRow.Index;
            string del = dataGridView2.Rows[index].Cells["Cno"].Value.ToString();
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";

            SqlConnection mycon = new SqlConnection(strCon);
            mycon.Open();

            string strDel = "DELETE FROM T_SC where Cno='"+del+"'AND Sno='"+Login.Uno+"'";
            SqlCommand mycmd = new SqlCommand(strDel, mycon);
            mycmd.ExecuteNonQuery();

            string strSel = "select Cno,Cname,Ccode from V_SC where Sno='" + Login.Uno + "'order by Cno";
            SqlDataAdapter myda2 = new SqlDataAdapter(strSel, mycon);
            DataTable dt2 = new DataTable();
            myda2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            mycon.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SCheck sCheck = new SCheck();
            sCheck.ShowDialog();
        }
    }
}
