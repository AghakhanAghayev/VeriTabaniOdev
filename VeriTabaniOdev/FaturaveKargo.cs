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
    public partial class FaturaveKargo : Form
    {
        public FaturaveKargo()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=odev; user ID=postgres; password=0817");

        private void button8_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kargofirmasi";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from fatura";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into \"fatura\" (\"faturano\", \"tarih\") values (@p1, @p2)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
                komut1.Parameters.AddWithValue("@p2", DateTime.Parse(maskedTextBox1.Text)); // Tarih formatına dikkat edin.
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Fatura bilgisi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("update \"fatura\" set \"tarih\"=@p1 where \"faturano\"=@p2", baglanti);
                komut3.Parameters.AddWithValue("@p2", int.Parse(textBox1.Text));
                komut3.Parameters.AddWithValue("@p1", DateTime.Parse(maskedTextBox1.Text)); // Tarih formatına dikkat edin.
                komut3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Fatura bilgisi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("insert into \"kargofirmasi\" (\"firmano\", \"kisiNo\", \"firmaAd\") values (@p1, @p2, @p3)", baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(textBox2.Text));
                komut2.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text));
                komut2.Parameters.AddWithValue("@p3", textBox4.Text);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kargo firmasi bilgisi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
