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
            Cliente = new Label();
            Estado = new Label();
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
            // Cliente
            // 
            Cliente.AutoSize = true;
            Cliente.Font = new Font("Segoe UI Historic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Cliente.Location = new Point(103, 68);
            Cliente.Name = "Cliente";
            Cliente.Size = new Size(65, 21);
            Cliente.TabIndex = 4;
            Cliente.Text = "Cliente";
            // 
            // Estado
            // 
            Estado.AutoSize = true;
            Estado.Font = new Font("Segoe UI Historic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Estado.Location = new Point(103, 147);
            Estado.Name = "Estado";
            Estado.Size = new Size(62, 21);
            Estado.TabIndex = 5;
            Estado.Text = "Estado";
            // 
            // FormEditarFactura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(475, 386);
            Controls.Add(Estado);
            Controls.Add(Cliente);
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
        private Label Cliente;
        private Label Estado;
    }
}