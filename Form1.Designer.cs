namespace ecspage
{
    partial class FormNuevaFactura
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMain = new Panel();
            panelNuevaFactura = new Panel();
            dgvItems = new DataGridView();
            panelTop = new Panel();
            btnAgregarProducto = new Button();
            label9 = new Label();
            panelResumen = new Panel();
            btnFacturasEmitidas = new Button();
            chbIGV = new CheckBox();
            label8 = new Label();
            lbTotal = new Label();
            label16 = new Label();
            lbImpuesto = new Label();
            lbSubtotal = new Label();
            label13 = new Label();
            label12 = new Label();
            gbFactura = new GroupBox();
            button1 = new Button();
            label11 = new Label();
            label14 = new Label();
            btnGenerarFactura = new Button();
            dtFechaEmision = new DateTimePicker();
            cmbEstado = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            txtDireccion = new TextBox();
            txtEmail = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            txtRUC = new TextBox();
            label2 = new Label();
            txtNombreCliente = new TextBox();
            btnAgregarCliente = new Button();
            label1 = new Label();
            cmbCliente = new ComboBox();
            panelInferior = new Panel();
            button1 = new Button();
            panelMain.SuspendLayout();
            panelNuevaFactura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            panelTop.SuspendLayout();
            panelResumen.SuspendLayout();
            gbFactura.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(panelNuevaFactura);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1084, 661);
            panelMain.TabIndex = 0;
            // 
            // panelNuevaFactura
            // 
            panelNuevaFactura.Controls.Add(dgvItems);
            panelNuevaFactura.Controls.Add(panelTop);
            panelNuevaFactura.Controls.Add(panelResumen);
            panelNuevaFactura.Controls.Add(gbFactura);
            panelNuevaFactura.Controls.Add(panelInferior);
            panelNuevaFactura.Dock = DockStyle.Fill;
            panelNuevaFactura.Location = new Point(0, 0);
            panelNuevaFactura.Name = "panelNuevaFactura";
            panelNuevaFactura.Size = new Size(1084, 661);
            panelNuevaFactura.TabIndex = 0;
            // 
            // dgvItems
            // 
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Dock = DockStyle.Fill;
            dgvItems.Location = new Point(353, 34);
            dgvItems.Name = "dgvItems";
            dgvItems.Size = new Size(538, 602);
            dgvItems.TabIndex = 9;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnAgregarProducto);
            panelTop.Controls.Add(label9);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(353, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(538, 34);
            panelTop.TabIndex = 7;
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.FlatStyle = FlatStyle.System;
            btnAgregarProducto.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarProducto.Location = new Point(509, 6);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(23, 23);
            btnAgregarProducto.TabIndex = 3;
            btnAgregarProducto.Text = "+";
            btnAgregarProducto.UseVisualStyleBackColor = true;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            label9.Location = new Point(159, 6);
            label9.Name = "label9";
            label9.Size = new Size(252, 25);
            label9.TabIndex = 0;
            label9.Text = "SELECCIÓN DE PRODUCTOS";
            // 
            // panelResumen
            // 
            panelResumen.Controls.Add(btnFacturasEmitidas);
            panelResumen.Controls.Add(chbIGV);
            panelResumen.Controls.Add(label8);
            panelResumen.Controls.Add(lbTotal);
            panelResumen.Controls.Add(label16);
            panelResumen.Controls.Add(lbImpuesto);
            panelResumen.Controls.Add(lbSubtotal);
            panelResumen.Controls.Add(label13);
            panelResumen.Controls.Add(label12);
            panelResumen.Dock = DockStyle.Right;
            panelResumen.Location = new Point(891, 0);
            panelResumen.Name = "panelResumen";
            panelResumen.Size = new Size(193, 636);
            panelResumen.TabIndex = 5;
            // 
            // btnFacturasEmitidas
            // 
            btnFacturasEmitidas.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFacturasEmitidas.Location = new Point(33, 598);
            btnFacturasEmitidas.Name = "btnFacturasEmitidas";
            btnFacturasEmitidas.Size = new Size(134, 32);
            btnFacturasEmitidas.TabIndex = 1;
            btnFacturasEmitidas.Text = "Facturas emitidas";
            btnFacturasEmitidas.UseVisualStyleBackColor = true;
            btnFacturasEmitidas.Click += btnFacturasEmitidas_Click;
            // 
            // chbIGV
            // 
            chbIGV.AutoSize = true;
            chbIGV.Location = new Point(33, 144);
            chbIGV.Name = "chbIGV";
            chbIGV.Size = new Size(15, 14);
            chbIGV.TabIndex = 26;
            chbIGV.UseVisualStyleBackColor = true;
            chbIGV.CheckedChanged += chbIGV_CheckedChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(54, 141);
            label8.Name = "label8";
            label8.Size = new Size(111, 17);
            label8.TabIndex = 25;
            label8.Text = "Precio incluye IGV";
            // 
            // lbTotal
            // 
            lbTotal.AutoSize = true;
            lbTotal.Location = new Point(121, 107);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(44, 15);
            lbTotal.TabIndex = 5;
            lbTotal.Text = "label17";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(21, 99);
            label16.Name = "label16";
            label16.Size = new Size(60, 25);
            label16.TabIndex = 4;
            label16.Text = "Total:";
            // 
            // lbImpuesto
            // 
            lbImpuesto.AutoSize = true;
            lbImpuesto.Location = new Point(121, 70);
            lbImpuesto.Name = "lbImpuesto";
            lbImpuesto.Size = new Size(44, 15);
            lbImpuesto.TabIndex = 3;
            lbImpuesto.Text = "label15";
            // 
            // lbSubtotal
            // 
            lbSubtotal.AutoSize = true;
            lbSubtotal.Location = new Point(121, 41);
            lbSubtotal.Name = "lbSubtotal";
            lbSubtotal.Size = new Size(44, 15);
            lbSubtotal.TabIndex = 2;
            lbSubtotal.Text = "label14";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(21, 70);
            label13.Name = "label13";
            label13.Size = new Size(60, 15);
            label13.TabIndex = 1;
            label13.Text = "Impuesto:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(21, 41);
            label12.Name = "label12";
            label12.Size = new Size(54, 15);
            label12.TabIndex = 0;
            label12.Text = "Subtotal:";
            // 
            // gbFactura
            // 
            gbFactura.Controls.Add(button1);
            gbFactura.Controls.Add(label11);
            gbFactura.Controls.Add(label14);
            gbFactura.Controls.Add(btnGenerarFactura);
            gbFactura.Controls.Add(dtFechaEmision);
            gbFactura.Controls.Add(cmbEstado);
            gbFactura.Controls.Add(label7);
            gbFactura.Controls.Add(label6);
            gbFactura.Controls.Add(txtDireccion);
            gbFactura.Controls.Add(txtEmail);
            gbFactura.Controls.Add(label5);
            gbFactura.Controls.Add(label4);
            gbFactura.Controls.Add(label3);
            gbFactura.Controls.Add(txtRUC);
            gbFactura.Controls.Add(label2);
            gbFactura.Controls.Add(txtNombreCliente);
            gbFactura.Controls.Add(btnAgregarCliente);
            gbFactura.Controls.Add(label1);
            gbFactura.Controls.Add(cmbCliente);
            gbFactura.Dock = DockStyle.Left;
            gbFactura.Location = new Point(0, 0);
            gbFactura.Name = "gbFactura";
            gbFactura.Size = new Size(353, 636);
            gbFactura.TabIndex = 3;
            gbFactura.TabStop = false;
            gbFactura.Enter += gbFactura_Enter;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9.75F);
            button1.Location = new Point(29, 598);
            button1.Name = "button1";
            button1.Size = new Size(98, 32);
            button1.TabIndex = 27;
            button1.Text = "Cerrar Sesión";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(12, 216);
            label11.Name = "label11";
            label11.Size = new Size(62, 21);
            label11.TabIndex = 26;
            label11.Text = "Factura";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(95, 9);
            label14.Name = "label14";
            label14.Size = new Size(161, 25);
            label14.TabIndex = 25;
            label14.Text = "NUEVA FACTURA";
            // 
            // btnGenerarFactura
            // 
            btnGenerarFactura.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGenerarFactura.Location = new Point(182, 598);
            btnGenerarFactura.Name = "btnGenerarFactura";
            btnGenerarFactura.Size = new Size(114, 32);
            btnGenerarFactura.TabIndex = 24;
            btnGenerarFactura.Text = "Generar Factura";
            btnGenerarFactura.UseVisualStyleBackColor = true;
            btnGenerarFactura.Click += btnGenerarFactura_Click_1;
            // 
            // dtFechaEmision
            // 
            dtFechaEmision.Location = new Point(129, 281);
            dtFechaEmision.Name = "dtFechaEmision";
            dtFechaEmision.Size = new Size(209, 23);
            dtFechaEmision.TabIndex = 18;
            // 
            // cmbEstado
            // 
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(129, 252);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(100, 23);
            cmbEstado.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 255);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 14;
            label7.Text = "Estado:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 287);
            label6.Name = "label6";
            label6.Size = new Size(102, 15);
            label6.TabIndex = 13;
            label6.Text = "Fecha de emisión:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(129, 180);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(180, 23);
            txtDireccion.TabIndex = 10;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(129, 151);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(180, 23);
            txtEmail.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 183);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 8;
            label5.Text = "Direccion:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 154);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 7;
            label4.Text = "Email:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 125);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 6;
            label3.Text = "RUC:";
            // 
            // txtRUC
            // 
            txtRUC.Location = new Point(129, 122);
            txtRUC.Name = "txtRUC";
            txtRUC.Size = new Size(180, 23);
            txtRUC.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 96);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 4;
            label2.Text = "Nombre:";
            // 
            // txtNombreCliente
            // 
            txtNombreCliente.Location = new Point(129, 93);
            txtNombreCliente.Name = "txtNombreCliente";
            txtNombreCliente.Size = new Size(180, 23);
            txtNombreCliente.TabIndex = 3;
            // 
            // btnAgregarCliente
            // 
            btnAgregarCliente.FlatStyle = FlatStyle.System;
            btnAgregarCliente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarCliente.Location = new Point(315, 55);
            btnAgregarCliente.Name = "btnAgregarCliente";
            btnAgregarCliente.Size = new Size(23, 23);
            btnAgregarCliente.TabIndex = 2;
            btnAgregarCliente.Text = "+";
            btnAgregarCliente.UseVisualStyleBackColor = true;
            btnAgregarCliente.Click += btnAgregarCliente_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 58);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 1;
            label1.Text = "Cliente:";
            // 
            // cmbCliente
            // 
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(65, 55);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(244, 23);
            cmbCliente.TabIndex = 0;
            cmbCliente.SelectedIndexChanged += cmbCliente_SelectedIndexChanged_1;
            // 
            // panelInferior
            // 
            panelInferior.Dock = DockStyle.Bottom;
            panelInferior.Location = new Point(0, 636);
            panelInferior.Name = "panelInferior";
            panelInferior.Size = new Size(1084, 25);
            panelInferior.TabIndex = 0;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9.75F);
            button1.Location = new Point(29, 598);
            button1.Name = "button1";
            button1.Size = new Size(98, 32);
            button1.TabIndex = 27;
            button1.Text = "Cerrar Sesión";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormNuevaFactura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 661);
            Controls.Add(panelMain);
            Name = "FormNuevaFactura";
            Text = "Facturación";
            Load += FormNuevaFactura_Load;
            panelMain.ResumeLayout(false);
            panelNuevaFactura.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelResumen.ResumeLayout(false);
            panelResumen.PerformLayout();
            gbFactura.ResumeLayout(false);
            gbFactura.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Panel panelNuevaFactura;
        private Panel panelInferior;
        private GroupBox gbFactura;
        private Button btnGenerarFactura;
        private DateTimePicker dtFechaEmision;
        private ComboBox cmbEstado;
        private Label label7;
        private Label label6;
        private TextBox txtDireccion;
        private TextBox txtEmail;
        private Label label5;
        private Label label4;
        private Label label3;
        private TextBox txtRUC;
        private Label label2;
        private TextBox txtNombreCliente;
        private Button btnAgregarCliente;
        private Label label1;
        private ComboBox cmbCliente;
        private Panel panelResumen;
        private Button btnFacturasEmitidas;
        private CheckBox chbIGV;
        private Label label8;
        private Label lbTotal;
        private Label label16;
        private Label lbImpuesto;
        private Label lbSubtotal;
        private Label label13;
        private Label label12;
        private Label label14;
        private Label label11;
        private Panel panelTop;
        private Label label9;
        private Button btnAgregarProducto;
        private DataGridView dgvItems;
        private Button button1;
    }
}
