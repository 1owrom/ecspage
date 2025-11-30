using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using ecspage.Bootstrap;
using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;

namespace ecspage
{
    public partial class FormListadoFacturas : System.Windows.Forms.Form
    {
        private readonly FormNuevaFactura _formMain; // puede ser null si se abre standalone

        // Servicios (resueltos por DI)
        private IFacturaService _facturas;
        private IInvoiceExporter _exporter;
        private IClienteRepository _clientes;

        // ------------ Constructores ------------
        public FormListadoFacturas()
        {
            InitializeComponent();
            Load += FormListadoFacturas_Load;
        }
        public FormListadoFacturas(FormNuevaFactura principal) : this()
        {
            _formMain = principal;
        }

        // ================================== LOAD ==================================
        private void FormListadoFacturas_Load(object sender, EventArgs e)
        {
            try
            {
                // Resolver dependencias
                _facturas = AppHost.Get<IFacturaService>();
                _exporter = AppHost.Get<IInvoiceExporter>();
                _clientes = AppHost.Get<IClienteRepository>();

                // Poblar combos
                CargarClientes();
                CargarEstados();

                // Rango por defecto: último mes
                dtDesde.Value = DateTime.Today.AddMonths(-1);
                dtHasta.Value = DateTime.Today;

                // al final de FormListadoFacturas_Load
                Refrescar(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar el listado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========================= Navegación =========================
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            if (_formMain != null)
                _formMain.VolverDesdeListado();   // vuelve al Form1 embebido
            else
                this.Close();                     // si se abrió como ventana independiente
        }

        private void btnDetalleFactura_Click(object sender, EventArgs e)
        {
            if (dgvListarFacturas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una factura para ver sus detalles",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Opción A: por celda (si la columna existe)
            if (!dgvListarFacturas.Columns.Contains("IdFactura"))
            {
                MessageBox.Show("No se encuentra la columna IdFactura en el listado. Refresca la vista.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cellVal = dgvListarFacturas.CurrentRow.Cells["IdFactura"].Value;
            if (cellVal == null || cellVal == DBNull.Value)
            {
                MessageBox.Show("No se pudo obtener el Id de la factura seleccionada.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idFactura = Convert.ToInt32(cellVal);

            using var frm = new FormDetallesFactura(idFactura);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
        }

        // ========================= Carga de combos =========================
        private void CargarClientes()
        {
            try
            {
                var list = _clientes.Listar(); // (Id, Nombre, Ruc, Email, Direccion)

                var dt = new DataTable();
                dt.Columns.Add("IdCliente", typeof(int));
                dt.Columns.Add("Nombre", typeof(string));

                // Fila "Todos"
                dt.Rows.Add(0, "Todos");

                foreach (var c in list)
                    dt.Rows.Add(c.Id, c.Nombre);

                cmbFiltrarCliente.DataSource = dt;
                cmbFiltrarCliente.DisplayMember = "Nombre";
                cmbFiltrarCliente.ValueMember = "IdCliente";
                cmbFiltrarCliente.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los clientes: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEstados()
        {
            cmbFiltrarEstado.Items.Clear();
            cmbFiltrarEstado.Items.Add("Pendiente");
            cmbFiltrarEstado.Items.Add("Pagada");
            cmbFiltrarEstado.Items.Add("Anulada");
            cmbFiltrarEstado.SelectedIndex = -1; // sin filtro
        }

        // ========================= Listado / Filtros =========================
        private void Refrescar(bool mostrarTodo)
        {
            try
            {
                int? cliId = null;
                string? estado = null;
                DateTime? desde = null;
                DateTime? hasta = null;

                if (!mostrarTodo)
                {
                    cliId = (cmbFiltrarCliente.SelectedValue is int v && v > 0) ? v : (int?)null;
                    estado = string.IsNullOrWhiteSpace(cmbFiltrarEstado.Text) ? null : cmbFiltrarEstado.Text;
                    desde = dtDesde.Value.Date;
                    hasta = dtHasta.Value.Date;
                }

                var filtro = new FiltroFacturas(cliId, estado, desde, hasta);
                var lista = _facturas.Listar(filtro);

                var dt = new DataTable();
                dt.Columns.Add("IdFactura", typeof(int));
                dt.Columns.Add("Cliente", typeof(string));
                dt.Columns.Add("Estado", typeof(string));
                dt.Columns.Add("FechaEmision", typeof(DateTime));
                dt.Columns.Add("Subtotal", typeof(decimal));
                dt.Columns.Add("Impuesto", typeof(decimal));
                dt.Columns.Add("Total", typeof(decimal));

                foreach (var f in lista)
                    dt.Rows.Add(f.Id, f.ClienteNombre, f.Estado, f.Fecha, f.Subtotal, f.Impuesto, f.Total);

                dgvListarFacturas.AutoGenerateColumns = true;
                dgvListarFacturas.DataSource = dt;
                dgvListarFacturas.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el listado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                cmbFiltrarCliente.SelectedValue = 0;
                cmbFiltrarEstado.SelectedIndex = -1;
                dtDesde.Value = DateTime.Today.AddMonths(-1);
                dtHasta.Value = DateTime.Today;
                Refrescar(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo limpiar filtros: " + ex.Message,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnFiltrar_Click(object sender, EventArgs e) => Refrescar(false);

        // ========================= Exportar PDF =========================
        private void btnExportarPDFMasivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListarFacturas.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una factura para exportar",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idFactura = Convert.ToInt32(dgvListarFacturas.CurrentRow.Cells["IdFactura"].Value);

                using var sfd = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Factura_{idFactura}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var r = _exporter.ExportToPdf(idFactura, sfd.FileName);
                    MessageBox.Show(r.Message, r.Success ? "Éxito" : "Error",
                        MessageBoxButtons.OK,
                        r.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar PDF: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========================= Anular =========================
        private void btnAnularFacturaMasivo_Click(object sender, EventArgs e)
        {
            if (dgvListarFacturas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una factura para anular.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idFactura = Convert.ToInt32(dgvListarFacturas.CurrentRow.Cells["IdFactura"].Value);
            string estado = dgvListarFacturas.CurrentRow.Cells["Estado"].Value?.ToString() ?? "";

            if (estado == "Anulada")
            {
                MessageBox.Show("La factura ya está anulada.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("¿Está seguro que desea anular esta factura?",
                "Confirmar Anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var r = _facturas.AnularFactura(idFactura);
                MessageBox.Show(r.Message, r.Success ? "Éxito" : "Error",
                    MessageBoxButtons.OK,
                    r.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (r.Success) Refrescar(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al anular la factura: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarFactura_Click_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1) Validar selección
            if (dgvListarFacturas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una factura para editar.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!dgvListarFacturas.Columns.Contains("IdFactura"))
            {
                MessageBox.Show("No se encuentra la columna IdFactura.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Obtener Id
            int idFactura = Convert.ToInt32(
                dgvListarFacturas.CurrentRow.Cells["IdFactura"].Value
            );

            // 3) Abrir el formulario de edición
            using (var frm = new FormEditarFactura(idFactura))
            {
                frm.StartPosition = FormStartPosition.CenterParent;

                // 4) Si el usuario guardó
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    Refrescar(true); // tu método ya listo
                }
            }
        }
    }
}
