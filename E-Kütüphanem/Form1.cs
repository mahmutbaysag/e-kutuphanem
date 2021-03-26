using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace E_Kütüphanem
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            StreamReader m = new StreamReader(@"ıd.txt");
            string mt = m.ReadToEnd(); m.Close();
            int uzunluk = mt.IndexOf(" ");
            string ıd, pas;
            ıd = mt.Substring(0, uzunluk);
            pas = mt.Substring(uzunluk+1);
            if(metroTextBox1.Text == ıd)
            {
                if(metroTextBox2.Text == pas)
                {
                    Form2 f2 = new Form2();
                    f2.Show();
                    this.Hide();
                }
                else
                {
                    DialogResult msj = MetroFramework.MetroMessageBox.Show(this, "Parola Yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DialogResult ms = MetroFramework.MetroMessageBox.Show(this, "Kullanıcı Adı Yanlış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            metroTextBox1.Focus();
        }
    }
}
