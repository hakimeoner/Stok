using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Takibi
{
    public partial class frmÜrünListele : Form

    {
        SqlConnection con = new SqlConnection();
        baglanti baglan = new baglanti();
        public frmÜrünListele()
        {
            con = baglan.con();
            InitializeComponent();
        }
        
        DataSet daset = new DataSet();
        private void kategorigetir()
        {
            baglan.open();
            
            SqlCommand komut = new SqlCommand("select *from KategoriBilgileri", con);
            SqlDataReader read = komut.ExecuteReader();
            //kayıtlar okunduğu sürece işlemi yapmasını sağlamak için
            while (read.Read())
            {
                    comboKategori.Items.Add(read["kategori"].ToString());
            }
            baglan.close();
            //kategori getir metodunu çağırmak için
            //kategorigetir();
        }
        private void frmÜrünListele_Load(object sender, EventArgs e)
        {
            ÜrünListele();
            kategorigetir();
        }

        private void ÜrünListele()
       {  
                baglan.open();

            
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urun", con);
            adtr.Fill(daset, "urun");
            dataGridView1.DataSource = daset.Tables["urun"];
            baglan.close();
        }
        // DataGrindView'a çift tıklayınca verilerin gelmesini sağlamak için
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BarkodNotxt.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            Kategoritxt.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            Markatxt.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            ÜrünAdıtxt.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            Miktarıtxt.Text = dataGridView1.CurrentRow.Cells["miktari"].Value.ToString();
            AlışFiyatıtxt.Text = dataGridView1.CurrentRow.Cells["alisfiyatı"].Value.ToString();
            SatışFiyatıtxt.Text = dataGridView1.CurrentRow.Cells["satisfiyatı"].Value.ToString();
        }

        private void Kategoritxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void True(object sender, EventArgs e)
        {

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
           // Güncelleme butonu için parametreler  
            baglan.open();
            SqlCommand komut = new SqlCommand("update urun set urunadi=@urunadi, miktari=@miktari, alisfiyati=@alisfiyati,satisfiyati=@satisfiyati where barkodno=@barkodno", con);
            komut.Parameters.AddWithValue("@barkodno", BarkodNotxt.Text);
            komut.Parameters.AddWithValue("@urunadi", ÜrünAdıtxt.Text);
            if (Miktarıtxt.Text.Length != 0)
            {
                komut.Parameters.AddWithValue("@miktari", int.Parse(Miktarıtxt.Text));
            }
            else {
                komut.Parameters.AddWithValue("@miktari", 0);
            }
            if (AlışFiyatıtxt.Text.Length != 0)
            {
                komut.Parameters.AddWithValue("@alisfiyati", double.Parse(AlışFiyatıtxt.Text));
            }
            else {
                komut.Parameters.AddWithValue("@alisfiyati", new Double());
            }
            if (SatışFiyatıtxt.Text.Length != 0)
            {
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(SatışFiyatıtxt.Text));
            }
            else {
                komut.Parameters.AddWithValue("@satisfiyati", new Double());
            }

            komut.ExecuteNonQuery();
            baglan.close();
            daset.Tables["urun"].Clear();
            ÜrünListele();
            MessageBox.Show("Güncelleme Gerçekleşti");
            foreach ( Control item in this.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnMarkaGüncelle_Click(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text != "")
            {
                    baglan.open();
                    SqlCommand komut = new SqlCommand("update urun set kategori=@kategori, marka=@marka, where barkodno=@barkodno", con);
                    komut.Parameters.AddWithValue("@barkodno", BarkodNotxt.Text);
                    komut.Parameters.AddWithValue("@kategori", comboKategori.Text);
                    komut.Parameters.AddWithValue("@marka", comboMarka.Text);

                    komut.ExecuteNonQuery();
                    baglan.close();
                    MessageBox.Show("Güncelleme Gerçekleşti");
                    daset.Tables["urun"].Clear();
                    ÜrünListele();
            }
            else
            {
                 MessageBox.Show("Barkod Numarası yazılı değil");
            }
            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            //daha önceki kayıtları ve içeriği (texti) silmek için
            comboMarka.Items.Clear();
            comboMarka.Text = "";
            baglan.open();
            SqlCommand komut = new SqlCommand("select *from MarkaBilgileri where kategori='" + comboKategori.SelectedItem + "'", con);
            SqlDataReader read = komut.ExecuteReader();
            //kayıtlar okunduğu sürece işlemi yapmasını sağlamak için
            while (read.Read())
            {
                    comboMarka.Items.Add(read["Markalar"].ToString());
            }
            baglan.close();
        }

        private void btnsilme_Click(object sender, EventArgs e)
        {
            baglan.open();
            SqlCommand komut = new SqlCommand("delete from urun where barkodno='"+ dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'", con);
            komut.Connection.Open();
            komut.ExecuteNonQuery();
            baglan.close();
            daset.Tables["Ürün"].Clear();
            ÜrünListele();
            MessageBox.Show("Kayıt Silindi");
        }

        private void txtBarkodNoAra_TextChanged(object sender, EventArgs e)
        {
            //geçici bir tablo tanımlamak için
            DataTable tablo = new DataTable();
            baglan.open();
            //arama yaptığımız karakterin gelmesi için yüzde işareti kullanılır
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urun where barkodno like'%" + txtBarkodNoAra.Text + "%'", con);
            //kayıtları tabloya aktarıp DataGrid'de göstermek için
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglan.close();
        }

        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
