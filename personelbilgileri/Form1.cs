using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace personelbilgileri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        SqlConnection bağlantı= new SqlConnection("Data Source=DESKTOP-LKN23GJ\\SQLEXPRESS;Initial Catalog=personeltakip;Integrated Security=True");
        
        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select *from Personelbilgi", bağlantı);
            DataSet ds =new DataSet();
            da.Fill(ds,"sanaltablo");
            dataGridView1.DataSource=ds.Tables["sanaltablo"];

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            bağlantı.Open();
            SqlCommand komut = new SqlCommand("insert into Personelbilgi(personelno,personelad,personelsoyad,gırıstarıhı,maas) values(@no,@ad,@soyad,@tarih,@maas)", bağlantı);
            komut.Parameters.AddWithValue("@no",txtno.Text);
            komut.Parameters.AddWithValue("@ad",txtad.Text);   
            komut.Parameters.AddWithValue("@soyad",txtsoyad.Text);
            komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@maas", txtmaas.Text);
            komut.ExecuteNonQuery();
            bağlantı.Close();
             Temizle();
               
          

        }
       void Temizle()
        {
            txtno.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            txtmaas.Text = "";
            txtarama.Text = "";


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtmaas.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnsıl_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = new DialogResult();
            sonuc = MessageBox.Show("Bu kaydı silmek istediğinize eminmisiniz", "Silme işlemi", MessageBoxButtons.YesNoCancel);
            if (sonuc == DialogResult.Yes)
            {
                bağlantı.Open();
                SqlCommand komut = new SqlCommand("Delete from Personelbilgi where personelno=@no", bağlantı);
                komut.Parameters.AddWithValue("@no",txtno.Text);
                komut.ExecuteNonQuery();
                bağlantı.Close();
                Temizle();

            }
        }
    }
}
