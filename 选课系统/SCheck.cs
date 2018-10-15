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
    public partial class SCheck : Form
    {
        public SCheck()
        {
            InitializeComponent();
        }

        private void SCheck_Load(object sender, EventArgs e)
        {
            //查询分数
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";
            SqlConnection mycon = new SqlConnection(strCon);
            string strSel = "select Cname, Ccode, Grade from V_SC where Sno =@uno";
            SqlDataAdapter myda = new SqlDataAdapter(strSel, mycon);

            SqlParameter uno = new SqlParameter("@uno", Login.Uno);
            myda.SelectCommand.Parameters.Add(uno);
            

            DataTable dt = new DataTable();
            myda.Fill(dt);
            dataGridView1.DataSource = dt;
            mycon.Close();

            dataGridView1.Columns["Grade"].HeaderText = "成绩";
            dataGridView1.Columns["Cname"].HeaderText = "课程名称";
            dataGridView1.Columns["Ccode"].HeaderText = "学分";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
