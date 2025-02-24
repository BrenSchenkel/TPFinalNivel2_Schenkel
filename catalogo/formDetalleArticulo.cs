using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;

namespace catalogo
{
    public partial class formDetalleArticulo : Form
    {
        private Articulo articulo = null;
        public formDetalleArticulo()
        {
            InitializeComponent();
        }

        public formDetalleArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formDetalleArticulo_Load(object sender, EventArgs e)
        {
            if (this.articulo != null) { 
                txtCodigo.Text = this.articulo.Codigo;
                txtNombre.Text = this.articulo.Nombre;
                txtDescripcion.Text = this.articulo.Descripcion;
                txtUrlImagen.Text = this.articulo.UrlImagen;
                cargarImagen(this.articulo.UrlImagen);
                txtMarca.Text = this.articulo.Marca.Descripcion;
                txtCategoria.Text = this.articulo.Categoria.Descripcion;
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbArticulo.Load(imagen);

            }
            catch (Exception ex)
            {
                pbArticulo.Load("https://static.vecteezy.com/system/resources/thumbnails/008/695/917/small_2x/no-image-available-icon-simple-two-colors-template-for-no-image-or-picture-coming-soon-and-placeholder-illustration-isolated-on-white-background-vector.jpg");
            }

        }
    }
}
