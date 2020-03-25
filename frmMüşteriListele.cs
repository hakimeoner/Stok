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
    public partial class frmMüşteriListele : Form
    {
        SqlConnection con = new SqlConnection();
        baglanti baglan = new baglanti();
        public frmMüşteriListele()
        {
           
            con = baglan.con();
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
 

       
        //kayıtları geçici olarak tutmak için
        DataSet daset = new DataSet();

        private void frmMüşteriListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }

        private void Kayıt_Göster()
        {
            baglan.open();
            //müşteri tablosundaki bütün kayıtları göstermek için
            SqlDataAdapter adtr = new SqlDataAdapter("select *from Müsteri", con);
            adtr.Fill(daset, "Müsteri");
            dataGridView1.DataSource = daset.Tables["Müsteri"];
            baglan.close();
        }

        private void frmMüşteriListele_DoubleClick(object sender, EventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglan.open();
            SqlCommand komut = new SqlCommand("update Müşteri set adsoyad=@adsoyad, telefon=@telefon, adres=@adres, email=@email where tc=@tc", con);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);

            baglan.close();
            //tabloyu temizleyip yeni kayıt getirmek için metot çağırıyoruz
            daset.Tables["Müşteri"].Clear();
            Kayıt_Göster();
            
            MessageBox.Show("Müşteri kaydı güncellendi");

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglan.open();
           
        SqlCommand komut = new SqlCommand(Text, con);
            komut.Connection.Open();
            komut.ExecuteNonQuery();
            baglan.close();
            daset.Tables["Müşteri"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Kayıt Silindi");

        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglan.open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from Müşteri where tc like'%" + txtTcAra.Text + "%'", con);
            //kayıtları tabloya aktarıp DataGrid'de gösterecegiz
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglan.close();

        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
