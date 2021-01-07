using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriaValidar;
using MiLibreriaSqlite;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using FoxLearn.License;

namespace EstacionamientoV4
{
    public partial class FormConfgiracion : FormBase
    {


        public FormConfgiracion()
        {
            InitializeComponent();

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.MultiSelect = false;
            cargarInformacionEstacionamiento();
            cargarLugares();
            cargarUsuarios();
            cargarSobrePrograma();

        }

        private static FormConfgiracion instance;

        public static FormConfgiracion getInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FormConfgiracion();
            }
            else
            {
                instance.BringToFront();
                instance.WindowState = FormWindowState.Normal;
            }


            return instance;

        }
       
    
        public void cargarInformacionEstacionamiento()
        {
            try
            {
                string CMD = string.Format("Select * From Estacionamiento_Datos");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    errorTxtNombreEsta.Text = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                    errorTxtDireccion.Text = ds.Tables[0].Rows[0]["direccion"].ToString().Trim();
                    errorTxtRfc.Text = ds.Tables[0].Rows[0]["rfc"].ToString().Trim();
                    txtTarifabp.Text = ds.Tables[0].Rows[0]["tarifa_boleto_perdido"].ToString().Trim();
                    txtTarifaChico.Text = ds.Tables[0].Rows[0]["tarifa_chico"].ToString().Trim();
                    txtTarifaG.Text = ds.Tables[0].Rows[0]["tarifa_grande"].ToString().Trim();
                    txtHorario.Text = ds.Tables[0].Rows[0]["horario"].ToString().Trim();
                    txtText.Text = ds.Tables[0].Rows[0]["texto"].ToString().Trim();
                    TxtTolerancia.Text = ds.Tables[0].Rows[0]["tolerancia"].ToString().Trim();
                }
          
                
            
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + error.Message, "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
        public void cargarLugares()
        {
            try
            {
                string CMD = string.Format("Select descripcion as lugar,status FROM Lugar order by idLugar ASC");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                dataGridView1.DataSource = ds.Tables[0];
              
            }
            catch (Exception e)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + e.Message, "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        public void cargarUsuarios()
        {
            try
            {
                string CMD = string.Format("Select usuario,tipo_usuario as tipo FROM Usuarios");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                dataGridView2.DataSource = ds.Tables[0];
                checkBoxMostrarPass.Checked = true;
                TxtUsuario.Clear();
                TxtContraseña.Clear();
                comboBoxTipoUsuario.SelectedIndex = 0;
                DataGridViewColumn column = dataGridView2.Columns[0];
                // column.Width = 150;
                // dataGridView2.Columns[1].Width = 100;
                label8.Text = "";
                label9.Text = "";
            }

            catch (Exception e)
            {
                MessageBox.Show("Fallo la operación cargar usuarios"+ Environment.NewLine+ e, "MiEstacionamiento",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }


        }
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            GuardarLugar();
          
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            BorrarLugar();
        }


        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            cargarLugares();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public bool ComprobarLugar(string lugar)
        {
            try
            {
                string lugarComprobar = lugar;

                string CMD = string.Format(" select descripcion FROM Lugar WHERE descripcion = '" + lugarComprobar + "'");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    // MessageBox.Show("el vehiculo ya esta registrado");
                    MessageBox.Show("El lugar ya esta registrado, no puede haber lugares duplicados", "MiEstacioanmiento",
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
                MessageBox.Show(error.Message);
                return false;
            }
        }

        public void AbrirVentanaEditarLugar()
        {
            
        }

        public void GuardarLugar()
        {
          try
             {
                float descripcion = 0;
                string CMD2 = string.Format("select COUNT (idLugar) as d from Lugar ");
                DataSet ds2 = ClassUtilidades.Ejecutar(CMD2);
                descripcion = Convert.ToSingle( ds2.Tables[0].Rows[0]["d"].ToString());
                descripcion = descripcion + 1;

                if (descripcion <= 70)
                {
                    string status = "0";
                    string CMD = string.Format("insert into  Lugar (status,descripcion) values('" + status + "' , '" + descripcion + "')");
                    DataSet ds = ClassUtilidades.Ejecutar(CMD);
                    cargarLugares();
                }
             }

          catch (Exception error)
             {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                
              }
        }
        
        public void BorrarLugar()
        {
             //string status = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow.Selected)
            {      
                 try
                    {
                        string CMD = string.Format(" DELETE FROM Lugar WHERE idLugar in (SELECT idLugar FROM Lugar WHERE status='0' or status is null order by idLugar DESC LIMIT 1)");
                        DataSet ds = ClassUtilidades.Ejecutar(CMD);
                        cargarLugares();
                    }

                 catch (Exception error)
                    {
                      MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                      MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
           }
       }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMostrarPass.Checked)
            {
                TxtContraseña.UseSystemPasswordChar = true;
            }
            else
            {
                TxtContraseña.UseSystemPasswordChar = false;
            }
        }
        public void GuardarUsuario()
        {
             if (Validar.Regexp("usuario", TxtUsuario, label8) && Validar.Regexp("password", TxtContraseña, label9))
             {
                if (ComprobarUsuario(TxtUsuario.Text.Trim()))
                {
                    try
                    {
                        string cmdCount = string.Format("select usuario , count (*) as no from Usuarios");
                        DataSet dsC = ClassUtilidades.Ejecutar(cmdCount);

                        if (Convert.ToSingle(dsC.Tables[0].Rows[0]["no"].ToString())<=10)
                        {                           
                            string CMD = string.Format("insert into  Usuarios (usuario,contraseña,tipo_usuario) values('" + TxtUsuario.Text.Trim() + "','" + TxtContraseña.Text.Trim() + "' ,'" + comboBoxTipoUsuario.Text.Trim() + "')");
                            DataSet ds = ClassUtilidades.Ejecutar(CMD);
                            MessageBox.Show("Se ha guardado exitosamente", "MiEstacionamiento",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            cargarUsuarios();

                        }
                        else
                        {
                            MessageBox.Show("No pueden existir más de 10 usuarios " , "MiEstacionamiento",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        }
                       
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error, no se ha podido completar la operacion: "+ Environment.NewLine + error.Message, "MiEstacionamiento",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return;
                    }
                }
             }
            else
            {
                MessageBox.Show("Los campos deben ser validos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           }
        public void AbrirVentanaEditarUsuario()
        {
            if (dataGridView2.Rows.Count > 0 && dataGridView2.CurrentRow.Selected)
            {
                FormEditarUsuarios feu = new FormEditarUsuarios();
                feu.usuario = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
             //   feu.comboBoxTipoUsuario.Text= dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString();
                if (feu.ShowDialog(this) == DialogResult.OK)
                {
                    cargarUsuarios();
                    feu.Close();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un lugar para editar", "MiEstacionamiento",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
        public void ComprobarUsuario_borrar()
        {
           // MessageBox.Show(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString());
            if( dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString()== "Administrador")
            {
                try
                {
                    string us = "select tipo_usuario , count (*) as u from Usuarios where tipo_usuario='Administrador'";
                    DataSet dsus = ClassUtilidades.Ejecutar(us);
                    if (Int32.Parse(dsus.Tables[0].Rows[0]["u"].ToString()) > 1)
                    {

                        BorrarUsuario();
                    }
                    else
                    {
                        MessageBox.Show("No se puede borrar este usuario. Debe de haber al menos un usuario Administrador", "MiEstacionamiento",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    }
                }

                catch (Exception error)
                {
                    MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
           else
            {
                BorrarUsuario();
            }
        }
        public void BorrarUsuario()
        {
            try
            {              
                    string usuarioBorrar = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
                    string CMD = string.Format(" DELETE FROM Usuarios WHERE usuario = '" + usuarioBorrar + "'");
                    DataSet ds = ClassUtilidades.Ejecutar(CMD);
                    cargarUsuarios();
                    MessageBox.Show("El usuario se ha  borrado exitosamente,cierre sesión para guardar cambios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);              
            }

            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        public bool ComprobarUsuario(string usuario)
        {
         try
            {
                string usuarioComprobar = usuario;

                string CMD = string.Format(" select usuario FROM Usuarios WHERE usuario = '" + usuarioComprobar + "'");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    // MessageBox.Show("el vehiculo ya esta registrado");
                   MessageBox.Show("El usuario ya esta registrado, no pueden haber usuarios duplicados", "MiEstacionamiento",
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

        public void GuardarInformacion()
        {
            
            if (!Validar.Regexp("nombreEstacionamiento", errorTxtNombreEsta, lblEstacionamiento) ||
                !Validar.Regexp("direccion", errorTxtDireccion, lblDireccion) ||
                !Validar.Regexp("rfc", errorTxtRfc, lblRfc) ||             
                !Validar.Regexp("digito", txtTarifaChico, lblTarifaCh) ||
                !Validar.Regexp("digito", txtTarifaG,lblTarifaGde ) ||
                !Validar.Regexp("digito", txtTarifabp, lblTarifabp)   ||
                !Validar.Regexp("numero", TxtTolerancia, lblTolerancia) 
                )
            {
                MessageBox.Show("Los campos deben ser validos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {

                    string cmd = string.Format("select * from Estacionamiento_Datos");
                    DataSet ds2 = ClassUtilidades.Ejecutar(cmd);


                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        string CMD = string.Format("UPDATE  Estacionamiento_Datos SET" +
                            " nombre = '" + errorTxtNombreEsta.Text.Trim() +
                            "',direccion = '" + errorTxtDireccion.Text.Trim() +
                            "',rfc= '" + errorTxtRfc.Text.Trim() +
                            "',tarifa_boleto_perdido='" + txtTarifabp.Text.Trim() +
                            "',tarifa_chico='" + txtTarifaChico.Text.Trim() +
                            "',tarifa_grande='" + txtTarifaG.Text.Trim() +                           
                            "',tolerancia='" + TxtTolerancia.Text.Trim() +
                            "',horario='" + txtHorario.Text.Trim() +
                            "',texto = '" + txtText.Text.Trim() +
                            "'");
                        DataSet ds = ClassUtilidades.Ejecutar(CMD);
                        MessageBox.Show("Se ha guardado exitosamente", "MiEstacioanmiento",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        try
                        {
                            string CMD = string.Format("insert into  Estacionamiento_Datos (nombre,direccion,rfc,tarifa_chico,tarifa_grande,tarifa_boleto_perdido,tolerancia,horario,texto) " +
                                "values('" + errorTxtNombreEsta.Text.Trim() + "','" + errorTxtDireccion.Text.Trim() + "' ,'" + errorTxtRfc.Text.Trim() + "' ,'" + txtTarifaChico.Text.Trim() + "' ,'" + txtTarifaG.Text.Trim() + "' ,'" + txtTarifabp.Text.Trim() + "' ,'" + TxtTolerancia.Text.Trim() + "','" + txtHorario.Text.Trim() + "','" + txtText.Text.Trim() + "')");
                            DataSet ds = ClassUtilidades.Ejecutar(CMD);
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + e.Message, "MiEstacionamiento",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                    }

                    cargarInformacionEstacionamiento();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error, no se ha podido completar la operacion: " + error.Message, "MiEstacioanmiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnAddUsuario_Click_1(object sender, EventArgs e)
        {
            GuardarUsuario();
        }

        private void BtnEliminarUsuario_Click(object sender, EventArgs e)
        {
            ComprobarUsuario_borrar();
        }

        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            // Regexp(@"^[A-Za-z][A-Za-z\d_.@]*$", TxtUsuario, label8);
            Validar.Regexp("usuario", TxtUsuario, label8);        
        }

        private void TxtContraseña_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("password", TxtContraseña, label9);
        }

        private void BtnEditUsuario_Click(object sender, EventArgs e)
        {
            AbrirVentanaEditarUsuario();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            cargarLugares();
            cargarUsuarios();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {         
            if (txtTarifabp.Text.ToString()=="")
            {
                txtTarifabp.Text = "0";
            }
           
            if (txtTarifaChico.Text.ToString() == "") {
                txtTarifaChico.Text = "0";
            }
            if (txtTarifaG.Text.ToString() == "")
            {
                txtTarifaG.Text = "0";
            }
            if (TxtTolerancia.Text.ToString() == "") {
                TxtTolerancia.Text = "0";
            }
            if(errorTxtRfc.Text.ToString()=="")
            {
                errorTxtRfc.Text = "-";
            }
            if (Validar.Regexp("nombreEstacionamiento", errorTxtNombreEsta, lblEstacionamiento) &&
                Validar.Regexp("direccion", errorTxtDireccion, lblDireccion) &&
                Validar.Regexp("digito", txtTarifabp, lblTarifabp) &&
                Validar.Regexp("digito", txtTarifaChico, lblTarifaCh) &&
                Validar.Regexp("digito", txtTarifaG, lblTarifaGde) &&
                Validar.Regexp("numero", TxtTolerancia, lblTolerancia) &&
                Validar.Regexp("texto",txtHorario,lblHorario)&&
                Validar.Regexp("rfc", errorTxtRfc, lblRfc))

            {
                GuardarInformacion();
               
            }

            else
            {
                MessageBox.Show("Los campos deben ser validos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void errorTxtNombreEsta_TextChanged_1(object sender, EventArgs e)
        {
            Validar.Regexp("nombreEstacionamiento", errorTxtNombreEsta, lblEstacionamiento);
        }

        private void errorTxtTelefono_TextChanged(object sender, EventArgs e)
        {
           // Validar.Regexp("telefono", errorTxtTelefono, LblTel);
        }

       
        private void errorTxtBox2_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("digito", txtTarifabp, lblTarifabp);
        }



        private void TxtTolerancia_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("numero", TxtTolerancia, lblTolerancia);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void errorTxtDireccion_TextChanged_1(object sender, EventArgs e)
        {
            Validar.Regexp("direccion", errorTxtDireccion, lblDireccion);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirVentanaEditarUsuario();
        }

        private void FormConfgiracion_Load(object sender, EventArgs e)
        {

        }

        private void TxtTolerancia_KeyPress(object sender, KeyPressEventArgs e)
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

        private void errorTxtRfc_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("rfc", errorTxtRfc, lblRfc);
        }

        private void errorTxtRfc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;

            }
        
            else if (Char.IsControl(e.KeyChar)) //si es un control se escribe 
            {
                e.Handled = false;              
            }
            else if (Char.IsLetter(e.KeyChar)) //si es un control se escribe 
            {
                e.Handled = false;
            }
            else if (e.KeyChar.ToString()=="-")
            {
                e.Handled = false;
            }

            else /*si no es las anteriores entonces no se escribe*/
            {
                e.Handled = true;
            }
        }

        private void refreshInfo_Click(object sender, EventArgs e)
        {
            cargarInformacionEstacionamiento();
        }

        private void TxtTarifaPerdido(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;

            }
            else if ((e.KeyChar == '.') && !txtTarifabp.Text.Contains(".")) //si ya tiene un punto no se escribe
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


        private void txtTarifabp_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("digito",txtTarifabp ,lblTarifabp);
        }

        private void txtHorario_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("texto",txtHorario,lblHorario);
        }

        private void txtTarifabp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;

            }
            else if ((e.KeyChar == '.') && !txtTarifabp.Text.Contains(".")) //si ya tiene un punto no se escribe
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

        private void txtTarifaG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;

            }
            else if ((e.KeyChar == '.') && !txtTarifaG.Text.Contains(".")) //si ya tiene un punto no se escribe
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

        private void txtTarifaChico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;

            }
            else if ((e.KeyChar == '.') && !txtTarifaChico.Text.Contains(".")) //si ya tiene un punto no se escribe
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

        private void txtTarifabp_TextChanged_1(object sender, EventArgs e)
        {
            Validar.Regexp("digito", txtTarifabp, lblTarifabp);
        }

        private void txtTarifaG_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("digito", txtTarifaG, lblTarifaGde);
        }

        private void txtTarifaChico_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("digito", txtTarifaChico, lblTarifaCh);
        }

        public void cargarSobrePrograma()
        {
            lblProductID.Text = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(lblProductID.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    lblProductName.Text = "MiEstacionamiento";
                    lblProductKey.Text = productKey;
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lblLicenseType.Text = string.Format(@"{0} días", (kv.Expiration - DateTime.Now.Date).Days);
                    }
                    else
                    {
                        lblLicenseType.Text = "Full";
                    }
                }
            }
        }
    
}

}

