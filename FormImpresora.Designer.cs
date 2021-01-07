namespace EstacionamientoV4
{
    partial class FormImpresora
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
            this.cmboTamaño = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboImpresoras = new System.Windows.Forms.ComboBox();
            this.btnImpresora = new System.Windows.Forms.Button();
            this.comboTicket = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.BackColor = System.Drawing.Color.Chocolate;
            this.BtnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.BtnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.BtnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.BtnAceptar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnAceptar.Location = new System.Drawing.Point(456, 289);
            this.BtnAceptar.Size = new System.Drawing.Size(99, 36);
            this.BtnAceptar.Text = "Aceptar";
            this.BtnAceptar.UseVisualStyleBackColor = false;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // cmboTamaño
            // 
            this.cmboTamaño.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboTamaño.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.cmboTamaño.FormattingEnabled = true;
            this.cmboTamaño.Items.AddRange(new object[] {
            "58",
            "80"});
            this.cmboTamaño.Location = new System.Drawing.Point(227, 105);
            this.cmboTamaño.Name = "cmboTamaño";
            this.cmboTamaño.Size = new System.Drawing.Size(204, 29);
            this.cmboTamaño.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(102, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tam. papel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(102, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 21);
            this.label2.TabIndex = 13;
            this.label2.Text = "Impresora";
            // 
            // cboImpresoras
            // 
            this.cboImpresoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImpresoras.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboImpresoras.FormattingEnabled = true;
            this.cboImpresoras.Location = new System.Drawing.Point(227, 150);
            this.cboImpresoras.Name = "cboImpresoras";
            this.cboImpresoras.Size = new System.Drawing.Size(204, 29);
            this.cboImpresoras.TabIndex = 25;
            // 
            // btnImpresora
            // 
            this.btnImpresora.BackColor = System.Drawing.Color.Gray;
            this.btnImpresora.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnImpresora.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.btnImpresora.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImpresora.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImpresora.ForeColor = System.Drawing.Color.Black;
            this.btnImpresora.Image = global::EstacionamientoV4.Properties.Resources.icons8_imprimir_32;
            this.btnImpresora.Location = new System.Drawing.Point(60, 189);
            this.btnImpresora.Name = "btnImpresora";
            this.btnImpresora.Size = new System.Drawing.Size(151, 42);
            this.btnImpresora.TabIndex = 26;
            this.btnImpresora.Text = "imprimir prueba";
            this.btnImpresora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImpresora.UseVisualStyleBackColor = false;
            this.btnImpresora.Click += new System.EventHandler(this.btnImpresora_Click);
            // 
            // comboTicket
            // 
            this.comboTicket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTicket.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTicket.FormattingEnabled = true;
            this.comboTicket.Items.AddRange(new object[] {
            "Ticket entrada",
            "Ticket salida",
            "Boleto perdido",
            "Ticket corte de caja"});
            this.comboTicket.Location = new System.Drawing.Point(227, 195);
            this.comboTicket.Name = "comboTicket";
            this.comboTicket.Size = new System.Drawing.Size(204, 29);
            this.comboTicket.TabIndex = 27;
            // 
            // FormImpresora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 337);
            this.Controls.Add(this.comboTicket);
            this.Controls.Add(this.btnImpresora);
            this.Controls.Add(this.cboImpresoras);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboTamaño);
            this.Name = "FormImpresora";
            this.Text = "FormImpresora";
            this.Load += new System.EventHandler(this.FormImpresora_Load);
            this.Controls.SetChildIndex(this.cmboTamaño, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cboImpresoras, 0);
            this.Controls.SetChildIndex(this.btnImpresora, 0);
            this.Controls.SetChildIndex(this.comboTicket, 0);
            this.Controls.SetChildIndex(this.BtnAceptar, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmboTamaño;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboImpresoras;
        public System.Windows.Forms.Button btnImpresora;
        private System.Windows.Forms.ComboBox comboTicket;
    }
}