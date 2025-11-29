namespace ecspage
{
    partial class FormAgregarCliente
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblNombre;
        private Label lblRuc;
        private Label lblEmail;
        private Label lblDireccion;

        private TextBox txtNombre;
        private TextBox txtRuc;
        private TextBox txtEmail;
        private TextBox txtDireccion;

        private Button btnGuardar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblNombre = new Label();
            lblRuc = new Label();
            lblEmail = new Label();
            lblDireccion = new Label();

            txtNombre = new TextBox();
            txtRuc = new TextBox();
            txtEmail = new TextBox();
            txtDireccion = new TextBox();

            btnGuardar = new Button();

            SuspendLayout();

            // lblNombre
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(40, 40);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.Text = "Nombre:";

            // txtNombre
            txtNombre.Location = new Point(150, 37);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 23);

            // lblRuc
            lblRuc.AutoSize = true;
            lblRuc.Location = new Point(40, 80);
            lblRuc.Name = "lblRuc";
            lblRuc.Size = new Size(33, 15);
            lblRuc.Text = "RUC:";

            // txtRuc
            txtRuc.Location = new Point(150, 77);
            txtRuc.Name = "txtRuc";
            txtRuc.Size = new Size(200, 23);

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(40, 120);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.Text = "Email:";

            // txtEmail
            txtEmail.Location = new Point(150, 117);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 23);

            // lblDireccion
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(40, 160);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(58, 15);
            lblDireccion.Text = "Dirección:";

            // txtDireccion
            txtDireccion.Location = new Point(150, 157);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(200, 23);

            // btnGuardar
            btnGuardar.Location = new Point(150, 210);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 30);
            btnGuardar.Text = "Guardar";

            // FormAgregarCliente
            ClientSize = new Size(420, 300);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblRuc);
            Controls.Add(txtRuc);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblDireccion);
            Controls.Add(txtDireccion);
            Controls.Add(btnGuardar);

            Name = "FormAgregarCliente";
            Text = "Agregar Cliente";

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
