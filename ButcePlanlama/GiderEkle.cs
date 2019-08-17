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
    public partial class GiderEkle : Form
    {
        public GiderEkle()
        {
            InitializeComponent();
        }
        string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = 'ButcePlanlama.accdb'";
        private void GiderEkle_Load(object sender, EventArgs e)
        {
            anagidergetir();
            hesapgetir();
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
            OleDbDataAdapter dadad = new OleDbDataAdapter("SELECT Kalem_Adi, Kalem_id FROM tbl_Kalem where Kalem_UstKalemId="+comboBox1.SelectedValue+"", vtbaglantisi);
            vtbaglantisi.Open();

            dadad.Fill(ds);
            vtbaglantisi.Close();

            comboBox2.DisplayMember = "Kalem_Adi";
            comboBox2.ValueMember = "Kalem_id";
            comboBox2.DataSource = ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                textBox2.Text = "0";

            OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
            vtbaglantisi.Open();
            OleDbCommand dada = new OleDbCommand("Insert Into tbl_Gider(Gider_KalemId,Gider_HesapId,Gider_OngorulenTutar,Gider_GerceklesenTutar,Gider_Aciklama,Gider_Tarih)Values("+comboBox2.SelectedValue+","+comboBox3.SelectedValue+",'"+textBox1.Text.Replace('.',',')+"','"+textBox2.Text.Replace('.', ',') + "','"+textBox3.Text+"','"+dateTimePicker1.Value.ToShortDateString().Replace('.','/')+"')", vtbaglantisi);
            dada.ExecuteNonQuery();
            vtbaglantisi.Close();
            MessageBox.Show("Kayıt Eklendi");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaKalemEkle anakalem = new AnaKalemEkle();
            anakalem.Closed += (s, args) => anagidergetir();
            anakalem.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AltKalemEkle anakalem = new AltKalemEkle();
            anakalem.Closed += (s, args) => anagidergetir();
            anakalem.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HesapEkle hesap = new HesapEkle();
            hesap.Closed += (s, args) => hesapgetir();
            hesap.Show();
        }
    }
}
