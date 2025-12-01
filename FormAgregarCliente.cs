using ecspage.Application.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ecspage
{
    public partial class FormAgregarCliente : Form
    {
        public FormAgregarCliente()
        {
            InitializeComponent();
        }

        private void FormAgregarCliente_Load(object sender, EventArgs e)
        {

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var nuevoCliente = new ClienteDTO()
            {
                Nombre = txtNombre.Text,
                Ruc = txtRuc.Text,
                Email = txtEmail.Text,
                Direccion = txtDireccion.Text
            };

            MessageBox.Show("Cliente creado correctamente");
        }
    }
}