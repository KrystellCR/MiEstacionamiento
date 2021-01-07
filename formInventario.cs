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
using System.Data.SQLite;
using System.Drawing.Printing;
namespace EstacionamientoV4
{
    public partial class formInventario : FormBase
    {
        public formInventario()
        {
            InitializeComponent();
            src_val = 0;
            cargarDataGrid();
        }
        string   nombre, direccion, rfc;
        int src_val;
        int x, y;
        DataSet ds;
        private static formInventario instance;
        int maxCar;       

        public static formInventario getInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new formInventario();
            }
            else
            {
                instance.BringToFront();
                instance.WindowState = FormWindowState.Normal;
            }
            return instance;
        }

        DataSet DS = new DataSet();
        DataSet DSA = new DataSet();
        DataSet DSp = new DataSet();
        SQLiteDataAdapter DP;
        DataTable dt, dta;
        string total = "0";
        float monto = 0, pension = 0;
        string limiteInferior, limiteSuperior, limiteSuperior_sinHoras, limiteInferior_sinHoras;
        string folioInferior="-", folioSuperior="-";
        string impI, impS;
        string cmd, cmdA, cmdp;
        float suma_estadia =0, suman_pension =0, total_suma = 0;

        public void cargarDataGrid()
        {
            refresh();
            try
            {
                limiteSuperior = dateSup.Value.Date.AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd HH:mm").Trim();
                limiteInferior = DateInf.Value.Date.ToString("yyyy-MM-dd HH:mm").Trim();
                limiteSuperior_sinHoras = dateSup.Value.Date.AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd").Trim();
                limiteInferior_sinHoras = DateInf.Value.Date.ToString("yyyy-MM-dd").Trim();
                impI = dateSup.Value.Date.AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd").Trim();
                impS = DateInf.Value.Date.ToString("yyyy-MM-dd").Trim();
                //  string cmd = string.Format("SELECT folio,placas,entrada,salida,tipo,observaciones,total as estadia,cargos, (total + cargos) as Total FROM Ticket_salida WHERE salida IS NOT NULL");
                try
                {
                    SQLiteConnection Con;
                    Con = new SQLiteConnection("Data Source=Dbestacionamiento.sqlite");
                    cmd = "SELECT folio,placas,entrada,salida,tipo,observaciones,total  FROM((Ticket_salida))  WHERE salida IS NOT NULL and salida BETWEEN ('" + limiteInferior + "') AND ( '" + limiteSuperior + "')";
                    DP = new SQLiteDataAdapter(cmd, Con);
                    DP.Fill(DS, "folio");
                    dataGridView1.DataSource = DS;
                    dt = DS.Tables[0];
                    dataGridView1.DataMember = "folio";
                    Con.Close();
                
                   
                       
                   
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error al conectarse a la base de base de datos: " + error.Message);
                }
                
                //mostrar el total
                Calcular();

                //calcula cuantos registrados hay en ese intervalo
                cmd = "Select Count(*) AS autosRegistrados FROM Ticket_salida   WHERE salida IS NOT NULL and salida BETWEEN '" + limiteInferior + "' AND  '" + limiteSuperior + "'";
                ds = ClassUtilidades.Ejecutar(cmd);
                lblRegistrados.Text = ds.Tables[0].Rows[0]["autosRegistrados"].ToString();
                cmdA = "Select folio,entrada from Ticket";
                DSA = ClassUtilidades.Ejecutar(cmdA);
                dta = DSA.Tables[0];
                //calcular folio inferior y superior
                if (float.Parse(ds.Tables[0].Rows[0]["autosRegistrados"].ToString()) > 0)
                    {
                        
                            cmd = "Select folio FROM Ticket_salida   WHERE salida IS NOT NULL and salida BETWEEN ('" + limiteInferior + "') AND  ('" + limiteSuperior + "' ) limit 1 ";
                            DataSet ds_folio = ClassUtilidades.Ejecutar(cmd);
                            folioInferior = ds_folio.Tables[0].Rows[0]["folio"].ToString();
                                                                                                     
                    }
                    else if (ds.Tables[0].Rows.Count >= 2)
                    {
                            cmd = "Select folio FROM Ticket_salida   WHERE salida IS NOT NULL and salida BETWEEN ('" + limiteInferior + "') AND  ('" + limiteSuperior + "' ) order by folio desc limit 1 ";
                            DataSet ds_folio = ClassUtilidades.Ejecutar(cmd);
                            folioSuperior = ds_folio.Tables[0].Rows[0]["folio"].ToString();                          
                    }


                cmd =  "Select Count(*) AS boletos_perdidos FROM Ticket_salida   WHERE cargos > 0 and salida IS NOT NULL and salida BETWEEN '" + limiteInferior + "' AND  '" + limiteSuperior + "'"; 
                DataSet ds_bp = ClassUtilidades.Ejecutar(cmd);
                lblbp.Text = ds_bp.Tables[0].Rows[0]["boletos_perdidos"].ToString();

                string query_contador = "Select Count(*) AS autosAdentro FROM Ticket WHERE (salida = '' or salida is null )";
                DataSet ds_qc;
                ds_qc = ClassUtilidades.Ejecutar(query_contador);

                if (ds_qc.Tables[0].Rows.Count > 0)
                {
                    lblAdentro.Text = ds_qc.Tables[0].Rows[0]["autosAdentro"].ToString();
                }

            }

            catch (Exception error)
            {
                    MessageBox.Show("Error al cargar  dataGrid: " + error.Message);
            }
           
        }

        public void refresh()
        {
            DS.Clear();
            DSA.Clear();
            DSp.Clear();
            cmdp = "";
            cmd = "";
            cmdA = "";
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            total = "";

            suman_pension = 0;
            suma_estadia = 0;
            monto = 0;
            pension = 0;
           
            lblRegistrados.Text = "0";
            lblAdentro.Text = "0";
            LblMonto.Text = "0";
            lblbp.Text = "0";
            lblMontoPension.Text = "0";
            lblMontoTotal.Text = "0";
          

        }
        public float sumaEstadia()
        {
            // string cmd = string.Format("SELECT SUM (total) as TOTAL FROM((Ticket_salida))  WHERE salida IS NOT NULL and salida BETWEEN '" + limiteInferior + "' AND  '" + limiteSuperior + "'");
            string cmd = string.Format("SELECT  CASE WHEN SUM (total) isnull then 0 ELSE  sum(total)  END as TOTAL FROM((Ticket_salida))  WHERE salida IS NOT NULL and salida BETWEEN '" + limiteInferior + "' AND  '" + limiteSuperior + "'");
            ds = ClassUtilidades.Ejecutar(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {

                suma_estadia = Convert.ToSingle(ds.Tables[0].Rows[0]["TOTAL"]);
                return suma_estadia;
            }
            else { return 0; }

            
        }
        public float sumaPension()
        {
            cmdp = "select CASE when  sum (transaccion_pension.total) isnull then 0 ELSE sum(transaccion_pension.total) END as TOTAL_pension from transaccion_pension where  fecha IS NOT NULL and fecha  between ('" + limiteInferior_sinHoras + "') AND ( '" + limiteSuperior_sinHoras + "')";
            DSp = ClassUtilidades.Ejecutar(cmdp);
            if (DSp.Tables[0].Rows.Count > 0)
            {
                suman_pension = Convert.ToSingle(DSp.Tables[0].Rows[0]["TOTAL_pension"]);              
                return suman_pension;
            }
            else { return 0; }
          
        }
        public void Calcular()
        {
           
           
            try { //se usa la variable pension y monto para hacer la operación
                pension = Convert.ToSingle(sumaPension());
                monto = Convert.ToSingle(sumaEstadia());
                LblMonto.Text = monto.ToString("N2");
                monto = pension + monto;
                lblMontoTotal.Text = monto.ToString("N2");
                lblMontoPension.Text = pension.ToString("N2");
                
            }
            catch (Exception error){
                lblMontoTotal.Text = "0";
                lblMontoPension.Text = "0";
                lblMontoTotal.Text = "0";
                MessageBox.Show("Error, no se ha podido completar la operacion calcular total: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            
        }

        private void BtnCargar_Click_1(object sender, EventArgs e)
        {
           
            cargarDataGrid();
        }

        private void leftImg_Click_1(object sender, EventArgs e)
        {
            
            src_val = src_val - 10;

            if (src_val <= 0)
            {
                src_val = 0;
            }
            DS.Clear();
            DP.Fill(DS, src_val, 10, "folio");
            
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rightImg_Click_1(object sender, EventArgs e)
        {
            
            src_val = src_val + 10;

            if (src_val >= int.Parse(lblRegistrados.Text))
            {
                src_val = 0;
            }

            DS.Clear();
            DP.Fill(DS, src_val, 10, "folio");
         
        }

        private void DateInf_ValueChanged(object sender, EventArgs e)
        {   
            cargarDataGrid();
        }

        private void dateSup_ValueChanged(object sender, EventArgs e)
        {
            cargarDataGrid();
        }

        private void formInventario_Load(object sender, EventArgs e)
        {
            DateInf.Value = DateTime.Today;
            dateSup.Value = DateTime.Today;

           
            DateInf.Format = DateTimePickerFormat.Custom;
            dateSup.Format = DateTimePickerFormat.Custom;
            DateInf.CustomFormat = "yyyy/MMMM/dd";
            dateSup.CustomFormat = "yyyy/MMMM/dd";
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
       }
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            
                /*tomar datos */
                string CMD;
                DataSet ds;
                try
                {
                    /*tomar datos */
                    CMD = string.Format("Select * From Estacionamiento_Datos");
                    ds = ClassUtilidades.Ejecutar(CMD);
                    nombre = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                    nombre = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                    direccion = ds.Tables[0].Rows[0]["direccion"].ToString().Trim();
                    rfc = ds.Tables[0].Rows[0]["rfc"].ToString().Trim();
                   
                    /*fin de tomar datos*/
                }
                catch (Exception error)
                {
                  MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            try
            {

                int SPACE = 0;
                Graphics g = e.Graphics;
               // g.DrawRectangle(Pens.Black, 5, 5, x, y);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                Font bold = new Font("Arial", 9, FontStyle.Bold);
                Font fBody1 = new Font("Arial", 9, FontStyle.Regular);
                Font fontSmall = new Font("Arial", 7, FontStyle.Regular);
                Font rs = new Font("Stencil", 25, FontStyle.Bold);
                Font fTType = new Font("", 150, FontStyle.Bold);
                SolidBrush sb = new SolidBrush(Color.Black);

                g.DrawRectangle(Pens.Aqua, 0, 0, x, y);
                g.DrawString(nombre, fBody1, sb, x / 2-10, 20, stringFormat);
                g.DrawString(direccion, fBody1, sb, x / 2-10, 40, stringFormat);
                g.DrawString(rfc, fBody1, sb, x / 2-10, 60, stringFormat);
                g.DrawString("IMP"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 80, stringFormat);
               
                g.DrawString("**************************************************************************", fBody1, sb, 10, 90);
                g.DrawString("CORTE DE CAJA", fBody1, sb, x/2, 110, stringFormat);
                g.DrawString(impI +" - "+ impS, fBody1, sb, x /2, 125, stringFormat);
                g.DrawString("Monto estadia: $"+ LblMonto.Text, fBody1, sb, x/2, 140,stringFormat);
                g.DrawString("Monto pensión: $" + lblMontoPension.Text, fBody1, sb, x / 2, 160, stringFormat);
                g.DrawString("Monto total: $" + lblMontoTotal.Text, bold, sb, x / 2, 180, stringFormat);
                g.DrawString("No. de registros de salida: "+ lblRegistrados.Text, fBody1, sb, x/2,210,stringFormat);
                g.DrawString("Vehic. adentro sin pension: "+ lblAdentro.Text, fBody1, sb, x/2, 230,stringFormat);
                g.DrawString("Boletos perdidos: " + lblbp.Text, fBody1, sb, x / 2, 250, stringFormat);
                g.DrawString("FOLIO      TOTAL", bold, sb, x / 2, 270, stringFormat);
                SPACE = 280;

                foreach (DataRow row in dt.Rows)
                {
                    g.DrawString(row[0].ToString() + "   $" + row[6].ToString(), fBody1, sb, x / 4, SPACE);
                    SPACE = SPACE + 10;                
                }
                SPACE = SPACE + 20;
                g.DrawString("VEHIC. DENTRO               ", bold, sb, x / 2, SPACE, stringFormat);
                SPACE = SPACE + 20;
                g.DrawString("FOLIO     ENTRADA", bold, sb, x / 2, SPACE, stringFormat);
                SPACE = SPACE + 10;
                foreach (DataRow row in dta.Rows)
                {
                    g.DrawString(row[0].ToString() + "  " + row[1].ToString(), fontSmall, sb, x / 8, SPACE);
                    SPACE = SPACE + 10;
                }



            }
            catch (Exception error)
            {
                MessageBox.Show("Error en la operacion de imprimir" + error.Message);

            }

        }

        public void printTicketInventario()
        {

            PrintDocument pd = new PrintDocument();
            x = 222;//222 315
            maxCar = 34; //34 47
            try
            {            
                string CMD = string.Format("select * from impresion Limit 1");
                DataSet ds = ClassUtilidades.Ejecutar(CMD);
                string impresora = ds.Tables[0].Rows[0]["impresora"].ToString();
                pd.PrinterSettings.PrinterName = impresora;
                x = (Int32.Parse(ds.Tables[0].Rows[0]["tamaño"].ToString()));
                maxCar = (Int32.Parse(ds.Tables[0].Rows[0]["caracteres"].ToString()));
                y = (Int32.Parse(lblRegistrados.Text)+ Int32.Parse(lblAdentro.Text)) * 10 +400;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al intentar imprimir por falta de datos, configure la impresora" + Environment.NewLine + error, "MiEstacionamiento",
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
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            printTicketInventario();
        }
    }
  }
