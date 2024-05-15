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
        MySqlDataAdapter adapter;
        MySqlCommand komut;

        string baglanticumlesi = "Server=localhost;Database=ögrenciii;Uid=root;Pwd=1234;";

        public MySqlConnection baglan()
        {

           
            
            MySqlConnection.ClearPool(connection);
            return connection;
        }
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
            
           adapter = new MySqlDataAdapter("SELECT * FROM ögrenciii.ogrenci", baglanticumlesi);
           table = new DataTable();
          connection.Open();
            adapter.Fill(table);
            gridadam.DataSource = table;
            connection.Close();


        }
        public void temizle()
        {
            txtad.Clear();
            txtno.Clear();
            txtsoyad.Clear();
            txttel.Clear();
        }
        string eklekodu;
        private void btnekle_Click(object sender, EventArgs e)
        {
            komut=new MySqlCommand();
            connection.Open();
            
            komut.Connection = connection;
            komut.CommandText= "INSERT INTO ogrenci(no,ad,soyad,tel)values ('" + txtno.Text + "','" + txtad.Text + "','" + txtsoyad.Text + "','" + txttel.Text + "')"; ;
            //eklekodu = 
            
            //komut.Parameters.AddWithValue("@no", int.Parse(txtno.Text));
           // komut.Parameters.AddWithValue("@ad",txtad.Text);
           // komut.Parameters.AddWithValue("@soyad",txtsoyad.Text);
           // komut.Parameters.AddWithValue("@tel",txttel.Text);
            komut.ExecuteNonQuery(); 
            connection.Close();
            Listele();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            komut=new MySqlCommand();
            connection.Open();
            komut.Connection = connection;
            komut.CommandText = "update ogrenci set ad='" + txtad + "',soyad='" + txtsoyad + "',tel='" + txttel.Text + "'where no='" + txtno.Text + "";
            komut.ExecuteNonQuery();
            connection.Close();
            Listele();

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            komut =new MySqlCommand();
            connection.Open();
            komut.Connection = connection;
            komut.CommandText = "delete from ogrenci where no=" + txtno + "";
            komut.ExecuteNonQuery();
            connection.Close();
            Listele();

        }

        private void txtarama_TextChanged(object sender, EventArgs e)
        {
            connection=new MySqlConnection("Server=localhost;Database=ögrenciii;Uid=root;Pwd=1234;");
            adapter=new MySqlDataAdapter("select * from ogrenci where ad like '"+txtarama.Text+"%'", connection);
            table = new DataTable();
            connection.Open();
            adapter.Fill(table);
            gridadam.DataSource = table;
            connection.Close();
        }

        private void gridadam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
