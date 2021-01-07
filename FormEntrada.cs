using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LibreriaValidar;
using System.Drawing.Printing;
using MiLibreriaSqlite;

namespace EstacionamientoV4
{
    public partial class FormEntrada : FormBase
    {
        public bool tipo_usuario;
        public string placas = "";
        decimal folioAux = 0;
        string folio = "";
        string entrada;
        string tipo = "";
        string obs = "";
        float total = 0;
        string nombre = "";
        string direccion = "";
        string rfc = "";
        int x;
        int y;
        string horario = "";
        string precioHora = "0", tarifabp = "0", tolerancia = "0", texto = "", estadia;
        int maxCar;
        string textoS, textoE;
        /*** VARIABLES CMD, DS ***/
        DataSet ds_imp;
        string CMD_imp;
        string CMDLugares;
        DataSet dsLugares;
        string CMDFolio;
        DataSet dsFolio;
        string cmdPlacas;
        DataSet dsPlacas;
        string query_guardar;
        DataSet ds_guardar;
        string cmdUpdateLugar;
        DataSet dsUpdateLugar;
        string folio_cargar_datos;
        string query_cargar_datos;
        DataSet ds_cargar_datos;
        DataSet ds_reimp_s;
        string cmd_reimp_s;
        string cmd_cobrar;
        DataSet ds_cobrar;
        DataSet ds_printTicket;
        string CMD_printTicket;
        string CMD_p2;
        DataSet ds_p2;
        String CMD_pd;
        DataSet ds_pd;
        String cmdPlacas_placas_cliente;
        DataSet ds_placas_cliente;

        /*** variables salida ***/
        public string exit;
        public string input;
        public float totalSalida;
        string placasSalida;
        float precio = 0;
        DateTime entradaSalida, salidaSalida;
        float cargosSalida = 0;
        float totalEstadia = 0;

        string print_entrada = "", print_salida = "", print_cargos_salida = "";
        public FormEntrada()
        {
            InitializeComponent();

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {

            FormConfgiracion formConf = FormConfgiracion.getInstance();
            formConf.Show();

        }

        private void txtPlacas_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            Validar.Regexp("texto", txtPlacas, lblPlacas);
            lugaresDisp();
            GetFolio();

        }

        private void btnInventario_Click(object sender, EventArgs e)
        {

            formInventario formI = formInventario.getInstance();
            formI.Show();

        }

