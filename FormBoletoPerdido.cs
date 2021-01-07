using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using MiLibreriaSqlite;
using LibreriaValidar;
namespace EstacionamientoV4
{
    public partial class FormBoletoPerdido : FormBase

    {   
        
        public FormBoletoPerdido()
        {
            InitializeComponent();
        }
        DataSet ds;
        public string variablex = "", variableP = "";
        private static FormBoletoPerdido instance;
        public static FormBoletoPerdido getInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FormBoletoPerdido();
            }
            else
            {
                instance.BringToFront();
            }

            return instance;
        }

        private void FormBoletoPerdido_Load(object sender, EventArgs e)
        { 
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
           
            cargarDataGrid();
        }

        private void BtnCargar_Click(object sender, EventArgs e)
        {

        }


        public void cargarDataGrid()
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                string placas = txtPlacas.Text.Trim();
                if (String.IsNullOrEmpty(placas))
                {
                    string  query = "SELECT folio,placas,observaciones,entrada FROM Ticket WHERE (salida IS NULL or salida ='' ) ORDER BY entrada desc";
                    ds = ClassUtilidades.selectTable(query, "", "", "", "",  "", "",0);
                    
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Width = 40;
                    dataGridView1.Columns[1].Width = 70;
                    dataGridView1.Columns[2].Width = 70;
                    dataGridView1.Columns[3].Width = 120;
                    
                }
                else
                {
                    string query = "SELECT folio,placas,entrada FROM Ticket WHERE placas =  @placas  and (salida IS NULL or salida= '') ORDER by entrada desc";
                    ds = ClassUtilidades.selectTable(query, "",placas, "", "", "", "",0);
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Width = 40;
                    dataGridView1.Columns[1].Width = 70;
                    dataGridView1.Columns[2].Width = 70;
                    dataGridView1.Columns[2].Width = 120;
                    
                }
            }
           

            catch (Exception error)
            {
                MessageBox.Show("La operación no se pudo completar" + Environment.NewLine +error.Message);
            }

        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {

            seleccionarFolio();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            seleccionarFolio();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarFolio();
        }

        public void seleccionarFolio ()
        {
            try
            {
                if (dataGridView1.Rows.Count > 0
                    && dataGridView1.CurrentRow.Selected 
                    && !String.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString()))
                   {
                      
                    variablex = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                    variableP= dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                    DialogResult = DialogResult.OK;
                   
                }
                else
                {
                    MessageBox.Show("No hay datos seleccionados", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("La operacion no se pudo completar"+ Environment.NewLine +error.Message);
            }

        }
        private void txtPlacas_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtPlacas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cargarDataGrid();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarFolio();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (Validar.ValidarFormulario(this, errorProvider1) == false)
            {
                cargarDataGrid();
            }

            else
            {
                MessageBox.Show("El campo placas no puede estar vacio", "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
