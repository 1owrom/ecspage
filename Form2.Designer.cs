namespace ecspage
{
    partial class FormListadoFacturas
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
            panelFiltros = new Panel();
            btnlimpiar = new Button();
            btnFiltrar = new Button();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            cmbFiltrarCliente = new ComboBox();
            cmbFiltrarEstado = new ComboBox();
            dtHasta = new DateTimePicker();
            dtDesde = new DateTimePicker();
            panelAcciones = new Panel();
            btnEditarFactura = new Button();
            btnRegresar = new Button();
            btnExportarPDFMasivo = new Button();
            btnAnularFacturaMasivo = new Button();
            btnDetalleFactura = new Button();
            dgvListarFacturas = new DataGridView();
            panelFiltros.SuspendLayout();
            panelAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvListarFacturas).BeginInit();
            SuspendLayout();
            // 
            // panelFiltros
            // 
            panelFiltros.Controls.Add(btnlimpiar);
            panelFiltros.Controls.Add(btnFiltrar);
            panelFiltros.Controls.Add(label6);
            panelFiltros.Controls.Add(label5);
            panelFiltros.Controls.Add(label4);
            panelFiltros.Controls.Add(label2);
            panelFiltros.Controls.Add(label1);
            panelFiltros.Controls.Add(cmbFiltrarCliente);
            panelFiltros.Controls.Add(cmbFiltrarEstado);
            panelFiltros.Controls.Add(dtHasta);
            panelFiltros.Controls.Add(dtDesde);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(0, 0);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(1084, 54);
            panelFiltros.TabIndex = 0;
            // 
            // btnlimpiar
            // 
            btnlimpiar.Location = new Point(997, 14);
            btnlimpiar.Name = "btnlimpiar";
            btnlimpiar.Size = new Size(75, 23);
            btnlimpiar.TabIndex = 12;
            btnlimpiar.Text = "Limpiar";
            btnlimpiar.UseVisualStyleBackColor = true;
            btnlimpiar.Click += btnlimpiar_Click;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(916, 14);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(75, 23);
            btnFiltrar.TabIndex = 11;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = true;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(736, 18);
            label6.Name = "label6";
            label6.Size = new Size(35, 15);
            label6.TabIndex = 10;
            label6.Text = "hasta";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(593, 17);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 9;
            label5.Text = "Desde";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(543, 16);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 8;
            label4.Text = "Fecha:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(371, 16);
            label2.Name = "label2";
            label2.Size = new Size(51, 17);
            label2.TabIndex = 6;
            label2.Text = "Estado:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(30, 15);
            label1.Name = "label1";
            label1.Size = new Size(50, 17);
            label1.TabIndex = 5;
            label1.Text = "Cliente:";
            // 
            // cmbFiltrarCliente
            // 
            cmbFiltrarCliente.FormattingEnabled = true;
            cmbFiltrarCliente.Location = new Point(86, 15);
            cmbFiltrarCliente.Name = "cmbFiltrarCliente";
            cmbFiltrarCliente.Size = new Size(270, 23);
            cmbFiltrarCliente.TabIndex = 3;
            // 
            // cmbFiltrarEstado
            // 
            cmbFiltrarEstado.FormattingEnabled = true;
            cmbFiltrarEstado.Location = new Point(428, 15);
            cmbFiltrarEstado.Name = "cmbFiltrarEstado";
            cmbFiltrarEstado.Size = new Size(100, 23);
            cmbFiltrarEstado.TabIndex = 2;
            // 
            // dtHasta
            // 
            dtHasta.Location = new Point(777, 13);
            dtHasta.Name = "dtHasta";
            dtHasta.Size = new Size(92, 23);
            dtHasta.TabIndex = 1;
            // 
            // dtDesde
            // 
            dtDesde.Location = new Point(638, 13);
            dtDesde.Name = "dtDesde";
            dtDesde.Size = new Size(92, 23);
            dtDesde.TabIndex = 0;
            // 
            // panelAcciones
            // 
            panelAcciones.Controls.Add(btnEditarFactura);
            panelAcciones.Controls.Add(btnRegresar);
            panelAcciones.Controls.Add(btnExportarPDFMasivo);
            panelAcciones.Controls.Add(btnAnularFacturaMasivo);
            panelAcciones.Controls.Add(btnDetalleFactura);
            panelAcciones.Dock = DockStyle.Bottom;
            panelAcciones.Location = new Point(0, 612);
            panelAcciones.Name = "panelAcciones";
            panelAcciones.Size = new Size(1084, 49);
            panelAcciones.TabIndex = 1;
            // 
            // btnEditarFactura
            // 
            btnEditarFactura.Location = new Point(299, 14);
            btnEditarFactura.Name = "btnEditarFactura";
            btnEditarFactura.Size = new Size(75, 23);
            btnEditarFactura.TabIndex = 4;
            btnEditarFactura.Text = "Editar";
            btnEditarFactura.UseVisualStyleBackColor = true;
            btnEditarFactura.Click += btnEditarFactura_Click;
            // 
            // btnRegresar
            // 
            btnRegresar.Location = new Point(997, 14);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(75, 23);
            btnRegresar.TabIndex = 3;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = true;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // btnExportarPDFMasivo
            // 
            btnExportarPDFMasivo.Location = new Point(174, 14);
            btnExportarPDFMasivo.Name = "btnExportarPDFMasivo";
            btnExportarPDFMasivo.Size = new Size(96, 23);
            btnExportarPDFMasivo.TabIndex = 2;
            btnExportarPDFMasivo.Text = "Exportar PDF";
            btnExportarPDFMasivo.UseVisualStyleBackColor = true;
            btnExportarPDFMasivo.Click += btnExportarPDFMasivo_Click;
            // 
            // btnAnularFacturaMasivo
            // 
            btnAnularFacturaMasivo.Location = new Point(93, 14);
            btnAnularFacturaMasivo.Name = "btnAnularFacturaMasivo";
            btnAnularFacturaMasivo.Size = new Size(75, 23);
            btnAnularFacturaMasivo.TabIndex = 1;
            btnAnularFacturaMasivo.Text = "Anular";
            btnAnularFacturaMasivo.UseVisualStyleBackColor = true;
            btnAnularFacturaMasivo.Click += btnAnularFacturaMasivo_Click;
            // 
            // btnDetalleFactura
            // 
            btnDetalleFactura.Location = new Point(12, 14);
            btnDetalleFactura.Name = "btnDetalleFactura";
            btnDetalleFactura.Size = new Size(75, 23);
            btnDetalleFactura.TabIndex = 0;
            btnDetalleFactura.Text = "Detalles";
            btnDetalleFactura.UseVisualStyleBackColor = true;
            btnDetalleFactura.Click += btnDetalleFactura_Click;
            // 
            // dgvListarFacturas
            // 
            dgvListarFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListarFacturas.Dock = DockStyle.Fill;
            dgvListarFacturas.Location = new Point(0, 54);
            dgvListarFacturas.Name = "dgvListarFacturas";
            dgvListarFacturas.Size = new Size(1084, 558);
            dgvListarFacturas.TabIndex = 2;
            // 
            // FormListadoFacturas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 661);
            Controls.Add(dgvListarFacturas);
            Controls.Add(panelAcciones);
            Controls.Add(panelFiltros);
            Name = "FormListadoFacturas";
            Text = "Ver Facturas";
            Load += FormListadoFacturas_Load;
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            panelAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvListarFacturas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFiltros;
        private Label label1;
        private ComboBox cmbFiltrarCliente;
        private ComboBox cmbFiltrarEstado;
        private DateTimePicker dtHasta;
        private DateTimePicker dtDesde;
        private Label label2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Panel panelAcciones;
        private DataGridView dgvListarFacturas;
        private Button btnRegresar;
        private Button btnExportarPDFMasivo;
        private Button btnAnularFacturaMasivo;
        private Button btnDetalleFactura;
        private Button btnlimpiar;
        private Button btnFiltrar;
        private Button btnEditarFactura;
    }
}