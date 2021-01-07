using System;
using System.Data;
using MiLibreriaSqlite;
using System.Windows.Forms;
using LibreriaValidar;
using System.Drawing;

namespace EstacionamientoV4
{
    public partial class EditarCliente : VentanaEmergente
    {
        public EditarCliente()
        {
            InitializeComponent();
        }
        string tarjetonInicio = "", folio;
        string CMD_cargar, CMD_guardar;
        DataSet ds_cargar, ds_guardar;
        string placas, cmdPlacas;
        DataSet ds_placas;
        string placasInicio;
       
        public void cargarDatos()
        {
            try {
                CMD_cargar = string.Format("select folio_cliente,nombre,placas,vehiculo,cobro from clientes where folio_cliente='" + txtEditTarjeton.Text + "'");
                ds_cargar = ClassUtilidades.Ejecutar(CMD_cargar);
                txtEditClient.Text = ds_cargar.Tables[0].Rows[0]["nombre"].ToString().Trim();
                txtEditPlacas.Text = ds_cargar.Tables[0].Rows[0]["placas"].ToString().Trim();
                txtEditVehic.Text  = ds_cargar.Tables[0].Rows[0]["vehiculo"].ToString().Trim();
                txtEditCobro.Text = ds_cargar.Tables[0].Rows[0]["cobro"].ToString().Trim();
                txtEditTarjeton.Text = ds_cargar.Tables[0].Rows[0]["folio_cliente"].ToString().Trim();
                tarjetonInicio = txtEditTarjeton.Text;
                placasInicio = txtEditPlacas.Text;

            }
            catch(Exception error)
            {
                MessageBox.Show("No se pudo completar la operación: " + Environment.NewLine + error, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           
        }
        public Boolean GuardarEdit()
        {
            //ComprobarPlacas_estacionamiento()
            if (comprobarCliente_placas() && ComprobarPlacas_estacionamiento()) { 
               try
                 {
                 
                   CMD_guardar = string.Format("UPDATE  clientes SET folio_cliente ='"+txtEditTarjeton.Text+"', placas ='" + txtEditPlacas.Text+ "', vehiculo ='" + txtEditVehic.Text + "', cobro ='" + txtEditCobro.Text + "' where folio_cliente='" + tarjetonInicio + "'");
                   ds_guardar = ClassUtilidades.Ejecutar(CMD_guardar);
                   ds_guardar.Clear();
                   CMD_guardar = "";
                   return true;
                 }
               catch (Exception error)
                {
                    MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
            }
            else { return false; }
        }
        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar.Regexp("texto_c", txtEditClient, lblEditc) &&
                Validar.Regexp("texto_c", txtEditPlacas, lblEditp) &&
                Validar.Regexp("texto_c", txtEditVehic, lblEditv) &&
                Validar.Regexp("digito_c", txtEditCobro, lblEditc)&&
                Validar.Regexp("numero_c", txtEditTarjeton, lblEditTarjeton))
            {
                txtEditTarjeton.Text = txtEditTarjeton.Text.PadLeft(5, '0');
                if (getFolio_cliente2())
                {
                    if (GuardarEdit())
                    {
                        DialogResult = DialogResult.OK;
                        txtEditClient.Clear();
                        txtEditCobro.Clear();
                        txtEditPlacas.Clear();
                        txtEditVehic.Clear();
                        txtEditTarjeton.Clear();
                     
                    }
                }
            }

            else
            {
                MessageBox.Show("Los campos deben ser validos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }
        public Boolean getFolio_cliente2()
        {
            if(tarjetonInicio != txtEditTarjeton.Text) { 
                try
                {
                    string CMD = string.Format(" select folio_cliente from clientes where folio_cliente = '" + txtEditTarjeton.Text + "'");
                    DataSet ds = ClassUtilidades.Ejecutar(CMD);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        lblEditTarjeton.ForeColor = Color.Red;
                        lblEditTarjeton.Text = "Tarjetón ocupado";
                        return false;
                    }
                    else
                    {
                        txtEditTarjeton.Text = txtEditTarjeton.Text.PadLeft(5, '0');
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
            else
            {
                return true;
               
            }

        }
        private void EditarCliente_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

        }

        private void txtEditCobro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;
            }
            else if ((e.KeyChar == '.') && !txtEditCobro.Text.Contains(".")) //si ya tiene un punto no se escribe
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

        private void txtEditCobro_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("digito_c", txtEditCobro, lblEditc);
        }

        private void txtEditTarjeton_KeyPress(object sender, KeyPressEventArgs e)
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

        public bool comprobarCliente_placas()
        {
            if (placasInicio != txtEditPlacas.Text)
            {
                try
                {
              
                    string placas = txtEditPlacas.Text;
                    string CMD = string.Format(" select folio_cliente from clientes where placas='" + txtEditPlacas.Text + "'");
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
            else
            {
                return true;
            }
           
        }

        /*COMPROBAR SI LAS PLACAS ESTAN REGISTRADAS DENTRO DEL ESTACIONAMIENTO COMO ESTADIA*/
        /* COMPROBAR PLACAS EN VEHIC. ESTADIA*/
        public bool ComprobarPlacas_estacionamiento()
        {
            try
            {
                placas = txtEditPlacas.Text.Trim();
                cmdPlacas = "select placas from Ticket  where salida IS NULL and placas= @placas";
                ds_placas = ClassUtilidades.selectTable(cmdPlacas, "", placas, "", "", "", "", 0);

                if (ds_placas.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("El vehiculo ya esta registrado dentro del estacionamiento", "MiEstacionamiento",
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


    }
}
