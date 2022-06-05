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

namespace WFsFinalPeliculas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridViewPeliculazas.Rows[dataGridViewPeliculazas.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refrescar();
        }
        private void Refrescar()
        {
            using (finalEntities db = new finalEntities())
            {
                var lst = from pel in db.tb_peliculazas
                          select pel;
                dataGridViewPeliculazas.DataSource = lst.ToList();
            }
        }
        private void buttonCrear_Click(object sender, EventArgs e)
        {
            Presentacion.FrmDatos oDatos = new Presentacion.FrmDatos();
            oDatos.ShowDialog();

            Refrescar();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                Presentacion.FrmDatos oDatos = new Presentacion.FrmDatos(id);
                oDatos.ShowDialog();
            }
            Refrescar();
        }

        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            DialogResult ev = MessageBox.Show("¿Esta seguro de borrar la pelicula?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ev == DialogResult.Yes)
            {
                int? id = GetId();
                if (id != null)
                {
                    using (finalEntities db = new finalEntities())
                    {
                        tb_peliculazas oTabla = db.tb_peliculazas.Find(id);
                        db.tb_peliculazas.Remove(oTabla);

                        db.SaveChanges();
                    }
                }
                Refrescar();
            }
            else
            {
                Refrescar();
            }
        }
    }
}
