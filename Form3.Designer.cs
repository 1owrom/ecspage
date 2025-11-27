namespace ecspage
{
    partial class FormDetallesFactura
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
            panelPreview = new Panel();
            btnRegresar = new Button();
            btnGuardarPDF = new Button();
            toolStrip = new ToolStrip();
            panelPreview.SuspendLayout();
            SuspendLayout();
            // 
            // panelPreview
            // 
            panelPreview.Controls.Add(btnRegresar);
            panelPreview.Controls.Add(btnGuardarPDF);
            panelPreview.Controls.Add(toolStrip);
            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Location = new Point(0, 0);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(1084, 661);
            panelPreview.TabIndex = 0;
            panelPreview.Visible = false;
            // 
            // btnRegresar
            // 
            btnRegresar.Location = new Point(997, 626);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(75, 23);
            btnRegresar.TabIndex = 4;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = true;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // btnGuardarPDF
            // 
            btnGuardarPDF.Location = new Point(12, 626);
            btnGuardarPDF.Name = "btnGuardarPDF";
            btnGuardarPDF.Size = new Size(83, 23);
            btnGuardarPDF.TabIndex = 1;
            btnGuardarPDF.Text = "Guardar PDF";
            btnGuardarPDF.UseVisualStyleBackColor = true;
            // 
            // toolStrip
            // 
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(1084, 25);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            // 
            // FormDetallesFactura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 661);
            Controls.Add(panelPreview);
            Name = "FormDetallesFactura";
            Text = "Detalles de la Factura";
            Load += FormDetallesFactura_Load;
            panelPreview.ResumeLayout(false);
            panelPreview.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelPreview;
        private ToolStrip toolStrip;
        private Button btnGuardarPDF;
        private Button btnRegresar;
    }
}