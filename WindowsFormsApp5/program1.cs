using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;



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
            //this.Location = new Point(0,0);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            _ec.baglanti.Close();
            string sql = "SELECT * FROM hastalar ";
            DataTable data = new DataTable();
            DataTable muayenesayfası = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlDataAdapter muayenesayfası1 = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand
            {
                CommandText = sql,
                Connection = _ec.baglanti
            };
            MySqlCommand muayenesayfası11 = new MySqlCommand
            {
                CommandText = "select* from muayene ",
                Connection = _ec.baglanti

            };

            adapter.SelectCommand = command;
            muayenesayfası1.SelectCommand = muayenesayfası11;
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            adapter.Fill(data);
            muayenesayfası1.Fill(muayenesayfası);
            dataGridView1.DataSource = data;
            dataGridView4.DataSource = muayenesayfası;
            tc.MaxLength = 11;
            telefon.MaxLength = 11;
            babatelefonu.MaxLength = 11;
            annetelefonu.MaxLength = 11;
            textBox6.MaxLength = 11;
            textBox12.MaxLength = 11;
            //hastakayittarihi.Text = DateTime.Now.ToShortDateString();
            richTextBox19.MaxLength = 11;
            richTextBox30.MaxLength = 11;
            richTextBox29.MaxLength = 11;
            richTextBox33.MaxLength = 11;
            _ec.baglanti.Close();
            MySqlCommand komut = new MySqlCommand();
            komut.CommandText = " select hastalar.dosya_no from hastalar order by hastalar.dosya_no desc limit 1 ";
            komut.Connection = _ec.baglanti;
            _ec.baglanti.Open();
            komut.ExecuteNonQuery();  //ExecuteNonQuery();
            MySqlDataReader dosyanooku = komut.ExecuteReader();
            if (dosyanooku.Read())
            {
                dosya_no.Text = ((Convert.ToInt32(dosyanooku["dosya_no"]) + 1)).ToString();
            }
            else
            {
                dosya_no.Text = "veri cekilemedi";
            }



            _ec.baglanti.Close();


            dataGridView1.ForeColor = Color.Black;
            dataGridView2.ForeColor = Color.Black;
            dataGridView4.ForeColor = Color.Black;

        }

        private void MaterialRaisedButton2_Click(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand ekle = new MySqlCommand("insert into hastalar  (dosya_no,tc,adi,soyadi,dogumtarihi,telefon,isyeri,okulu,meslegi,referans,adresi,babatelefonu,annetelefonu,babaadi,anneadi,baba_meslegi,anne_meslegi,hastakayittarihi,toplamucret,ikametyeri,notlar) values ('" + dosya_no.Text + "','" + tc.Text + "','" + adi.Text + "','" + soyadi.Text + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "','" + telefon.Text + "','" + isyeri.Text + "','" + okulu.Text + "','" + meslegi.Text + "','" + referans.Text + "','" + adresi.Text + "','" + babatelefonu.Text + "','" + annetelefonu.Text + "','" + babaadi.Text + "','" + anneadi.Text + "','" + baba_meslegi.Text + "','" + anne_meslegi.Text + "','" + dateTimePicker47.Value.ToString("yyyy-MM-dd") + "','" + toplamucret.Text + "' ,'" + ikametyeri.Text + "','" + richTextBox62.Text + "')", _ec.baglanti);
                MySqlCommand ekle1 = new MySqlCommand("insert into borclar (tc,borcm,tarih,borclar.borcno) values ('" + tc.Text + "','" + toplamucret.Text + "','" + dateTimePicker47.Value.ToString("yyyy-MM-dd") + "','" + dosya_no.Text + "')", _ec.baglanti);
                MySqlCommand ekle2 = new MySqlCommand("insert into odemeler  ( odememiktari, tc,borcno, odemetarihi) values ('" + richTextBox2.Text + "','" + tc.Text + "','" + dosya_no.Text + "','" + dateTimePicker47.Value.ToString("yyyy-MM-dd") + "')", _ec.baglanti);

                object sonuc = null;
                object sonuc1 = null;
                object sonuc2 = null;
                sonuc = ekle.ExecuteNonQuery();
                sonuc1 = ekle1.ExecuteNonQuery();
                sonuc2 = ekle2.ExecuteNonQuery();

                if (sonuc != null & sonuc1 != null & sonuc2 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // bağlantıyı kapatalım
                _ec.baglanti.Close();
                //MySqlCommand commandborclar = new MySqlCommand
                //{
                //    CommandText = "select dosya_no, odemeler.tc, adi, soyadi, borclar.borcm, odememiktari, odemetarihi, odemeler.odemeyialan, odemeler.odemeno from odemeler , borclar ",
                //    Connection = _ec.baglanti

                //};
                string sql = "SELECT * FROM hastalar";
                DataTable data = new DataTable();
                // DataTable databorclar = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                //MySqlDataAdapter adapterborclar = new MySqlDataAdapter
                //{
                //    SelectCommand = commandborclar
                //};
                command.CommandText = sql;
                command.Connection = _ec.baglanti;
                adapter.SelectCommand = command;
                adapter.Fill(data);
                //  adapterborclar.Fill(databorclar);
                dataGridView1.DataSource = data;
                //dataGridView2.DataSource = databorclar;
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//Yeni hasta kaydet

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            foreach (Control item in tabPage1.Controls)
            {
                if (item is RichTextBox)
                {
                    RichTextBox txt = item as RichTextBox;
                    txt.Clear();
                }
                dateTimePicker2.Value = DateTime.Today;
                dateTimePicker47.Value = DateTime.Today;
                _ec.baglanti.Close();
                MySqlCommand komut = new MySqlCommand();
                komut.CommandText = " select hastalar.dosya_no from hastalar order by hastalar.dosya_no desc limit 1 ";
                komut.Connection = _ec.baglanti;
                _ec.baglanti.Open();
                komut.ExecuteNonQuery();
                MySqlDataReader dosyanooku = komut.ExecuteReader();
                if (dosyanooku.Read())
                {
                    dosya_no.Text = ((Convert.ToInt32(dosyanooku["dosya_no"]) + 1)).ToString();
                }
                else
                {
                    dosya_no.Text = "veri cekilemedi";
                }
                richTextBox2.Text = "0";
                toplamucret.Text = "";
            }


        }//Yeni hasta sayfasını temizle

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
        }//Hasta bilgileri temizle

        //private void MaterialFlatButton12_Click(object sender, EventArgs e)
        //{

        //    foreach (Control item in tabPage4.Controls)


        //    {
        //        if (item is CheckBox)
        //        {
        //            CheckBox check = item as CheckBox;
        //            check.Enabled = false;
        //        }


        //    }


        //}

        private void Databasecifttık(object sender, EventArgs e)
        {

            textBox13.Clear();
            materialLabel52.Text = "".ToString();
            materialLabel54.Text = "".ToString();
            materialLabel53.Text = "".ToString();
            materialLabel56.Text = "".ToString();
            materialLabel57.Text = "".ToString();
            materialLabel58.Text = "".ToString();
            materialLabel59.Text = "".ToString();
            richTextBox26.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString(); richTextBox19.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); richTextBox25.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString(); richTextBox24.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString(); richTextBox33.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString(); richTextBox20.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString(); richTextBox22.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString(); richTextBox28.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString(); richTextBox18.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString(); richTextBox34.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString(); richTextBox30.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString(); richTextBox29.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString(); richTextBox32.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString(); richTextBox31.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString(); richTextBox32.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString(); richTextBox28.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString(); richTextBox27.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString(); richTextBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); richTextBox8.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString(); richTextBox7.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString(); richTextBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            richTextBox9.Text = Convert.ToInt32(dataGridView1.CurrentRow.Cells[69].Value).ToString(); textBox13.Text = dataGridView1.CurrentRow.Cells[30].Value.ToString();
            materialCheckBox7.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[17].Value.ToString()); materialCheckBox6.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[18].Value.ToString()); materialCheckBox5.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[19].Value.ToString()); materialCheckBox4.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[20].Value.ToString()); materialCheckBox3.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[21].Value.ToString()); materialCheckBox2.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[22].Value.ToString()); materialCheckBox1.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[23].Value.ToString()); materialCheckBox15.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[24].Value.ToString()); materialCheckBox14.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[25].Value.ToString()); materialCheckBox13.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[26].Value.ToString()); materialCheckBox12.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[27].Value.ToString()); materialCheckBox17.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[28].Value.ToString()); materialCheckBox16.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[29].Value.ToString()); materialCheckBox18.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[31].Value.ToString()); materialCheckBox20.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[32].Value.ToString()); materialCheckBox22.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[33].Value.ToString()); materialCheckBox23.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[34].Value.ToString()); materialCheckBox19.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[35].Value.ToString()); materialCheckBox21.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[36].Value.ToString()); materialCheckBox24.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[37].Value.ToString()); materialCheckBox25.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[38].Value.ToString()); materialCheckBox28.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[39].Value.ToString()); materialCheckBox27.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[40].Value.ToString()); materialCheckBox26.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[41].Value.ToString()); materialCheckBox29.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[42].Value.ToString()); materialCheckBox30.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[43].Value.ToString()); materialCheckBox31.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[44].Value.ToString()); materialCheckBox32.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[45].Value.ToString()); materialCheckBox53.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[46].Value.ToString()); materialCheckBox33.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[47].Value.ToString()); materialCheckBox34.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[48].Value.ToString()); materialCheckBox35.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[49].Value.ToString()); materialCheckBox36.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[50].Value.ToString()); materialCheckBox37.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[51].Value.ToString()); materialCheckBox38.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[52].Value.ToString()); materialCheckBox39.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[53].Value.ToString()); materialCheckBox40.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[54].Value.ToString()); materialCheckBox42.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[55].Value.ToString()); materialCheckBox43.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[56].Value.ToString()); materialCheckBox44.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[57].Value.ToString()); materialCheckBox45.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[58].Value.ToString()); materialCheckBox46.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[59].Value.ToString()); materialCheckBox47.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[60].Value.ToString()); materialCheckBox48.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[61].Value.ToString()); materialCheckBox18.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[31].Value.ToString()); materialCheckBox49.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[62].Value.ToString()); materialCheckBox50.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[63].Value.ToString()); materialCheckBox51.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[64].Value.ToString()); materialCheckBox52.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[65].Value.ToString()); materialCheckBox54.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[66].Value.ToString()); materialCheckBox41.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[67].Value.ToString()); materialCheckBox8.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[70].Value.ToString()); materialCheckBox9.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[71].Value.ToString()); materialCheckBox10.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[72].Value.ToString()); materialCheckBox11.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[73].Value.ToString());
            richTextBox21.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString(); richTextBox1.Text = dataGridView1.CurrentRow.Cells[74].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dateTimePicker3.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            dateTimePicker63.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[68].Value.ToString());


            richTextBox3.Text = dataGridView1.CurrentRow.Cells[76].Value.ToString();
            richTextBox4.Text = dataGridView1.CurrentRow.Cells[77].Value.ToString();
            richTextBox10.Text = dataGridView1.CurrentRow.Cells[78].Value.ToString();
            richTextBox11.Text = dataGridView1.CurrentRow.Cells[79].Value.ToString();
            richTextBox12.Text = dataGridView1.CurrentRow.Cells[80].Value.ToString();
            richTextBox13.Text = dataGridView1.CurrentRow.Cells[81].Value.ToString();
            richTextBox14.Text = dataGridView1.CurrentRow.Cells[82].Value.ToString();
            richTextBox15.Text = dataGridView1.CurrentRow.Cells[83].Value.ToString();
            richTextBox16.Text = dataGridView1.CurrentRow.Cells[84].Value.ToString();
            richTextBox42.Text = dataGridView1.CurrentRow.Cells[85].Value.ToString();
            richTextBox41.Text = dataGridView1.CurrentRow.Cells[86].Value.ToString();
            richTextBox40.Text = dataGridView1.CurrentRow.Cells[87].Value.ToString();
            richTextBox48.Text = dataGridView1.CurrentRow.Cells[88].Value.ToString();
            richTextBox47.Text = dataGridView1.CurrentRow.Cells[89].Value.ToString();
            richTextBox46.Text = dataGridView1.CurrentRow.Cells[90].Value.ToString();
            richTextBox45.Text = dataGridView1.CurrentRow.Cells[91].Value.ToString();
            richTextBox44.Text = dataGridView1.CurrentRow.Cells[92].Value.ToString();
            richTextBox43.Text = dataGridView1.CurrentRow.Cells[93].Value.ToString();
            richTextBox39.Text = dataGridView1.CurrentRow.Cells[94].Value.ToString();
            richTextBox38.Text = dataGridView1.CurrentRow.Cells[95].Value.ToString();
            richTextBox37.Text = dataGridView1.CurrentRow.Cells[96].Value.ToString();
            richTextBox36.Text = dataGridView1.CurrentRow.Cells[97].Value.ToString();
            richTextBox35.Text = dataGridView1.CurrentRow.Cells[98].Value.ToString();
            richTextBox17.Text = dataGridView1.CurrentRow.Cells[99].Value.ToString();
            richTextBox60.Text = dataGridView1.CurrentRow.Cells[100].Value.ToString();
            richTextBox59.Text = dataGridView1.CurrentRow.Cells[101].Value.ToString();
            richTextBox58.Text = dataGridView1.CurrentRow.Cells[102].Value.ToString();
            richTextBox57.Text = dataGridView1.CurrentRow.Cells[103].Value.ToString();
            richTextBox56.Text = dataGridView1.CurrentRow.Cells[104].Value.ToString();
            richTextBox55.Text = dataGridView1.CurrentRow.Cells[105].Value.ToString();
            richTextBox54.Text = dataGridView1.CurrentRow.Cells[106].Value.ToString();
            richTextBox53.Text = dataGridView1.CurrentRow.Cells[107].Value.ToString();
            richTextBox52.Text = dataGridView1.CurrentRow.Cells[108].Value.ToString();
            richTextBox51.Text = dataGridView1.CurrentRow.Cells[109].Value.ToString();
            richTextBox50.Text = dataGridView1.CurrentRow.Cells[110].Value.ToString();
            richTextBox49.Text = dataGridView1.CurrentRow.Cells[111].Value.ToString();
            richTextBox61.Text = dataGridView1.CurrentRow.Cells[149].Value.ToString();
            richTextBox75.Text = dataGridView1.CurrentRow.Cells[150].Value.ToString();

            richTextBox74.Text = dataGridView1.CurrentRow.Cells[151].Value.ToString();
            richTextBox73.Text = dataGridView1.CurrentRow.Cells[152].Value.ToString();
            richTextBox72.Text = dataGridView1.CurrentRow.Cells[153].Value.ToString();
            richTextBox71.Text = dataGridView1.CurrentRow.Cells[154].Value.ToString();
            richTextBox70.Text = dataGridView1.CurrentRow.Cells[155].Value.ToString();
            richTextBox69.Text = dataGridView1.CurrentRow.Cells[156].Value.ToString();
            richTextBox68.Text = dataGridView1.CurrentRow.Cells[157].Value.ToString();
            richTextBox67.Text = dataGridView1.CurrentRow.Cells[158].Value.ToString();
            richTextBox66.Text = dataGridView1.CurrentRow.Cells[159].Value.ToString();
            richTextBox65.Text = dataGridView1.CurrentRow.Cells[160].Value.ToString();
            richTextBox64.Text = dataGridView1.CurrentRow.Cells[161].Value.ToString();

            if (dataGridView1.CurrentRow.Cells[75].Value.ToString() == "")
            {
                dateTimePicker64.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker64.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[75].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[112].Value.ToString() == "")
            {
                dateTimePicker4.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker4.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[112].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[113].Value.ToString() == "")
            {
                dateTimePicker5.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker5.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[113].Value.ToString());

            }


            if (dataGridView1.CurrentRow.Cells[114].Value.ToString() == "")
            {
                dateTimePicker7.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker7.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[114].Value.ToString());

            }

            if (dataGridView1.CurrentRow.Cells[115].Value.ToString() == "")
            {
                dateTimePicker6.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker6.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[115].Value.ToString());

            }

            if (dataGridView1.CurrentRow.Cells[116].Value.ToString() == "")
            {
                dateTimePicker9.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker9.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[116].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[117].Value.ToString() == "")
            {
                dateTimePicker8.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker8.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[117].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[118].Value.ToString() == "")
            {
                dateTimePicker11.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker11.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[118].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[119].Value.ToString() == "")
            {
                dateTimePicker10.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker10.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[119].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[120].Value.ToString() == "")
            {
                dateTimePicker13.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker13.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[120].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[121].Value.ToString() == "")
            {
                dateTimePicker12.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker12.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[121].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[122].Value.ToString() == "")
            {
                dateTimePicker15.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker15.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[122].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[123].Value.ToString() == "")
            {
                dateTimePicker14.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker14.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[123].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[124].Value.ToString() == "")
            {
                dateTimePicker27.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker27.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[124].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[125].Value.ToString() == "")
            {
                dateTimePicker26.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker26.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[125].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[126].Value.ToString() == "")
            {
                dateTimePicker25.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker25.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[126].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[127].Value.ToString() == "")
            {
                dateTimePicker24.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker24.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[127].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[128].Value.ToString() == "")
            {
                dateTimePicker23.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker23.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[128].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[129].Value.ToString() == "")
            {
                dateTimePicker22.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker22.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[129].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[130].Value.ToString() == "")
            {
                dateTimePicker21.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker21.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[130].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[131].Value.ToString() == "")
            {
                dateTimePicker20.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker20.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[131].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[132].Value.ToString() == "")
            {
                dateTimePicker19.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker19.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[132].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[133].Value.ToString() == "")
            {
                dateTimePicker18.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker18.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[133].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[134].Value.ToString() == "")
            {
                dateTimePicker17.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker17.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[134].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[135].Value.ToString() == "")
            {
                dateTimePicker16.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker16.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[135].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[136].Value.ToString() == "")
            {
                dateTimePicker39.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker39.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[136].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[137].Value.ToString() == "")
            {
                dateTimePicker38.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker38.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[137].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[138].Value.ToString() == "")
            {
                dateTimePicker37.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker37.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[138].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[139].Value.ToString() == "")
            {
                dateTimePicker36.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker36.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[139].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[140].Value.ToString() == "")
            {
                dateTimePicker35.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker35.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[140].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[141].Value.ToString() == "")
            {
                dateTimePicker34.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker34.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[141].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[142].Value.ToString() == "")
            {
                dateTimePicker33.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker33.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[142].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[143].Value.ToString() == "")
            {
                dateTimePicker32.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker32.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[143].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[144].Value.ToString() == "")
            {
                dateTimePicker31.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker31.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[144].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[145].Value.ToString() == "")
            {
                dateTimePicker30.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker30.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[145].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[146].Value.ToString() == "")
            {
                dateTimePicker29.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker29.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[146].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[147].Value.ToString() == "")
            {
                dateTimePicker28.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker28.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[147].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[162].Value.ToString() == "")
            {
                dateTimePicker62.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker62.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[162].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[163].Value.ToString() == "")
            {
                dateTimePicker61.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker61.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[163].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[164].Value.ToString() == "")
            {
                dateTimePicker60.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker60.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[164].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[165].Value.ToString() == "")
            {
                dateTimePicker59.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker59.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[165].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[166].Value.ToString() == "")
            {
                dateTimePicker58.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker58.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[166].Value.ToString());
            }

            if (dataGridView1.CurrentRow.Cells[167].Value.ToString() == "")
            {
                dateTimePicker57.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker57.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[167].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[168].Value.ToString() == "")
            {
                dateTimePicker56.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker56.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[168].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[169].Value.ToString() == "")
            {
                dateTimePicker55.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker55.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[169].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[170].Value.ToString() == "")
            {
                dateTimePicker54.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker54.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[170].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[171].Value.ToString() == "")
            {
                dateTimePicker53.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker53.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[171].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[172].Value.ToString() == "")
            {
                dateTimePicker52.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker52.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[172].Value.ToString());
            }
            if (dataGridView1.CurrentRow.Cells[173].Value.ToString() == "")
            {
                dateTimePicker51.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker51.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[173].Value.ToString());
            }
        }//Database çifttık

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            if (textBox1.Text.Trim() == "")
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM hastalar", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec.baglanti.Close();
            }
            else
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM hastalar where adi like '%" + textBox1.Text + "%'", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec.baglanti.Close();
            }
        } // ad ile arama hasta bilgileri

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            if (textBox2.Text.Trim() == "")
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM hastalar", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec.baglanti.Close();
            }
            else
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM hastalar where soyadi like '%" + textBox2.Text + "%'", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec.baglanti.Close();
            }
        }//soyad ile arama hasta bilgileri

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            if (textBox3.Text.Trim() == "")
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM hastalar", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec.baglanti.Close();
            }
            else
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM hastalar where tc like '%" + textBox3.Text + "%'", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                _ec.baglanti.Close();
            }
        }//tc arama hasta bilgileri

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            if (textBox4.Text.Trim() == "")
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter4 = new MySqlDataAdapter("SELECT * FROM hastalar", _ec.baglanti);
                DataTable data4 = new DataTable();
                adapter4.Fill(data4);
                dataGridView1.DataSource = data4;
                _ec.baglanti.Close();
            }
            else
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter4 = new MySqlDataAdapter("SELECT * FROM hastalar where dosya_no like '%" + textBox4.Text + "%'", _ec.baglanti);
                DataTable data4 = new DataTable();
                adapter4.Fill(data4);
                dataGridView1.DataSource = data4;
                _ec.baglanti.Close();
            }
        }//dosya no ile arama hasta bilgileri

        private void textBox6_TextChanged(object sender, EventArgs e)//ödemede textbox değişince
        {
            _ec.baglanti.Close();
            materialLabel52.Text = "".ToString();
            materialLabel54.Text = "".ToString();
            materialLabel53.Text = "".ToString();
            materialLabel56.Text = "".ToString();
            materialLabel57.Text = "".ToString();
            materialLabel58.Text = "".ToString();
            materialLabel59.Text = "".ToString();

            if (textBox6.Text.Trim() == "")
            {
                _ec.baglanti.Open();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter();
                MySqlCommand commandborclar = new MySqlCommand
                {
                    CommandText = "select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari, odemetarihi, odemeler.odemeyialan, odemeler.odemeno from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where hastalar.tc= '" + textBox6.Text + "'",
                    Connection = _ec.baglanti

                };
                adapterborclar.SelectCommand = commandborclar;
                adapterborclar.Fill(databorclar);
                dataGridView2.DataSource = databorclar;
                _ec.baglanti.Close();
                materialLabel52.Text = "".ToString();
                materialLabel54.Text = "".ToString();
                materialLabel53.Text = "".ToString();
                materialLabel56.Text = "".ToString();
                materialLabel57.Text = "".ToString();
                materialLabel58.Text = "".ToString();
                materialLabel59.Text = "".ToString();


            }
            else
            {
                _ec.baglanti.Open();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter();
                MySqlCommand commandborclar = new MySqlCommand
                {

                    CommandText = "select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari, odemetarihi, odemeler.odemeyialan, odemeler.odemeno from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where hastalar.tc= '" + textBox6.Text + "'",
                    // CommandText = "select	odemeler.borcno	, odemeler.odememiktari , odemeler.odemetarihi, hastalar.adi	, hastalar.soyadi, hastalar.tc  from odemeler , borclar , hastalar where odemeler.borcno = borclar.borcno and borclar.tc= '" + textBox6.Text + "' and borclar.tc = hastalar.tc",
                    //CommandText = "select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari,odemetarihi from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.tc=odemeler.tc  where hastalar.tc= '" + textBox6.Text + "'",

                    Connection = _ec.baglanti

                };
                adapterborclar.SelectCommand = commandborclar;
                adapterborclar.Fill(databorclar);
                dataGridView2.DataSource = databorclar;
                _ec.baglanti.Close();


            }



        }// tc ile ödeme sorgusu

        private void materialFlatButton9_Click(object sender, EventArgs e)// tc label ile ödeme sorgu
        {
            try
            {

                decimal borc = 0;
                decimal odeme = 0;
                _ec.baglanti.Open();
                MySqlCommand cmd = new MySqlCommand("select sum(borclar.borcm) as borctoplami from borclar , hastalar where    borclar.tc= '" + textBox6.Text + "' and borclar.tc = hastalar.tc", _ec.baglanti);
                int Count = Convert.ToInt32(cmd.ExecuteScalar());
                if (Count != 0)
                {
                    materialLabel56.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    materialLabel57.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    materialLabel58.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    materialLabel59.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                    MySqlDataReader oku = cmd.ExecuteReader();


                    while (oku.Read())
                    {
                        borc = Convert.ToDecimal(oku["borctoplami"]);

                    }

                }

                _ec.baglanti.Close();
                _ec.baglanti.Open();
                cmd = new MySqlCommand("select sum(odemeler.odememiktari) as odemetoplami from odemeler, borclar , hastalar where borclar.tc = odemeler.tc and  borclar.tc= '" + textBox6.Text + "' and borclar.tc = hastalar.tc", _ec.baglanti);
                Count = Convert.ToInt32(cmd.ExecuteScalar());
                if (Count != 0)
                {
                    MySqlDataReader oku = cmd.ExecuteReader();
                    while (oku.Read())
                    {
                        odeme = Convert.ToDecimal(oku["odemetoplami"]);

                    }
                }

                _ec.baglanti.Close();
                materialLabel52.Text = (borc).ToString();
                materialLabel54.Text = (odeme).ToString();
                materialLabel53.Text = (borc - odeme).ToString();

            }
            catch
            {
                MessageBox.Show("Tc yanlış veya hatalı Lütfen Tekrar deneyiniz");
                materialLabel52.Text = "".ToString();
                materialLabel54.Text = "".ToString();
                materialLabel53.Text = "".ToString();
            }
        }

        private void materialCheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox7.Checked == true)
            {
                MySqlCommand muayene = new MySqlCommand(" update hastalar set muayene='True' where tc=@tc", _ec.baglanti);
                muayene.Parameters.AddWithValue("@tc", richTextBox19.Text);
                muayene.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand muayene = new MySqlCommand(" update hastalar set muayene='False' where tc=@tc", _ec.baglanti);
                muayene.Parameters.AddWithValue("@tc", richTextBox19.Text);
                muayene.ExecuteNonQuery();

            }
            _ec.baglanti.Close();



        }//muayene  değişim

        private void materialCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox6.Checked == true)
            {
                MySqlCommand dosyadolduruldu = new MySqlCommand(" update hastalar set dosyadolduruldu='True' where tc=@tc", _ec.baglanti);
                dosyadolduruldu.Parameters.AddWithValue("@tc", richTextBox19.Text);
                dosyadolduruldu.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand dosyadolduruldu = new MySqlCommand(" update hastalar set dosyadolduruldu='False' where tc=@tc", _ec.baglanti);
                dosyadolduruldu.Parameters.AddWithValue("@tc", richTextBox19.Text);
                dosyadolduruldu.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }//dosyadolduruldu  değişim

        private void materialCheckBox5_CheckedChanged(object sender, EventArgs e)// Rönten çekildi  değişim

        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox6.Checked == true)
            {
                MySqlCommand rontgencekildi = new MySqlCommand(" update hastalar set rontgencekildi='True' where tc=@tc", _ec.baglanti);
                rontgencekildi.Parameters.AddWithValue("@tc", richTextBox19.Text);
                rontgencekildi.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand rontgencekildi = new MySqlCommand(" update hastalar set rontgencekildi='False' where tc=@tc", _ec.baglanti);
                rontgencekildi.Parameters.AddWithValue("@tc", richTextBox19.Text);
                rontgencekildi.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox4_CheckedChanged(object sender, EventArgs e)//Fotograf çekildi  değişim
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox6.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set fotografcekildi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set fotografcekildi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox3_CheckedChanged(object sender, EventArgs e)// Polisajyapıldı
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox3.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set polisajyapildi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set polisajyapildi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox2.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ustbondingyapistirildi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ustbondingyapistirildi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }//ust bonding yapıştırıldı

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox1.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set altbondingyapistirildi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set altbondingyapistirildi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();

        }// altbonding yapıştırıldı

        private void materialCheckBox15_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox15.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ust12_niti='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ust12_niti='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }// ust12_niti

        private void materialCheckBox14_CheckedChanged(object sender, EventArgs e)// ust16_niti
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox14.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ust16_niti='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ust16_niti='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox13_CheckedChanged(object sender, EventArgs e)//ust16x22niti
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox13.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ust16x22niti='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ust16x22niti='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox12_CheckedChanged(object sender, EventArgs e)//ust16x22celik
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox12.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ust16x22celik='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ust16x22celik='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox17_CheckedChanged(object sender, EventArgs e)//ustchain takıldı
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox17.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ustcahintakildi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ustcahintakildi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox16_CheckedChanged(object sender, EventArgs e)//altchain takıldı
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox16.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set altchaintakildi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set altchaintakildi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox18_CheckedChanged(object sender, EventArgs e)//sagucgensagucgen
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox18.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sagucgen='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sagucgen='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox20_CheckedChanged(object sender, EventArgs e)//Sagkare
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox20.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sagkare='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sagkare='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox22_CheckedChanged(object sender, EventArgs e)//sagsinif2
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox22.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sagsinif2='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sagsinif2='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox23_CheckedChanged(object sender, EventArgs e)//sagsinif3
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox23.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sagsinif3='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sagsinif3='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox19_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox19.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set solucgen='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set solucgen='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }//Solucgen

        private void materialCheckBox21_CheckedChanged(object sender, EventArgs e)//Sol kare
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox21.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set solkare='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set solkare='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox24_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox24.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set solsinif2='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set solsinif2='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }//solsinif2

        private void materialCheckBox25_CheckedChanged(object sender, EventArgs e)//Solsinif3
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox25.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set solsinif3='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set solsinif3='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox28_CheckedChanged(object sender, EventArgs e)//Rme takıldı
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox28.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set rme='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set rme='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox27_CheckedChanged(object sender, EventArgs e)//rmekontroledildi
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox27.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set rmekontroledildi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set rmekontroledildi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox26_CheckedChanged(object sender, EventArgs e)//rme olcualindi
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox26.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set rmeolcualindi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set rmeolcualindi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox29_CheckedChanged(object sender, EventArgs e)//Twinblok
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox29.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set twinblok='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set twinblok='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox30_CheckedChanged(object sender, EventArgs e)//twinblokolcu
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox30.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set twinblokolcu='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set twinblokolcu='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox31_CheckedChanged(object sender, EventArgs e)//twinblokkontrol
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox30.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set twinblokkontrol='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set twinblokkontrol='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox32_CheckedChanged(object sender, EventArgs e)//twinblok expansiyon
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox32.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set expansiyon='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set expansiyon='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox53_CheckedChanged(object sender, EventArgs e)//yuzmaskesi verildi
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox53.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set yuzmaskesi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set yuzmaskesi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox33_CheckedChanged(object sender, EventArgs e)//yuzmaskesiolcu
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox33.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set yuzmaskesiolcu='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set yuzmaskesiolcu='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox34_CheckedChanged(object sender, EventArgs e)//yuzmaskesikontrol
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox34.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set yuzmaskesikontrol='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set yuzmaskesikontrol='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox35_CheckedChanged(object sender, EventArgs e)//tekcheta
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox35.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set tekcheta='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set tekcheta='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox36_CheckedChanged(object sender, EventArgs e)//ciftcheta
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox36.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ciftcheta='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ciftcheta='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox37_CheckedChanged(object sender, EventArgs e)//ictemizlik
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox37.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ictemizlik='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ictemizlik='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox38_CheckedChanged(object sender, EventArgs e)//bitimolcu
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox38.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set bitimolcu='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set bitimolcu='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox39_CheckedChanged(object sender, EventArgs e)//fircamacun
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox39.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set fircamacun='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set fircamacun='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox40_CheckedChanged(object sender, EventArgs e)//sokum
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox40.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sokum='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set sokum='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox41_CheckedChanged(object sender, EventArgs e)//bitimolcu1
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox41.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set bitimolcu1='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set bitimolcu1='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox42_CheckedChanged(object sender, EventArgs e)//plakteslim
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox42.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set plakteslim='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set plakteslim='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox43_CheckedChanged(object sender, EventArgs e)//onamformu
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox43.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set onamformu='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set onamformu='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox44_CheckedChanged(object sender, EventArgs e)//rontgen
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox44.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set rontgen='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set rontgen='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox45_CheckedChanged(object sender, EventArgs e)//fotograf
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox45.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set fotograf='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set fotograf='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox46_CheckedChanged(object sender, EventArgs e)//6aykontrol
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox46.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set 6aykontrol='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set 6aykontrol='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox47_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox47.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set 1yilkontrol='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set 1yilkontrol='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }//1yilkontrol

        private void materialCheckBox48_CheckedChanged(object sender, EventArgs e)//retainertamir
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox48.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set retainertamir='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set retainertamir='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox49_CheckedChanged(object sender, EventArgs e)//kirikplakyenileme
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox49.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set kirikplakyenileme='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set kirikplakyenileme='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox50_CheckedChanged(object sender, EventArgs e)//kayipplakyenilendi
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox50.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set kayipplakyenilendi='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set kayipplakyenilendi='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox51_CheckedChanged(object sender, EventArgs e)//ucretliplak
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox51.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ucretliplak='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ucretliplak='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox52_CheckedChanged(object sender, EventArgs e)//ucretsizplak
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox52.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ucretsizplak='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set ucretsizplak='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox54_CheckedChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox54.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set kopanbrakettamiri='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set kopanbrakettamiri='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (textBox13.Text.Trim() == "")
            {
                MySqlCommand textboxdegisim = new MySqlCommand(" update hastalar set braketkopupyapistirilmasayisi='0' where tc=@tc", _ec.baglanti);
                textboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                textboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand textboxdegisim = new MySqlCommand(" update hastalar set braketkopupyapistirilmasayisi='" + textBox13.Text + " ' where tc=@tc", _ec.baglanti);
                textboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                textboxdegisim.ExecuteNonQuery();
                // MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set braketkopupyapistirilmasayisi='" + Convert.ToInt16(textBox13.Text) + "' where tc=@tc", _ec.baglanti);
            //checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
            //checkboxdegisim.ExecuteNonQuery();

            _ec.baglanti.Close();
        }//braket sayısı

        private void materialCheckBox8_CheckedChanged(object sender, EventArgs e)//alt12_niti
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox8.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set alt12_niti='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set alt12_niti='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox9_CheckedChanged(object sender, EventArgs e)//alt16_niti
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox9.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set alt16_niti='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set alt16_niti='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox10_CheckedChanged(object sender, EventArgs e)//alt16x22niti
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox10.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set alt16x22niti='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set alt16x22niti='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void materialCheckBox11_CheckedChanged(object sender, EventArgs e)//alt16x22celik
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            if (materialCheckBox11.Checked == true)
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set alt16x22celik='True' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set alt16x22celik='False' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }
            _ec.baglanti.Close();
        }

        private void MaterialFlatButton13_Click(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            _ec.baglanti.Open();
            try
            {
                MySqlCommand checkboxdegisim = new MySqlCommand(" update hastalar set bitistarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' where tc=@tc", _ec.baglanti);
                checkboxdegisim.Parameters.AddWithValue("@tc", richTextBox19.Text);
                checkboxdegisim.ExecuteNonQuery();
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//hasta bitirrrr

        private void materialFlatButton6_Click(object sender, EventArgs e)//Güncelleme
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle = new MySqlCommand("update hastalar SET hastalar.tc= '" + richTextBox19.Text + "', adi='" + richTextBox25.Text + "',soyadi='" + richTextBox24.Text + "', dogumtarihi='" + dateTimePicker3.Value.ToString("yyyy-MM-dd") + "', hastakayittarihi='" + dateTimePicker63.Value.ToString("yyyy-MM-dd") + "' , telefon='" + richTextBox33.Text + "',isyeri='" + richTextBox20.Text + "',okulu='" + richTextBox22.Text + "',meslegi='" + richTextBox21.Text + "',referans='" + richTextBox18.Text + "',adresi='" + richTextBox34.Text + "',babatelefonu='" + richTextBox30.Text + "',annetelefonu='" + richTextBox29.Text + "', babaadi='" + richTextBox31.Text + "', anneadi='" + richTextBox32.Text + "', baba_meslegi= '" + richTextBox28.Text + "', anne_meslegi='" + richTextBox27.Text + "', toplamucret='" + richTextBox9.Text + "', ikametyeri='" + richTextBox1.Text + "', notlar= '" + richTextBox61.Text + "' where hastalar.dosya_no='" + richTextBox26.Text + "'", _ec.baglanti);
                MySqlCommand guncelle1 = new MySqlCommand("update borclar set borclar.borcm='" + richTextBox9.Text + "', borclar.tc= '" + richTextBox19.Text + "' where borclar.borcno = '" + richTextBox26.Text + "'", _ec.baglanti);
                MySqlCommand guncellex = new MySqlCommand("update odemeler set odemeler.tc='" + richTextBox19.Text + "' where odemeler.borcno = '" + richTextBox26.Text + "'", _ec.baglanti);

                object sonuc = null;
                object sonuc1 = null;
                object sonucx = null;
                sonuc = guncelle.ExecuteNonQuery();
                sonuc1 = guncelle1.ExecuteNonQuery();
                sonucx = guncellex.ExecuteNonQuery();
                if (sonuc != null & sonuc1 != null & sonucx != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
                MySqlCommand commandborclar = new MySqlCommand
                {
                    CommandText = "select dosya_no, hastalar.tc, adi, soyadi, borclar.borcm, odememiktari, odemetarihi, odemeler.odemeyialan, odemeler.odemeno from  hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where hastalar.tc= '" + textBox6.Text + "'  ",
                    Connection = _ec.baglanti

                };
                string sql = "SELECT * FROM hastalar";
                DataTable data = new DataTable();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter
                {
                    SelectCommand = commandborclar
                };
                command.CommandText = sql;
                command.Connection = _ec.baglanti;
                adapter.SelectCommand = command;
                adapter.Fill(data);
                adapterborclar.Fill(databorclar);
                dataGridView1.DataSource = data;
                dataGridView2.DataSource = databorclar;
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)//Ödmeyi gerçekleştir
        {
            _ec.baglanti.Close();
            if (materialLabel56.Text.Trim() == "")
            {
                MessageBox.Show("TC arama yap veya Tc girilmemiş(Dosya no olmamış olabilir kontrol et)");

            }
            try
            {

                _ec.baglanti.Open();
                MySqlCommand odemeyap = new MySqlCommand("insert into odemeler  (Borcno, tc, odememiktari, odemetarihi,odemeler.odemeyialan) values ('" + materialLabel56.Text + "','" + materialLabel57.Text + "','" + textBox10.Text + "','" + dateTimePicker48.Value.ToString("yyyy-MM-dd") + "','" + textBox11.Text + "')", _ec.baglanti);
                object sonucodemeyap = null;
                sonucodemeyap = odemeyap.ExecuteNonQuery();
                if (sonucodemeyap != null)
                {
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    {

                        decimal borc = 0;
                        decimal odeme = 0;
                        _ec.baglanti.Close();
                        _ec.baglanti.Open();
                        MySqlCommand cmd = new MySqlCommand("select sum(borclar.borcm) as borctoplami from borclar , hastalar where    borclar.tc= '" + textBox6.Text + "' and borclar.tc = hastalar.tc", _ec.baglanti);
                        int Count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (Count != 0)
                        {
                            materialLabel56.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                            materialLabel57.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                            materialLabel58.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                            materialLabel59.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                            MySqlDataReader oku = cmd.ExecuteReader();


                            while (oku.Read())
                            {
                                borc = Convert.ToDecimal(oku["borctoplami"]);

                            }

                        }

                        _ec.baglanti.Close();
                        _ec.baglanti.Open();

                        cmd = new MySqlCommand("select sum(odemeler.odememiktari) as odemetoplami from odemeler, borclar , hastalar where borclar.tc = odemeler.tc and  borclar.tc= '" + textBox6.Text + "' and borclar.tc = hastalar.tc", _ec.baglanti);
                        Count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (Count != 0)
                        {
                            MySqlDataReader oku = cmd.ExecuteReader();
                            while (oku.Read())
                            {
                                odeme = Convert.ToDecimal(oku["odemetoplami"]);

                            }
                        }

                        _ec.baglanti.Close();
                        materialLabel52.Text = (borc).ToString();
                        materialLabel54.Text = (odeme).ToString();
                        materialLabel53.Text = (borc - odeme).ToString();

                    }

                }


                else
                    MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // bağlantıyı kapatalım
                _ec.baglanti.Close();
                MySqlCommand commandborclar = new MySqlCommand
                {
                    CommandText = "SELECT*from borclar ",
                    Connection = _ec.baglanti

                };
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBox10.Clear();
            textBox11.Clear();

            if (textBox6.Text.Trim() == "")//ababa
            {
                _ec.baglanti.Close();
                _ec.baglanti.Open();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter();
                MySqlCommand commandborclar = new MySqlCommand
                {
                    CommandText = "select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari, odemetarihi, odemeler.odemeyialan from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where hastalar.tc= '" + textBox6.Text + "'",
                    Connection = _ec.baglanti

                };
                adapterborclar.SelectCommand = commandborclar;
                adapterborclar.Fill(databorclar);
                dataGridView2.DataSource = databorclar;
                _ec.baglanti.Close();
                materialLabel52.Text = "".ToString();
                materialLabel54.Text = "".ToString();
                materialLabel53.Text = "".ToString();
                materialLabel56.Text = "".ToString();
                materialLabel57.Text = "".ToString();
                materialLabel58.Text = "".ToString();
                materialLabel59.Text = "".ToString();


            }
            else
            {
                _ec.baglanti.Close();
                _ec.baglanti.Open();
                DataTable databorclar = new DataTable();
                MySqlDataAdapter adapterborclar = new MySqlDataAdapter();
                MySqlCommand commandborclar = new MySqlCommand
                {

                    CommandText = "select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari, odemetarihi, odemeler.odemeyialan, odemeno from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where hastalar.tc= '" + textBox6.Text + "'",
                    // CommandText = "select	odemeler.borcno	, odemeler.odememiktari , odemeler.odemetarihi, hastalar.adi	, hastalar.soyadi, hastalar.tc  from odemeler , borclar , hastalar where odemeler.borcno = borclar.borcno and borclar.tc= '" + textBox6.Text + "' and borclar.tc = hastalar.tc",
                    //CommandText = "select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari,odemetarihi from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.tc=odemeler.tc  where hastalar.tc= '" + textBox6.Text + "'",

                    Connection = _ec.baglanti

                };
                adapterborclar.SelectCommand = commandborclar;
                adapterborclar.Fill(databorclar);
                dataGridView2.DataSource = databorclar;
                _ec.baglanti.Close();


            }




        }

        private void richTextBox2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
        }

        private void toplamucret_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void richTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); //&& e.KeyChar != '.';
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }

        private void Button3_Click(object sender, EventArgs e)//1.seans kayıt
        {

            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 1s= '" + richTextBox3.Text + "',1t= '" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button4_Click(object sender, EventArgs e)//2.seans kayıt
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 2s= '" + richTextBox4.Text + "',2t='" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button6_Click(object sender, EventArgs e)//3.seans kayıt
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 3s= '" + richTextBox10.Text + "',3t='" + dateTimePicker7.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button5_Click(object sender, EventArgs e)//4.seans kayıt
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 4s= '" + richTextBox11.Text + "', 4t='" + dateTimePicker6.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 5s= '" + richTextBox12.Text + "',5t='" + dateTimePicker9.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//5.seans kayıt

        private void Button7_Click(object sender, EventArgs e)//6.seans kayıt
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 6s= '" + richTextBox13.Text + "',6t='" + dateTimePicker8.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button10_Click(object sender, EventArgs e)//7.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 7s= '" + richTextBox14.Text + "',7t='" + dateTimePicker11.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button9_Click(object sender, EventArgs e)//8.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 8s= '" + richTextBox15.Text + "',8t='" + dateTimePicker10.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button12_Click(object sender, EventArgs e)//9.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 9s= '" + richTextBox16.Text + "',9t='" + dateTimePicker13.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button11_Click(object sender, EventArgs e)//10.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 10s= '" + richTextBox42.Text + "',10t='" + dateTimePicker12.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 11s= '" + richTextBox41.Text + "',11t='" + dateTimePicker15.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//11.seans

        private void Button13_Click(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 12s= '" + richTextBox40.Text + "',12t='" + dateTimePicker14.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//12.seans

        private void Button26_Click(object sender, EventArgs e)//13.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 13s= '" + richTextBox48.Text + "',13t='" + dateTimePicker27.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button25_Click(object sender, EventArgs e)//14.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 14s= '" + richTextBox47.Text + "',14t='" + dateTimePicker26.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button24_Click(object sender, EventArgs e)//15.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 15s= '" + richTextBox46.Text + "',15t='" + dateTimePicker25.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button23_Click(object sender, EventArgs e)//16.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 16s= '" + richTextBox45.Text + "',16t='" + dateTimePicker24.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button22_Click(object sender, EventArgs e)//17.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 17s= '" + richTextBox44.Text + "',17t='" + dateTimePicker23.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button21_Click(object sender, EventArgs e)//18.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 18s= '" + richTextBox43.Text + "',18t='" + dateTimePicker22.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button20_Click(object sender, EventArgs e)//19.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 19s= '" + richTextBox39.Text + "',19t='" + dateTimePicker21.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button19_Click(object sender, EventArgs e)//20.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 20s= '" + richTextBox38.Text + "',20t='" + dateTimePicker20.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button18_Click(object sender, EventArgs e)//21.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 21s= '" + richTextBox37.Text + "',21t='" + dateTimePicker19.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button17_Click(object sender, EventArgs e)//22.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 22s= '" + richTextBox36.Text + "',22t='" + dateTimePicker18.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button16_Click(object sender, EventArgs e)//23.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 23s= '" + richTextBox35.Text + "',23t='" + dateTimePicker17.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button15_Click(object sender, EventArgs e)//24.seans
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 24s= '" + richTextBox17.Text + "',24t='" + dateTimePicker16.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void Button38_Click(object sender, EventArgs e)//25.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 25s= '" + richTextBox60.Text + "',25t='" + dateTimePicker39.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button37_Click(object sender, EventArgs e)//26.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 26s= '" + richTextBox59.Text + "',26t='" + dateTimePicker38.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button36_Click(object sender, EventArgs e)//27.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 27s= '" + richTextBox58.Text + "',27t='" + dateTimePicker37.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button35_Click(object sender, EventArgs e)//28.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 28s= '" + richTextBox57.Text + "',28t='" + dateTimePicker36.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button34_Click(object sender, EventArgs e)//29.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 29s= '" + richTextBox56.Text + "',29t='" + dateTimePicker35.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button33_Click(object sender, EventArgs e)//30.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 30s= '" + richTextBox55.Text + "',30t='" + dateTimePicker34.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button32_Click(object sender, EventArgs e)//31.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 31s= '" + richTextBox54.Text + "',31t='" + dateTimePicker33.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button31_Click(object sender, EventArgs e)//32.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 32s= '" + richTextBox53.Text + "',32t='" + dateTimePicker32.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button30_Click(object sender, EventArgs e)//33.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 33s= '" + richTextBox52.Text + "',33t='" + dateTimePicker31.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button29_Click(object sender, EventArgs e)//34.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 34s= '" + richTextBox51.Text + "',34t='" + dateTimePicker30.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button28_Click(object sender, EventArgs e)//35.seans
        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 35s= '" + richTextBox50.Text + "',35t='" + dateTimePicker29.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void Button27_Click(object sender, EventArgs e)//36.seans

        //{
        //    _ec.baglanti.Close();
        //    try
        //    {

        //        _ec.baglanti.Open();
        //        MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 36s= '" + richTextBox49.Text + "',36t='" + dateTimePicker28.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
        //        object sonuc3 = null;
        //        sonuc3 = guncelle3.ExecuteNonQuery();
        //        if (sonuc3 != null)
        //            MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        else
        //            MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //// bağlantıyı kapatalım
        //        _ec.baglanti.Close();
        //    }


        //    catch (Exception HataYakala)
        //    {
        //        MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}



        private void MaterialFlatButton5_Click(object sender, EventArgs e)//Biten hastalar
        {
            _ec.baglanti.Close();
            string sql1 = "Select * from hastatakip.hastalar where bitistarihi is not null";
            DataTable data1 = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand command1 = new MySqlCommand
            {
                CommandText = sql1,
                Connection = _ec.baglanti
            };

            adapter1.SelectCommand = command1;
            _ec.baglanti.Open();
            adapter1.Fill(data1);
            dataGridView1.DataSource = data1;
            _ec.baglanti.Close();


            //for(int i = 0; i < dataGridView1.Rows.Count - 1; i++) tabloyu renklendirmek için
            //{
            //    DataGridViewCellStyle renk = new DataGridViewCellStyle();
            //    if (dataGridView1.Rows[i].Cells[75].Value.ToString() == "")

            //    {
            //        renk.BackColor = Color.DarkRed;
            //        renk.ForeColor = Color.DarkGreen;
            //    }
            //    else
            //    {
            //        renk.ForeColor = Color.Black;
            //        renk.BackColor = Color.Yellow;
            //    }
            //    dataGridView1.Rows[i].DefaultCellStyle = renk;
            //}
        }

        private void MaterialFlatButton8_Click(object sender, EventArgs e)//devam eden hastalar
        {
            _ec.baglanti.Close();
            string sql1 = "Select * from hastatakip.hastalar where bitistarihi is null";
            DataTable data1 = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand command1 = new MySqlCommand
            {
                CommandText = sql1,
                Connection = _ec.baglanti
            };

            adapter1.SelectCommand = command1;
            _ec.baglanti.Open();
            adapter1.Fill(data1);
            dataGridView1.DataSource = data1;
            _ec.baglanti.Close();
        }

        private void MaterialFlatButton10_Click(object sender, EventArgs e)//tüm hastalar
        {
            _ec.baglanti.Close();
            string sql2 = "SELECT * FROM hastalar ";
            DataTable data2 = new DataTable();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            MySqlCommand command2 = new MySqlCommand
            {
                CommandText = sql2,
                Connection = _ec.baglanti
            };

            adapter2.SelectCommand = command2;
            _ec.baglanti.Open();
            adapter2.Fill(data2);
            dataGridView1.DataSource = data2;
            _ec.baglanti.Close();
        }


        private void MaterialFlatButton12_Click(object sender, EventArgs e)//Günlük tahsilat
        {
            dataGridView3.DataSource = "";
            try
            {

                decimal borc1 = 0;


                _ec.baglanti.Open();

                MySqlCommand cmd1 = new MySqlCommand("select sum(odemeler.odememiktari) as odemetoplami from odemeler where odemeler.odemetarihi= '" + dateTimePicker40.Value.ToString("yyyy-MM-dd") + "'", _ec.baglanti);
                MySqlCommand cmdtablo = new MySqlCommand("select dosya_no ,  adi, soyadi, hastalar.tc, toplamucret,  ( select  sum(odememiktari) from odemeler where odemeler.borcno=borclar.borcno and odemeler.borcno  IN (select odemeler.borcno from odemeler where odemetarihi='" + dateTimePicker40.Value.ToString("yyyy-MM-dd") + "'group by dosya_no ) ) as Toplamodeme , sum(odememiktari) as bugunodenen, (toplamucret- ( select  sum(odememiktari) from odemeler where odemeler.borcno=borclar.borcno and odemeler.borcno  IN (select odemeler.borcno from odemeler where Odemetarihi='" + dateTimePicker40.Value.ToString("yyyy-MM-dd") + "'group by dosya_no ) )) as Kalan, odemetarihi, odemeler.odemeyialan from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where odemeler.odemetarihi='" + dateTimePicker40.Value.ToString("yyyy-MM-dd") + "' group by dosya_no", _ec.baglanti);
                DataTable tabloverisi = new DataTable();

                MySqlDataAdapter tabloverisi1 = new MySqlDataAdapter();
                tabloverisi1.SelectCommand = cmdtablo;
                tabloverisi1.Fill(tabloverisi);
                dataGridView3.DataSource = tabloverisi;
                int Count = Convert.ToInt32(cmd1.ExecuteScalar());
                if (Count != 0)
                {

                    MySqlDataReader oku = cmd1.ExecuteReader();



                    while (oku.Read())
                    {
                        borc1 = Convert.ToDecimal(oku["odemetoplami"]);
                        materialLabel63.Text = borc1.ToString();

                    }

                }
                _ec.baglanti.Close();
            }

            catch
            {
                MessageBox.Show("Bu tarihte ödeme bilgisi bulunamadı. Tarihte ödeme olduğundan eminiseniz Lütfen ilgiliye haber veriniz");

            }
            _ec.baglanti.Close();
        }

        private void MaterialFlatButton11_Click_1(object sender, EventArgs e)
        {
            if (textBox5.Text.ToString() == "Ender")

            {
                materialFlatButton12.Visible = true;
                materialFlatButton14.Visible = true;
                materialFlatButton16.Visible = true;
                dataGridView3.Visible = true;
                materialLabel63.Visible = true;
                materialLabel64.Visible = true;
                materialLabel66.Visible = true;
                dateTimePicker40.Visible = true;
                dateTimePicker41.Visible = true;
                dateTimePicker42.Visible = true;
                dateTimePicker43.Visible = true;
                dateTimePicker44.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                dateTimePicker45.Visible = true;
                dateTimePicker46.Visible = true;
                materialFlatButton15.Visible = true;
                materialLabel65.Visible = true;
            }
            else
            {
                MessageBox.Show("Yanlış Şifre");
                materialFlatButton12.Visible = false;
                materialFlatButton14.Visible = false;
                materialFlatButton16.Visible = false;
                dataGridView3.Visible = false;
                materialLabel63.Visible = false;
                materialLabel64.Visible = false;
                materialLabel66.Visible = false;
                dateTimePicker40.Visible = false;
                dateTimePicker41.Visible = false;
                dateTimePicker42.Visible = false;
                dateTimePicker43.Visible = false;
                dateTimePicker44.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                dateTimePicker45.Visible = false;
                dateTimePicker46.Visible = false;
                materialFlatButton15.Visible = false;
                materialLabel65.Visible = false;
            }
            textBox5.Clear();
        }

        private void MaterialFlatButton14_Click(object sender, EventArgs e)//tarihler arası tahsilat
        {
            dataGridView3.DataSource = "";
            try
            {

                decimal borc2 = 0;


                _ec.baglanti.Open();
                MySqlCommand cmd2 = new MySqlCommand("select sum(odemeler.odememiktari) as odemetoplami from odemeler where odemeler.odemetarihi between '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "'", _ec.baglanti);
                //MySqlCommand cmdtablo = new MySqlCommand("select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari, odemetarihi from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where odemeler.odemetarihi between '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "'", _ec.baglanti);
                MySqlCommand cmdtablo = new MySqlCommand(" SELECT odemeler.borcno , hastalar.adi, hastalar.soyadi,borcm, ( select  sum(odememiktari) from odemeler where odemeler.borcno=borclar.borcno and odemeler.borcno  IN (select odemeler.borcno from odemeler where odemetarihi between '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "' group by odemeler.borcno ) ) as Toplamodeme , ( select  sum(odememiktari) from odemeler where odemeler.borcno=borclar.borcno and odemetarihi between '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "'  group by odemeler.borcno ) as odemeler , borcm-  ( select  sum(odememiktari) from odemeler where odemeler.borcno=borclar.borcno and odemeler.borcno  IN (select odemeler.borcno from odemeler where odemetarihi between '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "' group by odemeler.borcno ) ) as Kalan , odemeler.odemeyialan FROM odemeler , borclar, hastalar where odemeler.borcno=borclar.borcno and odemeler.tc=hastalar.tc and odemetarihi between '" + dateTimePicker41.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker42.Value.ToString("yyyy-MM-dd") + "' GROUP BY borcno", _ec.baglanti);
                DataTable tabloverisi = new DataTable();

                MySqlDataAdapter tabloverisi1 = new MySqlDataAdapter();
                tabloverisi1.SelectCommand = cmdtablo;
                tabloverisi1.Fill(tabloverisi);
                dataGridView3.DataSource = tabloverisi;
                int Count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                if (Count1 != 0)
                {

                    MySqlDataReader oku = cmd2.ExecuteReader();


                    while (oku.Read())
                    {
                        borc2 = Convert.ToDecimal(oku["odemetoplami"]);
                        materialLabel66.Text = borc2.ToString();
                    }

                }
                _ec.baglanti.Close();
            }

            catch
            {
                MessageBox.Show("Bu tarihte ödeme bilgisi bulunamadı. Tarihte ödeme olduğundan eminiseniz Lütfen ilgiliye haber veriniz");
                materialLabel63.Text = "";
            }
            _ec.baglanti.Close();
        }

        private void MaterialFlatButton16_Click(object sender, EventArgs e)//Tarihler arası kalan borçlar
        {
            try
            {

                decimal borc3 = 0;
                decimal odeme3 = 0;
                _ec.baglanti.Close();
                _ec.baglanti.Open();
                MySqlCommand cmd3 = new MySqlCommand("select sum(hastalar.toplamucret) as toplamucret from hastalar where hastalar.hastakayittarihi  between '" + dateTimePicker43.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker44.Value.ToString("yyyy-MM-dd") + "' ", _ec.baglanti);
                int Count = Convert.ToInt32(cmd3.ExecuteScalar());
                if (Count != 0)
                {

                    MySqlDataReader oku = cmd3.ExecuteReader();


                    while (oku.Read())
                    {
                        borc3 = Convert.ToDecimal(oku["toplamucret"]);
                        label13.Text = borc3.ToString();


                    }

                }

                _ec.baglanti.Close();
                _ec.baglanti.Open();
                cmd3 = new MySqlCommand("select sum(odemeler.odememiktari) as odemetoplami1 from odemeler where odemeler.odemetarihi between '" + dateTimePicker43.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker44.Value.ToString("yyyy-MM-dd") + "' ", _ec.baglanti);
                Count = Convert.ToInt32(cmd3.ExecuteScalar());
                if (Count != 0)
                {
                    MySqlDataReader oku = cmd3.ExecuteReader();
                    while (oku.Read())
                    {
                        odeme3 = Convert.ToDecimal(oku["odemetoplami1"]);
                        label14.Text = odeme3.ToString();
                    }
                }

                _ec.baglanti.Close();

                materialLabel64.Text = (borc3 - odeme3).ToString();

            }
            catch
            {
                MessageBox.Show("Bu tarihte ödeme bilgisi bulunamadı. Tarihte ödeme olduğundan eminiseniz Lütfen ilgiliye haber veriniz");
                materialLabel63.Text = "";

            }
        }



        private void MaterialFlatButton15_Click(object sender, EventArgs e)
        {
            try
            {

                decimal hastasayisi = 0;

                _ec.baglanti.Close();
                _ec.baglanti.Open();
                MySqlCommand cmd5 = new MySqlCommand("select count(hastalar.hastakayittarihi) as hastasayisi from hastalar where hastalar.hastakayittarihi between '" + dateTimePicker45.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker46.Value.ToString("yyyy-MM-dd") + "'", _ec.baglanti);

                MySqlCommand cmdtablo = new MySqlCommand("select dosya_no , hastalar.tc, adi, soyadi, borcm, odememiktari, odemetarihi, hastalar.hastakayittarihi from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where hastalar.hastakayittarihi between '" + dateTimePicker45.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker46.Value.ToString("yyyy-MM-dd") + "' group by dosya_no", _ec.baglanti);
                DataTable tabloverisi = new DataTable();

                MySqlDataAdapter tabloverisi1 = new MySqlDataAdapter();
                tabloverisi1.SelectCommand = cmdtablo;
                tabloverisi1.Fill(tabloverisi);
                dataGridView3.DataSource = tabloverisi;

                int Count5 = Convert.ToInt32(cmd5.ExecuteScalar());
                if (Count5 != 0)
                {

                    MySqlDataReader oku = cmd5.ExecuteReader();


                    while (oku.Read())
                    {
                        hastasayisi = Convert.ToDecimal(oku["hastasayisi"]);
                        materialLabel65.Text = hastasayisi.ToString();
                    }

                }
                _ec.baglanti.Close();
            }

            catch (Exception hata)
            {
                // MessageBox.Show("Bu tarihte kişi bilgisi bulunamadı. Tarihte kişi olduğundan eminiseniz Lütfen ilgiliye haber veriniz");
                MessageBox.Show(hata.ToString());
                materialLabel65.Text = "";
            }
            _ec.baglanti.Close();

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.PasswordChar = '*';
        }

        private void DataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox7.Clear();
                label21.Text = "";
                label19.Text = "";
                textBox7.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
                label21.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                label19.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            }
            if (checkBox1.Checked == false)
            {
                textBox7.Clear();
                label21.Text = "";
                label19.Text = "";

            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)

            {

                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                textBox7.Visible = true;
                button39.Visible = true;

            }
            else
            {
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                label20.Visible = false;
                label21.Visible = false;
                textBox7.Visible = false;
                button39.Visible = false;

            }
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelley = new MySqlCommand("update odemeler set odemeler.odememiktari='" + textBox7.Text + "' where odemeler.borcno = '" + label21.Text + "' and odemeler.odemeno='" + label19.Text + "'", _ec.baglanti);


                object sonucy = null;
                sonucy = guncelley.ExecuteNonQuery();
                if (sonucy != null)
                {
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MySqlCommand ekranyenile = new MySqlCommand("select dosya_no, hastalar.tc, adi, soyadi,borcm, odememiktari, odemetarihi, odemeler.odemeyialan, odemeler.odemeno from hastalar join borclar on hastalar.tc=borclar.tc join odemeler on borclar.borcno=odemeler.borcno where hastalar.tc= '" + textBox6.Text + "'", _ec.baglanti);

                    MySqlDataAdapter ekran1 = new MySqlDataAdapter();
                    DataTable data1 = new DataTable();


                    ekran1.SelectCommand = ekranyenile;
                    _ec.baglanti.Close();
                    _ec.baglanti.Open();
                    ekran1.Fill(data1);
                    dataGridView2.DataSource = data1;
                    _ec.baglanti.Close();

                }

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();

            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//odemelerde odeme miktarını güncelleme

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                materialFlatButton11.PerformClick();
                textBox5.Clear();


            }
        }

        private void Button40_Click(object sender, EventArgs e)
        {

            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand muayene = new MySqlCommand("insert into muayene  (muayene.misim ,muayene.msoyisim,muayene.mdogumtarihi,muayene.mtelefon,muayene.mmuayenetarihi,muayene.mfiyat,muayene.mreferans,muayene.mnotlar) values ('" + textBox8.Text + "','" + textBox9.Text + "','" + dateTimePicker49.Value.ToString("yyyy-MM-dd") + "','" + textBox12.Text + "','" + dateTimePicker50.Value.ToString("yyyy-MM-dd") + "','" + textBox14.Text + "','" + textBox15.Text + "','" + richTextBox63.Text + "')", _ec.baglanti);

                object sonucm = null;
                sonucm = muayene.ExecuteNonQuery();

                if (sonucm != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // bağlantıyı kapatalım
                _ec.baglanti.Close();
                string sql1 = "SELECT * FROM muayene";
                DataTable data = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                command.CommandText = sql1;
                command.Connection = _ec.baglanti;
                adapter.SelectCommand = command;
                adapter.Fill(data);
                dataGridView4.DataSource = data;
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _ec.baglanti.Close();

        }//muayene kaydet

        private void DataGridView4_DoubleClick(object sender, EventArgs e)//MUAYENE ÇİFT TIKLANINCA
        {

            textBox8.Clear();
            textBox9.Clear();
            dateTimePicker49.Value = DateTime.Today;
            dateTimePicker50.Value = DateTime.Today;
            textBox12.Clear();
            textBox15.Clear();
            richTextBox63.Clear();
            label31.Text = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            textBox8.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            textBox9.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker49.Value = Convert.ToDateTime(dataGridView4.CurrentRow.Cells[3].Value.ToString());
            textBox12.Text = dataGridView4.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker50.Value = Convert.ToDateTime(dataGridView4.CurrentRow.Cells[5].Value.ToString());
            textBox14.Text = dataGridView4.CurrentRow.Cells[6].Value.ToString();
            textBox15.Text = dataGridView4.CurrentRow.Cells[7].Value.ToString();
            richTextBox63.Text = dataGridView4.CurrentRow.Cells[8].Value.ToString();


        }

        private void Button41_Click(object sender, EventArgs e)
        {
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncellemuayene = new MySqlCommand("update muayene SET muayene.misim= '" + textBox8.Text + "', muayene.msoyisim='" + textBox9.Text + "', muayene.mtelefon='" + textBox12.Text + "', muayene.mdogumtarihi='" + dateTimePicker49.Value.ToString("yyyy-MM-dd") + "',muayene.mmuayenetarihi='" + dateTimePicker50.Value.ToString("yyyy-MM-dd") + "',muayene.mfiyat='" + textBox14.Text + "',muayene.mreferans='" + textBox15.Text + "',muayene.mnotlar='" + richTextBox63.Text + "' where muayene.mno='" + label31.Text + "'", _ec.baglanti);

                object sonucmuayene = null;
                sonucmuayene = guncellemuayene.ExecuteNonQuery();
                if (sonucmuayene != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Günceleme gerçekleşmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();

            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _ec.baglanti.Close();
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();

                string sql1 = "SELECT * FROM muayene";
                DataTable data = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                command.CommandText = sql1;
                command.Connection = _ec.baglanti;
                adapter.SelectCommand = command;
                adapter.Fill(data);
                dataGridView4.DataSource = data;
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _ec.baglanti.Close();
        }

        private void Button42_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
            textBox9.Clear();
            dateTimePicker49.Value = DateTime.Today;
            dateTimePicker50.Value = DateTime.Today;
            textBox12.Clear();
            textBox15.Clear();
            richTextBox63.Clear();

            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();

                string sql1 = "SELECT * FROM muayene";
                DataTable data = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand();
                command.CommandText = sql1;
                command.Connection = _ec.baglanti;
                adapter.SelectCommand = command;
                adapter.Fill(data);
                dataGridView4.DataSource = data;
            }

            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _ec.baglanti.Close();

        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            if (textBox18.Text.Trim() == "")
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM muayene", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView4.DataSource = data;
                _ec.baglanti.Close();
            }
            else
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM muayene where muayene.misim like '%" + textBox18.Text + "%'", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView4.DataSource = data;
                _ec.baglanti.Close();
            }
        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            if (textBox17.Text.Trim() == "")
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM muayene", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView4.DataSource = data;
                _ec.baglanti.Close();
            }
            else
            {
                _ec.baglanti.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM muayene where muayene.msoyisim like '%" + textBox17.Text + "%'", _ec.baglanti);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView4.DataSource = data;
                _ec.baglanti.Close();
            }
        }

        private void MaterialFlatButton17_Click(object sender, EventArgs e)//yedek alma
        {
            try
            {
                string conString = "Server=localhost;database=hastatakip; UID=root; password=";
                // Server = 192.168.1.101; database = hastatakip; UID = root; password =
                string file = "E:\\test_backup.sql";

                using (MySqlConnection conn = new MySqlConnection(conString))
                {
                    using (MySqlCommand yedek = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(yedek))
                        {

                            yedek.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(file);
                            conn.Close();
                            MessageBox.Show("welldone");

                        }
                    }
                }
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _ec.baglanti.Close();

        }

        private void Button54_Click(object sender, EventArgs e)//37.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 37s= '" + richTextBox75.Text + "',37t='" + dateTimePicker62.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button53_Click(object sender, EventArgs e)//38.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 38s= '" + richTextBox74.Text + "',38t='" + dateTimePicker61.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button52_Click(object sender, EventArgs e)//39.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 39s= '" + richTextBox73.Text + "',39t='" + dateTimePicker60.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button51_Click(object sender, EventArgs e)//40.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 40s= '" + richTextBox72.Text + "',40t='" + dateTimePicker59.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button50_Click(object sender, EventArgs e)//41SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 41s= '" + richTextBox71.Text + "',41t='" + dateTimePicker58.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button49_Click(object sender, EventArgs e)//42.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 42s= '" + richTextBox70.Text + "',42t='" + dateTimePicker57.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button48_Click(object sender, EventArgs e)//43S
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 43s= '" + richTextBox69.Text + "',43t='" + dateTimePicker56.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button47_Click(object sender, EventArgs e)//44SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 44s= '" + richTextBox68.Text + "',44t='" + dateTimePicker55.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button46_Click(object sender, EventArgs e)//45SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 45s= '" + richTextBox67.Text + "',45t='" + dateTimePicker54.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button45_Click(object sender, EventArgs e)//46SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 46s= '" + richTextBox66.Text + "',46t='" + dateTimePicker53.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button44_Click(object sender, EventArgs e)//47.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 47s= '" + richTextBox65.Text + "',47t='" + dateTimePicker52.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button43_Click(object sender, EventArgs e)//48SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 48s= '" + richTextBox64.Text + "',48t='" + dateTimePicker51.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button38_Click_1(object sender, EventArgs e)//25.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 25s= '" + richTextBox60.Text + "',25t='" + dateTimePicker39.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button37_Click(object sender, EventArgs e)//26.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 26s= '" + richTextBox59.Text + "',26t='" + dateTimePicker38.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button36_Click(object sender, EventArgs e)//27.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 27s= '" + richTextBox58.Text + "',27t='" + dateTimePicker37.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button35_Click(object sender, EventArgs e)//28.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 28s= '" + richTextBox57.Text + "',28t='" + dateTimePicker36.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button34_Click(object sender, EventArgs e)//29.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 29s= '" + richTextBox56.Text + "',29t='" + dateTimePicker35.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button33_Click(object sender, EventArgs e)//30SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 30s= '" + richTextBox55.Text + "',30t='" + dateTimePicker34.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button32_Click(object sender, EventArgs e)//31.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 31s= '" + richTextBox54.Text + "',31t='" + dateTimePicker33.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button31_Click(object sender, EventArgs e)//32.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 32s= '" + richTextBox53.Text + "',32t='" + dateTimePicker32.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button30_Click(object sender, EventArgs e)//33.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 33s= '" + richTextBox52.Text + "',33t='" + dateTimePicker31.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button29_Click(object sender, EventArgs e)//34.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 34s= '" + richTextBox51.Text + "',34t='" + dateTimePicker30.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button28_Click(object sender, EventArgs e)//35.SEANS
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 35s= '" + richTextBox50.Text + "',35t='" + dateTimePicker29.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            _ec.baglanti.Close();
            try
            {

                _ec.baglanti.Open();
                MySqlCommand guncelle3 = new MySqlCommand("update hastalar SET 36s= '" + richTextBox49.Text + "',36t='" + dateTimePicker28.Value.ToString("yyyy-MM-dd") + "'  where hastalar.tc='" + richTextBox19.Text + "'", _ec.baglanti);
                object sonuc3 = null;
                sonuc3 = guncelle3.ExecuteNonQuery();
                if (sonuc3 != null)
                    MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                    MessageBox.Show("Sisteme Güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //// bağlantıyı kapatalım
                _ec.baglanti.Close();
            }


            catch (Exception HataYakala)
            {
                MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}







