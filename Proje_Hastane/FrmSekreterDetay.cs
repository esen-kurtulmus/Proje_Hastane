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

namespace Proje_Hastane
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
        
        sqlbaglantisi bgl=new sqlbaglantisi();  

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
lblTC.Text = TCnumara;
            
            //as soyad
            SqlCommand komut1=new SqlCommand("select  sekreteradsoyad from tbl_sekreter where sekreterTC=@p1 ",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1=komut1.ExecuteReader();   
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();    
            }
            bgl.baglanti().Close();

            //branşları datagride aktarma
            DataTable dt1=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //doktorları listeye aktarma
            DataTable dt2=new DataTable();  
            SqlDataAdapter da2=new SqlDataAdapter("select (doktorad+ ' '+doktorsoyad) as doktorbrans from tbl_doktorlar ", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource= dt2;
            //Branşı comboboxa aktarma
            SqlCommand komut2 = new SqlCommand("select bransad from tbl_branslar", bgl.baglanti());
            SqlDataReader dr2=komut2.ExecuteReader(); 
            while(dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);

            }
            bgl.baglanti().Close();


        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into tbl_randevular (randevutarih, randevusaat,randevubrans, randevudoktor) values (@r1, @r2, @r3, @r4)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("randevu oluşturuldu.");

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut=new SqlCommand("select doktorad,doktorsoyad from tbl_doktorlar where doktorbrans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr=komut.ExecuteReader();
            while(dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);
                
            }
bgl.baglanti().Close();
        }

        private void btnDuyuruolustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", rchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.");
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
FrmDoktorPaneli drp=new FrmDoktorPaneli();
drp.Show(); 
        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans frb=new FrmBrans();
            frb.Show();
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl=new FrmRandevuListesi();
            frl.Show();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr=new FrmDuyurular();
            fr.Show();
        }
    }
}
