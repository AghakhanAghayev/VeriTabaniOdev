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
    public partial class KitaplarFrm : Form
    {
        public KitaplarFrm()
        {
            InitializeComponent();
        }

        // Bağlantı dizesi
        string connectionString = "server=localHost; port=5432; Database=odev; user ID=postgres; password=0817";

        private void KitaplarFrm_Load(object sender, EventArgs e)
        {
            // Kitap türleri için verileri çek
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();

                // Kitap türleri
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"kitaptur\"", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox2.DisplayMember = "turadi";
                comboBox2.ValueMember = "kitapturNo";
                comboBox2.DataSource = dt;

                // Yazarlar
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("SELECT \"yazarNo\", \"ad\" || ' ' || \"soyad\" AS ad_soyad FROM \"yazar\"", baglanti);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                comboBox1.DisplayMember = "ad_soyad";
                comboBox1.ValueMember = "yazarNo";
                comboBox1.DataSource = dt2;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from \"kitaplar\"";
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
                NpgsqlCommand komut1 = new NpgsqlCommand("insert into \"kitaplar\" (\"urunNo\", \"kitapadi\", \"yazarno\", \"turno\") values (@p1, @p2, @p3, @p4)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
                komut1.Parameters.AddWithValue("@p2", textBox2.Text);
                komut1.Parameters.AddWithValue("@p3", (int)(comboBox1.SelectedValue));
                komut1.Parameters.AddWithValue("@p4", (int)(comboBox2.SelectedValue));
                komut1.ExecuteNonQuery();
            }
            MessageBox.Show("Ürün başarıyla eklendi.");
        }
    }
}
