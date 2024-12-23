using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VeriTabaniOdev
{
    public partial class UrunSipairFrm : Form
    {
        public UrunSipairFrm()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=odev; user ID=postgres; password=0817");

        // Bağlantıyı açmaya çalışan bir metot
        private void OpenConnection()
        {
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM \"urunsiparis\"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenConnection();  // Bağlantıyı açıyoruz

            NpgsqlCommand komut1 = new NpgsqlCommand("INSERT INTO \"urunsiparis\" (\"siparisno\", \"urunNo\", \"siparismiktari\", \"birimfiyat\", \"no\") VALUES (@p1, @p2, @p3, @p4, @p5)", baglanti);

            komut1.Parameters.AddWithValue("@p5", int.Parse(textBox1.Text)); // no
            komut1.Parameters.AddWithValue("@p1", int.Parse(textBox2.Text)); // siparisno
            komut1.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text)); // urunNo
            komut1.Parameters.AddWithValue("@p3", int.Parse(textBox4.Text)); // siparismiktari
            komut1.Parameters.AddWithValue("@p4", int.Parse(textBox5.Text)); // birimfiyat

            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün siparişi başarıyla alındı.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrolü
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki 'no' değerini alıyoruz
                int no = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["no"].Value);

                // Bağlantıyı açıyoruz
                OpenConnection();

                // Silme işlemi için sorgu
                NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM \"urunsiparis\" WHERE \"no\" = @p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", no); // Seçilen 'no' değerini kullanıyoruz

                // Sorguyu çalıştırıyoruz
                komut2.ExecuteNonQuery();

                // Bağlantıyı kapatıyoruz
                baglanti.Close();

                // Kullanıcıya başarı mesajı veriyoruz
                MessageBox.Show("Sipariş başarıyla silindi");

                // DataGridView'i güncellemek için veritabanındaki verileri tekrar çekiyoruz
                string sorgu = "SELECT * FROM \"urunsiparis\"";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                // Eğer hiçbir satır seçili değilse kullanıcıyı uyarıyoruz
                MessageBox.Show("Lütfen silmek istediğiniz siparişi seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenConnection();  // Bağlantıyı açıyoruz

            NpgsqlCommand komut3 = new NpgsqlCommand("UPDATE \"urunsiparis\" SET \"siparisno\" = @p1, \"urunNo\" = @p2, \"siparismiktari\" = @p3, \"birimfiyat\" = @p4 WHERE \"no\" = @p5", baglanti);

            komut3.Parameters.AddWithValue("@p5", int.Parse(textBox1.Text)); // no
            komut3.Parameters.AddWithValue("@p1", int.Parse(textBox2.Text)); // siparisno
            komut3.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text)); // urunNo
            komut3.Parameters.AddWithValue("@p3", int.Parse(textBox4.Text)); // siparismiktari
            komut3.Parameters.AddWithValue("@p4", int.Parse(textBox5.Text)); // birimfiyat

            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sipariş başarıyla güncellendi.");
        }
    }
}
