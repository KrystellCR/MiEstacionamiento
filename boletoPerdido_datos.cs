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

namespace EstacionamientoV4
{
    public partial class boletoPerdido_datos : FormBase
    {
        public boletoPerdido_datos()
        {
            InitializeComponent();
        }
        DataSet ds;
        public string variablex = "", variableP = "";
        float tarifabp = 0;
        int maxCar, x, y;
        string nombre, texto, direccion;
        private static boletoPerdido_datos instance;
        public static boletoPerdido_datos getInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new boletoPerdido_datos();
            }
            else
            {
                instance.BringToFront();
            }

            return instance;
        }

        private void boletoPerdido_datos_Load(object sender, EventArgs e)
        {
            try {
                comboBoxIdent.SelectedIndex = 0;
                string CMD = string.Format("Select tarifa_boleto_perdido From Estacionamiento_Datos");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                tarifabp = Convert.ToSingle(ds.Tables[0].Rows[0]["tarifa_boleto_perdido"].ToString());
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion, ingrese la tarifa de boleto perdido : " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FormBoletoPerdido formboletoPerdido = new FormBoletoPerdido();
            if (formboletoPerdido.ShowDialog(this) == DialogResult.OK)
            {
                txtFolio.Text = formboletoPerdido.variablex;
                textPlacas.Text = formboletoPerdido.variableP;
                variablex = formboletoPerdido.variablex;
                
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (guardar())
            {
                printTicket();
                if (MessageBox.Show("¿Desea imprimir otro ticket?", "Aviso", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    printTicket();
                }

                DialogResult = DialogResult.OK;

            }

        }

        public bool guardar()
        {
            if (!String.IsNullOrEmpty(txtFolio.Text.Trim()) &&
                !String.IsNullOrEmpty(textBoxNombre.Text.Trim()) &&
                !String.IsNullOrEmpty(txtMarca.Text.Trim()) &&
                !String.IsNullOrEmpty(textPlacas.Text.Trim()) &&
                !String.IsNullOrEmpty(comboBoxIdent.Text.Trim()) &&
                !String.IsNullOrEmpty(txtNo.Text.Trim()))
            {
                try
                {
                    string folio = txtFolio.Text.ToString();
                    string CMD2 = string.Format("UPDATE Ticket set cargos= ('"+ tarifabp.ToString() +"') where folio= ('" + folio + "')");
                    ds = ClassUtilidades.Ejecutar(CMD2); ;
                    return true;
                }

                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Debe de llenar todos los campos" , "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                return false;
            }

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

        }

        /** configuracion de la hoja del ticket **/
        public void printTicket()
        {
            PrintDocument pd = new PrintDocument();
            x = 222;//222 315
            y = 620;//centesimas de pulgada (para saltos de 10 , es saltos *10 +20 //21 saltos
            //y = 480;
            maxCar = 34; //34 47

            try
            {
                string CMD = string.Format("select * from impresion Limit 1");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                string impresora = ds.Tables[0].Rows[0]["impresora"].ToString();
                pd.PrinterSettings.PrinterName = impresora;
                x = (Int32.Parse(ds.Tables[0].Rows[0]["tamaño"].ToString()));
                maxCar = (Int32.Parse(ds.Tables[0].Rows[0]["caracteres"].ToString()));
                maxCar = maxCar - 8; //porque el texto esta en mayusculas
                

            }
            catch (Exception error)
            {
                MessageBox.Show("Error al intentar imprimir por falta de datos, configure la impresora" + Environment.NewLine+ error, "MiEstacionamiento",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            PaperSize ps = new PaperSize("", x, y);
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

            pd.PrintController = new StandardPrintController();
            pd.DefaultPageSettings.Margins.Left = 0;
            pd.DefaultPageSettings.Margins.Right = 0;
            pd.DefaultPageSettings.Margins.Top = 0;
            pd.DefaultPageSettings.Margins.Bottom = 0;
            pd.DefaultPageSettings.PaperSize = ps;

            try
            {
                pd.Print(); //manda a imprimir el texto
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*configura el posicionamiento del texto en el ticket */
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {


            try
            {
                /*tomar datos */
                string CMD = string.Format("Select * From Estacionamiento_Datos");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                nombre = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                direccion = ds.Tables[0].Rows[0]["direccion"].ToString().Trim();
                /*fin de tomar datos*/
                texto = "Bajo protesta de decir la verdad y derivado de" +
                    " haber extraviado el boleto/ticket de estacionamiento con numero de folio: " +
                    " " + txtFolio.Text.Trim() + " el cual ampara la estancia de vehiculo marca: " 
                    + txtMarca.Text.Trim()+ " placas: " + textPlacas.Text.Trim()+ 
                    "; con identeficacion oficial: "+ comboBoxIdent.Text+ " con folio: "
                    + txtNo.Text.Trim()+ " retirando dicho vehiculo del estacionamiento ubicado en: " + direccion+ 
                    " y acepto dejar sin efecto el boleto/ticket original en caso de localizarlo posteriormente," +
                    "eximiendo de cualquier responsbilidad civil y penal a el estacionamiento de comercio y/o: " + nombre+ Environment.NewLine  +Environment.NewLine+ " Nombre y firma" ;
                texto=texto.ToUpper();
               
            }
            catch (Exception error)
            {
                MessageBox.Show("La operación imprimir boleto perdido falló:" + Environment.NewLine +error, "MiEstacionamiento",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            try
            {
                int SPACE = 110;
                Graphics g = e.Graphics;
                g.DrawRectangle(Pens.Aqua, 0, 0, x, y);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                Font fBody = new Font("Arial", 10, FontStyle.Bold);
                Font fBody1 = new Font("Arial", 9, FontStyle.Regular);
                Font fontSmall = new Font("Arial", 7, FontStyle.Regular);
                Font rs = new Font("Stencil", 25, FontStyle.Bold);
                Font fTType = new Font("", 150, FontStyle.Bold);
                SolidBrush sb = new SolidBrush(Color.Black);
                g.DrawString(WordWrap(nombre, maxCar), fBody1, sb, x / 2 - 10, 20, stringFormat);
                g.DrawString("IMP." + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 40, stringFormat);
                g.DrawString("*************************************************************", fBody, sb, 0, 50);
                g.DrawString(WordWrap(textBoxNombre.Text.Trim(), maxCar).ToUpper(), fBody1, sb, 0, 70);
                g.DrawString(WordWrap(texto, maxCar), fBody1, sb, 0, SPACE);
              //  g.DrawString(WordWrap("NOMBRE Y FIRMA", maxCar), fBody1, sb, 0, 350);

            }
            catch (Exception error)
            {
                MessageBox.Show("Error en la operacion de imprimir por falta de datos: " + Environment.NewLine +error.Message);
            }

        }

        public static string WordWrap(string text, int width)
        {
            int pos, next;
            string xx;
            StringBuilder sb = new StringBuilder();

            // Lucidity check
            if (width < 1)
                return text;

            // Parse each line of text
            for (pos = 0; pos < text.Length; pos = next)
            {
                // Find end of line
                int eol = text.IndexOf(Environment.NewLine, pos);
                if (eol == -1)
                    next = eol = text.Length;
                else
                    next = eol + Environment.NewLine.Length;

                // Copy this line of text, breaking into smaller lines as needed
                if (eol > pos)
                {
                    do
                    {
                        int len = eol - pos;
                        if (len > width)
                            len = BreakLine(text, pos, width);
                        sb.Append(text, pos, len);
                        sb.Append(Environment.NewLine);

                        // Trim whitespace following break
                        pos += len;
                        while (pos < eol && Char.IsWhiteSpace(text[pos]))
                            pos++;
                    } while (eol > pos);
                }
                else sb.Append(Environment.NewLine); // Empty line
            }
            xx = sb.ToString();
            return xx;
        }

        /// <summary>
        /// Locates position to break the given line so as to avoid
        /// breaking words.
        /// </summary>
        /// <param name="text">String that contains line of text</param>
        /// <param name="pos">Index where line of text starts</param>
        /// <param name="max">Maximum line length</param>
        /// <returns>The modified line length</returns>
        private static int BreakLine(string text, int pos, int max)
        {
            // Find last whitespace in line
            int i = max;
            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
                i--;

            // If no whitespace found, break at maximum length
            if (i < 0)
                return max;

            // Find start of whitespace
            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
                i--;

            // Return length of text before whitespace
            return i + 1;
        }

        private void placasKeyPress(object sender, KeyPressEventArgs e)
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

