using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFsFinalPeliculas.Modelos;

namespace WFsFinalPeliculas.Presentacion
{
    public partial class FrmDatos : Form
    {
        public int? id;
        tb_peliculazas oPeliculas = null;
        public FrmDatos(int? id= null)
        {
            InitializeComponent();
            this.id = id;

            if(id!= null)
            {
                CargaDatos();
            }
        }
        private void CargaDatos()
        {
            using (finalEntities db = new finalEntities())
            {
                oPeliculas = db.tb_peliculazas.Find(id);
                textBoxTituloPeli.Text = oPeliculas.Titulo_de_Pelicula;
                textBoxDirector.Text = oPeliculas.Director_es_;
                dateTimePickerEstreno.Value = oPeliculas.Fecha_Estreno.Value;
                textBoxValor.Text = Convert.ToString(oPeliculas.Valoracion__10);
                textBoxProtas.Text = oPeliculas.Protagonista_s_;
                textBoxId.Text = Convert.ToString(oPeliculas.Id);
            }
        }
        public void MapeoDatos()
        {
            using (finalEntities db = new finalEntities())
            {
                if (id == null)
                    oPeliculas = new tb_peliculazas();
             
                double v = double.Parse(textBoxValor.Text);
                oPeliculas.Id = int.Parse(textBoxId.Text);
                oPeliculas.Titulo_de_Pelicula = textBoxTituloPeli.Text;
                oPeliculas.Director_es_ = textBoxDirector.Text;
                oPeliculas.Fecha_Estreno = dateTimePickerEstreno.Value;
                oPeliculas.Valoracion__10 = v;
                oPeliculas.Protagonista_s_ = textBoxProtas.Text;
                
                if (id == null)
                    db.tb_peliculazas.Add(oPeliculas);
                else
                {
                    db.Entry(oPeliculas).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        private void buttonSaveDatos_Click(object sender, EventArgs e)
        {
            MapeoDatos();   
            this.Close();
        }
    }
}
