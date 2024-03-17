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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=kutuphane.mdb");
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM kullanicilar WHERE kimlik = 1";
            OleDbCommand komut = new OleDbCommand(sql, baglanti);
            baglanti.Open();

            OleDbDataReader oku = komut.ExecuteReader();
            oku.Read();

            if(textBox1.Text == oku.GetValue(1).ToString() && textBox2.Text == oku.GetValue(2).ToString())
            {
                Form1 kitaplar = new Form1();
                kitaplar.ShowDialog();

            }
            
            baglanti.Close();
        }
    }
}
