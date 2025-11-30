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
            var lista = _clientes.Listar();
            cmbCliente.DataSource = lista;
            cmbCliente.DisplayMember = "Nombre";
            cmbCliente.ValueMember = "IdCliente";
            cmbCliente.SelectedValue = factura.IdCliente;

            // 👉 Cargar estados
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("Pagada");
            cmbEstado.Items.Add("Anulada");
            cmbEstado.Text = factura.Estado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int nuevoCliente = Convert.ToInt32(cmbCliente.SelectedValue);
            string nuevoEstado = cmbEstado.Text;

            var r = _facturas.EditarFactura(_idFactura, nuevoCliente, nuevoEstado);

            MessageBox.Show(r.Message,
                r.Success ? "Éxito" : "Error",
                MessageBoxButtons.OK,
                r.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (r.Success)
                this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
