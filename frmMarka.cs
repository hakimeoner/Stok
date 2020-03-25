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
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=HASAR-1;Initial Catalog=Stok_Takip;Integrated Security=True");
        bool durum;
        private void markakontrol()
        {
            //istediğimiz işlemi true, istemediğimiz işlemi false olarak tanımlarsak
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboBox1.Text==read["kategori"].ToString() && textBox1.Text == read["marka"].ToString() || comboBox1.Text == "" || textBox1.Text=="")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            markakontrol();
            if (durum == true)
            {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("inset into MarkaBilgileri(kategori,marka) values('" + comboBox1.Text + "''" + textBox1.Text + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Marka eklendi");
            }
            else
            {
                    MessageBox.Show("Böyle bir kategori ve marka bulunmakta", "Uyarı");
            }
            //TextBox'ı temizleyip mesaj almak için
            textBox1.Text = "";
            comboBox1.Text = "";
        }

        private void frmMarka_Load(object sender, EventArgs e)
        {
            //kategorileri comboBox1'e getirmek için
            kategorigetir();
        }

        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from KategoriBilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            //kayıtlar okunduğu sürece işlemi yapmasını sağlamak için
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }
    }
}
