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
    public partial class formCatalogo : Form
    {
        public List<Articulo> listaArticulos;
        public formCatalogo()
        {
            InitializeComponent();
        }

        private void formCatalogo_Load(object sender, EventArgs e)
        {
            cargarTablaArticulos();
            cargarDropDowns();
            if (dgvArticulos.CurrentRow != null)
            {
                btnDetalle.Enabled = true;
                btnAgregar.Enabled = true;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnDetalle.Enabled = false;
                btnAgregar.Enabled = false;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void cargarTablaArticulos()
        {
            ArticuloService articuloService = new ArticuloService();
            try
            {
                listaArticulos = articuloService.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;

        }
        private void cargarDropDowns()
        {
            MarcaService marcaService = new MarcaService();
            CategoriaService categoriaService = new CategoriaService();
            try
            {
                List<Marca> listaMarcas = marcaService.listar();
                listaMarcas.Insert(0, new Marca());
                cbMarca.DataSource = listaMarcas;
                cbMarca.ValueMember = "Id";
                cbMarca.DisplayMember = "Descripcion";
                List<Categoria> listaCategorias = categoriaService.listar();
                listaCategorias.Insert(0, new Categoria());
                cbCategoria.DataSource = listaCategorias;
                cbCategoria.ValueMember = "Id";
                cbCategoria.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            formAltaArticulo alta = new formAltaArticulo();
            alta.ShowDialog();
            cargarTablaArticulos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            formAltaArticulo editar = new formAltaArticulo(seleccionado);
            editar.ShowDialog();
            cargarTablaArticulos();

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                formDetalleArticulo detalle = new formDetalleArticulo(seleccionado);
                detalle.ShowDialog();
            }
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            formAltaMarca altaMarca = new formAltaMarca();
            altaMarca.ShowDialog();
            cargarDropDowns();
        }

        private void btnAgregarCat_Click(object sender, EventArgs e)
        {
            formAltaMarca altaCategoria = new formAltaMarca("Categoria");
            altaCategoria.ShowDialog();
            cargarDropDowns();
        }


        private void dgvArticulos_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                btnDetalle.Enabled = true;
                btnAgregar.Enabled = true;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnDetalle.Enabled = false;
                btnAgregar.Enabled = false;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloService service = new ArticuloService();
            Articulo seleccionado;

            try
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                DialogResult rta = MessageBox.Show("Estas seguro que queres eliminar " + seleccionado.Nombre + " ?", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (rta == DialogResult.Yes) {
                    service.eliminar(seleccionado.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cargarTablaArticulos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string marca = "";
            string categoria = "";
            string filtro= "";

            ArticuloService articuloService = new ArticuloService();

            if (cbMarca.SelectedIndex > 0)
            {
                marca = cbMarca.SelectedItem.ToString();
            }
            if (cbCategoria.SelectedIndex > 0)
            {
                categoria = cbCategoria.SelectedItem.ToString();
            }
            if (tbFiltro.Text.Length != 0)
            {
                filtro = tbFiltro.Text;
            }

            try
            {
                dgvArticulos.DataSource = articuloService.filtrar(marca, categoria, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
