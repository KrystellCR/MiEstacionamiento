namespace EstacionamientoV4
{
    partial class FormAgregarCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNombre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtPlacas = new System.Windows.Forms.TextBox();
            this.txtVehi = new System.Windows.Forms.TextBox();
            this.txtCobro = new System.Windows.Forms.TextBox();
            this.datePago = new System.Windows.Forms.DateTimePicker();
            this.lblTarifabp = new System.Windows.Forms.Label();
            this.lblCob = new System.Windows.Forms.Label();
            this.lblVehic = new System.Windows.Forms.Label();
            this.lblPlacas = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTarjeton = new System.Windows.Forms.Label();
            this.txtTarjeton = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(550, 9);
            this.label3.Visible = false;
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(80)))), ((int)(((byte)(65)))));
            this.BtnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            this.BtnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            this.BtnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAceptar.ForeColor = System.Drawing.Color.Silver;
            this.BtnAceptar.Location = new System.Drawing.Point(292, 420);
            this.BtnAceptar.Size = new System.Drawing.Size(100, 34);
            this.BtnAceptar.TabIndex = 7;
            this.BtnAceptar.Text = "Aceptar";
            this.BtnAceptar.UseVisualStyleBackColor = false;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblNombre.Location = new System.Drawing.Point(64, 106);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(61, 17);
            this.lblNombre.TabIndex = 10;
            this.lblNombre.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(17, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Siguiente pago";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(75, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Cobro";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(61, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Vehiculo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(75, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Placas";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(148, 106);
            this.txtNombre.MaxLength = 19;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(301, 23);
            this.txtNombre.TabIndex = 1;
            // 
            // txtPlacas
            // 
            this.txtPlacas.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlacas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlacas.Location = new System.Drawing.Point(148, 153);
            this.txtPlacas.MaxLength = 15;
            this.txtPlacas.Name = "txtPlacas";
            this.txtPlacas.Size = new System.Drawing.Size(301, 23);
            this.txtPlacas.TabIndex = 2;
            this.txtPlacas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlacas_KeyPress);
            // 
            // txtVehi
            // 
            this.txtVehi.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehi.Location = new System.Drawing.Point(148, 203);
            this.txtVehi.MaxLength = 15;
            this.txtVehi.Name = "txtVehi";
            this.txtVehi.Size = new System.Drawing.Size(301, 23);
            this.txtVehi.TabIndex = 3;
            // 
            // txtCobro
            // 
            this.txtCobro.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCobro.Location = new System.Drawing.Point(148, 312);
            this.txtCobro.MaxLength = 5;
            this.txtCobro.Name = "txtCobro";
            this.txtCobro.Size = new System.Drawing.Size(76, 23);
            this.txtCobro.TabIndex = 5;
            this.txtCobro.TextChanged += new System.EventHandler(this.txtCobro_TextChanged);
            this.txtCobro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCobro_KeyPress);
            // 
            // datePago
            // 
            this.datePago.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePago.Location = new System.Drawing.Point(148, 255);
            this.datePago.MinDate = new System.DateTime(2019, 2, 11, 0, 0, 0, 0);
            this.datePago.Name = "datePago";
            this.datePago.Size = new System.Drawing.Size(301, 23);
            this.datePago.TabIndex = 4;
            this.datePago.Value = new System.DateTime(2019, 2, 11, 0, 0, 0, 0);
            // 
            // lblTarifabp
            // 
            this.lblTarifabp.AutoSize = true;
            this.lblTarifabp.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarifabp.Location = new System.Drawing.Point(455, 113);
            this.lblTarifabp.Name = "lblTarifabp";
            this.lblTarifabp.Size = new System.Drawing.Size(11, 16);
            this.lblTarifabp.TabIndex = 30;
            this.lblTarifabp.Text = ".";
            // 
            // lblCob
            // 
            this.lblCob.AutoSize = true;
            this.lblCob.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCob.Location = new System.Drawing.Point(239, 319);
            this.lblCob.Name = "lblCob";
            this.lblCob.Size = new System.Drawing.Size(11, 16);
            this.lblCob.TabIndex = 32;
            this.lblCob.Text = ".";
            // 
            // lblVehic
            // 
            this.lblVehic.AutoSize = true;
            this.lblVehic.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehic.Location = new System.Drawing.Point(455, 207);
            this.lblVehic.Name = "lblVehic";
            this.lblVehic.Size = new System.Drawing.Size(11, 16);
            this.lblVehic.TabIndex = 33;
            this.lblVehic.Text = ".";
            // 
            // lblPlacas
            // 
            this.lblPlacas.AutoSize = true;
            this.lblPlacas.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlacas.Location = new System.Drawing.Point(455, 160);
            this.lblPlacas.Name = "lblPlacas";
            this.lblPlacas.Size = new System.Drawing.Size(11, 16);
            this.lblPlacas.TabIndex = 36;
            this.lblPlacas.Text = ".";
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNom.Location = new System.Drawing.Point(455, 113);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(11, 16);
            this.lblNom.TabIndex = 37;
            this.lblNom.Text = ".";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.Location = new System.Drawing.Point(398, 420);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 34);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(75, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 77;
            this.label1.Text = "Tarjetón";
            // 
            // lblTarjeton
            // 
            this.lblTarjeton.AutoSize = true;
            this.lblTarjeton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarjeton.Location = new System.Drawing.Point(239, 367);
            this.lblTarjeton.Name = "lblTarjeton";
            this.lblTarjeton.Size = new System.Drawing.Size(11, 16);
            this.lblTarjeton.TabIndex = 79;
            this.lblTarjeton.Text = ".";
            // 
            // txtTarjeton
            // 
            this.txtTarjeton.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarjeton.Location = new System.Drawing.Point(148, 360);
            this.txtTarjeton.MaxLength = 5;
            this.txtTarjeton.Name = "txtTarjeton";
            this.txtTarjeton.Size = new System.Drawing.Size(76, 23);
            this.txtTarjeton.TabIndex = 6;
            this.txtTarjeton.TextChanged += new System.EventHandler(this.txtTarjeton_TextChanged);
            this.txtTarjeton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTarjeton_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Chocolate;
            this.label7.Location = new System.Drawing.Point(145, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(215, 17);
            this.label7.TabIndex = 80;
            this.label7.Text = "Recomendable usar fecha del 1-28";
            // 
            // FormAgregarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 481);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblTarjeton);
            this.Controls.Add(this.txtTarjeton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.lblPlacas);
            this.Controls.Add(this.lblVehic);
            this.Controls.Add(this.lblCob);
            this.Controls.Add(this.lblTarifabp);
            this.Controls.Add(this.datePago);
            this.Controls.Add(this.txtCobro);
            this.Controls.Add(this.txtVehi);
            this.Controls.Add(this.txtPlacas);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNombre);
            this.Name = "FormAgregarCliente";
            this.Text = "FormAgregarCliente";
            this.Load += new System.EventHandler(this.FormAgregarCliente_Load);
            this.Controls.SetChildIndex(this.lblNombre, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtNombre, 0);
            this.Controls.SetChildIndex(this.txtPlacas, 0);
            this.Controls.SetChildIndex(this.txtVehi, 0);
            this.Controls.SetChildIndex(this.txtCobro, 0);
            this.Controls.SetChildIndex(this.datePago, 0);
            this.Controls.SetChildIndex(this.BtnAceptar, 0);
            this.Controls.SetChildIndex(this.lblTarifabp, 0);
            this.Controls.SetChildIndex(this.lblCob, 0);
            this.Controls.SetChildIndex(this.lblVehic, 0);
            this.Controls.SetChildIndex(this.lblPlacas, 0);
            this.Controls.SetChildIndex(this.lblNom, 0);
            this.Controls.SetChildIndex(this.btnCancelar, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtTarjeton, 0);
            this.Controls.SetChildIndex(this.lblTarjeton, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTarifabp;
        private System.Windows.Forms.Label lblCob;
        private System.Windows.Forms.Label lblVehic;
        private System.Windows.Forms.Label lblPlacas;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.TextBox txtNombre;
        public System.Windows.Forms.TextBox txtPlacas;
        public System.Windows.Forms.TextBox txtVehi;
        public System.Windows.Forms.TextBox txtCobro;
        public System.Windows.Forms.DateTimePicker datePago;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTarjeton;
        public System.Windows.Forms.TextBox txtTarjeton;
        private System.Windows.Forms.Label label7;
    }
}