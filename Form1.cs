using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using ecspage.Bootstrap;
using ecspage.Application.Contracts;
using ecspage.Infrastructure.Abstractions;

namespace ecspage
{
    public partial class FormNuevaFactura : System.Windows.Forms.Form
    {
        // ========= MODELO PARA EL GRID =========cambio de prueba
        private sealed class ItemFactura
        {
            public int? ProductoId { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
            public int Stock { get; set; }
        }

        // ========= BINDINGS / CACHES =========
        private readonly BindingList<ItemFactura> _items = new();
        private readonly BindingSource _bsItems = new();
        private DataTable _dtClientes = new();
        private DataTable _dtProductos = new();

        private const int ID_OCASIONAL = 0;

        // ========= SERVICIOS =========
        private IFacturaService _facturas;
        private IProductoService _productos;
        private ITotalesCalculator _calc;
        private IClienteRepository _clienteService; // solo para poblar combos

        // Snapshot de la vista principal dentro de panelMain
        private Control[] _homeViewSnapshot = Array.Empty<Control>();
        private bool _homeViewCaptured = false;

        public FormNuevaFactura()
        {
            InitializeComponent();
            Load += FormNuevaFactura_Load;
        }

        // ================================== LOAD ==================================  //prueba
        private void FormNuevaFactura_Load(object sender, EventArgs e)
        {
            try
            {
                // Resolver dependencias
                _facturas = AppHost.Get<IFacturaService>();
                _productos = AppHost.Get<IProductoService>();
                _calc = AppHost.Get<ITotalesCalculator>();
                _clienteService = AppHost.Get<IClienteRepository>();

                ConfigurarUIEstaticas();
                ConfigurarGridItems();

                CargarClientes();
                CargarProductos();

                RecalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un problema al iniciar el formulario:\n" + ex.Message,
                    "Error de inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Generar Factura
            btnGenerarFactura.Click -= btnGenerarFactura_Click;
            btnGenerarFactura.Click += btnGenerarFactura_Click;

            // Facturas emitidas
            btnFacturasEmitidas.Click -= btnFacturasEmitidas_Click;
            btnFacturasEmitidas.Click += btnFacturasEmitidas_Click;

            // Capturar la vista inicial del panel para poder restaurarla al volver del listado
            if (!_homeViewCaptured)
            {
                _homeViewSnapshot = panelMain.Controls.Cast<Control>().ToArray();
                _homeViewCaptured = true;
            }
        }

        private void ConfigurarUIEstaticas()
        {
            // Estados
            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new object[] { "Pendiente", "Pagada" });
            cmbEstado.SelectedIndex = 0;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            // Fecha
            if (dtFechaEmision != null)
                dtFechaEmision.Value = DateTime.Now;

            // IGV
            chbIGV.CheckedChanged -= chbIGV_CheckedChanged;
            chbIGV.CheckedChanged += chbIGV_CheckedChanged;
        }

