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
    public partial class Course : Form
    {
        public static string tempCcode;
        public Course()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Course_Load(object sender, EventArgs e)
        {
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";
            SqlConnection mycon = new SqlConnection(strCon);
            string strSel = "select * from T_Course";
            SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            dataGridView1.DataSource = dt;
            mycon.Close();
            dataGridView1.Columns["Cno"].HeaderText = "课程号";
            dataGridView1.Columns["Cname"].HeaderText = "课程名称";
            dataGridView1.Columns["Ccode"].HeaderText = "学分";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strCno = textBox1.Text;
            string strCname = textBox2.Text;
            string strCcode = textBox3.Text;
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";

            if(button1.Text == "增加")
            {
                if (strCno != "" && strCname != "" && strCcode != "")
                {
                    SqlConnection mycon = new SqlConnection(strCon);
                    mycon.Open();

                    string strCheck = "select count(*) from T_Course where Cno=" + strCno;
                    SqlCommand mychk = new SqlCommand(strCheck, mycon);
                    if ((int)mychk.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("该课程号已存在！");
                        textBox1.Focus();
                    }
                    else
                    {
                        string strUp = "INSERT INTO[dbo].[T_Course]([Cno], [Cname], [Ccode]) VALUES('" + strCno + "',N'" + strCname + "','" + strCcode + "')";
                        SqlCommand mycmd = new SqlCommand(strUp, mycon);
                        mycmd.ExecuteNonQuery();
                        MessageBox.Show("添加成功！");
                    }
                    string strSel = "select * from T_Course";
                    SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);
                    DataTable dt = new DataTable();
                    myda.Fill(dt);
                    dataGridView1.DataSource = dt;
                    mycon.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }
            else
            {
                SqlConnection mycon = new SqlConnection(strCon);
                mycon.Open();

                string strUp = "UPDATE T_Course set Cname='"+strCname+"' ,Ccode='"+strCcode+"' ,Cno='"+strCno+"' where Cno='"+tempCcode+"'";
                SqlCommand mycmd = new SqlCommand(strUp, mycon);
                mycmd.ExecuteNonQuery();

                string strSel = "select * from T_Course";
                SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);
                DataTable dt = new DataTable();
                myda.Fill(dt);
                dataGridView1.DataSource = dt;
                mycon.Close();
                button1.Text = "增加";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox1.Enabled = true;
            }
            
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[index].Cells["Cno"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[index].Cells["Cname"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[index].Cells["Ccode"].Value.ToString();
            tempCcode = textBox1.Text;
            button1.Text = "修改";
            textBox1.Enabled = false;
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            string strCno = dataGridView1.Rows[index].Cells["Cno"].Value.ToString();
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";

            SqlConnection mycon = new SqlConnection(strCon);
            mycon.Open();

            string strDel = "delete from T_Course where Cno = '" + strCno + "'";
            SqlCommand mycmd = new SqlCommand(strDel, mycon);
            mycmd.ExecuteNonQuery();

            string strSel = "select * from T_Course";
            SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);
            DataTable dt = new DataTable();
            myda.Fill(dt);
            dataGridView1.DataSource = dt;
            mycon.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
