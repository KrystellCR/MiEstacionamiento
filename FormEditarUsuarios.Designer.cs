namespace EstacionamientoV4
{
    partial class FormEditarUsuarios
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
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBoxMostrarPass = new System.Windows.Forms.CheckBox();
            this.TxtUsuario = new LibreriaValidar.ErrorTxtBox();
            this.TxtContraseña = new LibreriaValidar.ErrorTxtBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.BackColor = System.Drawing.Color.Chocolate;
            this.BtnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.BtnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.BtnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAceptar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnAceptar.Location = new System.Drawing.Point(470, 281);
            this.BtnAceptar.Size = new System.Drawing.Size(89, 34);
            this.BtnAceptar.Text = "Aceptar";
            this.BtnAceptar.UseVisualStyleBackColor = false;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(389, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = ".";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(389, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 40;
            this.label8.Text = ".";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label14.Location = new System.Drawing.Point(106, 178);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 21);
            this.label14.TabIndex = 38;
            this.label14.Text = "Contraseña";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label13.Location = new System.Drawing.Point(136, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 21);
            this.label13.TabIndex = 37;
            this.label13.Text = "Usuario";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label12.Location = new System.Drawing.Point(236, 153);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(148, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "Maximo 15 minimo 6 caracteres";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label11.Location = new System.Drawing.Point(241, 205);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(148, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "Maximo 10 minimo 8 caracteres";
            // 
            // checkBoxMostrarPass
            // 
            this.checkBoxMostrarPass.AutoSize = true;
            this.checkBoxMostrarPass.Checked = true;
            this.checkBoxMostrarPass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMostrarPass.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBoxMostrarPass.Location = new System.Drawing.Point(239, 221);
            this.checkBoxMostrarPass.Name = "checkBoxMostrarPass";
            this.checkBoxMostrarPass.Size = new System.Drawing.Size(116, 17);
            this.checkBoxMostrarPass.TabIndex = 34;
            this.checkBoxMostrarPass.Text = "Ocultar contraseña";
            this.checkBoxMostrarPass.UseVisualStyleBackColor = true;
            this.checkBoxMostrarPass.CheckedChanged += new System.EventHandler(this.checkBoxMostrarPass_CheckedChanged);
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUsuario.Location = new System.Drawing.Point(239, 123);
            this.TxtUsuario.MaxLength = 15;
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.ShortcutsEnabled = false;
            this.TxtUsuario.Size = new System.Drawing.Size(144, 27);
            this.TxtUsuario.SoloNumeros = true;
            this.TxtUsuario.SoloNumerosAlert = true;
            this.TxtUsuario.TabIndex = 32;
            this.TxtUsuario.Validar = false;
            this.TxtUsuario.TextChanged += new System.EventHandler(this.TxtUsuario_TextChanged);
            // 
            // TxtContraseña
            // 
            this.TxtContraseña.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtContraseña.Location = new System.Drawing.Point(239, 175);
            this.TxtContraseña.MaxLength = 10;
            this.TxtContraseña.Name = "TxtContraseña";
            this.TxtContraseña.ShortcutsEnabled = false;
            this.TxtContraseña.Size = new System.Drawing.Size(144, 27);
            this.TxtContraseña.SoloNumeros = false;
            this.TxtContraseña.SoloNumerosAlert = false;
            this.TxtContraseña.TabIndex = 31;
            this.TxtContraseña.UseSystemPasswordChar = true;
            this.TxtContraseña.Validar = false;
            this.TxtContraseña.TextChanged += new System.EventHandler(this.TxtContraseña_TextChanged);
            // 
            // FormEditarUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 338);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.checkBoxMostrarPass);
            this.Controls.Add(this.TxtUsuario);
            this.Controls.Add(this.TxtContraseña);
            this.Name = "FormEditarUsuarios";
            this.Text = "FormEditarUsuarios";
            this.Load += new System.EventHandler(this.FormEditarUsuarios_Load);
            this.Controls.SetChildIndex(this.BtnAceptar, 0);
            this.Controls.SetChildIndex(this.TxtContraseña, 0);
            this.Controls.SetChildIndex(this.TxtUsuario, 0);
            this.Controls.SetChildIndex(this.checkBoxMostrarPass, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBoxMostrarPass;
        private LibreriaValidar.ErrorTxtBox TxtUsuario;
        public LibreriaValidar.ErrorTxtBox TxtContraseña;
    }
}