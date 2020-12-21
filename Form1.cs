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

namespace CRUDUsers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        #region Helper
        private void Refrescar()
        {
            using (CRUDWFormEntities db = new CRUDWFormEntities())
            {
                var lst = from d in db.Users
                          select d;

                dataGridView1.DataSource = lst.ToList();
            }
        }

        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Presentacion.FrmTable ofrmTable = new Presentacion.FrmTable();
            ofrmTable.ShowDialog();

            Refrescar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int? id = GetId();

            if (id != null)
            {
                Presentacion.FrmTable oFrmTable = new Presentacion.FrmTable(id);
                oFrmTable.ShowDialog();

                Refrescar();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int? id = GetId();

            if (id != null)
            {
                using (CRUDWFormEntities db = new CRUDWFormEntities())
                {
                    Users oUsers = db.Users.Find(id);
                    db.Users.Remove(oUsers);

                    db.SaveChanges();
                }

                Refrescar();
            }
        }
    }
}
