using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButcePlanlama
{
    public partial class HesapEkle : Form
    {
        public HesapEkle()
        {
            InitializeComponent();
        }
        string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = 'ButcePlanlama.accdb'";
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
                vtbaglantisi.Open();
                OleDbCommand hoayda = new OleDbCommand("select* from tbl_Hesap where Hesap_Adi='" + textBox1.Text + "'", vtbaglantisi);
                var ahey = hoayda.ExecuteScalar();
                vtbaglantisi.Close();
                if (ahey == null)
                {
                    vtbaglantisi.Open();
                    OleDbCommand dada = new OleDbCommand("Insert Into tbl_Hesap(Hesap_Adi)Values('" + textBox1.Text + "')", vtbaglantisi);
                    dada.ExecuteNonQuery();
                    vtbaglantisi.Close();
                    MessageBox.Show("Kayıt Eklendi");
                    this.Close();
                }
                else
                    MessageBox.Show("Aynı İsimde Kayıt Var!");
            }
            else
                MessageBox.Show("Boş Bırakılamaz!");
        }
    }
}
