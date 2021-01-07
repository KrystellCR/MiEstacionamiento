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
namespace EstacionamientoV4
{
    public partial class dialogoBorrarRegistros : VentanaEmergente
    {
        public dialogoBorrarRegistros()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {

                try
                {
                    string cmd = "Select * FROM Usuarios WHERE tipo_usuario='Administrador' and usuario = '" + textBox1.Text.ToString().Trim() + "'  and contraseña ='" + textBox2.Text.ToString().Trim() + "'";
                    DataSet ds = ClassUtilidades.Ejecutar(cmd);
                    FormEntrada formEntrada = new FormEntrada();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("DE CLICK EN CONTINUAR PARA BORRAR TODOS LOS REGISTROS DE SALIDA, EN CANCELAR PARA SALIR DE LA PANTALLA", "Información", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            cmd = "delete from Ticket_salida";
                            ds = ClassUtilidades.Ejecutar(cmd);
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            DialogResult = DialogResult.Cancel;
                        }
                    }

                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrecta, intente de nuevo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                }
                catch (Exception error)
                {
                    MessageBox.Show("No se pudo  completar  la  operacion  :"  + Environment.NewLine +error, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
