using ecspage.Application.Services;
using ecspage.Infrastructure.Persistence;
using ecspage.Infrastructure.Repositories;
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
    public partial class Form5 : Form
    {
        private readonly AuthService _authService;

        public Form5()
        {
            InitializeComponent();

            var factory = new SqlConnectionFactoryAdapter();
            var repo = new UsuarioRepository(factory);
            _authService = new AuthService(repo);

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario = textBoxUsuario.Text.Trim();
            string contrasena = textBoxContrasena.Text.Trim();

            var resultado = _authService.Login(usuario, contrasena);

            if (resultado == null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
                return;
            }

            MessageBox.Show("Inicio de sesión exitoso");

            FormNuevaFactura f = new FormNuevaFactura();
            f.Show();

            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Deseas salir de la aplicación?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
