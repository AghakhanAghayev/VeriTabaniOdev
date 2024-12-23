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
    public partial class Kisiler : Form
    {
        public Kisiler()
        {
            InitializeComponent();
        }

        // Bağlantıyı global olarak tanımlıyoruz
        string connectionString = "server=localHost; port=5432; Database=odev; user ID=postgres; password=0817";

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from \"kisi\"";
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
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into \"kisi\" (\"kisiNo\", \"adi\", \"soyadi\", \"kisiTipi\") values (@p1, @p2, @p3, @p4)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
                komut1.Parameters.AddWithValue("@p2", textBox2.Text);
                komut1.Parameters.AddWithValue("@p3", textBox3.Text);
                komut1.Parameters.AddWithValue("@p4", textBox4.Text);
                komut1.ExecuteNonQuery();
            }
            MessageBox.Show("Kişi başarıyla eklendi.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrolü
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki kisiNo değerini alıyoruz
                int kisiNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["kisiNo"].Value);

                using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
                {
                    baglanti.Open();
                    NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM \"kisi\" WHERE \"kisiNo\" = @p1", baglanti);
                    komut2.Parameters.AddWithValue("@p1", kisiNo); // Seçilen kişinin kisiNo değerini kullanıyoruz
                    komut2.ExecuteNonQuery();
                }
                MessageBox.Show("Kişi başarıyla silindi");
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kişiyi seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("update \"kisi\" set \"adi\"=@p1, \"soyadi\"=@p2, \"kisiTipi\"=@p3 where \"kisiNo\"=@p4", baglanti);
                komut3.Parameters.AddWithValue("@p4", int.Parse(textBox1.Text));
                komut3.Parameters.AddWithValue("@p1", textBox2.Text);
                komut3.Parameters.AddWithValue("@p2", textBox3.Text);
                komut3.Parameters.AddWithValue("@p3", textBox4.Text);
                komut3.ExecuteNonQuery();
            }
            MessageBox.Show("Kişi başarıyla güncellendi.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();
                string sorgu = "SELECT * FROM \"kisi\" WHERE \"kisiTipi\" = @p1";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                da.SelectCommand.Parameters.AddWithValue("@p1", textBox5.Text);

                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
