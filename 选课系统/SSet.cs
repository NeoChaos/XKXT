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
    public partial class SSet : Form
    {
        public SSet()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cno = textBox1.Text;
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";
            SqlConnection mycon = new SqlConnection(strCon);
            string strSel = "select Sno,Grade from T_SC where Cno='"+cno+"'";
            SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            dataGridView1.DataSource = dt;

            mycon.Open();
            strSel = "select Cname from T_Course where Cno='" + cno + "'";
            SqlCommand mycmd = new SqlCommand(strSel, mycon);
            SqlDataReader reader = mycmd.ExecuteReader();
            if (reader.Read())
            {
                string cname = reader.GetString(reader.GetOrdinal("Cname"));
                groupBox1.Text = "课程：" + cname;
            }
            else
            {
                MessageBox.Show("课程不存在！");
                groupBox1.Text = "课程";
                textBox1.Focus();
            }
            
            mycon.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            string sno = dataGridView1.Rows[index].Cells["Sno"].Value.ToString();
            string sgrade = textBox2.Text;
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";
            string cno = textBox1.Text;

            SqlConnection mycon = new SqlConnection(strCon);
            mycon.Open();

            string strUp = "UPDATE T_SC set Grade='" + sgrade + "' where Sno='" + sno + "' AND Cno='"+cno+"'";
            SqlCommand mycmd = new SqlCommand(strUp, mycon);
            mycmd.ExecuteNonQuery();

            string strSel = "select Sno,Grade from T_SC where Cno='" + cno + "'";
            SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            dataGridView1.DataSource = dt;
            mycon.Close();

            dataGridView1.Columns["Sno"].HeaderText = "学号";
            dataGridView1.Columns["Grade"].HeaderText = "成绩";
        }

        private void SSet_Load(object sender, EventArgs e)
        {

        }
    }
}
