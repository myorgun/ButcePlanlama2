using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButcePlanlama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = 'ButcePlanlama.accdb'";

        private void Form1_Load(object sender, EventArgs e)
        {
            cbAy.Text = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("tr"));
            numericUpDown1.Text = DateTime.Now.Year.ToString();
            gidergetir(cbAy.Text, numericUpDown1.Text);

        }

        private void gidergetir(string ay, string yil)
        {
            string rakamay = "0";
            if(ay== "Ocak")
                rakamay = "01";
            else if (ay == "Şubat")
                rakamay = "02";
            else if (ay == "Mart")
                rakamay = "03";
            else if (ay == "Nisan")
                rakamay = "04";
            else if (ay == "Mayıs")
                rakamay = "05";
            else if (ay == "Haziran")
                rakamay = "06";
            else if (ay == "Temmuz")
                rakamay = "07";
            else if (ay == "Ağustos")
                rakamay = "08";
            else if (ay == "Eylül")
                rakamay = "09";
            else if (ay == "Ekim")
                rakamay = "10";
            else if (ay == "Kasım")
                rakamay = "11";
            else if (ay == "Aralık")
                rakamay = "12";

            ds.Tables[0].Clear();
            OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
            OleDbDataAdapter dadad = new OleDbDataAdapter("SELECT DISTINCT tbl_Kalem.Kalem_UstKalemId as [No], (Select top 1 klm.Kalem_Adi  from tbl_Kalem as klm where klm.Kalem_id =tbl_Kalem.Kalem_UstKalemId  ) as [Ürün / Hizmet Adı], Round((Select top 1 sum(Gider_OngorulenTutar) from tbl_Gider where Gider_KalemId = tbl_Kalem.Kalem_id ),2) as [Öngörülen Tutar], Round((Select top 1 sum(Gider_GerceklesenTutar) from tbl_Gider where Gider_KalemId = tbl_Kalem.Kalem_id ),2) as [Gerçeklesen Tutar], ((Select top 1 sum(Gider_OngorulenTutar) from tbl_Gider where Gider_KalemId = tbl_Kalem.Kalem_id )-(Select top 1 sum(Gider_GerceklesenTutar) from tbl_Gider where Gider_KalemId = tbl_Kalem.Kalem_id )) as [Fark] FROM (tbl_Gider LEFT JOIN tbl_Hesap ON tbl_Gider.Gider_HesapId = tbl_Hesap.Hesap_id) LEFT JOIN tbl_Kalem ON tbl_Gider.Gider_KalemId = tbl_Kalem.Kalem_id where tbl_Gider.Gider_Tarih BETWEEN #01/" + rakamay+"/"+yil+ "# AND #" + DateTime.DaysInMonth(Convert.ToInt32(yil), Convert.ToInt32(rakamay)).ToString() + "/" + rakamay + "/" + yil + "#", vtbaglantisi);
            vtbaglantisi.Open();
            dadad.Fill(ds, "Gider");
            vtbaglantisi.Close();
            adgvGiderTbl.DataSource = ds;
            adgvGiderTbl.DataMember = "Gider";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GiderEkle gider = new GiderEkle();
            gider.Closed += (s, args) => gidergetir(cbAy.Text, numericUpDown1.Text);
            gider.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gidergetir(cbAy.Text, numericUpDown1.Text);
        }

        private void adgvGiderTbl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (adgvGiderTbl.SelectedRows[0].Cells[0].Value != null)
                {
                    GiderDetay gider = new GiderDetay();
                    gider.id = adgvGiderTbl.SelectedRows[0].Cells[0].Value.ToString();
                    gider.ay = cbAy.Text;
                    gider.yil = numericUpDown1.Text;
                    gider.gideradi= adgvGiderTbl.SelectedRows[0].Cells[1].Value.ToString();
                    gider.Closed += (s, args) => gidergetir(cbAy.Text, numericUpDown1.Text);
                    gider.Show();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
