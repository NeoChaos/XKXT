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
    public partial class Login : Form
    {
        public static string Uno;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reg form = new Reg();
            form.ShowDialog();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (Lno.Text != "" && Lpwd.Text != "")
            {
                string strCon = "Data Source=.\\sqlexpress;Initial Catalog=JXGL;Integrated Security=True";
                SqlConnection mycon = new SqlConnection(strCon);
                mycon.Open();

                //创建查询命令
                using (SqlCommand mycmd = mycon.CreateCommand())
                {
                    Uno = Lno.Text;
                    //查询学号
                    mycmd.CommandText = "select * from T_Student where Sno='" + Uno + "'";
                    //保存查询到的数据在reader这个变量
                    using (SqlDataReader reader = mycmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //获取该用户对应的密码
                            string ckPwd = reader.GetString(reader.GetOrdinal("Spwd"));
                            //判断密码
                            if (Lpwd.Text == ckPwd)
                            {
                                Form1 formMain = new Form1(this);
                                this.Hide();
                                formMain.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("密码错误！");
                                Lpwd.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("该用户不存在！");
                            Lno.Focus();
                        }
                    }
                }
            }

            else
            {
                MessageBox.Show("请填写用户名与密码！");
                Lno.Focus();
            }
        }

        private void 课程管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CLogin cLogin = new CLogin();
            if(DialogResult.OK == cLogin.ShowDialog())
            {
                Course course = new Course();
                this.Hide();
                course.ShowDialog();
                this.Close();
            }

        }

        private void 系统维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CLogin cLogin = new CLogin();
            if (DialogResult.OK == cLogin.ShowDialog())
            {
                SSet sSet = new SSet();
                this.Hide();
                sSet.ShowDialog();
                this.Close();
            }
        }
    }
}
