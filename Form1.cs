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
using System.Text.RegularExpressions;
using MiLibreriaSqlite;
using System.Data.SQLite;
using FoxLearn.License;

namespace EstacionamientoV4
{
    
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();          
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
        }
        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            if (registrar())
            {
                Login();
            }
            
        }

        public void Login()
        {
           
            if (Validar.ValidarFormulario(this, errorProvider1) == false)
            {

                try
                {
                    string cmd = "Select * FROM Usuarios WHERE usuario = '" + txtUsuario.Text.ToString().Trim() + "'  and contraseña ='" + textBoxPassword.Text.ToString().Trim() + "'";
                    DataSet ds = ClassUtilidades.Ejecutar(cmd);
                    FormEntrada formEntrada = new FormEntrada();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["tipo_usuario"].ToString() == "Usuario")
                        {
                            formEntrada.btnInventario.Hide();
                            formEntrada.btnConfiguracion.Hide();                           
                            formEntrada.btnBorrar.Hide();
                            formEntrada.tipo_usuario = true;

                        }
                        else
                        {
                            formEntrada.tipo_usuario = false;
                        }
                        this.Hide();
                        formEntrada.Show();
                        
                    } 
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrecta, intente de nuevo" , "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }

                catch (Exception error)
                { 
                    MessageBox.Show("No se pudo completar la operación: "+ Environment.NewLine +error, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }          
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();

        }

        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();            
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (registrar())
                {
                    Login();
                }
            }
        }
    

        private void checkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !checkShowPass.Checked;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Rectangle rect = new Rectangle(txtUsuario.Location.X, txtUsuario.Location.Y, txtUsuario.ClientSize.Width, txtUsuario.ClientSize.Height);
            rect.Inflate(2, 2); // border thickness
            System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics, rect, Color.DeepSkyBlue, ButtonBorderStyle.Solid);

            System.Drawing.Rectangle rect2 = new Rectangle(textBoxPassword.Location.X, textBoxPassword.Location.Y, textBoxPassword.ClientSize.Width, textBoxPassword.ClientSize.Height);
            rect2.Inflate(1, 1); // border thickness
            System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics, rect2, Color.DeepSkyBlue, ButtonBorderStyle.Solid);

        }

        public bool registrar()
        {
            KeyManager km = new KeyManager(ComputerInfo.GetComputerId());
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    // lblProductName.Text = "FoxLearn";
                    // lblProductKey.Text = productKey;
                    if (kv.Type == LicenseType.TRIAL)
                    {
                      
                        string.Format(@"{0} days", (kv.Expiration - DateTime.Now.Date).Days);
                    

                        if (DateTime.Now.Date > kv.Expiration)
                        {
                            using (fmRegistration frm = new fmRegistration())
                            {
                                frm.ShowDialog();
                            }
                            return false;
                        }
                        else
                        {                        
                            return true;
                        }
                    }
                    else
                    {
                        return true;   //lblLicenseType.Text = "Full";
                    }

                }
                else
                {
                    using (fmRegistration frm = new fmRegistration())
                    {
                        frm.ShowDialog();
                    }
                    return false;
                }
            }
            else
            {
                using (fmRegistration frm = new fmRegistration())
                {
                    frm.ShowDialog();
                }
                return false;
            }
        } 

    }
}
