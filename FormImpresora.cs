using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using MiLibreriaSqlite;

namespace EstacionamientoV4
{
    public partial class FormImpresora : VentanaEmergente
    {
        public FormImpresora()
        {
            InitializeComponent();

        }
        string direccion ="", rfc ="",nombre = "", texto = "", horario = "", precioHora = "", precioDia = "", tolerancia = "";
        int x, y=500, maxCar;
        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            string CMD;
            DataSet ds;
        
            try
            {
                /*tomar datos */
                double c = Convert.ToSingle(cmboTamaño.Text.Trim().ToString())/1.9;//1.7
                int caracteres= (int)Math.Round(c);
            
                Double tamaño = Math.Floor(Convert.ToSingle(cmboTamaño.Text.Trim()) * 0.0393701)*100;

                CMD = string.Format("SELECT * FROM impresion");
                ds = ClassUtilidades.Ejecutar(CMD);
                if (ds.Tables[0].Rows.Count > 0)
                {
                  //  CMD = string.Format("UPDATE impresion SET impresora='" + cboImpresoras.Text.Trim() + "' ,tamaño='" + tamaño + "',caracteres='" + caracteres + "' WHERE id ='2')");
                    CMD = string.Format("UPDATE  impresion SET impresora ='" + cboImpresoras.Text.Trim() + "',tamaño='" + tamaño + "',caracteres='" + caracteres + "',  imp_tam ='" + cmboTamaño.Text.Trim() + "' where id='1'");
                   // DataSet ds = ClassUtilidades.Ejecutar(CMD);
                    ds = ClassUtilidades.Ejecutar(CMD);
                }
                else
                {
                    CMD = string.Format("insert into impresion (impresora,tamaño,caracteres,imp_tam) values ('" + cboImpresoras.Text.Trim() + "','" + tamaño + "','" + caracteres + "','" + cmboTamaño.Text.ToString() + "')");
                    ds = ClassUtilidades.Ejecutar(CMD);
                }

          
                DialogResult = DialogResult.OK;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void FormImpresora_Load(object sender, EventArgs e)
        {
            
            string NombreImpresora = "";//Donde guardare el nombre de la impresora por defecto
            cboImpresoras.Items.Clear();
            cmboTamaño.SelectedIndex = 0;
            comboTicket.SelectedIndex = 0;
            //Busco la impresora por defecto
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                cboImpresoras.Items.Add(a.PrinterName);
                if (a.IsDefaultPrinter)
                {
                    NombreImpresora = PrinterSettings.InstalledPrinters[i].ToString();
                    cboImpresoras.SelectedIndex = i;
                    cboImpresoras.SelectedText = NombreImpresora;
                }
            }

            string CMD = string.Format("SELECT * FROM impresion");
            DataSet ds = ClassUtilidades.Ejecutar(CMD);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmboTamaño.Text = ds.Tables[0].Rows[0]["imp_tam"].ToString().Trim();
                cboImpresoras.Text = ds.Tables[0].Rows[0]["impresora"].ToString().Trim();

            }
            else
            {
                cmboTamaño.SelectedIndex = 0;
            }
            

        }

        private void label3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnImpresora_Click(object sender, EventArgs e)
        {
       
            printTicket();
        }


        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                /*tomar datos */


                string CMD = string.Format("Select * From Estacionamiento_Datos");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                nombre = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                texto = ds.Tables[0].Rows[0]["texto"].ToString().Trim();
                rfc= ds.Tables[0].Rows[0]["rfc"].ToString().Trim();
                horario = ds.Tables[0].Rows[0]["horario"].ToString().Trim();
                direccion = ds.Tables[0].Rows[0]["direccion"].ToString().Trim();
                precioHora = ds.Tables[0].Rows[0]["tarifa_chico"].ToString();
                precioDia = ds.Tables[0].Rows[0]["tarifa_boleto_perdido"].ToString();
                tolerancia = ds.Tables[0].Rows[0]["tolerancia"].ToString();

                /*fin de tomar datos*/


