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
using negocio;

namespace catalogo
{
    public partial class formAltaArticulo : Form
    {
        private Articulo articulo = null;
        public formAltaArticulo()
        {
            InitializeComponent();
        }
        //constructor para editar
        public formAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Editar Artículo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formAltaArticulo_Load(object sender, EventArgs e)
        {   
            cargarDropDowns();
            if (articulo != null) { 
                tbCodigo.Text = articulo.Codigo;
                tbNombre.Text = articulo.Nombre;
                tbDescripcion.Text = articulo.Descripcion;
                tbImagenUrl.Text = articulo.UrlImagen;
                cargarImagen(articulo.UrlImagen);
                cbMarca.SelectedValue = articulo.Marca.Id;
                cbCategoria.SelectedValue = articulo.Categoria.Id;
                tbPrecio.Text = articulo.Precio.ToString();
            }
            btnAceptar.Enabled = validateFields();
        }
        private void cargarDropDowns()
        {
            MarcaService marcaService = new MarcaService();
            CategoriaService categoriaService = new CategoriaService();
            try
            {
                List<Marca> listaMarcas = marcaService.listar();
                cbMarca.DataSource = listaMarcas;
                cbMarca.ValueMember = "Id";
                cbMarca.DisplayMember = "Descripcion";
                List<Categoria> listaCategorias = categoriaService.listar();
                cbCategoria.DataSource = listaCategorias;
                cbCategoria.ValueMember = "Id";
                cbCategoria.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
        private void tbImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbImagenUrl.Text);
        }

        private bool validateFields()
        {
            if(
                tbCodigo.Text != "" &&
                tbNombre.Text != "" &&
                tbDescripcion.Text != "" &&
                tbImagenUrl.Text != "" &&
                cbMarca.SelectedValue != null &&
                cbCategoria.SelectedValue != null &&
                tbPrecio.Text != ""
            )
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        private void tbCodigo_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = validateFields();
        }

        private void tbNombre_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = validateFields();
        }

        private void tbDescripcion_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = validateFields();
        }

        private void tbImagenUrl_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = validateFields();
        }

        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = validateFields();
        }

        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = validateFields();
        }

        private void tbPrecio_TextChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = validateFields();
        }
        private void tbPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloService articuloService = new ArticuloService();

            try
            {
                if(articulo == null)
                {
                    articulo = new Articulo();
                }

                articulo.Codigo = tbCodigo.Text;
                articulo.Nombre = tbNombre.Text;
                articulo.Descripcion = tbDescripcion.Text;
                articulo.UrlImagen = tbImagenUrl.Text;
                articulo.Marca = (Marca)cbMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(tbPrecio.Text);

                if(articulo.Id != 0)
                {
                    articuloService.modificar(articulo);
                    MessageBox.Show("Articulo modificafo exitosamente");
                }
                else
                {
                    articuloService.agregar(articulo);
                    MessageBox.Show("Articulo agregado exitosamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Close();
            }
        }
    }
}
