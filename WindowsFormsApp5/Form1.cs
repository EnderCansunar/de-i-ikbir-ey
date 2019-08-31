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


namespace WindowsFormsApp5
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        Database _ec = new Database();
        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM hastalar";
            DataTable data = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = sql;
            command.Connection = _ec.baglanti;
            adapter.SelectCommand = command;
            _ec.baglanti.Open();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
            tc.MaxLength = 11;
            telefon.MaxLength = 11;
            babatelefonu.MaxLength = 11;
            annetelefonu.MaxLength = 11;
            textBox6.MaxLength = 11;
            _ec.baglanti.Close();
        }

        private void MaterialRaisedButton2_Click(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand ekle = new MySqlCommand("insert into hastalar (dosya_no,tc,adi,soyadi,dogumtarihi,telefon,isyeri,okulu,meslegi,referans,adresi,babatelefonu,annetelefonu,babaadi,anneadi,baba_meslegi,anne_meslegi) values ('" + dosya_no.Text + "','" + tc.Text + "','" + adi.Text + "','" + soyadi.Text + "','" + dogumtarihi.Text + "','" + telefon.Text + "','" + isyeri.Text + "','" + okulu.Text + "','" + meslegi.Text + "','" + referans.Text + "','" + adresi.Text + "','" + babatelefonu.Text + "','" + annetelefonu.Text + "','" + babaadi.Text + "','" + anneadi.Text + "','" + baba_meslegi.Text + "','" + anne_meslegi.Text + "')", _ec.baglanti);
                object sonuc = null;
                sonuc = ekle.ExecuteNonQuery();
                if (sonuc != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         
                else
                    MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // bağlantıyı kapatalım
                _ec.baglanti.Close();
                string sql = "SELECT * FROM hastalar";
                DataTable data = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                command.CommandText = sql;
                command.Connection = _ec.baglanti;
                adapter.SelectCommand = command;
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            foreach (Control item in tabPage1.Controls)
            {
                if (item is RichTextBox)
                {
                    RichTextBox txt = item as RichTextBox;
                    txt.Clear();
                }
            }

        }

        private void MaterialFlatButton7_Click(object sender, EventArgs e)
        {
            foreach (Control item in tabPage2.Controls)
            {
                if (item is RichTextBox)
                {
                    RichTextBox txt = item as RichTextBox;
                    txt.Clear();
                }
                if (item is TextBox)
                {
                   TextBox txt = item as TextBox;
                    txt.Clear();
                }
            }
        }

        private void MaterialFlatButton12_Click(object sender, EventArgs e)
        {
            
            foreach (Control item in tabPage4.Controls)
                

            {
                if (item is CheckBox)
                {
                    CheckBox check = item as CheckBox;
                    check.Enabled = false;
                }
            }
        }

        private void Databasecifttık(object sender, EventArgs e)
        {
            foreach (Control item in tabPage4.Controls)


            {
                if (item is CheckBox)
                {
                    CheckBox check = item as CheckBox;
                    check.Enabled = false;
                }
            }
            richTextBox26.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString(); richTextBox19.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); richTextBox25.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString(); richTextBox24.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();richTextBox23.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString(); richTextBox33.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString(); richTextBox20.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString(); richTextBox22.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString(); richTextBox28.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString(); richTextBox18.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString(); richTextBox34.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString(); richTextBox30.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString(); richTextBox32.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString(); richTextBox31.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString(); richTextBox32.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString(); richTextBox28.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString(); richTextBox27.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString();
            materialCheckBox7.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[17].Value.ToString());// materialCheckBox6.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[18].Value.ToString()); materialCheckBox5.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[19].Value.ToString()); materialCheckBox4.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[20].Value.ToString()); materialCheckBox3.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[21].Value.ToString()); materialCheckBox2.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[22].Value.ToString()); materialCheckBox1.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[23].Value.ToString()); materialCheckBox15.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[24].Value.ToString());

        }

    }
}


   