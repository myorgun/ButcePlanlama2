using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ButcePlanlama
{
    public partial class AltGiderDetay : Form
    {
        public AltGiderDetay()
        {
            InitializeComponent();
        }
        public string id="";
        public string ust = "";
        string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = 'ButcePlanlama.accdb'";
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AltGiderDetay_Load(object sender, EventArgs e)
        {
            anagidergetir();
            hesapgetir();
            comboBox1.SelectedValue = Convert.ToInt32(ust);

            DataSet ds = new DataSet();
            OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
            OleDbCommand dadad = new OleDbCommand("SELECT * FROM  tbl_Gider where Gider_id=" + id + " ", vtbaglantisi);
            vtbaglantisi.Open();
            OleDbDataReader okuyavrum = dadad.ExecuteReader();
            while(okuyavrum.Read())
            {
                comboBox2.SelectedValue = okuyavrum[1];
                comboBox3.SelectedValue = okuyavrum[2];
                textBox1.Text = okuyavrum[3].ToString();
                textBox2.Text = okuyavrum[4].ToString();
                dateTimePicker1.Value =Convert.ToDateTime(okuyavrum[6]);
                textBox3.Text = okuyavrum[5].ToString();
            }
            vtbaglantisi.Close();

        }

        private void hesapgetir()
        {
            DataTable ds = new DataTable();
            OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
            OleDbDataAdapter dadad = new OleDbDataAdapter("SELECT Hesap_Adi, Hesap_id FROM tbl_Hesap", vtbaglantisi);
            vtbaglantisi.Open();

            dadad.Fill(ds);
            vtbaglantisi.Close();

            comboBox3.DisplayMember = "Hesap_Adi";
            comboBox3.ValueMember = "Hesap_id";
            comboBox3.DataSource = ds;
        }

        private void anagidergetir()
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            DataTable ds = new DataTable();
            OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
            OleDbDataAdapter dadad = new OleDbDataAdapter("SELECT Kalem_Adi, Kalem_id FROM tbl_Kalem where Kalem_UstKalemId=" + comboBox1.SelectedValue + "", vtbaglantisi);
            vtbaglantisi.Open();

            dadad.Fill(ds);
            vtbaglantisi.Close();

            comboBox2.DisplayMember = "Kalem_Adi";
            comboBox2.ValueMember = "Kalem_id";
            comboBox2.DataSource = ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
                OleDbCommand alektemovik = new OleDbCommand("Update tbl_Gider Set Gider_KalemId=" + comboBox2.SelectedValue + ", Gider_HesapId=" + comboBox3.SelectedValue + ", Gider_OngorulenTutar='" + textBox1.Text.Replace('.', ',') + "', Gider_GerceklesenTutar='" + textBox2.Text.Replace('.', ',') + "', Gider_Aciklama='" + textBox3.Text + "', Gider_Tarih='" + dateTimePicker1.Value.ToShortDateString() + "' where Gider_id="+id+"", vtbaglantisi);
                vtbaglantisi.Open();
                alektemovik.ExecuteNonQuery();
                vtbaglantisi.Close();
                MessageBox.Show("Kayıt Güncellendi");
                this.Close();
        }
            catch (Exception)
            {
                MessageBox.Show("Kayıt Güncellenemedi");
            }
}

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
                OleDbCommand alektemovik = new OleDbCommand("Delete from tbl_Gider where Gider_id=" + id + "", vtbaglantisi);
                vtbaglantisi.Open();
                alektemovik.ExecuteNonQuery();
                vtbaglantisi.Close();
                MessageBox.Show("Kayıt Silindi");
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt Silinemedi");
            }
        }
    }
}
