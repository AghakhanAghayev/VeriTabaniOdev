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
    public partial class KitapTurleri : Form
    {
        public KitapTurleri()
        {
            InitializeComponent();
        }

        // Bağlantı dizesi
        string connectionString = "server=localHost; port=5432; Database=odev; user ID=postgres; password=0817";

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from \"kitaptur\"";
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into \"kitaptur\" (\"kitapturNo\", \"turadi\") values (@p1, @p2)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
                komut1.Parameters.AddWithValue("@p2", textBox2.Text);
                komut1.ExecuteNonQuery();
            }
            MessageBox.Show("Kitap türü başarıyla eklendi.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrolü
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki kitapturNo değerini alıyoruz
                int kitapturNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["kitapturNo"].Value);

                using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
                {
                    baglanti.Open();
                    // Silme işlemi için sorgu
                    NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM \"kitaptur\" WHERE \"kitapturNo\" = @p1", baglanti);
                    komut2.Parameters.AddWithValue("@p1", kitapturNo); // Seçilen türün kitapturNo değerini kullanıyoruz
                    komut2.ExecuteNonQuery();
                }
                MessageBox.Show("Kitap türü başarıyla silindi");

                // DataGridView verilerini güncellemek için veritabanındaki verileri tekrar çekebilirsiniz
                button1.PerformClick(); // Veritabanından yeni verileri alarak grid'i güncelle
            }
            else
            {
                // Eğer hiçbir satır seçili değilse kullanıcıyı uyarıyoruz
                MessageBox.Show("Lütfen silmek istediğiniz kitap türünü seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("update \"kitaptur\" set \"turadi\" = @p1 where \"kitapturNo\" = @p2", baglanti);
                komut3.Parameters.AddWithValue("@p2", int.Parse(textBox1.Text));
                komut3.Parameters.AddWithValue("@p1", textBox2.Text);
                komut3.ExecuteNonQuery();
            }
            MessageBox.Show("Kitap türü başarıyla güncellendi.");
        }
    }
}
