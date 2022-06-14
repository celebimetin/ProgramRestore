using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProgramRestore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Veri dosyasını seçiniz|*.bak";
            openFile.ShowDialog();
            txtDosya.Text = openFile.FileName;
        }

        private void btnYukle_Click(object sender, EventArgs e)
        {
            if (txtDosya.Text != "")
            {
                try
                {
                    string strSql = @""; // burası yapılacak eksik
                    Cursor.Current = Cursors.WaitCursor;
                    string yedekYolu = txtDosya.Text;
                    Application.DoEvents();
                    string str = Application.StartupPath + @""; // burası yapılacak eksik

                    using (SqlConnection connection = new SqlConnection(strSql))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(@"USE Master; If Exists(Select * From sys.databases where name='BarcodeSalesDb') Drop Database[" + str + "]; RESTORE DATABASE[" + str + "] FROM DISK=N'" + txtDosya.Text + "'", connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    MessageBox.Show("Veriler yüklenmiştir.");
                    Process.Start(Application.StartupPath + "\\BarcodeSales.exe");
                    Cursor.Current = Cursors.Default;
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}