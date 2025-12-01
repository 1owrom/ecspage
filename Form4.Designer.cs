namespace ecspage
{
    partial class FormRegistrarProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistrarProductos));
            panelRegistrarProductos = new Panel();
            btnLimpiar = new Button();
            dgvListarProductos = new DataGridView();
            btnRegistrarProducto = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtStockDisponible = new TextBox();
            txtPrecioUnitario = new TextBox();
            txtProducto = new TextBox();
            panelRegistrarProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvListarProductos).BeginInit();
            SuspendLayout();
            // 
            // panelRegistrarProductos
            // 
            panelRegistrarProductos.BackColor = SystemColors.ControlLightLight;
            panelRegistrarProductos.Controls.Add(btnLimpiar);
            panelRegistrarProductos.Controls.Add(dgvListarProductos);
            panelRegistrarProductos.Controls.Add(btnRegistrarProducto);
            panelRegistrarProductos.Controls.Add(label4);
            panelRegistrarProductos.Controls.Add(label3);
            panelRegistrarProductos.Controls.Add(label2);
            panelRegistrarProductos.Controls.Add(label1);
            panelRegistrarProductos.Controls.Add(txtStockDisponible);
            panelRegistrarProductos.Controls.Add(txtPrecioUnitario);
            panelRegistrarProductos.Controls.Add(txtProducto);
            panelRegistrarProductos.Location = new Point(12, 12);
            panelRegistrarProductos.Name = "panelRegistrarProductos";
            panelRegistrarProductos.Size = new Size(780, 287);
            panelRegistrarProductos.TabIndex = 0;
            // 
            // btnLimpiar
            // 
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Popup;
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(211, 196);
            btnLimpiar.Margin = new Padding(0);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(30, 30);
            btnLimpiar.TabIndex = 15;
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvListarProductos
            // 
            dgvListarProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListarProductos.Dock = DockStyle.Right;
            dgvListarProductos.Location = new Point(290, 0);
            dgvListarProductos.Name = "dgvListarProductos";
            dgvListarProductos.Size = new Size(490, 287);
            dgvListarProductos.TabIndex = 14;
            dgvListarProductos.CellContentClick += dgvListarProductos_CellContentClick;
            dgvListarProductos.CellDoubleClick += dgvListarProductos_CellDoubleClick;
            // 
            // btnRegistrarProducto
            // 
            btnRegistrarProducto.Location = new Point(99, 196);
            btnRegistrarProducto.Name = "btnRegistrarProducto";
            btnRegistrarProducto.Size = new Size(75, 30);
            btnRegistrarProducto.TabIndex = 13;
            btnRegistrarProducto.Text = "Registrar";
            btnRegistrarProducto.UseVisualStyleBackColor = true;
            btnRegistrarProducto.Click += btnRegistrarProducto_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            label4.Location = new Point(48, 17);
            label4.Name = "label4";
            label4.Size = new Size(202, 25);
            label4.TabIndex = 12;
            label4.Text = "AGREGAR PRODUCTO";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 146);
            label3.Name = "label3";
            label3.Size = new Size(97, 15);
            label3.TabIndex = 11;
            label3.Text = "Stock disponible:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 102);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 10;
            label2.Text = "Precio unitario:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 61);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 9;
            label1.Text = "Producto:";
            // 
            // txtStockDisponible
            // 
            txtStockDisponible.Location = new Point(153, 143);
            txtStockDisponible.Name = "txtStockDisponible";
            txtStockDisponible.Size = new Size(116, 23);
            txtStockDisponible.TabIndex = 8;
            // 
            // txtPrecioUnitario
            // 
            txtPrecioUnitario.Location = new Point(153, 99);
            txtPrecioUnitario.Name = "txtPrecioUnitario";
            txtPrecioUnitario.Size = new Size(116, 23);
            txtPrecioUnitario.TabIndex = 7;
            // 
            // txtProducto
            // 
            txtProducto.Location = new Point(99, 58);
            txtProducto.Name = "txtProducto";
            txtProducto.Size = new Size(170, 23);
            txtProducto.TabIndex = 6;
            // 
            // FormRegistrarProductos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 311);
            Controls.Add(panelRegistrarProductos);
            Name = "FormRegistrarProductos";
            Text = "Registro de productos";
            Load += FormRegistrarProductos_Load;
            panelRegistrarProductos.ResumeLayout(false);
            panelRegistrarProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvListarProductos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelRegistrarProductos;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtStockDisponible;
        private TextBox txtPrecioUnitario;
        private TextBox txtProducto;
        private Button btnRegistrarProducto;
        private DataGridView dgvListarProductos;
        private Button btnLimpiar;
    }
}