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
    public partial class GiderDetay : Form
    {
        public GiderDetay()
        {
            InitializeComponent();
        }
        public string ay="";
        public string yil="";
        public string id="";
        public string gideradi = "";
        string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = 'ButcePlanlama.accdb'";
        private void GiderDetay_Load(object sender, EventArgs e)
        {
            label1.Text = gideradi;
            label6.Text = ay + " " + yil;
            gidergetir(ay, yil, id);
        }
        private void gidergetir(string ay, string yil,string idd)
        {
            string rakamay = "0";
            if (ay == "Ocak")
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
            DataSet ds = new DataSet();
            OleDbConnection vtbaglantisi = new OleDbConnection(vtyolu);
            OleDbDataAdapter dadad = new OleDbDataAdapter("SELECT tbl_Gider.Gider_id as [No],tbl_Hesap.Hesap_Adi as [Hesap Adı],tbl_Kalem.Kalem_Adi as [Ürün / Hizmet Adı],tbl_Gider.Gider_OngorulenTutar as [Öngörülen Tutar], tbl_Gider.Gider_GerceklesenTutar as [Gerçeklesen Tutar], (tbl_Gider.Gider_OngorulenTutar - tbl_Gider.Gider_GerceklesenTutar) as [Fark], tbl_Gider.Gider_Tarih as [Tarih] FROM(tbl_Gider LEFT JOIN tbl_Hesap ON tbl_Gider.Gider_HesapId = tbl_Hesap.Hesap_id) LEFT JOIN tbl_Kalem ON tbl_Gider.Gider_KalemId = tbl_Kalem.Kalem_id where tbl_Kalem.Kalem_UstKalemId=" + idd+" and tbl_Gider.Gider_Tarih BETWEEN #01/" + rakamay + "/" + yil + "# AND #"+ DateTime.DaysInMonth(Convert.ToInt32(yil), Convert.ToInt32(rakamay)).ToString() +"/" + rakamay + "/" + yil + "#;", vtbaglantisi);
            vtbaglantisi.Open();
            dadad.Fill(ds,"gider");
            vtbaglantisi.Close();
            adgvGiderDetay.DataSource = ds;
            adgvGiderDetay.DataMember = "gider";
            double toh = 0,tgh=0;
            for (int i = 0; i < adgvGiderDetay.RowCount; i++)
            {
                toh += Convert.ToDouble(adgvGiderDetay.Rows[i].Cells[3].Value);
                tgh+= Convert.ToDouble(adgvGiderDetay.Rows[i].Cells[4].Value);
            }
            label3.Text = toh.ToString() + " TL";
            label4.Text = tgh.ToString() + " TL";

        }

        private void adgvGiderDetay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (adgvGiderDetay.SelectedRows[0].Cells[0].Value != null)
                {
                    AltGiderDetay gider = new AltGiderDetay();
                    gider.id = adgvGiderDetay.SelectedRows[0].Cells[0].Value.ToString();
                    gider.ust = id;
                    gider.Closed += (s, args) => gidergetir(ay, yil, id);
                    gider.ShowDialog();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
