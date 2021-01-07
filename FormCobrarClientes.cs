using System;
using System.Data;
using System.Windows.Forms;
using MiLibreriaSqlite;
using System.Drawing;
using System.Text;
using LibreriaValidar;
using System.Drawing.Printing;
namespace EstacionamientoV4
{
    public partial class FormCobrarClientes : VentanaEmergente
    {
        public string fecha_pago = "", fecha_ultimo_pago = "";
        int maxCar=0, x=0, y=0;
        string nombre="", rfc="", direccion="", texto="", horario="", today="", nextDate;
        string CMD_cobrar;
        DataSet ds_cobrar;

        string CMD_print;
        DataSet ds_print;
        string CMD_p2;
        DataSet ds_p2;
        private void FormCobrarClientes_Load(object sender, EventArgs e)
        {

            today = "";
            nextDate = "";
        }

        public FormCobrarClientes()
        {
            InitializeComponent();
        }
        public string getNextDate()
        {
            try {
                /*si la fecha de ultimo pago es vaacia quiere decir que no se ha dado la cuota 
                inicia entonces la fecha del ultimo pago seguira siendo la misma*/
                if (fecha_ultimo_pago != "") { 
                DateTime dateTimeObj = DateTime.Parse(fecha_pago);
                DateTime futureDate = dateTimeObj.AddMonths(1);
                return futureDate.ToString("yyyy-MM-dd");
               }
                else
                {
                    return fecha_pago;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return "";
            }


        }
        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (cobrar())
            {
                printTicket();
                DialogResult = DialogResult.OK;
               
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            lblCobro.Text = "";
            lblNC.Text = "";
            fecha_pago = "";
            fecha_ultimo_pago = "";
            DialogResult = DialogResult.Cancel;
        }


        public Boolean cobrar()
        {
            try
            {
               
                today = DateTime.Today.ToString("yyyy-MM-dd");
                //cuando se updetea la fecha_pago se lanza una disparador  a la tabla pension_transaccion
                CMD_cobrar = string.Format("UPDATE  clientes SET fecha_pago ='" + getNextDate() + "',fecha_ultimo_pago='" + today + "' where folio_cliente='" + lblFolio.Text + "'");
                ds_cobrar = ClassUtilidades.Ejecutar(CMD_cobrar);
                CMD_cobrar = "";
                ds_cobrar.Clear();
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;

            }
        }

        public void printTicket()
        {
            PrintDocument pd2 = new PrintDocument();
           
             x = 222;//222 315
             y = 310;//centesimas de pulgada (para saltos de 10 , es saltos *10 +20 //21 saltos
            maxCar = 34; //34 47

            try
            {
                CMD_print = string.Format("select * from impresion Limit 1");
                ds_print = ClassUtilidades.Ejecutar(CMD_print);
                string impresora = ds_print.Tables[0].Rows[0]["impresora"].ToString();
                pd2.PrinterSettings.PrinterName = impresora;
                x = (Int32.Parse(ds_print.Tables[0].Rows[0]["tamaño"].ToString()));
                maxCar = (Int32.Parse(ds_print.Tables[0].Rows[0]["caracteres"].ToString()));
                CMD_print = "";
                ds_print.Clear();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al intentar imprimir por falta de datos, configure la impresora" + Environment.NewLine + error, "MiEstacionamiento",
             MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }

            try
            {
                
                 PaperSize ps2 = new PaperSize("", x, y);
                 pd2.PrintPage += new PrintPageEventHandler(pd2_PrintPage);

                pd2.PrintController = new StandardPrintController();
                pd2.DefaultPageSettings.Margins.Left = 0;
                pd2.DefaultPageSettings.Margins.Right = 0;
                pd2.DefaultPageSettings.Margins.Top = 0;
                pd2.DefaultPageSettings.Margins.Bottom = 0;
                pd2.DefaultPageSettings.PaperSize = ps2;
                pd2.Print();
                if (MessageBox.Show("¿Desea imprimir otro ticket?", "Aviso", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    pd2.Print();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        void pd2_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                /*tomar datos */
                CMD_p2 = string.Format("Select * From Estacionamiento_Datos");
                ds_p2 = ClassUtilidades.Ejecutar(CMD_p2);
                if (ds_p2.Tables[0].Rows.Count > 0)
                {
                    nombre = ds_p2.Tables[0].Rows[0]["nombre"].ToString().Trim();
                    texto = ds_p2.Tables[0].Rows[0]["texto"].ToString().Trim();
                    horario = ds_p2.Tables[0].Rows[0]["horario"].ToString().Trim();
                    rfc = ds_p2.Tables[0].Rows[0]["rfc"].ToString().Trim();
                    direccion = ds_p2.Tables[0].Rows[0]["direccion"].ToString().Trim();
                }
                else
                {

                }

                /*fin de tomar datos*/
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al cargar los datos en ticket salida " + Environment.NewLine + error, "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                int SPACE = 130;
                Graphics g = e.Graphics;
                g.DrawRectangle(Pens.Aqua, 0, 0, x, y);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                Font fBody = new Font("Arial", 10, FontStyle.Bold);
                Font fBody1 = new Font("Arial", 9, FontStyle.Regular);
                Font fontSmall = new Font("Arial", 8, FontStyle.Regular);
                Font rs = new Font("Stencil", 25, FontStyle.Bold);
                Font fTType = new Font("", 150, FontStyle.Bold);
                SolidBrush sb = new SolidBrush(Color.Black);


                g.DrawString(nombre, fBody1, sb, x / 2 - 10, 10, stringFormat);
                g.DrawString(WordWrap(direccion, maxCar), fBody1, sb, x / 2 - 10, 30, stringFormat);
                g.DrawString(WordWrap(rfc, maxCar), fBody1, sb, x / 2 - 10, 50, stringFormat);
                g.DrawString("IMP." + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 70, stringFormat);
                g.DrawString("*************************************************************", fBody1, sb, 0, 90);
                RectangleF srcRect = new RectangleF(x / 4 - 10, 40, x / 2, 50);
                g.DrawString("Tarjetón:" + lblFolio.Text.ToString(), fBody1, sb, x / 2, 110, stringFormat);
                g.DrawString("Cuota: " + lblCobro.Text, fBody1, sb, x / 2, SPACE, stringFormat);
                g.DrawString("Cliente: "+ lblNC.Text, fontSmall, sb, x / 2, SPACE+20, stringFormat);
              
                string fecha = getNextDate();
                g.DrawString("Se vence el: " + fecha, fontSmall, sb, x / 2, SPACE + 40, stringFormat);
                g.DrawString((horario), fontSmall, sb, x / 2, y - 80, stringFormat);
                   
            }
            catch (Exception error)
            {

                MessageBox.Show("Error en la operacion de imprimir :  " + error.Message);
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


    }
}
