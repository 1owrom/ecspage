using ecspage.Application.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace ecspage
{
    public partial class FormAgregarCliente : Form
    {
        // Propiedad para devolver el cliente creado
        public ClienteDTO ClienteCreado { get; private set; }

        public FormAgregarCliente()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClienteCreado = new ClienteDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Ruc = txtRuc.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Direccion = txtDireccion.Text.Trim()
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // === DTO simple para enviar el cliente creado ===
    public class ClienteDTO
    {
        public int Id { get; set; } // en blanco, pero luego se llenará con la BD
        public string Nombre { get; set; }
        public string Ruc { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }
}
