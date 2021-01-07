using System;
using System.Data;
using System.Windows.Forms;
using LibreriaValidar;
using MiLibreriaSqlite;
using System.Drawing;

namespace EstacionamientoV4
{
    public partial class FormAgregarCliente : VentanaEmergente
    {
       

        string CMD_add;
        DataSet ds_add;
        string cmd_comprobar;
        DataSet ds_comprobar;

        string cmd_borrar;
        DataSet ds_borrar;

        string cmd_guardar;
        DataSet ds_guardar;

        string placas;
        string cmdPlacas;
        DataSet ds_placas;
        public FormAgregarCliente()
        {
            InitializeComponent();
            lblTarjeton.Text = "";
            datePago.MinDate = DateTime.Today.AddDays(1);
            datePago.Format = DateTimePickerFormat.Short;
   

        }
       
        private void BtnAceptar_Click(object sender, EventArgs e)
        {      
            if (Validar.Regexp("texto_c", txtNombre, lblNom) && 
                Validar.Regexp("texto_c", txtPlacas, lblPlacas) &&
                Validar.Regexp("texto_c", txtVehi, lblVehic) &&
                Validar.Regexp("digito_c", txtCobro, lblCob))
            {
                txtTarjeton.Text = txtTarjeton.Text.PadLeft(5, '0');
                if (getFolio_cliente())
                {
                        if (guardarCliente())
                        {
                        DialogResult = DialogResult.OK;
                        lblTarjeton.Text = "";
                     }
                }
            }

             else
            {
               MessageBox.Show("Los campos deben ser validos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               return;
             }

        }
        public Boolean  getFolio_cliente() {

            try { 
                CMD_add = string.Format(" select folio_cliente from clientes where folio_cliente = '" + txtTarjeton.Text + "'");
                ds_add = ClassUtilidades.Ejecutar(CMD_add);
                
                if (ds_add.Tables[0].Rows.Count > 0)
                {
                   
                    lblTarjeton.ForeColor = Color.Red;
                    lblTarjeton.Text = "Tarjetón ocupado";
                    return false;
                }
                else
                {                    
                    txtTarjeton.Text = txtTarjeton.Text.PadLeft(5, '0');
                    return true;
                }
               
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + error.Message, "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
        
        }

        public bool comprobarClienteBlackList()
        {
            try
            {
                string cliente = txtNombre.Text;
                string placas = txtPlacas.Text;
                cmd_comprobar = string.Format(" select id from black_list where nombre = '" + cliente + "'");
                ds_comprobar = ClassUtilidades.Ejecutar(cmd_comprobar);

                if (ds_comprobar.Tables[0].Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Este cliente esta suspendido, si se da de alta se eliminará su historial anterior y comenzará uno nuevo", "MiEstacionamiento", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.OK)
                    {
                        borrardeBlackList();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return true;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion " + error.Message, "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
        }
        public void borrardeBlackList()
        {
            try
            {
                string cliente = txtNombre.Text;
                string placas = txtPlacas.Text;
                cmd_borrar = string.Format(" DELETE FROM black_list WHERE nombre = '" + cliente + "'");
                ds_borrar = ClassUtilidades.Ejecutar(cmd_borrar);

               
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion " + error.Message, "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
      
            }
        }
        public bool comprobarCliente()
        {
            try
            {
                string cliente = txtNombre.Text;
                string placas = txtPlacas.Text;

                string CMD = string.Format(" select folio_cliente from clientes where nombre = '" + cliente + "' or placas='" + placas + "'");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    MessageBox.Show("El cliente o la placa ya esta registrado, no pueden haber registros duplicados", "MiEstacionamiento",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + error.Message, "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
        }
        public Boolean guardarCliente()
        {           
          if (comprobarCliente() && comprobarClienteBlackList())
             {
                if (ComprobarPlacas_estacionamiento()) {
                    try
                    {
                        string fecha = datePago.Value.Date.ToString("yyyy-MM-dd").Trim();
                        cmd_guardar = string.Format("insert into clientes (folio_cliente,nombre,placas,vehiculo,cobro,fecha_pago) values " +
                       "('" + txtTarjeton.Text + "' ,'" + txtNombre.Text + "' , '" + txtPlacas.Text + "', '" + txtVehi.Text + "', '" + txtCobro.Text + "', '" + fecha + "')");
                        ds_guardar = ClassUtilidades.Ejecutar(cmd_guardar);

                        cmd_guardar = "";
                        return true;
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return false;
                    }
                }
                     
              }
            return false;
             }

        private void txtCobro_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("digito", txtCobro, lblCob);
        }

        private void txtCobro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;             

            }
            else if ((e.KeyChar == '.') && !txtCobro.Text.Contains(".")) //si ya tiene un punto no se escribe
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //si es un control se escribe 
            {
                e.Handled = false;
            }

            else /*si no es las anteriores entonces no se escribe*/
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FormAgregarCliente_Load(object sender, EventArgs e)
        {
            datePago.Value = datePago.MinDate;
        }

        private void txtTarjeton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //si es un control se escribe 
            {
                e.Handled = false;
            }
            else /*si no es las anteriores entonces no se escribe*/
            {
                e.Handled = true;
            }
        }

        private void txtTarjeton_TextChanged(object sender, EventArgs e)
        {
            lblTarjeton.Text = "";
        }

        /*COMPROBAR SI LAS PLACAS ESTAN REGISTRADAS DENTRO DEL ESTACIONAMIENTO COMO ESTADIA*/
        /* COMPROBAR PLACAS EN VEHIC. ESTADIA*/
        public bool ComprobarPlacas_estacionamiento()
        {
            try
            {
                placas = txtPlacas.Text.Trim();
                cmdPlacas = "select placas from Ticket  where salida IS NULL and placas= @placas";
                ds_placas = ClassUtilidades.selectTable(cmdPlacas, "", placas, "", "", "", "", 0);

                if (ds_placas.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("La placa del vehículo ya esta registrada dentro del estacionamiento,ingrese otra placa", "MiEstacionamiento",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    cmdPlacas = "";
                    ds_placas.Clear();
                    return false;
                }
                else
                {
                    return true;
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
        }

        private void txtPlacas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;

            }
            else if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;

            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;

            }
            else if (Char.IsSeparator(e.KeyChar) && (!e.KeyChar.Equals(' ')))
            {
                e.Handled = false;

            }
            else if (e.KeyChar.Equals('-'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
        }
    }
}
