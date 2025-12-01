namespace ecspage
{
    partial class FormEditarFactura
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
            btnGuardar = new Button();
            btnCancelar = new Button();
            cmbCliente = new ComboBox();
            cmbEstado = new ComboBox();
            lblCliente = new Label();
            lblEstado = new Label();
            SuspendLayout();
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = SystemColors.ActiveCaption;
            btnGuardar.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.Location = new Point(80, 260);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(104, 50);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.RosyBrown;
            btnCancelar.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(274, 260);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(98, 50);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // cmbCliente
            // 
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(209, 70);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(150, 23);
            cmbCliente.TabIndex = 2;
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(209, 147);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(150, 23);
            cmbEstado.TabIndex = 3;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI Historic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCliente.Location = new Point(103, 68);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(65, 21);
            lblCliente.TabIndex = 4;
            lblCliente.Text = "Cliente";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI Historic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstado.Location = new Point(103, 147);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(62, 21);
            lblEstado.TabIndex = 5;
            lblEstado.Text = "Estado";
            // 
            // FormEditarFactura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(475, 386);
            Controls.Add(lblEstado);
            Controls.Add(lblCliente);
            Controls.Add(cmbEstado);
            Controls.Add(cmbCliente);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Name = "FormEditarFactura";
            Text = "FormEditarFactura";
            Load += FormEditarFactura_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGuardar;
        private Button btnCancelar;
        private ComboBox cmbCliente;
        private ComboBox cmbEstado;
        private Label lblCliente;
        private Label lblEstado;
    }
}