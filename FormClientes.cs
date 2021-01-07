using System;
using System.Data;
using System.Windows.Forms;
using MiLibreriaSqlite;
using System.Drawing;
using System.Drawing.Printing;

namespace EstacionamientoV4
{
    public partial class FormClientes : FormBase
    {
        public FormClientes()
        {
            InitializeComponent();
            this.dataClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataClientes.MultiSelect = false;
            this.dataBlackList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataBlackList.MultiSelect = false;
           /*autosize*/

          
       
        }
        public string nombre_cliente="";
        public string cobro_cliente="";
        public string fecha_pago = "";
        public string f_cliente = "";
        public string fecha_ultimo_pago = "";

        DataSet ds_bl;
        string CMD_bl;
        string CMD_clientes;
        DataSet ds_clientes;
        string today;
        string cmd_ultimo;
        DataSet ds_ultimo;


        int maxCar = 0, x = 0, y = 0;
        string nombre = "", rfc = "", direccion = "", texto = "", horario = "";
        private static FormClientes instance;

        public static FormClientes getInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FormClientes();
            }
            else
            {
                instance.BringToFront();
                instance.WindowState = FormWindowState.Normal;
            }

            return instance;
        }
        private void BtnAddCliente_Click(object sender, EventArgs e)
        {
            FormAgregarCliente faC = new FormAgregarCliente();
            if (faC.ShowDialog(this) == DialogResult.OK)
            {
                faC.Close();
            }
            cargarClientes();
            cargarBlackList();
        }
        public void cargarBlackList()
        {
            try
            {
                CMD_bl = string.Format("Select nombre as Cliente,fecha_de_baja as Fecha_de_baja from black_list  order by  fecha_de_baja DESC");
                ds_bl = ClassUtilidades.Ejecutar(CMD_bl);
                dataBlackList.DataSource = ds_bl.Tables[0];
               

            }
            catch (Exception e)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + e.Message, "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnCargar_Click_1(object sender, EventArgs e)
        {
            cargarClientes();
            cargarBlackList();
        }

      

        private void dataClientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataClientes.Rows)
            {
                if (row.Cells["Estatus"].Value.ToString() != "Corriente")
                {                
                    row.Cells["Estatus"].Style.BackColor = Color.IndianRed;
                    row.Cells["Estatus"].Style.ForeColor = Color.White;

                }
              
            }

        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            cargarClientes();
            cargarBlackList();
        }

        public void cargarClientes()
        {
            try
            {              
                today=DateTime.Today.ToString("yyyy-MM-dd");
                CMD_clientes = string.Format("Select folio_cliente as Folio,nombre as Cliente,placas as Placas,vehiculo as Vehiculo,cobro as Cuota, fecha_ultimo_pago as Fecha_ultimo_pago,fecha_pago as Fecha_de_pago, CASE when fecha_ultimo_pago isnull then 'Pendiente'  WHEN Cast(julianday('" + today + "') - julianday(fecha_pago)As integer) <= 0 THEN 'Corriente'  WHEN Cast(julianday('" + today + "') - julianday(fecha_pago)As integer) > 0  THEN 'Pendiente' END As Estatus  from clientes order by  Estatus DESC");
                ds_clientes = ClassUtilidades.Ejecutar(CMD_clientes);
                dataClientes.DataSource = ds_clientes.Tables[0];
                dataClientes.Columns[0].Width = 70;


            }
            catch (Exception e)
            {
                MessageBox.Show("CARGAR CLIENTES Error, no se ha podido completar la operacion: " + e.Message, "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            

        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {

            if (dataClientes.Rows.Count > 0
                    && dataClientes.CurrentRow.Selected
                    && !String.IsNullOrEmpty(dataClientes.Rows[dataClientes.CurrentRow.Index].Cells[0].Value.ToString()))
            {

                FormCobrarClientes fcc = new FormCobrarClientes();

                fcc.fecha_ultimo_pago = dataClientes.Rows[dataClientes.CurrentRow.Index].Cells["Fecha_ultimo_pago"].Value.ToString();
                fcc.lblNC.Text = dataClientes.Rows[dataClientes.CurrentRow.Index].Cells[1].Value.ToString();
                fcc.lblCobro.Text = dataClientes.Rows[dataClientes.CurrentRow.Index].Cells[4].Value.ToString();
                fcc.fecha_pago =    dataClientes.Rows[dataClientes.CurrentRow.Index].Cells["Fecha_de_pago"].Value.ToString();
                fcc.lblFolio.Text = dataClientes.Rows[dataClientes.CurrentRow.Index].Cells[0].Value.ToString();
                        
                if (fcc.ShowDialog(this) == DialogResult.OK)
                {
                    fcc.Close();                
                    fcc.lblNC.Text = "";
                    fcc.lblCobro.Text = "";
                    fcc.lblFolio.Text = "";
                    fcc.fecha_pago = "";
                    fcc.fecha_ultimo_pago = "";
                }

            }
            else
            {
                MessageBox.Show("No hay datos seleccionados", "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }

            cargarClientes();
            cargarBlackList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataClientes.Rows.Count > 0
                   && dataClientes.CurrentRow.Selected
                   && !String.IsNullOrEmpty(dataClientes.Rows[dataClientes.CurrentRow.Index].Cells[0].Value.ToString()))
              {
                    EditarCliente editar_cliente = new EditarCliente();
                    editar_cliente.txtEditTarjeton.Text = dataClientes.Rows[dataClientes.CurrentRow.Index].Cells[0].Value.ToString();

                if (editar_cliente.ShowDialog(this) == DialogResult.OK)
                 {
                    editar_cliente.Close();
                    editar_cliente.txtEditTarjeton.Text = "";
                }
               }
           
            cargarClientes();
            cargarBlackList();
        }

        public Boolean suspenderCliente()
        {
            if (dataClientes.Rows.Count > 0
                  && dataClientes.CurrentRow.Selected
                  && !String.IsNullOrEmpty(dataClientes.Rows[dataClientes.CurrentRow.Index].Cells[0].Value.ToString()))
            {
                string suspender_folio = dataClientes.Rows[dataClientes.CurrentRow.Index].Cells[0].Value.ToString();
                try
                {
                    string CMD = string.Format("DELETE FROM clientes WHERE folio_cliente = '" + suspender_folio + "'");
                    DataSet ds = ClassUtilidades.Ejecutar(CMD);
                    return true;
                    
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }

            }
            else
            {
                MessageBox.Show("No hay datos seleccionados", "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                return false;
            }
           
        }
       
       
        private void btnSuspender_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea suspender a este cliente?", "Aviso", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                suspenderCliente();
                cargarBlackList();
                cargarClientes();
            }         

        }
      

        private void btnCargar_Click(object sender, EventArgs e)
        {
            cargarBlackList();
            cargarClientes();
            nombre_cliente = "";
            cobro_cliente = "";
            fecha_pago = "";
            f_cliente = "";
            fecha_ultimo_pago = "";
            CMD_bl = "";
            CMD_clientes = "";
            today = "";
    }
  }
}



