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

namespace VeriTabaniOdev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Bağlantı dizesi
        string connectionString = "server=localHost; port=5432; Database=odev; user ID=postgres; password=0817";

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from urun";
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
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into \"urun\" (\"urunNo\", \"urunadi\", \"urunturu\", \"stok\", \"rafbogesi\", \"maaliyet\", \"satis\") values (@p1, @p2, @p3, @p4, @p5, @p6, @p7)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
                komut1.Parameters.AddWithValue("@p2", textBox2.Text);
                komut1.Parameters.AddWithValue("@p3", textBox3.Text);
                komut1.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));
                komut1.Parameters.AddWithValue("@p5", int.Parse(textBox5.Text));
                komut1.Parameters.AddWithValue("@p6", int.Parse(textBox6.Text));
                komut1.Parameters.AddWithValue("@p7", int.Parse(textBox7.Text));
                komut1.ExecuteNonQuery();
            }
            MessageBox.Show("Ürün başarıyla eklendi.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrolü
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki urunNo değerini alıyoruz
                int urunNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["urunNo"].Value);

                using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
                {
                    baglanti.Open();
                    NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM \"urun\" WHERE \"urunNo\" = @p1", baglanti);
                    komut2.Parameters.AddWithValue("@p1", urunNo); // Seçilen ürünün urunNo değerini kullanıyoruz
                    komut2.ExecuteNonQuery();
                }
                MessageBox.Show("Ürün başarıyla silindi");

                // Veritabanındaki değişiklikleri datagridview'a yansıtmak için verileri tekrar çekebilirsiniz
                button1.PerformClick(); // Veritabanından yeni verileri alarak grid'i güncelle
            }
            else
            {
                // Eğer hiçbir satır seçili değilse kullanıcıyı uyarıyoruz
                MessageBox.Show("Lütfen silmek istediğiniz ürünü seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("update \"urun\" set \"urunadi\"=@p1, \"urunturu\"=@p2, \"stok\"=@p3, \"rafbogesi\"=@p4, \"maaliyet\"=@p5, \"satis\"=@p6 where \"urunNo\"=@p7", baglanti);
                komut3.Parameters.AddWithValue("@p7", int.Parse(textBox1.Text));
                komut3.Parameters.AddWithValue("@p1", textBox2.Text);
                komut3.Parameters.AddWithValue("@p2", textBox3.Text);
                komut3.Parameters.AddWithValue("@p3", int.Parse(textBox4.Text));
                komut3.Parameters.AddWithValue("@p4", int.Parse(textBox5.Text));
                komut3.Parameters.AddWithValue("@p5", int.Parse(textBox6.Text));
                komut3.Parameters.AddWithValue("@p6", int.Parse(textBox7.Text));
                komut3.ExecuteNonQuery();
            }
            MessageBox.Show("Ürün başarıyla güncellendi.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();
                string sorgu = "SELECT * FROM \"urun\" WHERE \"urunturu\" = @p1";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                da.SelectCommand.Parameters.AddWithValue("@p1", textBox8.Text);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}