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
    public partial class IletisimBilgileriFrm : Form
    {
        public IletisimBilgileriFrm()
        {
            InitializeComponent();
        }

        // Bağlantı dizesi
        string connectionString = "server=localHost; port=5432; Database=odev; user ID=postgres; password=0817";

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from \"iletisimbilgileri\"";
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
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into \"iletisimbilgileri\" (\"iletisimNo\", \"telefon\", \"adres\", \"kisiNo\") values (@p1, @p2, @p3, @p4)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
                komut1.Parameters.AddWithValue("@p2", textBox2.Text);
                komut1.Parameters.AddWithValue("@p3", textBox3.Text);
                komut1.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));
                komut1.ExecuteNonQuery();
            }
            MessageBox.Show("İletişim bilgisi başarıyla eklendi.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int iletisimNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["iletisimNo"].Value);

                using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
                {
                    baglanti.Open();
                    NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM \"iletisimbilgileri\" WHERE \"iletisimNo\" = @p1", baglanti);
                    komut2.Parameters.AddWithValue("@p1", iletisimNo);
                    komut2.ExecuteNonQuery();
                }
                MessageBox.Show("İletişim bilgisi başarıyla silindi");

                // DataGridView'i güncelle
                button1.PerformClick();
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz iletişim bilgisini seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("update \"iletisimbilgileri\" set \"telefon\"=@p1, \"adres\"=@p2, \"kisiNo\"=@p3 where \"iletisimNo\"=@p4", baglanti);
                komut3.Parameters.AddWithValue("@p4", int.Parse(textBox1.Text));
                komut3.Parameters.AddWithValue("@p1", textBox2.Text);
                komut3.Parameters.AddWithValue("@p2", textBox3.Text);
                komut3.Parameters.AddWithValue("@p3", int.Parse(textBox4.Text));
                komut3.ExecuteNonQuery();
            }
            MessageBox.Show("İletişim bilgisi başarıyla güncellendi.");
        }
    }
}