        // ============================= CLIENTES =============================
        private void CargarClientes()
        {
            try
            {
                var lista = _clienteService.Listar(); // (Id,Nombre,Ruc,Email,Direccion)

                _dtClientes = new DataTable();
                _dtClientes.Columns.Add("IdCliente", typeof(int));
                _dtClientes.Columns.Add("Nombre", typeof(string));
                _dtClientes.Columns.Add("RucDni", typeof(string));
                _dtClientes.Columns.Add("Email", typeof(string));
                _dtClientes.Columns.Add("Direccion", typeof(string));
                _dtClientes.Columns.Add("NombreCompleto", typeof(string));

                // Fila “Seleccionar…”
                _dtClientes.Rows.Add(ID_OCASIONAL, "Seleccionar Cliente", "", "", "", "Seleccionar Cliente");

                foreach (var c in lista)
                {
                    _dtClientes.Rows.Add(
                        c.Id,
                        c.Nombre,
                        c.Ruc ?? "",
                        c.Email ?? "",
                        c.Direccion ?? "",
                        $"{c.Nombre} - {c.Ruc}"
                    );
                }

                cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbCliente.DisplayMember = "NombreCompleto";
                cmbCliente.ValueMember = "IdCliente";
                cmbCliente.DataSource = _dtClientes;

                cmbCliente.SelectedIndexChanged -= cmbCliente_SelectedIndexChanged;
                cmbCliente.SelectedIndexChanged += cmbCliente_SelectedIndexChanged;
                cmbCliente.SelectedValue = ID_OCASIONAL;

                // Limpia campos
                ActualizarCamposCliente();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los clientes:\n" + ex.Message,
                    "Error al cargar clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Solo se puede agregar si el combo está en "Seleccionar Cliente"
                if (cmbCliente.SelectedValue is int idSel && idSel != ID_OCASIONAL)
                {
                    MessageBox.Show(
                        "Para agregar un nuevo cliente, primero selecciona la opción \"Seleccionar Cliente\".",
                        "Acción no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    cmbCliente.SelectedValue = ID_OCASIONAL;
                    return;
                }

                // 2) Captura y saneo
                var nombre = txtNombreCliente.Text.Trim();
                var ruc = txtRUC.Text.Trim();
                var email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                var dir = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim();

                // 3) Validaciones de campos
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("Ingrese el nombre del cliente.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreCliente.Focus();
                    return;
                }

                if (!EsRucDniValido(ruc))
                {
                    MessageBox.Show("RUC/DNI inválido. Debe tener 8 (DNI) o 11 (RUC) dígitos numéricos.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRUC.Focus();
                    return;
                }

                // 4) No duplicar por RUC/DNI (validación en memoria)
                bool existeEnCache = _dtClientes.AsEnumerable()
                    .Any(r => string.Equals(r.Field<string>("RucDni") ?? "",
                                            ruc,
                                            StringComparison.OrdinalIgnoreCase));

                if (existeEnCache)
                {
                    MessageBox.Show("Ya existe un cliente con ese RUC/DNI.",
                        "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 5) Crear en BD (si en BD hay UNIQUE también nos protege)
                var nuevoId = _clienteService.Crear(nombre, ruc, email, dir);

                // 6) Agregar al DataTable del combo sin perder la fila "Seleccionar Cliente"
                _dtClientes.Rows.Add(nuevoId, nombre, ruc, email ?? "", dir ?? "", $"{nombre} - {ruc}");
                _dtClientes.AcceptChanges();

                // 7) Seleccionar al nuevo cliente
                cmbCliente.SelectedValue = nuevoId;

                MessageBox.Show("Cliente agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex) // por UNIQUE en BD (RUC/DNI duplicado)
            {
                MessageBox.Show(ex.Message, "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar el cliente:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper: valida 8 u 11 dígitos numéricos
        private static bool EsRucDniValido(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return false;
            if (!valor.All(char.IsDigit)) return false;
            return valor.Length == 8 || valor.Length == 11;
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e) => ActualizarCamposCliente();

        private void ActualizarCamposCliente()
        {
            try
            {
                if (cmbCliente.SelectedValue is not int id || id == ID_OCASIONAL)
                {
                    txtNombreCliente.Text = "";
                    txtRUC.Text = "";
                    txtEmail.Text = "";
                    txtDireccion.Text = "";
                    return;
                }

                var row = _dtClientes.AsEnumerable().FirstOrDefault(r => r.Field<int>("IdCliente") == id);
                if (row == null) return;

                txtNombreCliente.Text = row.Field<string>("Nombre") ?? "";
                txtRUC.Text = row.Field<string>("RucDni") ?? "";
                txtEmail.Text = row.Field<string>("Email") ?? "";
                txtDireccion.Text = row.Field<string>("Direccion") ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo actualizar la información del cliente:\n" + ex.Message,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ============================= PRODUCTOS / GRID =============================
        private void ConfigurarGridItems()
        {
            dgvItems.AutoGenerateColumns = false;

            _bsItems.DataSource = _items;
            dgvItems.DataSource = _bsItems;

            dgvItems.CurrentCellDirtyStateChanged -= DgvItems_CurrentCellDirtyStateChanged;
            dgvItems.CurrentCellDirtyStateChanged += DgvItems_CurrentCellDirtyStateChanged;

            dgvItems.CellEndEdit -= dgvItems_CellEndEdit;
            dgvItems.CellEndEdit += dgvItems_CellEndEdit;

            dgvItems.RowsRemoved -= DgvItems_RowsRemoved;
            dgvItems.RowsRemoved += DgvItems_RowsRemoved;

            dgvItems.CellContentClick -= dgvItems_CellContentClick;
            dgvItems.CellContentClick += dgvItems_CellContentClick;

            dgvItems.EditingControlShowing -= dgvItems_EditingControlShowing;
            dgvItems.EditingControlShowing += dgvItems_EditingControlShowing;

            dgvItems.CellBeginEdit -= dgvItems_CellBeginEdit;
            dgvItems.CellBeginEdit += dgvItems_CellBeginEdit;
        }

        private void DgvItems_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (!dgvItems.IsCurrentCellDirty) return;
            if (dgvItems.CurrentCell is DataGridViewComboBoxCell or DataGridViewCheckBoxCell)
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DgvItems_RowsRemoved(object? sender, DataGridViewRowsRemovedEventArgs e)
        {
            RecalcularTotales();
        }

        private void dgvItems_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = dgvItems.Columns[e.ColumnIndex].Name;
            if (col == "colCantidad")
            {
                // Protege contra valores inválidos
                if (e.RowIndex >= _items.Count) return;
                var it = _items[e.RowIndex];

                if (it.Cantidad <= 0)
                {
                    it.Cantidad = 1;
                    _bsItems.ResetItem(e.RowIndex);
                }

                if (it.ProductoId.HasValue && it.Cantidad > it.Stock)
                {
                    MessageBox.Show(
                        $"La cantidad ingresada supera el stock disponible ({it.Stock}).",
                        "Stock insuficiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    it.Cantidad = it.Stock; // fuerza al máximo permitido
                    _bsItems.ResetItem(e.RowIndex);
                }
            }

            RecalcularTotales();
        }

        private void dgvItems_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            // Bloquea edición en Precio
            if (dgvItems.Columns[e.ColumnIndex].Name == "colPrecio")
                e.Cancel = true;
            // 2) Bloquea volver a abrir el combo de Producto
            if (dgvItems.Columns[e.ColumnIndex].Name == "colProducto")
            {
                if (e.RowIndex >= 0 && e.RowIndex < _items.Count)
                {
                    var item = _items[e.RowIndex];
                    // Si ya tiene producto seleccionado, no permitimos re-editar
                    if (item.ProductoId.HasValue)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void CargarProductos()
        {
            try
            {
                var prods = _productos.ListarActivos(); // (Id,Nombre,Precio,Stock)

                _dtProductos = new DataTable();
                _dtProductos.Columns.Add("IdProducto", typeof(int));
                _dtProductos.Columns.Add("Nombre", typeof(string));
                _dtProductos.Columns.Add("PrecioUnitario", typeof(decimal));
                _dtProductos.Columns.Add("Stock", typeof(int));

                foreach (var p in prods)
                    _dtProductos.Rows.Add(p.Id, p.Nombre, p.Precio, p.Stock);

                dgvItems.Columns.Clear();

                // Producto (Combo)
                var colProducto = new DataGridViewComboBoxColumn
                {
                    Name = "colProducto",
                    HeaderText = "Producto",
                    DataPropertyName = "ProductoId",
                    DataSource = _dtProductos,
                    DisplayMember = "Nombre",
                    ValueMember = "IdProducto",
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                    Width = 230
                };
                dgvItems.Columns.Add(colProducto);

                // Precio
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colPrecio",
                    HeaderText = "Precio",
                    DataPropertyName = "Precio",
                    Width = 80
                });

                // Cantidad
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colCantidad",
                    HeaderText = "Cantidad",
                    DataPropertyName = "Cantidad",
                    Width = 80
                });

                // Eliminar
                dgvItems.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "colEliminar",
                    HeaderText = "Eliminar",
                    Text = "X",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                });

                // Binding
                dgvItems.DataSource = _bsItems;
                dgvItems.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los productos:\n" + ex.Message,
                    "Error al cargar productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvItems.CurrentCell is DataGridViewComboBoxCell && e.Control is ComboBox cb)
            {
                cb.SelectedIndexChanged -= ComboProducto_Changed;
                cb.SelectedIndexChanged += ComboProducto_Changed;
            }
        }

        private void ComboProducto_Changed(object sender, EventArgs e)
        {
            try
            {
                if (sender is not ComboBox cb) return;
                if (cb.SelectedValue is null) return;
                if (!int.TryParse(cb.SelectedValue.ToString(), out var idSel)) return;

                var rowIndex = dgvItems.CurrentCell?.RowIndex ?? -1;
                if (rowIndex < 0) return;
                if (rowIndex >= _items.Count) _items.Add(new ItemFactura());

                var prod = _dtProductos.AsEnumerable().FirstOrDefault(r => r.Field<int>("IdProducto") == idSel);
                if (prod is null) return;

                var it = _items[rowIndex];
                it.ProductoId = idSel;
                it.Precio = prod.Field<decimal>("PrecioUnitario");
                it.Stock = prod.Field<int>("Stock");
                if (it.Cantidad <= 0) it.Cantidad = 1;

                _bsItems.ResetBindings(false);
                RecalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo actualizar el ítem:\n" + ex.Message,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvItems_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvItems.Columns[e.ColumnIndex].Name != "colEliminar") return;

            // Protección extra por si el evento se dispara durante edición
            if (e.RowIndex >= _items.Count) return;

            _items.RemoveAt(e.RowIndex);
            _bsItems.ResetBindings(false);
            RecalcularTotales();
        }

        // ============================= TOTALES =============================
        private void chbIGV_CheckedChanged(object sender, EventArgs e) => RecalcularTotales();

        private void RecalcularTotales()
        {
            try
            {
                var items = _items.Select(i => (Cantidad: (decimal)i.Cantidad, Precio: i.Precio));

                // Subtotal sin IGV por defecto
                decimal subtotal = items.Sum(x => x.Cantidad * x.Precio);
                decimal impuesto = 0m;
                decimal total = subtotal;

                if (chbIGV.Checked)
                {
                    var r = _calc.Calcular(items, precioIncluyeIGV: false);
                    subtotal = r.Subtotal;
                    impuesto = r.Impuesto;
                    total = r.Total;
                }

                lbSubtotal.Text = subtotal.ToString("0.00");
                lbImpuesto.Text = impuesto.ToString("0.00");
                lbTotal.Text = total.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron recalcular los totales:\n" + ex.Message,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // ============================= GUARDAR / GENERAR FACTURA =============================
        private void btnGenerarFactura_Click(object? sender, EventArgs e)
        {
            try
            {
                // 1) Cliente seleccionado
                if (cmbCliente.SelectedValue is not int idCliente || idCliente == 0)
                {
                    MessageBox.Show("Seleccione un cliente.", "Cliente inválido",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCliente.Focus();
                    return;
                }

                // 2) Ítems válidos (con producto y cantidad > 0)
                var itemsValidos = _items.Where(i => i.ProductoId.HasValue && i.Cantidad > 0).ToList();
                if (itemsValidos.Count == 0)
                {
                    MessageBox.Show("Agregue al menos un producto con cantidad mayor a 0.",
                        "Sin productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3) Evita superar stock (doble seguridad: UI + BD)
                foreach (var it in itemsValidos)
                {
                    // Si por alguna razón el stock del ítem no está seteado, lo completamos desde la tabla de productos
                    if (it.Stock <= 0 && it.ProductoId.HasValue)
                    {
                        var prow = _dtProductos.AsEnumerable()
                            .FirstOrDefault(r => r.Field<int>("IdProducto") == it.ProductoId.Value);
                        if (prow != null)
                            it.Stock = prow.Field<int>("Stock");
                    }

                    // Solo advertimos si conocemos el stock (> 0)
                    if (it.Stock > 0 && it.Cantidad > it.Stock)
                    {
                        MessageBox.Show(
                            $"La cantidad del producto seleccionado supera el stock disponible ({it.Stock}).",
                            "Stock insuficiente",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }
                }

                // 4) Valida stock contra BD (por si otro usuario vendió en medio)
                var detalleCmds = itemsValidos
                    .Select(i => new CrearFacturaDetalleCmd(i.ProductoId!.Value, i.Cantidad, i.Precio))
                    .ToList();

                var stock = _productos.VerificarStock(detalleCmds);
                if (!stock.Success)
                {
                    MessageBox.Show(stock.Message, "Stock insuficiente",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5) Arma comando y guarda
                var cmd = new CrearFacturaCommand(
                    ClienteId: idCliente,
                    FechaEmision: dtFechaEmision.Value.Date,
                    Estado: cmbEstado.Text,
                    PrecioIncluyeIGV: chbIGV.Checked ? false : false, // no usamos este flag en UI ahora
                    Detalles: detalleCmds
                );

                var r = _facturas.CrearFactura(cmd);

                // 6) Mensaje resultado
                MessageBox.Show(r.Message, r.Success ? "Éxito" : "Error",
                    MessageBoxButtons.OK,
                    r.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                // 7) Limpieza post-OK
                if (r.Success)
                {
                    _items.Clear();
                    RecalcularTotales();
                    cmbCliente.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar la factura:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ============================= NAVEGACIÓN (opcional) =============================
        private void btnFacturasEmitidas_Click(object? sender, EventArgs e)
        {
            try
            {
                // Garantiza que la vista home esté capturada
                if (!_homeViewCaptured)
                {
                    _homeViewSnapshot = panelMain.Controls.Cast<Control>().ToArray();
                    _homeViewCaptured = true;
                }

                var frm = new FormListadoFacturas(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };

                panelMain.Controls.Clear();
                panelMain.Controls.Add(frm);

                frm.Show();
                frm.GetType().GetMethod("Refrescar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                   .Invoke(frm, new object[] { true });

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el listado de facturas:\n" + ex.Message,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void VolverDesdeListado()
        {
            try
            {
                // Descarta cualquier Form embebido (por si quedó en el panel)
                foreach (Control c in panelMain.Controls.OfType<FormRegistrarProductos>().ToList())
                    c.Dispose();

                panelMain.SuspendLayout();
                panelMain.Controls.Clear();

                // Restaura la vista original del panel
                if (_homeViewSnapshot.Length > 0)
                {
                    // Clave: volver a agregar en el mismo orden
                    panelMain.Controls.AddRange(_homeViewSnapshot);
                }

                panelMain.ResumeLayout();

                // (Opcional) refresca datos/labels si corresponde
                RecalcularTotales();
                // Si al entrar al listado ocultaste algo fuera del panel, vuélvelo a mostrar aquí.
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo restaurar la vista principal:\n" + ex.Message,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //Nuevo cambio: Agregar productos en nuevo formulario
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            using (var frm = new FormRegistrarProductos())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        // Nuevo cambio: método público para actualizar productos
        public void RefrescarProductosEnFactura()
        {
            CargarProductos();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Form5 login = new Form5();
            login.Show();
            this.Close();   // Cierra esta ventana
        }
    }
}