                int SPACE = 130;
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
                g.DrawString(nombre, fBody1, sb, x / 2 - 10, 10, stringFormat);



             
                if (comboTicket.Text.Trim() == "Ticket entrada")
                {
                    g.DrawString("IMP." + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 20, stringFormat);
                    g.DrawString("*************************************************************", fBody1, sb, 0, 30);

                    RectangleF srcRect = new RectangleF(x / 4 - 10, 40, x / 2, 50);
                    Zen.Barcode.Code128BarcodeDraw barcode2 = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                    g.DrawImage(barcode2.Draw("000000000", 50), srcRect);
                    g.DrawString("FOLIO: 000000000", fBody1, sb, x / 2, 100, stringFormat);
                    g.DrawString("Entrada: 00:00:00", fBody1, sb, x / 2, 120, stringFormat);
                    g.DrawString(WordWrap(texto, maxCar), fBody1, sb, 0, SPACE);
                    SPACE = SPACE + 10;
                    g.DrawString("Costo por Hora........$" + precioHora, fontSmall, sb, x / 2 - 10, y - 50, stringFormat);
                    g.DrawString("Costo boleto perdido. $ 0", fontSmall, sb, x / 2 - 10, y - 60, stringFormat);
                    g.DrawString("Tolerancia.............  0", fontSmall, sb, x / 2 - 10, y - 70, stringFormat);

                    g.DrawString((horario), fontSmall, sb, x / 2, y - 80, stringFormat);
                }
                else if (comboTicket.Text.Trim() == "Ticket salida")
                {
                    g.DrawString(nombre, fBody1, sb, x / 2 - 10, 10, stringFormat);
                    g.DrawString(WordWrap(direccion, maxCar), fBody1, sb, x / 2 - 10, 30, stringFormat);
                    g.DrawString(WordWrap(rfc, maxCar), fBody1, sb, x / 2 - 10, 50, stringFormat);
                    g.DrawString("IMP." + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 70, stringFormat);
                    g.DrawString("*************************************************************", fBody1, sb, 0, 90);
                    RectangleF srcRect = new RectangleF(x / 4 - 10, 40, x / 2, 50);
                    g.DrawString("FOLIO: 000000000", fBody1, sb, x / 2, 110, stringFormat);
                    g.DrawString("Entrada: 00:00:00", fBody1, sb, x / 2, SPACE, stringFormat);
                    g.DrawString("Salida: 00:00:00 ", fBody1, sb, x / 2, SPACE + 20, stringFormat);
                    g.DrawString("Cargos boleto perdio:  $0", fBody1, sb, x / 2, SPACE + 80, stringFormat);
                    g.DrawString("0 dias 0 horas 0 minutos", fBody1, sb, x / 2, SPACE + 60, stringFormat);
                    g.DrawString("Total:  $0", fBody1, sb, x / 2, SPACE + 100, stringFormat);
                    g.DrawString((horario), fontSmall, sb, x / 2, y - 80, stringFormat);
                }

                else if (comboTicket.Text == "Ticket corte de caja")
                {
                    g.DrawRectangle(Pens.Aqua, 0, 0, x, y);
                    g.DrawString(nombre, fBody1, sb, x / 2, 20, stringFormat);
                    g.DrawString(direccion, fBody1, sb, x / 2, 40, stringFormat);
                    g.DrawString(rfc, fBody1, sb, x / 2, 60, stringFormat);
                    g.DrawString("IMP" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 80, stringFormat);

                    g.DrawString("**************************************************************************", fBody1, sb, 10, 100);
                    g.DrawString("CORTE DE CAJA", fBody1, sb, x / 2, 120, stringFormat);
                    g.DrawString("00/00/00  00/00/00", fBody1, sb, x / 2, 150, stringFormat);
                    g.DrawString("Monto....................$0" ,  fBody1, sb, x / 2, 170, stringFormat);
                    g.DrawString("No. de registros de salida:0", fBody1, sb, x / 2, 190, stringFormat);
                    g.DrawString("Vehic.dentro: 0", fBody1, sb, x / 2, 210, stringFormat);
                    g.DrawString("Boletos perdidos: 0", fBody1, sb, x / 2, 230, stringFormat);
                    g.DrawString("FOLIO                   " + "TOTAL", fBody1, sb, x / 2, 250, stringFormat);
                    
                  

                }

                else if(comboTicket.Text =="Boleto perdido")
                {
                    y = 570;
                    /*fin de tomar datos*/

                    texto = " BAJO PROTESTA DE DECIR LA VERDAD Y DERIVADO DE " +
                        "HABER EXTRAVIADO EL BOLETO/TICKET DE ESTACIONAMIENTO CON NUMERO DE FOLIO: "+
                         " 000000000  EL CUAL AMPARA LA ESTANCIA DEL VEHICULO MARCA: "+
                         " NISSAN PLACAS:  123456789"+
                        " ACREDITANDOME CON IDENTIFICACION OFICIAL:  INE CON FOLIO: 0123456789 RETIRANDO DICHO VEHICULO DEL ESTACIONAMIENTO UBICADO EN: " + direccion.ToUpper() +
                        " Y ACEPTO DEJAR SIN FECTO EL BOLETO/TICKET ORIGINAL EN CASO DE LOCALIZARLO POSTERIORMENTE," +
                        "EXIMIENDO DE CUALQUIER RESPONSABILIDAD CIVIL Y PENAL A EL ESTACIONAMIENTO DE COMERCIO Y/O: " + nombre.ToUpper() + "\n\n\r" + " NOMBRE Y FIRMA";

                    g.DrawString(nombre, fBody1, sb, x / 2 - 10, 10, stringFormat);
                    g.DrawString("IMP." + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 20, stringFormat);
                    g.DrawString("*************************************************************", fBody1, sb, 0, 30);
                    g.DrawString("NOMBRE COMPLETO", fBody1, sb, 0, 60);
                    g.DrawString(WordWrap(texto, maxCar-9), fBody1, sb, 0, 100);
                    y = 500;
                }
                
            }
            catch (Exception error)
            {
                MessageBox.Show("Error en la operacion de imprimir por falta de datos: " + error.Message);
            }

        }

  
        public void printTicket()
        {
            PrintDocument pd = new PrintDocument();
            /*tomar datos */
            double c = Convert.ToSingle(cmboTamaño.Text.Trim().ToString()) / 1.7;
            maxCar = (int)Math.Round(c);
            Double tamaño = Math.Floor(Convert.ToSingle(cmboTamaño.Text.Trim()) * 0.0393701) * 100;
            x =  (Int32.Parse(tamaño.ToString()));
            pd.PrinterSettings.PrinterName = cboImpresoras.Text.Trim();
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
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
