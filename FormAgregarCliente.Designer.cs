using System.Drawing;
using System.Windows.Forms;

namespace ecspage
{
    partial class FormAgregarCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label labelTitulo;
        private Label labelNombre;
        private TextBox txtNombre;
        private Label labelRuc;
        private TextBox txtRuc;
        private Label labelEmail;
        private TextBox txtEmail;
        private Label labelDireccion;
        private TextBox txtDireccion;
        private Button btnGuardar;

        /// <summary>
        /// Clean up any resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            labelTitulo = new Label();
            labelNombre = new Label();
            txtNombre = new TextBox();
            labelRuc = new Label();
            txtRuc = new TextBox();
            labelEmail = new Label();
            txtEmail = new TextBox();
            labelDireccion = new Label();
            txtDireccion = new TextBox();
            btnGuardar = new Button();
            SuspendLayout();
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            labelTitulo.Location = new Point(180, 20);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(193, 32);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "Agregar Cliente";
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(60, 100);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(54, 15);
            labelNombre.TabIndex = 1;
            labelNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(150, 97);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 23);
            txtNombre.TabIndex = 2;
            // 
            // labelRuc
            // 
            labelRuc.AutoSize = true;
            labelRuc.Location = new Point(60, 150);
            labelRuc.Name = "labelRuc";
            labelRuc.Size = new Size(33, 15);
            labelRuc.TabIndex = 3;
            labelRuc.Text = "RUC:";
            // 
            // txtRuc
            // 
            txtRuc.Location = new Point(150, 147);
            txtRuc.Name = "txtRuc";
            txtRuc.Size = new Size(250, 23);
            txtRuc.TabIndex = 4;
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(60, 200);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(39, 15);
            labelEmail.TabIndex = 5;
            labelEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(150, 197);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 23);
            txtEmail.TabIndex = 6;
            // 
            // labelDireccion
            // 
            labelDireccion.AutoSize = true;
            labelDireccion.Location = new Point(60, 250);
            labelDireccion.Name = "labelDireccion";
            labelDireccion.Size = new Size(60, 15);
            labelDireccion.TabIndex = 7;
            labelDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(150, 247);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(250, 23);
            txtDireccion.TabIndex = 8;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(220, 320);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(150, 35);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // FormAgregarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(labelTitulo);
            Controls.Add(labelNombre);
            Controls.Add(txtNombre);
            Controls.Add(labelRuc);
            Controls.Add(txtRuc);
            Controls.Add(labelEmail);
            Controls.Add(txtEmail);
            Controls.Add(labelDireccion);
            Controls.Add(txtDireccion);
            Controls.Add(btnGuardar);
            Name = "FormAgregarCliente";
            Text = "Agregar Cliente";
            Load += FormAgregarCliente_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
