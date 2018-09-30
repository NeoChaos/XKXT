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
    public partial class Reg : Form
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void Reg_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";
            String strNo = Bno.Text;
            String strName = Bname.Text;
            String strSex = Bsex.Text;
            String strAge = Bage.Text;
            String strDept = Bdept.Text;
            String strPwd1 = Bpwd1.Text;
            String strPwd2 = Bpwd2.Text;

            SqlConnection mycon = new SqlConnection(strCon);
            mycon.Open();
            if (strPwd1 == strPwd2)
            {
                string strCheck = "select count(*) from T_Student where Sno=" + strNo;
                SqlCommand mychk = new SqlCommand(strCheck, mycon);
                if ((int)mychk.ExecuteScalar() > 0)
                {
                    MessageBox.Show("该学号已经注册！");
                    Bno.Focus();
                }
                else
                {
                    string strUp = "INSERT INTO[dbo].[T_Student]([Sno], [Sname], [Ssex], [Sage], [Sdept], [Spwd]) VALUES(" + strNo + ", N'" + strName + "', N'" + strSex + "', " + strAge + ", N'" + strDept + "',N'" + strPwd1 + "')";
                    SqlCommand mycmd = new SqlCommand(strUp, mycon);
                    mycmd.ExecuteNonQuery();
                    MessageBox.Show("注册成功！");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("密码不一致！");
                Bpwd1.Focus();
            }
        }
    }
}
