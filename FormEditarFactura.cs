using ecspage.Application.Contracts;
using ecspage.Bootstrap;
using ecspage.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Ajustes inciales de fomulario de edicion de factura

namespace ecspage
{
   
    public partial class FormEditarFactura : Form
    {
        private readonly IFacturaService _facturas;
        private readonly IClienteRepository _clientes;
        private readonly int _idFactura;

        public FormEditarFactura(int idFactura)
        {
            InitializeComponent();

            _facturas = AppHost.Get<IFacturaService>();
            _clientes = AppHost.Get<IClienteRepository>();
            _idFactura = idFactura;

            Load += FormEditarFactura_Load;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }
        //// Configuración completa del formulario de edición carga del estados
        private void FormEditarFactura_Load(object sender, EventArgs e)
        {
            var factura = _facturas.ObtenerFactura(_idFactura);

            if (factura == null)
            {
                MessageBox.Show("Factura no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // 👉 Cargar clientes
            var listaTuplas = _clientes.Listar();

            var lista = listaTuplas
                .Select(c => new ClienteItem { Id = c.Id, Nombre = c.Nombre })
                .ToList();

            cmbCliente.DataSource = lista;
            cmbCliente.DisplayMember = "Nombre";
            cmbCliente.ValueMember = "Id";
            cmbCliente.SelectedValue = factura.IdCliente;

            // 👉 Cargar estados
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("Pagada");
            cmbEstado.Items.Add("Anulada");
            cmbEstado.Text = factura.Estado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //  Lógica final para guardar los cambios del estado
            // 🟦 Validar selección del cliente
            if (cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🟦 Obtener datos del formulario
            int nuevoCliente = (int)cmbCliente.SelectedValue;
            string nuevoEstado = cmbEstado.Text.Trim();

            if (string.IsNullOrEmpty(nuevoEstado))
            {
                MessageBox.Show("Seleccione un estado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🟩 Ejecutar la edición
            var r = _facturas.EditarFactura(_idFactura, nuevoCliente, nuevoEstado);

            // 🟦 Mostrar mensaje
            MessageBox.Show(
                r.Message,
                r.Success ? "Éxito" : "Error",
                MessageBoxButtons.OK,
                r.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error
            );

            // 🟩 Si todo ok, cerrar el formulario
            if (r.Success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
