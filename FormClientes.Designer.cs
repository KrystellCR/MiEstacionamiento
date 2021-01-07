namespace EstacionamientoV4
{
    partial class FormClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClientes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.BtnCargar = new System.Windows.Forms.Button();
            this.btnSuspender = new System.Windows.Forms.Button();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.BtnAddCliente = new System.Windows.Forms.Button();
            this.dataClientes = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dataBlackList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataClientes)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataBlackList)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1184, 561);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.tabPage4.Controls.Add(this.BtnCargar);
            this.tabPage4.Controls.Add(this.btnSuspender);
            this.tabPage4.Controls.Add(this.btnCobrar);
            this.tabPage4.Controls.Add(this.btnEdit);
            this.tabPage4.Controls.Add(this.BtnAddCliente);
            this.tabPage4.Controls.Add(this.dataClientes);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1176, 531);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Pensión";
            // 
            // BtnCargar
            // 
            this.BtnCargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(80)))), ((int)(((byte)(65)))));
            this.BtnCargar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            this.BtnCargar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            this.BtnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCargar.Image = ((System.Drawing.Image)(resources.GetObject("BtnCargar.Image")));
            this.BtnCargar.Location = new System.Drawing.Point(170, 28);
            this.BtnCargar.Name = "BtnCargar";
            this.BtnCargar.Size = new System.Drawing.Size(135, 34);
            this.BtnCargar.TabIndex = 36;
            this.BtnCargar.UseVisualStyleBackColor = false;
            this.BtnCargar.Click += new System.EventHandler(this.BtnCargar_Click_1);
            // 
            // btnSuspender
            // 
            this.btnSuspender.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnSuspender.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSuspender.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSuspender.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.btnSuspender.ForeColor = System.Drawing.Color.Silver;
            this.btnSuspender.Image = global::EstacionamientoV4.Properties.Resources.icons8_cancelar_25;
            this.btnSuspender.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSuspender.Location = new System.Drawing.Point(1012, 28);
            this.btnSuspender.Name = "btnSuspender";
            this.btnSuspender.Size = new System.Drawing.Size(140, 34);
            this.btnSuspender.TabIndex = 35;
            this.btnSuspender.Text = "Suspender";
            this.btnSuspender.UseVisualStyleBackColor = false;
            this.btnSuspender.Click += new System.EventHandler(this.btnSuspender_Click);
            // 
            // btnCobrar
            // 
            this.btnCobrar.BackColor = System.Drawing.Color.Chocolate;
            this.btnCobrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnCobrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.btnCobrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCobrar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobrar.ForeColor = System.Drawing.Color.Silver;
            this.btnCobrar.Image = global::EstacionamientoV4.Properties.Resources.icons8_cash_25;
            this.btnCobrar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCobrar.Location = new System.Drawing.Point(24, 28);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(140, 34);
            this.btnCobrar.TabIndex = 34;
            this.btnCobrar.Text = "Cobrar";
            this.btnCobrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCobrar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCobrar.UseVisualStyleBackColor = false;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Chocolate;
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.btnEdit.ForeColor = System.Drawing.Color.Silver;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.Location = new System.Drawing.Point(866, 28);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(140, 34);
            this.btnEdit.TabIndex = 33;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // BtnAddCliente
            // 
            this.BtnAddCliente.BackColor = System.Drawing.Color.Chocolate;
            this.BtnAddCliente.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.BtnAddCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.BtnAddCliente.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnAddCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddCliente.ForeColor = System.Drawing.Color.Silver;
            this.BtnAddCliente.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddCliente.Image")));
            this.BtnAddCliente.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnAddCliente.Location = new System.Drawing.Point(720, 28);
            this.BtnAddCliente.Name = "BtnAddCliente";
            this.BtnAddCliente.Size = new System.Drawing.Size(140, 34);
            this.BtnAddCliente.TabIndex = 32;
            this.BtnAddCliente.Text = "Agregar";
            this.BtnAddCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnAddCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.BtnAddCliente.UseVisualStyleBackColor = false;
            this.BtnAddCliente.Click += new System.EventHandler(this.BtnAddCliente_Click);
            // 
            // dataClientes
            // 
            this.dataClientes.AllowUserToAddRows = false;
            this.dataClientes.AllowUserToDeleteRows = false;
            this.dataClientes.AllowUserToOrderColumns = true;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.White;
            this.dataClientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle22;
            this.dataClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataClientes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataClientes.DefaultCellStyle = dataGridViewCellStyle24;
            this.dataClientes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataClientes.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataClientes.Location = new System.Drawing.Point(24, 68);
            this.dataClientes.Name = "dataClientes";
            this.dataClientes.ReadOnly = true;
            this.dataClientes.RowHeadersWidth = 40;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.White;
            this.dataClientes.RowsDefaultCellStyle = dataGridViewCellStyle25;
            this.dataClientes.Size = new System.Drawing.Size(1128, 390);
            this.dataClientes.TabIndex = 3;
            this.dataClientes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataClientes_CellFormatting);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.tabPage5.Controls.Add(this.dataBlackList);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1176, 531);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Suspendidos";
            // 
            // dataBlackList
            // 
            this.dataBlackList.AllowUserToAddRows = false;
            this.dataBlackList.AllowUserToDeleteRows = false;
            this.dataBlackList.AllowUserToOrderColumns = true;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.Color.White;
            this.dataBlackList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle26;
            this.dataBlackList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataBlackList.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataBlackList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataBlackList.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataBlackList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataBlackList.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataBlackList.Location = new System.Drawing.Point(169, 45);
            this.dataBlackList.Name = "dataBlackList";
            this.dataBlackList.ReadOnly = true;
            this.dataBlackList.RowHeadersWidth = 40;
            this.dataBlackList.Size = new System.Drawing.Size(611, 390);
            this.dataBlackList.TabIndex = 4;
            // 
            // FormClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1200, 600);
            this.Name = "FormClientes";
            this.Text = "Pensión";
            this.Load += new System.EventHandler(this.FormClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataClientes)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataBlackList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataClientes;
        private System.Windows.Forms.DataGridView dataBlackList;
        public System.Windows.Forms.Button btnCobrar;
        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button BtnAddCliente;
        public System.Windows.Forms.Button btnSuspender;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button BtnCargar;
    }
}