        private void btinReeimprimir_Click(object sender, EventArgs e)
        {
            try
            {



                CMD_imp = "SELECT * from Ticket  order by folio desc limit 1; ";
                ds_imp = ClassUtilidades.Ejecutar(CMD_imp);
                folio = ds_imp.Tables[0].Rows[0]["folio"].ToString().Trim();
                entrada = ds_imp.Tables[0].Rows[0]["entrada"].ToString().Trim();

                if (ds_imp.Tables[0].Rows.Count > 0)
                {
                    printTicket("entrada");
                }
                else
                {
                    MessageBox.Show("No se encontraron registros de entrada", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                CMD_imp = "";
                ds_imp.Clear();
            }

            catch (Exception error)
            {
                MessageBox.Show("No se encontraron registros de entrada " + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }



        }

        private void btnRegSalida_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Aviso", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
        public void ClearForm()
        {
            txtPlacas.Clear();
            textBoxDescripcion.Clear();
            lugaresDisp();
            lblObs.Text = "";
            lblPlacas.Text = "";
            comboBoxTipo.SelectedIndex = 1;

        }
        public void lugaresDisp()
        {

            try
            {
                CMDLugares = string.Format("Select Count(*) AS vacios FROM lugar WHERE status = '0' ");
                dsLugares = ClassUtilidades.Ejecutar(CMDLugares);
                lblLugares.Text = dsLugares.Tables[0].Rows[0]["vacios"].ToString().Trim();
                CMDLugares = "";
                dsLugares.Clear();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


        }

        public string GetFolio()
        {
            try
            {
                CMDFolio = "SELECT folio from Ticket_salida UNION all SELECT folio  FROM Ticket order by folio desc limit 1; ";
                dsFolio = ClassUtilidades.Ejecutar(CMDFolio);
                if (!String.IsNullOrEmpty(dsFolio.Tables[0].Rows[0]["folio"].ToString()))
                {
                    folioAux = Convert.ToDecimal(dsFolio.Tables[0].Rows[0]["folio"]);
                    folioAux = folioAux + 1;
                    folio = folioAux.ToString("0000000000.##");
                }
                else
                {
                    folio = "0000000000.##";
                }

                lblFolio.Text = folio;
                CMDFolio = "";
                dsFolio.Clear();
                return folio;
            }
            catch (Exception error)
            {
                lblFolio.Text = "0000000000";
                folio = "0000000000"; ;
                return folio;
            }

        }

        private void FormEntrada_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            txtPlacas.Select();
            comboBoxTipo.SelectedIndex = 1;
            lugaresDisp();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (Validar.Regexp("texto", txtPlacas, lblPlacas))
            {
                if (ComprobarPlacas())
                {
                    if (ComprobarPlacas_clientes())
                    {
                        lugaresDisp();
                        Guardar();
                    }
                }
            }

            else
            {
                MessageBox.Show("Los campos deben ser validos", "MiEstacionamiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public bool ComprobarPlacas()
        {
            try
            {
                placas = txtPlacas.Text.Trim();
                cmdPlacas = "select placas from Ticket  where salida IS NULL and placas= @placas";
                dsPlacas = ClassUtilidades.selectTable(cmdPlacas, "", placas, "", "", "", "", 0);

                if (dsPlacas.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("La placa del vehículo ya esta registrada dentro del estacionamiento,ingrese otra placa", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    cmdPlacas = "";
                    dsPlacas.Clear();
                    return false;
                }
                else
                {
                    cmdPlacas = "";
                    dsPlacas.Clear();
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

        public void Guardar()
        {

            try
            {
                placas = txtPlacas.Text.Trim();
                entrada = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (lblLugares.Text == "0")
                {
                    MessageBox.Show("No hay cajones disponibles ", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    return;
                }

                tipo = comboBoxTipo.Text.Trim();
                obs = textBoxDescripcion.Text.ToString();
                total = 0;
                query_guardar = "insert into Ticket(folio,placas, entrada, tipo, observaciones,total) values(@folio,@placas,@entrada,@tipo,@obs,@total) ";
                ds_guardar = ClassUtilidades.selectTable(query_guardar, GetFolio(), placas, entrada, "", tipo, obs, total);
                /************************** se manda a llamar a la clase generar ticket *****/
                printTicket("entrada");
                ds_guardar.Clear();
                query_guardar = "";
                ClearForm();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al guardar, no se ha podido completar la operacion: " + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }

        private void btnImpresora_Click(object sender, EventArgs e)
        {
            FormImpresora fi = new FormImpresora();
            fi.ShowDialog(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblHora.Text = DateTime.Now.ToString("HH:mm:ss");//ToShortTimeString();
            this.labelFecha.Text = DateTime.Now.ToShortDateString();
        }

        public void UpdateLugar(string idlugar)
        {

            try
            {
                cmdUpdateLugar = "UPDATE Lugar SET  status = '1' where idLugar='" + idlugar + "'";
                dsUpdateLugar = ClassUtilidades.Ejecutar(cmdUpdateLugar);
                cmdUpdateLugar = "";
                dsUpdateLugar.Clear();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }

        private void textBoxDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void CargarDatos()
        {

            try
            {
                folio_cargar_datos = (txtBuscarFolio.Text.Trim());
                query_cargar_datos = "SELECT * FROM TickeT WHERE folio = @folio and (salida IS NULL or salida = '')";
                ds_cargar_datos = ClassUtilidades.selectTable(query_cargar_datos, folio_cargar_datos, "", "", "", "", "", 0);
                if (ds_cargar_datos.Tables[0].Rows.Count > 0)
                {
                    txtEntrada.Text = ds_cargar_datos.Tables[0].Rows[0]["entrada"].ToString().Trim();
                    txtSalida.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    textoE = ds_cargar_datos.Tables[0].Rows[0]["entrada"].ToString().Trim();
                    textoS = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    tipo = ds_cargar_datos.Tables[0].Rows[0]["tipo"].ToString().Trim();
                    cargosSalida = Convert.ToSingle(ds_cargar_datos.Tables[0].Rows[0]["cargos"].ToString());
                    placasSalida = ds_cargar_datos.Tables[0].Rows[0]["placas"].ToString().Trim();
                    txtObs.Text = ds_cargar_datos.Tables[0].Rows[0]["observaciones"].ToString().Trim();
                    folio_cargar_datos = "";
                    query_cargar_datos = "";
                    ds_cargar_datos.Clear();
                    CalcularTotal();
                }
                else
                {
                    MessageBox.Show("No se encontraron datos", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    lblTotal.Text = "";
                    lblTotalEstadia.Text = "";
                    lblCargos.Text = "";
                    txtObs.Clear();
                    txtEntrada.Clear();
                    txtSalida.Clear();
                    textoE = "";
                    textoS = "";
                    lblEstadia.Text = "";

                }
            }
            catch (Exception error)
            {
                MessageBox.Show("No se encontraron datos" + error, "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            }

        }

        public void CalcularTotal()

        {
            if (Validar.ValidarFormulario(this, errorProvider1) == false)
            {
                if (!String.IsNullOrEmpty(txtEntrada.Text.Trim()) && !String.IsNullOrEmpty(txtSalida.Text.Trim()))
                {
                    entradaSalida = Convert.ToDateTime(textoE);
                    salidaSalida = Convert.ToDateTime(textoS);
                    float total = 0;
                    float precioCh = 0, precioG = 0;
                    float tolerancia = 0;

                    try
                    {
                        string CMD = string.Format("SELECT tarifa_chico,tarifa_grande,tolerancia FROM Estacionamiento_Datos");
                        DataSet ds = ClassUtilidades.Ejecutar(CMD);
                        precioCh = Convert.ToSingle(ds.Tables[0].Rows[0]["tarifa_chico"].ToString());
                        precioG = Convert.ToSingle(ds.Tables[0].Rows[0]["tarifa_grande"].ToString());

                        tolerancia = Convert.ToSingle(ds.Tables[0].Rows[0]["tolerancia"].ToString());
                        TimeSpan span = salidaSalida.Subtract(entradaSalida);
                        float horas = Convert.ToSingle(string.Format(span.Hours.ToString()));
                        float minutos = Convert.ToSingle(string.Format(span.Minutes.ToString()));
                        float dias = Convert.ToSingle(string.Format(span.Days.ToString()));

                        lblEstadia.Text = "Dias: " + span.Days.ToString() + "  Horas: " + span.Hours.ToString() + "  Minutos: " + span.Minutes.ToString();
                        estadia = lblEstadia.Text;

                        if (minutos > tolerancia)
                        {
                            horas = horas + 1;
                        }

                        if (tipo == "Chico")
                        {

                            precio = precioCh;
                        }
                        else if (tipo == "Grande")
                        {
                            precio = precioG;
                        }
                        totalEstadia = (dias * (precio * 24)) + (horas * precio);
                        total = (dias * (precio * 24)) + (horas * precio) + cargosSalida;
                        lblTotal.Text = total.ToString("N2");
                        lblTotalEstadia.Text = totalEstadia.ToString("N2");
                        if (cargosSalida != 0)
                        {
                            lblCargos.Text = "Cargo boleto perdido:$   " + cargosSalida.ToString("N2");
                        }
                        else
                        {
                            lblCargos.Text = "";
                        }
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show("Ingrese las tarifas para cobrar" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else
                {
                    MessageBox.Show("Ingrese numero de folio", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnBoletoPerdido_Click(object sender, EventArgs e)
        {
            boletoPerdido_datos bpd = new boletoPerdido_datos();
            if (bpd.ShowDialog(this) == DialogResult.OK)
            {
                txtBuscarFolio.Text = bpd.variablex;


                if (Validar.ValidarFormulario(this, errorProvider1) == false)
                {
                    if (!String.IsNullOrEmpty(txtBuscarFolio.Text.Trim()))
                    {
                        CargarDatos();
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese numero de folio", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }
        }

        public void refresh()
        {
            lblTotal.Text = "";
            lblTotalEstadia.Text = "";
            lblCargos.Text = "";
            txtBuscarFolio.Clear();
            txtObs.Clear();
            txtEntrada.Clear();
            txtSalida.Clear();
            textoE = "";
            textoS = "";
            lblEstadia.Text = "";
            print_entrada = "";
            print_cargos_salida = "";
            estadia = "";
            print_salida = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Validar.ValidarFormulario(this, errorProvider1) == false)
            {
                if (!String.IsNullOrEmpty(txtBuscarFolio.Text.Trim()))
                {
                    CargarDatos();
                }
            }
            else
            {
                MessageBox.Show("Ingrese numero de folio", "MiEstacionamiento",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnReeimprimirSalida_Click(object sender, EventArgs e)
        {
            try
            {

                cmd_reimp_s = "SELECT * from Ticket_salida  order by salida desc limit 1; ";
                ds_reimp_s = ClassUtilidades.Ejecutar(cmd_reimp_s);


                if (ds_reimp_s.Tables[0].Rows.Count > 0)
                {

                    folio = ds_reimp_s.Tables[0].Rows[0]["folio"].ToString().Trim();
                    total = Convert.ToSingle(ds_reimp_s.Tables[0].Rows[0]["total"].ToString());
                    placasSalida = ds_reimp_s.Tables[0].Rows[0]["placas"].ToString().Trim();
                    entradaSalida = Convert.ToDateTime(ds_reimp_s.Tables[0].Rows[0]["entrada"].ToString().Trim());
                    salidaSalida = Convert.ToDateTime(ds_reimp_s.Tables[0].Rows[0]["salida"].ToString().Trim());
                    print_entrada = ds_reimp_s.Tables[0].Rows[0]["entrada"].ToString().Trim();
                    print_salida = ds_reimp_s.Tables[0].Rows[0]["salida"].ToString().Trim();
                    print_cargos_salida= ds_reimp_s.Tables[0].Rows[0]["cargos"].ToString().Trim();
                    TimeSpan span = salidaSalida.Subtract(entradaSalida);
                    float horas = Convert.ToSingle(string.Format(span.Hours.ToString()));
                    float minutos = Convert.ToSingle(string.Format(span.Minutes.ToString()));
                    float dias = Convert.ToSingle(string.Format(span.Days.ToString()));
                    estadia = "Dias: " + span.Days.ToString() + "  Horas: " + span.Hours.ToString() + "  Minutos: " + span.Minutes.ToString();
                    cmd_reimp_s = "";
                    ds_reimp_s.Clear();
                    printTicket("ultimo_salida");
                }
                else
                {
                    MessageBox.Show("No se encontraron registros de entrada", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }

            }

            catch (Exception error)
            {
                MessageBox.Show("Error, no se ha podido completar la operacion: " + Environment.NewLine + error.Message, "MiEstacionamiento",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(txtEntrada.Text.Trim()) && !String.IsNullOrEmpty(txtSalida.Text.Trim()) && !String.IsNullOrEmpty(lblTotal.Text.Trim()))
            {

                folio = (txtBuscarFolio.Text.Trim());
                exit = txtSalida.Text.Trim();
                input = txtEntrada.Text.Trim();
                string obs = txtObs.Text.Trim();
                total = float.Parse(lblTotal.Text.Trim());
                try
                {

                    cmd_cobrar = "SELECT folio FROM TickeT WHERE folio = @folio  and (salida IS NULL or salida = '')";
                    ds_cobrar = ClassUtilidades.selectTable(cmd_cobrar, folio, "", "", "", "", "", 0);
                    if (ds_cobrar.Tables[0].Rows.Count > 0)
                    {
                        string query = "UPDATE Ticket SET  salida = @salida, observaciones =@obs, total =@total  WHERE folio = @folio and (salida IS NULL or salida ='')";
                        DataSet ds = ClassUtilidades.selectTable(query, folio, "", "", exit, "", obs, total);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo completar la operación,elnúmero de folio no coinside con los datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }

                    cmd_cobrar = "";
                    ds_cobrar.Clear();
                    printTicket("salida");
                    refresh();
                    lugaresDisp();


                }

                catch (Exception error)
                {
                    MessageBox.Show("No se pudo completar la operación" + error.Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No se pudo completar la operación por falta de datos ", "Información ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblCargos_Click(object sender, EventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿ESTÁ SEGURO QUE DESEA BORRAR LOS REGISTROS DE FORMA PERMANENTE?", "Información", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                dialogoBorrarRegistros dbr = new dialogoBorrarRegistros();
                if (dbr.ShowDialog(this) == DialogResult.OK)
                {
                    /*despues de borrar se refresca*/
                    ClearForm();
                  
                }
            }
        }

        private void txtBuscarFolio_KeyUp(object sender, KeyEventArgs e)
        {

        }

        public void printTicket(string tipo)
        {
            PrintDocument pd = new PrintDocument();
            PrintDocument pd2 = new PrintDocument();
            PrintDocument pd3 = new PrintDocument();
            x = 222;//222 315
            y = 500;//centesimas de pulgada (para saltos de 10 , es saltos *10 +20 //21 saltos
            maxCar = 34; //34 47

            try
            {
                CMD_printTicket = string.Format("select * from impresion Limit 1");
                ds_printTicket = ClassUtilidades.Ejecutar(CMD_printTicket);
                string impresora = ds_printTicket.Tables[0].Rows[0]["impresora"].ToString();
                pd.PrinterSettings.PrinterName = impresora;
                pd2.PrinterSettings.PrinterName = impresora;
                pd3.PrinterSettings.PrinterName = impresora;
                x = (Int32.Parse(ds_printTicket.Tables[0].Rows[0]["tamaño"].ToString()));
                maxCar = (Int32.Parse(ds_printTicket.Tables[0].Rows[0]["caracteres"].ToString()));
                CMD_printTicket = "";
                ds_printTicket.Clear();

            }
            catch (Exception error)
            {
                MessageBox.Show("Error al intentar imprimir por falta de datos, configure la impresora" + Environment.NewLine + error, "MiEstacionamiento",
             MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }

            try
            {
                if (tipo == "entrada")
                {
                    PaperSize ps = new PaperSize("", x, y);
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

                    pd.PrintController = new StandardPrintController();
                    pd.DefaultPageSettings.Margins.Left = 0;
                    pd.DefaultPageSettings.Margins.Right = 0;
                    pd.DefaultPageSettings.Margins.Top = 0;
                    pd.DefaultPageSettings.Margins.Bottom = 0;
                    pd.DefaultPageSettings.PaperSize = ps;
                    pd.Print();                  
                }
                else if (tipo == "salida")
                {
                    y = 310;
                    PaperSize ps2 = new PaperSize("", x, y);
                    pd2.PrintPage += new PrintPageEventHandler(pd2_PrintPage);

                    pd2.PrintController = new StandardPrintController();
                    pd2.DefaultPageSettings.Margins.Left = 0;
                    pd2.DefaultPageSettings.Margins.Right = 0;
                    pd2.DefaultPageSettings.Margins.Top = 0;
                    pd2.DefaultPageSettings.Margins.Bottom = 0;
                    pd2.DefaultPageSettings.PaperSize = ps2;
                    pd2.Print();
                }
                else if(tipo== "ultimo_salida")
                {
                    y = 310;
                    PaperSize ps3 = new PaperSize("", x, y);
                    pd3.PrintPage += new PrintPageEventHandler(pd3_PrintPage);

                    pd3.PrintController = new StandardPrintController();
                    pd3.DefaultPageSettings.Margins.Left = 0;
                    pd3.DefaultPageSettings.Margins.Right = 0;
                    pd3.DefaultPageSettings.Margins.Top = 0;
                    pd3.DefaultPageSettings.Margins.Bottom = 0;
                    pd3.DefaultPageSettings.PaperSize = ps3;
                    pd3.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnPension_Click(object sender, EventArgs e)
        {
            FormClientes formC = FormClientes.getInstance();
            if (tipo_usuario)
            {
                formC.BtnAddCliente.Hide();
                formC.btnEdit.Hide();
                formC.btnSuspender.Hide();
                formC.tabControl1.TabPages.Remove(formC.tabPage5);
            }
            formC.Show();
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
                    CMD_p2 = "";
                    ds_p2.Clear();
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
                Font fontSmall = new Font("Arial", 7, FontStyle.Regular);
                Font rs = new Font("Stencil", 25, FontStyle.Bold);
                Font fTType = new Font("", 150, FontStyle.Bold);
                SolidBrush sb = new SolidBrush(Color.Black);


                g.DrawString(nombre, fBody1, sb, x / 2 - 10, 10, stringFormat);
                g.DrawString(WordWrap(direccion, maxCar), fBody1, sb, x / 2 - 10, 30, stringFormat);
                g.DrawString(WordWrap(rfc, maxCar), fBody1, sb, x / 2 - 10, 50, stringFormat);
                g.DrawString("IMP." + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 70, stringFormat);
                g.DrawString("*************************************************************", fBody1, sb, 0, 90);
                RectangleF srcRect = new RectangleF(x / 4 - 10, 40, x / 2, 50);
                g.DrawString(("FOLIO:" + folio.ToString()), fBody1, sb, x / 2, 110, stringFormat);
                g.DrawString("Entrada: " + txtEntrada.Text, fBody1, sb, x / 2, SPACE, stringFormat);
                g.DrawString("Salida:  " + txtSalida.Text, fBody1, sb, x / 2, SPACE + 20, stringFormat);
                g.DrawString("Cargos boleto perdio:  $" + cargosSalida, fBody1, sb, x / 2, SPACE + 80, stringFormat);
                g.DrawString(lblEstadia.Text, fBody1, sb, x / 2, SPACE + 60, stringFormat);
                g.DrawString("Total:  $" + total, fBody1, sb, x / 2, SPACE + 100, stringFormat);
                g.DrawString((horario), fontSmall, sb, x / 2, y - 50, stringFormat);


            }
            catch (Exception error)
            {
                MessageBox.Show("Error en la operacion de imprimir por falta de datos: " + error.Message);
            }

        }

        private void FormEntrada_FormClosing(object sender, FormClosingEventArgs e)
        {         
            if (e.CloseReason.ToString() == "UserClosing")
            {
                if (MessageBox.Show("¿Desea cerrar la aplicación?", "Aviso", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {                 
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }

            }
          
        }

        private void txtPlacas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) //si es digito entonces se escribe
            {
                e.Handled = false;

            }

            else if (Char.IsControl(e.KeyChar)) //si es un control se escribe 
            {
                e.Handled = false;
            }
            else if (Char.IsLetter(e.KeyChar)) //si es un letra se escribe 
            {
                e.Handled = false;
            }
            else if (e.KeyChar.ToString() == "-"|| (e.KeyChar.ToString() == " "))//si es un guion se escribe
            {
                e.Handled = false;
            }

            else /*si no es las anteriores entonces no se escribe*/
            {
                e.Handled = true;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtBuscarFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                /*valida si el texto folio esta vacio */
                if (Validar.ValidarFormulario(this, errorProvider1) == false)
                {

                    CargarDatos();

                }
                else
                {
                    MessageBox.Show("El campo folio no puede estar vacio", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                /*tomar datos */
                CMD_pd = string.Format("Select * From Estacionamiento_Datos");
                ds_pd = ClassUtilidades.Ejecutar(CMD_pd);
                nombre = ds_pd.Tables[0].Rows[0]["nombre"].ToString().Trim();
                texto = ds_pd.Tables[0].Rows[0]["texto"].ToString().Trim();
                horario = ds_pd.Tables[0].Rows[0]["horario"].ToString().Trim();
                tarifabp = ds_pd.Tables[0].Rows[0]["tarifa_boleto_perdido"].ToString().Trim();
                tolerancia = ds_pd.Tables[0].Rows[0]["tolerancia"].ToString();
                if (comboBoxTipo.Text == "Chico")
                {
                    precioHora = ds_pd.Tables[0].Rows[0]["tarifa_chico"].ToString();
                }
                else
                {
                    precioHora = ds_pd.Tables[0].Rows[0]["tarifa_grande"].ToString();
                }



                /*fin de tomar datos*/
            }
            catch (Exception error)
            {

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
                Font fontSmall = new Font("Arial", 7, FontStyle.Regular);
                Font rs = new Font("Stencil", 25, FontStyle.Bold);
                Font fTType = new Font("", 150, FontStyle.Bold);
                SolidBrush sb = new SolidBrush(Color.Black);

                g.DrawString(WordWrap(nombre, maxCar), fBody1, sb, x / 2 - 10, 10, stringFormat);
                g.DrawString("IMP." + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 20, stringFormat);
                g.DrawString("*************************************************************", fBody1, sb, 0, 30);
                RectangleF srcRect = new RectangleF(x / 4 - 10, 40, x / 2, 50);
                Zen.Barcode.Code128BarcodeDraw barcode2 = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                g.DrawImage(barcode2.Draw(folio.ToString(), 50), srcRect);
                g.DrawString(("FOLIO:" + folio.ToString()), fBody1, sb, x / 2, 100, stringFormat);
                g.DrawString(("Entrada:" + entrada.ToString()), fBody1, sb, x / 2, 120, stringFormat);
                g.DrawString(WordWrap(texto, maxCar), fBody1, sb, 0, SPACE);
                SPACE = SPACE + 10;
                g.DrawString("Costo por Hora........$" + precioHora, fontSmall, sb, x / 2 - 10, y - 50, stringFormat);
                g.DrawString("Costo boleto perdido. $" + tarifabp, fontSmall, sb, x / 2 - 10, y - 60, stringFormat);
                g.DrawString("Tolerancia.............  " + tolerancia, fontSmall, sb, x / 2 - 10, y - 70, stringFormat);

                g.DrawString((horario), fontSmall, sb, x / 2, y - 80, stringFormat);


            }
            catch (Exception error)
            {
                MessageBox.Show("Error en la operacion de imprimir por falta de datos: " + error.Message);
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



        void pd3_PrintPage(object sender, PrintPageEventArgs e)
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
                    CMD_p2 = "";
                    ds_p2.Clear();
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
                Font fontSmall = new Font("Arial", 7, FontStyle.Regular);
                Font rs = new Font("Stencil", 25, FontStyle.Bold);
                Font fTType = new Font("", 150, FontStyle.Bold);
                SolidBrush sb = new SolidBrush(Color.Black);


                g.DrawString(nombre, fBody1, sb, x / 2 - 10, 10, stringFormat);
                g.DrawString(WordWrap(direccion, maxCar), fBody1, sb, x / 2 - 10, 30, stringFormat);
                g.DrawString(WordWrap(rfc, maxCar), fBody1, sb, x / 2 - 10, 50, stringFormat);
                g.DrawString("IMP." + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), fontSmall, sb, x / 2, 70, stringFormat);
                g.DrawString("*************************************************************", fBody1, sb, 0, 90);
                RectangleF srcRect = new RectangleF(x / 4 - 10, 40, x / 2, 50);
                g.DrawString(("FOLIO:" + folio.ToString()), fBody1, sb, x / 2, 110, stringFormat);
                g.DrawString("Entrada: " + print_entrada, fBody1, sb, x / 2, SPACE, stringFormat);
                g.DrawString("Salida:  " + print_salida, fBody1, sb, x / 2, SPACE + 20, stringFormat);
                g.DrawString("Cargos boleto perdio:  $" + print_cargos_salida, fBody1, sb, x / 2, SPACE + 80, stringFormat);
                g.DrawString(estadia, fBody1, sb, x / 2, SPACE + 60, stringFormat);
                g.DrawString("Total:  $" + total, fBody1, sb, x / 2, SPACE + 100, stringFormat);
                g.DrawString((horario), fontSmall, sb, x / 2, y - 50, stringFormat);


            }
            catch (Exception error)
            {
                MessageBox.Show("Error en la operacion de imprimir por falta de datos: " + error.Message);
            }

        }



        //COMPROBAR CUANDO ES UN CLIENTE
        public bool ComprobarPlacas_clientes()
        {
            try
            {

                cmdPlacas_placas_cliente = "select placas from clientes  where  placas= ('" + txtPlacas.Text.Trim() + "')";
                ds_placas_cliente = ClassUtilidades.Ejecutar(cmdPlacas_placas_cliente);


                if (ds_placas_cliente.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("La placa del vehículo ya esta registrada dentro del estacionamiento como pensión,ingrese otra placa", "MiEstacionamiento",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    cmdPlacas_placas_cliente = "";
                    ds_placas_cliente.Clear();
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
