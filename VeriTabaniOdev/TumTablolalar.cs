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
    public partial class TumTablolalar : Form
    {
        public TumTablolalar()
        {
            InitializeComponent();
        }

        private void TumTablolalar_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            IletisimBilgileriFrm iletisimBilgileriFrm=new IletisimBilgileriFrm();
            iletisimBilgileriFrm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kisiler kisiler = new Kisiler();
            kisiler.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusteriFrm mstr=new MusteriFrm();
            mstr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PersonelFrm personel = new PersonelFrm();
            personel.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();    
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            KitaplarFrm kitaplar = new KitaplarFrm();
            kitaplar.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            KitapTurleri kitapTurleri = new KitapTurleri();
            kitapTurleri.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            YazarlarFrm yazarlarFrm = new YazarlarFrm();
            yazarlarFrm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ElektronikEsyalarFrm elektronikEsyalarFrm=new ElektronikEsyalarFrm();
            elektronikEsyalarFrm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            VideoOyunlari videoOyunlari = new VideoOyunlari();
            videoOyunlari.Show();   
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FaturaveKargo faturaveKargo = new FaturaveKargo();
            faturaveKargo.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            UrunSipairFrm urunsiparis = new UrunSipairFrm();
            urunsiparis.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SiparisFrm siparisFrm = new SiparisFrm();
                siparisFrm.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
            fonksiyonlar.Show();
        }
    }
}
