using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//SQL Server kütüphanesini eklemek için
using System.Data.SqlClient;
namespace Stok_Takibi
{
    public partial class frmMüşteriEkle : Form
    {
        SqlConnection con = new SqlConnection();
        baglanti baglan = new baglanti();

        public frmMüşteriEkle()
        {
            con = baglan.con();
            InitializeComponent();
        }
     

        private void frmMüşteriEkle_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          //kayıt ekleme işlemi      
                baglan.open();
                SqlCommand komut = new SqlCommand("insert into Müsteri(tc, adsoyad, telefon, adres, email) values(@tc, @adsoyad, @telefon, @adres, @email)", con);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                komut.Parameters.AddWithValue("@email", txtEmail.Text);
                komut.ExecuteNonQuery();

            //işlemi onaylamak için

            baglan.close();

         // işlemin sonunda kayıt eklendiğini bildirmek için
                MessageBox.Show("Müşteri kaydı oluşturuldu");

         // işlem tamamlanınca TextBoxları silmek için
                foreach (Control item in this.Controls)
            {
                    if (item is TextBox)
                {
                        item.Text = "";
                }
            }
        }
    }
}
