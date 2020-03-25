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
    public partial class frmÜrünEkleme : Form
    {
        SqlConnection con = new SqlConnection();
        baglanti baglan = new baglanti();
        public frmÜrünEkleme()
        {
            con = baglan.con();
            InitializeComponent();
        }
        bool durum;
        private void barkodkontrol()
        {
            //istediğimiz işlemi true, istemediğimiz işlemi false olarak tanımlarsak
            durum = true;
            //Bu durum kontrolü sadece bağlantı kapalıysa bağlantı açmayı sağlar.
            baglan.open();
            

            SqlCommand komut = new SqlCommand("Select *From  Urun", con);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text==read["barkodno"].ToString() || txtBarkodNo.Text=="")
                {
                    durum = false;
                }
            }
            baglan.close();
        }
        private object read;

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

       
        
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
           kategorigetir();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmÜrünEkleme_Load(object sender, EventArgs e)
        {

        }

        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            //daha önceki kayıtları ve içeriği (texti) silmek için
            comboMarka.Items.Clear();
            comboMarka.Text = "";
            baglan.open();
            SqlCommand komut = new SqlCommand("select *from MarkaBilgileri where kategori='"+comboKategori.SelectedItem+"'", con);
            SqlDataReader read = komut.ExecuteReader();
            //kayıtlar okunduğu sürece işlemi yapmasını sağlamak için
            while (read.Read())
            {
                comboMarka.Items.Add(read["Markalar"].ToString());
            }
            baglan.close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum == true)
            {
                baglan.open();
                SqlCommand komut = new SqlCommand("insert into Urun(barkodno, kategori, marka,urunadi,miktari,alisfiyati,satisfiyati,tarih) values(@barkodno, @kategori, @marka,@urunadi,@miktari,@alisfiyati,@satisfiyati,@tarih)", con);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@kategori", comboKategori.Text);
                komut.Parameters.AddWithValue("@marka", comboMarka.Text);
                komut.Parameters.AddWithValue("@urunadi", txtÜrünAdı.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(txtMiktarı.Text));
                komut.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlışFiyatı.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatışFiyatı.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglan.close();
                MessageBox.Show("Ürün Eklendi");
            }
            // Eğer aynı barkod numarası varsa ekrana uyarı vermek için
            else
            {
                MessageBox.Show("Böyle bir barkod numarası bulunmakta", "Uyarı");
            }
            comboMarka.Items.Clear();

            //ComboBox ve TextBoxları temizlemek için
            if (groupBox1 != null) { 
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }
        }

        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text == "")
            {
                
                foreach( Control item in groupBox2.Controls)
                {
                    if( item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }

            // Barkod numarasını yazdığımzda bilgilerin TextBox'lara gelmesini sağlamak için
            baglan.open();
            SqlCommand komut = new SqlCommand("select *from urun where barkodno like '"+BarkodNotxt.Text+"'", con);
            SqlDataReader read = komut.ExecuteReader();

            baglan.close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.open();
            SqlCommand komut = new SqlCommand("update urun set miktari=miktari'"+int.Parse(Miktarıtxt.Text)+"' where barkodno='"+BarkodNotxt.Text+"'",con);
            komut.ExecuteNonQuery();
            baglan.close();
            foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            MessageBox.Show("Var olan ürüne ekleme yapıldı");
        }
    }
}
