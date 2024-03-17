using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=kutuphane.mdb");
        String kimlik; // seçilen kitabın veritabanı ID si

        void kitaplari_goster()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM kitaplar",baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            baglanti.Close();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            kitaplari_goster();

            panel1.Location = new Point(10,10);
            panel2.Location = new Point(10, 10);
            panel3.Location = new Point(10, 10);
            panel4.Location = new Point(10, 10);
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            kimlik = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            String kitapAdi = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            String yazarAdi = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            String yayinEvi = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            String sayfaSayisi = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            String konum = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            if(kitapAdi == "")
            {
                button3.Enabled = false;
                button4.Enabled = false;
            } else
            {
                button3.Enabled = true;
                button4.Enabled = true;
            }

            label19.Text = kitapAdi;
            label18.Text = yazarAdi;
            label17.Text = yayinEvi;

            textBox10.Text = kitapAdi;
            textBox9.Text = yazarAdi;
            textBox8.Text = yayinEvi;
            textBox7.Text = sayfaSayisi;
            textBox6.Text = konum;

            textBox17.Text = kitapAdi;
            textBox16.Text = yazarAdi;
            textBox15.Text = yayinEvi;
            textBox14.Text = sayfaSayisi;
            textBox13.Text = konum;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO kitaplar (kitap_adi,yazar,yayin_evi,sayfa_sayisi,yer ) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";

            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            kitaplari_goster();
            panel1.Visible = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            MessageBox.Show("Yeni Kitap Eklendi");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE kitaplar SET kitap_adi = '" + textBox10.Text + "',yazar = '" + textBox9.Text 
                + "',yayin_evi = '" + textBox8.Text + "',sayfa_sayisi = '" + textBox7.Text + "',yer = '" + textBox6.Text
                + "' WHERE kimlik = " + kimlik;

            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            kitaplari_goster();

            panel2.Visible= false;

            MessageBox.Show("Kitap Bilgileri Güncellendi.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;

            string sql = "SELECT * FROM kullanicilar WHERE kimlik = 1";
            OleDbCommand komut = new OleDbCommand(sql, baglanti);
            baglanti.Open();

            OleDbDataReader oku = komut.ExecuteReader();
            oku.Read();

            textBox11.Text = oku.GetValue(1).ToString();
            baglanti.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Enabled = false;
            string sorgu = "UPDATE kullanicilar SET kullanici_adi = '" + textBox11.Text + "',parola = '" + textBox12.Text+ "' WHERE kimlik = 1";

            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kullanıcı adı ve Parola değişti.");
            groupBox1.Visible = false;

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            button9.Enabled = true;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            button9.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM kitaplar WHERE kimlik = " + kimlik;

            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            kitaplari_goster();

            MessageBox.Show("Kitap Silindi");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
