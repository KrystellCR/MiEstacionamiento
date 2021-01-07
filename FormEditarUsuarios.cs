using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiLibreriaSqlite;
using LibreriaValidar;

namespace EstacionamientoV4
{
    public partial class FormEditarUsuarios : VentanaEmergente
    {
        public FormEditarUsuarios()
        {
            InitializeComponent();
        }
        public string usuario = "";

        private void FormEditarUsuarios_Load(object sender, EventArgs e)
        {
            TxtUsuario.Text = usuario;
            string CMD = string.Format(" Select contraseña FROM Usuarios WHERE usuario = '" + usuario + "'");
            DataSet ds = ClassUtilidades.Ejecutar(CMD);
            TxtContraseña.Text = ds.Tables[0].Rows[0]["contraseña"].ToString().Trim();
            label8.Text = "";
            label9.Text = "";
        }

        private void checkBoxMostrarPass_CheckedChanged(object sender, EventArgs e)
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

        public void EditarUsuario()
        {
            
            FormConfgiracion fc = new FormConfgiracion();
            if(usuario != TxtUsuario.Text.Trim())
            {
                if (Validar.Regexp("usuario", TxtUsuario, label8) && (Validar.Regexp("contraseña", TxtContraseña, label9)))
                {
                    if (fc.ComprobarUsuario(TxtUsuario.Text.Trim()))
                    {
                        updateUsuario();
                    }
                }
            }
            else
            {
                updateUsuario();
            }
        }
        public void updateUsuario()
        {
            try
            {
                string CMD = string.Format("UPDATE  Usuarios SET usuario ='" + TxtUsuario.Text + "', contraseña ='" + TxtContraseña.Text + "' where usuario='" + usuario + "'");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                MessageBox.Show("Se ha guardado exitosamente", "MiEstacioanmiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }

            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            Validar.Regexp("usuario", TxtUsuario, label8);
           
        }

        private void TxtContraseña_TextChanged(object sender, EventArgs e)
        {
           
            Validar.Regexp("password", TxtContraseña, label9);
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            EditarUsuario();
        }
    }
}
