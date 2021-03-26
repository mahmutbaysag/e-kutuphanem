using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace E_Kütüphanem
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        OleDbCommand komut;
        OleDbConnection baglanti;
        OleDbDataAdapter adaptor;
        DataSet veri;

        void vericagir()
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0; Data Source=kitaplarım.accdb");
            adaptor = new OleDbDataAdapter("Select * from Kitaplar", baglanti);
            veri = new DataSet();
            baglanti.Open();
            adaptor.Fill(veri, "Kitaplar");
            metroGrid1.DataSource = veri.Tables["Kitaplar"];
            baglanti.Close();
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            vericagir();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            if(metroTextBox1.Text == "")
            {
                DialogResult ms = MetroFramework.MetroMessageBox.Show(this, "Lütfen Tüm Bilgileri Giriniz!", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                komut = new OleDbCommand();
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "insert into Kitaplar (Ad, Türü, Yazar, Tarih, Sayfa, Durum) values(@kadı, @ktürü, @kyazarı, @tarih, @sayfa, @durum)";

                komut.Parameters.AddWithValue("@kadı", Convert.ToString(metroTextBox1.Text));
                komut.Parameters.AddWithValue("@ktürü", Convert.ToString(metroTextBox2.Text));
                komut.Parameters.AddWithValue("@kyazarı", Convert.ToString(metroTextBox3.Text));
                komut.Parameters.AddWithValue("@tarih", Convert.ToString(metroDateTime1.Text));
                komut.Parameters.AddWithValue("@sayfa", Convert.ToString(metroTextBox4.Text));
                komut.Parameters.AddWithValue("@durum", Convert.ToString(metroComboBox1.Text));


                komut.ExecuteNonQuery();
                baglanti.Close();
                vericagir();
                DialogResult msj;
                msj = MetroFramework.MetroMessageBox.Show(this, "Yeni Kitap Eklendi", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            if(metroTextBox1.Text == "")
            {
                DialogResult ms = MetroFramework.MetroMessageBox.Show(this, "Lütfen Tüm Bilgileri Giriniz!", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                string sorgu = "Update Kitaplar Set Ad=@ad,Türü=@tür,Yazar=@yazar,tarih=@tarih,sayfa=@sayfa,durum=@durum Where Kimlik=@ıd";
                komut = new OleDbCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@ad", Convert.ToString(metroTextBox1.Text));
                komut.Parameters.AddWithValue("@tür", Convert.ToString(metroTextBox2.Text));
                komut.Parameters.AddWithValue("@yazar", Convert.ToString(metroTextBox3.Text));
                komut.Parameters.AddWithValue("@tarih", Convert.ToString(metroDateTime1.Text));
                komut.Parameters.AddWithValue("@sayfa", Convert.ToString(metroTextBox4.Text));
                komut.Parameters.AddWithValue("@durum", Convert.ToString(metroComboBox1.Text));

                komut.Parameters.AddWithValue("@ıd", metroGrid1.CurrentRow.Cells[0].Value.ToString());
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                vericagir();

                DialogResult msj = MetroFramework.MetroMessageBox.Show(this, "Kitap Güncellendi.", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete From Kitaplar Where Kimlik=@no";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@no", metroGrid1.CurrentRow.Cells[0].Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            vericagir();
            DialogResult msj = MetroFramework.MetroMessageBox.Show(this, "Kitap Silindi.", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void MetroGrid1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            metroTextBox1.Text = metroGrid1.CurrentRow.Cells[1].Value.ToString();
            metroTextBox2.Text = metroGrid1.CurrentRow.Cells[2].Value.ToString();
            metroTextBox3.Text = metroGrid1.CurrentRow.Cells[3].Value.ToString();
            metroDateTime1.Text = metroGrid1.CurrentRow.Cells[4].Value.ToString();
            metroTextBox4.Text = metroGrid1.CurrentRow.Cells[5].Value.ToString();
            metroComboBox1.Text = metroGrid1.CurrentRow.Cells[6].Value.ToString();
        }

        private void MetroButton4_Click(object sender, EventArgs e)
        {
            metroTextBox1.Text = "";
            metroTextBox2.Text = "";
            metroTextBox3.Text = "";
            metroTextBox4.Text = "";
            metroComboBox1.Text = "";
        }
    }
}
