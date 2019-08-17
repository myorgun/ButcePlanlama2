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
    public partial class AltKalemEkle : Form
    {
        public AltKalemEkle()
        {
            InitializeComponent();
        }
        string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = 'ButcePlanlama.accdb'";
        private void AltKalemEkle_Load(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
            OleDbDataAdapter dadad = new OleDbDataAdapter("SELECT Kalem_Adi, Kalem_id FROM tbl_Kalem where Kalem_UstKalemId=0", vtbaglantisi);
            vtbaglantisi.Open();

            dadad.Fill(ds);
            vtbaglantisi.Close();

            comboBox1.DisplayMember = "Kalem_Adi";
            comboBox1.ValueMember = "Kalem_id";
            comboBox1.DataSource = ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
                vtbaglantisi.Open();
                OleDbCommand hoayda = new OleDbCommand("select* from tbl_Kalem where Kalem_Adi='" + textBox1.Text + "' and Kalem_UstKalemId="+comboBox1.SelectedValue+"", vtbaglantisi);
                var ahey = hoayda.ExecuteScalar();
                vtbaglantisi.Close();
                if (ahey == null)
                {
                    vtbaglantisi.Open();
                    OleDbCommand dada = new OleDbCommand("Insert Into tbl_Kalem(Kalem_Adi,Kalem_UstKalemId)Values('" + textBox1.Text + "'," + comboBox1.SelectedValue + ")", vtbaglantisi);
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
