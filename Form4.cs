using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ecspage.Bootstrap;
using ecspage.Application.Contracts;
using ecspage.Application.Services;


namespace ecspage
{
    public partial class FormRegistrarProductos : System.Windows.Forms.Form
    {
        private readonly IProductoService _productoService;
        private DataTable _dtProductos = new DataTable();
        private int? _productoEditandoId = null;

        public FormRegistrarProductos()
        {
            InitializeComponent();
            _productoService = AppHost.Get<IProductoService>();
        }

        private void FormRegistrarProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }
        private void CargarProductos()
        {
            try
            {
                var prods = _productoService.ListarActivos();

                _dtProductos = new DataTable();
                _dtProductos.Columns.Add("IdProducto", typeof(int));
                _dtProductos.Columns.Add("Nombre", typeof(string));
                _dtProductos.Columns.Add("PrecioUnitario", typeof(decimal));
                _dtProductos.Columns.Add("Stock", typeof(int));

                foreach (var p in prods)
                    _dtProductos.Rows.Add(p.Id, p.Nombre, p.Precio, p.Stock);

                dgvListarProductos.Columns.Clear();
                dgvListarProductos.AutoGenerateColumns = false;

                // Nombre
                dgvListarProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colNombre",
                    HeaderText = "Producto",
                    DataPropertyName = "Nombre",
                    Width = 200,
                    ReadOnly = true
                });

                // Precio
                dgvListarProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colPrecio",
                    HeaderText = "Precio",
                    DataPropertyName = "PrecioUnitario",
                    Width = 80,
                    ReadOnly = true
                });

                // Stock
                dgvListarProductos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colStock",
                    HeaderText = "Stock",
                    DataPropertyName = "Stock",
                    Width = 80,
                    ReadOnly = true
                });

                // Botón Eliminar (lo usaremos luego)
                dgvListarProductos.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "colEliminar",
                    HeaderText = "Eliminar",
                    Text = "X",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                });

                dgvListarProductos.DataSource = _dtProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los productos:\n" + ex.Message,
                    "Error al cargar productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarProducto_Click(object sender, EventArgs e)
        {
            var nombre = txtProducto.Text.Trim();

            if (!decimal.TryParse(txtPrecioUnitario.Text, out var precio))
            {
                MessageBox.Show("Por favor ingresa un valor válido", "Precio inválido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!int.TryParse(txtStockDisponible.Text, out var stock))
            {
                MessageBox.Show("Por favor ingresa un stock válido", "Stock inválido.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Result result;

            if (_productoEditandoId == null)
            {
                result = _productoService.Crear(nombre, precio, stock, out var nuevoId);
            }
            else
            {
                result = _productoService.Actualizar(
                    _productoEditandoId.Value,
                    nombre,
                    precio,
                    stock,
                    true
                );
            }

            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            string mensaje = _productoEditandoId == null
            ? "Producto registrado correctamente."
            : "Producto modificado correctamente.";

            MessageBox.Show(mensaje, "Operación exitosa",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);


            txtProducto.Clear();
            txtPrecioUnitario.Clear();
            txtStockDisponible.Clear();
            txtProducto.Focus();
            _productoEditandoId = null;
            btnRegistrarProducto.Text = "Registrar";

            CargarProductos();
            if (this.Owner is FormNuevaFactura frmPrincipal)
                frmPrincipal.RefrescarProductosEnFactura();
        }

        private void dgvListarProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvListarProductos.Columns[e.ColumnIndex].Name != "colEliminar") return;

            var rowView = dgvListarProductos.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (rowView == null) return;

            var row = rowView.Row;
            int id = (int)row["IdProducto"];
            string nombre = row["Nombre"].ToString() ?? "";

            var confirm = MessageBox.Show(
                $"¿Seguro que deseas eliminar el producto: {nombre} ?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            var result = _productoService.Eliminar(id);

            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Producto eliminado correctamente.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarProductos();

            if (this.Owner is FormNuevaFactura frmPrincipal)
            {
                frmPrincipal.RefrescarProductosEnFactura();
            }
        }

        private void dgvListarProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var rowView = dgvListarProductos.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (rowView == null) return;

            var row = rowView.Row;

            _productoEditandoId = (int)row["IdProducto"];
            txtProducto.Text = row["Nombre"].ToString() ?? "";
            txtPrecioUnitario.Text = row["PrecioUnitario"].ToString();
            txtStockDisponible.Text = row["Stock"].ToString();

            btnRegistrarProducto.Text = "Modificar";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtProducto.Clear();
            txtPrecioUnitario.Clear();
            txtStockDisponible.Clear();
            txtProducto.Focus();

            btnRegistrarProducto.Text = "Registrar";
        }
    }
}
