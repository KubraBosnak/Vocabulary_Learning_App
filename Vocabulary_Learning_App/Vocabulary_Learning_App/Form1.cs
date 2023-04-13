using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Vocabulary_Learning_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Kübra\source\GitHub\Vocabulary_Learning_App\dbSozluk.accdb");

        Random rast = new Random();
        int sure = 90;
        int kelime = 0;

        void getir()
        {
            int sayi;
            sayi = rast.Next(1, 2490);
            lblCevap.Text = sayi.ToString();

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM sozluk WHERE id=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", sayi);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr[1].ToString();
                lblCevap.Text = dr[2].ToString();
                lblCevap.Text = lblCevap.Text.ToLower();
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            getir();
            textBox2.Focus();
            timer1.Start();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text==lblCevap.Text)
            {

                kelime++;
                lblKelime.Text = kelime.ToString();
                getir();
                textBox2.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            lblSure.Text = sure.ToString();
            if (sure == 0)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                timer1.Stop();
            }
        }
    }
}
