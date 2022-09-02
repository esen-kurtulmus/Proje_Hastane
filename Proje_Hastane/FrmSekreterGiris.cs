﻿using System;
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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select*from tbl_sekreter where sekreterTC=@p1 and sekretersifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTc.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr=komut.ExecuteReader(); 
            if(dr.Read())
            {
FrmSekreterDetay frs=new FrmSekreterDetay();
                frs.TCnumara = mskTc.Text;
                frs.Show();
                this.Hide();    
            }

            else
            {
                MessageBox.Show("Hatalı TC&Şifre");
            }
             bgl.baglanti().Close(); 

        }
    }
}
