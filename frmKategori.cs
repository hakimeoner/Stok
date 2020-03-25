using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Stok_Takibi
{
    public partial class frmKategori : Form
    {
        public frmKategori()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=HASAR-1;Initial Catalog=Stok_Takip;Integrated Security=True");
        //aynı ürün kaydedilmesin diye
        bool durum;
        private void kategorikontrol()
        {
            //istediğimiz işlemi true, istemediğimiz işlemi false olarak tanımlarsak
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if(textBox1.Text==read["kategori"].ToString() || textBox1.Text=="")
                {
                    durum=false;
                }
            }
        }
        private void frmKategori_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            kategorikontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("inset into KategoriBilgileri(kategori) values('" + textBox1.Text + "')", baglanti);

                baglanti.Close();
                //TextBox'ı temizleyip mesaj almak için
                textBox1.Text = "";
                MessageBox.Show("Kategori eklendi");
            }
            else
            {
                MessageBox.Show("Böyle bir kategori zaten var", "Uyarı");
            }
        }
    }
}
