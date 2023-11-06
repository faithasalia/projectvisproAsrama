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

namespace projectdataasrama
{
    public partial class Form2 : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        public Form2()
        {
            alamat = "server=localhost; database=db_projectvispro; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
        
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btncari_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtboxNama.Text != "")
                {
                    query = string.Format("select * from table_asrama where Nama = '{0}'", txtboxNama.Text);
                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    perintah.ExecuteNonQuery();
                    adapter.Fill(ds);
                    koneksi.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            txtboxNIM.Text = kolom["NIM"].ToString();
                            txtboxnotlpn1.Text = kolom["No.Telpon"].ToString();
                            txtboxnotlpn2.Text = kolom["No.Telpon OrangTua"].ToString();
                            txtboxkamar.Text = kolom["Kamar"].ToString();

                            btntambah.Enabled = false;
                            btnhapus.Enabled = true;
                            btnupdate.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("UPDATE `table_asrama` SET 'Nama'='{0}','NIM'='{1}','No.Telpon'='{2}','No.Telpon Orangtua'='{3}', 'kamar'='{4}' where 0", txtboxNama.Text, txtboxNIM.Text, txtboxnotlpn1.Text, txtboxnotlpn2.Text, txtboxkamar.Text);
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();

                Form2_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("insert into 'table_asrama' ('Nama','NIM','No.Telpon','No.Telpon Orangtua','Kamar') VALUES ('{0}','{1}', '{2}','{3}','{4}')", txtboxNama.Text, txtboxNIM.Text, txtboxnotlpn1.Text, txtboxnotlpn2.Text, txtboxkamar.Text);
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                int res = perintah.ExecuteNonQuery();

                koneksi.Close();
                if (res == 1)
                {
                    MessageBox.Show("Insert data success");
                    Form2_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Insert data Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnhapus_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("DELETE FROM 'table_asrama' where Nama = '{0}'", txtboxNama.Text);
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnkembali_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();

            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Form2_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select * from table_asrama");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 150;
                dataGridView1.Columns[0].HeaderText = "Nama";
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[1].HeaderText = "NIM";
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[2].HeaderText = "No.Telpon";
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[3].HeaderText = "No.Telpon OrangTua";
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[4].HeaderText = "Kamar";

                txtboxNama.Clear();
                txtboxNIM.Clear();
                txtboxnotlpn1.Clear();
                txtboxnotlpn2.Clear();
                txtboxkamar.Clear();

                btncari.Enabled = false;
                btntambah.Enabled = false;
                btnhapus.Enabled = false;
                btnupdate.Enabled = false;
                btnClear.Enabled = false;
                btncari.Enabled = true;
                btntambah.Enabled = true;
                btnupdate.Enabled = true;
                btnClear.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

