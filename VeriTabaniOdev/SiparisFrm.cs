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
    public partial class SiparisFrm : Form
    {
        public SiparisFrm()
        {
            InitializeComponent();
        }

        // Bağlantı dizesi
        string connectionString = "server=localHost; port=5432; Database=odev; user ID=postgres; password=0817";

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM \"siparis\"";
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
                NpgsqlCommand komut1 = new NpgsqlCommand("INSERT INTO \"siparis\" (\"siparisno\", \"kisiNo\", \"faturano\", \"firmano\", \"musterino\") VALUES (@p1, @p2, @p3, @p4, @p5)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
                komut1.Parameters.AddWithValue("@p2", int.Parse(textBox2.Text));
                komut1.Parameters.AddWithValue("@p3", int.Parse(textBox3.Text));
                komut1.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));
                komut1.Parameters.AddWithValue("@p5", int.Parse(textBox5.Text));
                komut1.ExecuteNonQuery();
            }
            MessageBox.Show("Sipariş hazırlığı bilgisi başarıyla eklendi.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int siparisno = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["siparisno"].Value);

                using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
                {
                    baglanti.Open();
                    NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM \"siparis\" WHERE \"siparisno\" = @p1", baglanti);
                    komut2.Parameters.AddWithValue("@p1", siparisno);
                    komut2.ExecuteNonQuery();
                }

                MessageBox.Show("Sipariş bilgisi başarıyla silindi");

                // DataGridView'i güncellemek için veritabanındaki verileri tekrar çekiyoruz
                button1_Click(sender, e); // Verileri tekrar çekmek için button1_Click() metodu çağrılır
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz sipariş bilgisini seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("UPDATE \"siparis\" SET \"kisiNo\" = @p2, \"faturano\" = @p3, \"firmano\" = @p4, \"musterino\" = @p5 WHERE \"siparisno\" = @p6", baglanti);
                komut3.Parameters.AddWithValue("@p6", int.Parse(textBox1.Text));
                komut3.Parameters.AddWithValue("@p2", int.Parse(textBox2.Text));
                komut3.Parameters.AddWithValue("@p3", int.Parse(textBox3.Text));
                komut3.Parameters.AddWithValue("@p4", int.Parse(textBox4.Text));
                komut3.Parameters.AddWithValue("@p5", int.Parse(textBox5.Text));
                komut3.ExecuteNonQuery();
            }
            MessageBox.Show("Sipariş hazırlığı bilgisi başarıyla güncellendi.");
        }
    }
}
