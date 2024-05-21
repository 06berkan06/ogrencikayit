using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Xml.Linq;

namespace ogrencikayit
{
    public partial class Form1 : Form
    {
      
        MySqlConnection connection;
        DataTable table;   
        MySqlCommand komut;
        MySqlDataReader reader;
        string baglanticumlesi = "Server=localhost;Database=ogrenci;Uid=root;Pwd=1234;";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }
       
        private void Listele()
        {


            connection = new MySqlConnection(baglanticumlesi);
            connection.Open();
            komut = new MySqlCommand("SELECT * FROM ogrenci.ogrenci", connection);

            
            
            table = new DataTable();
            reader = komut.ExecuteReader();
            table.Load(reader);



            gridadam.DataSource = table;
            connection.Close();


        }
      
        private void btnekle_Click(object sender, EventArgs e)
        {
          
            komut = new MySqlCommand();
            connection.Open();
            
            komut.Connection = connection;
            komut.CommandText= "INSERT INTO ogrenci(no,ad,soyad,tel)values (@no,@ad,@soyad,@tel)";

            komut.Parameters.AddWithValue("@no", int.Parse(txtno.Text));
            komut.Parameters.AddWithValue("@ad", txtad.Text);
            komut.Parameters.AddWithValue("@soyad", txtsoyad.Text);
            komut.Parameters.AddWithValue("@tel", txttel.Text);
            komut.ExecuteNonQuery(); 
            connection.Close();
            Listele();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            komut = new MySqlCommand();
            connection.Open();

            komut.Connection = connection;
            komut.CommandText = "update ogrenci set ad=@ad,soyad=@soyad,tel=@tel  where no=@no";
          

            komut.Parameters.AddWithValue("@no", int.Parse(txtno.Text));
            komut.Parameters.AddWithValue("@ad", txtad.Text);
            komut.Parameters.AddWithValue("@soyad", txtsoyad.Text);
            komut.Parameters.AddWithValue("@tel", txttel.Text);
            komut.ExecuteNonQuery();
            connection.Close();
            Listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            
            komut =new MySqlCommand();
            connection.Open();
            komut.Connection = connection;
            komut.CommandText = "delete from ogrenci where no=" + int.Parse(txtno.Text);
            komut.ExecuteNonQuery();
            connection.Close();
            Listele();

        }

        private void txtarama_TextChanged(object sender, EventArgs e)
        {
            connection = new MySqlConnection(baglanticumlesi);
            connection.Open();
            komut = new MySqlCommand("select * from ogrenci where ad like '" + txtarama.Text + "%'", connection);



            table = new DataTable();
            reader = komut.ExecuteReader();
            table.Load(reader);



            gridadam.DataSource = table;
            connection.Close();
          
        }

        private void gridadam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
