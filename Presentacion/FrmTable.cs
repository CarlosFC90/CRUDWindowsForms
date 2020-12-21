using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUDUsers.Models;

namespace CRUDUsers.Presentacion
{
    public partial class FrmTable : Form
    {
        public int? id;
        Users oUser = null;

        public FrmTable(int? id = null)
        {
            InitializeComponent();

            this.id = id;
            if (id != null)
            {
                CargaDatos();
            }
        }

        private void CargaDatos()
        {
            using (CRUDWFormEntities db = new CRUDWFormEntities())
            {
                oUser = db.Users.Find(id);
                txtBoxName.Text = oUser.Nombre;
                txtBoxEmail.Text = oUser.Correo;
                dateTimePicker.Value = oUser.Fecha_Nacimiento;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(CRUDWFormEntities db = new CRUDWFormEntities())
            {
                if(id == null)
                {
                    oUser = new Users();
                }

                oUser.Nombre = txtBoxName.Text;
                oUser.Correo = txtBoxEmail.Text;
                oUser.Fecha_Nacimiento = dateTimePicker.Value;

                if(id == null)
                    db.Users.Add(oUser);

                else
                {
                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();

                this.Close();
            }
        }
    }
}
