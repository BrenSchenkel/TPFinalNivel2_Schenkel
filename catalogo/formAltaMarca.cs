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
    public partial class formAltaMarca : Form
    {
        Marca marca = null;
        Categoria categoria = null;
        public formAltaMarca()
        {
            InitializeComponent();
        }
        public formAltaMarca(string tipo)
        {
            InitializeComponent();
            if(tipo == "Categoria")
            {
                Text = "Agregar Categoría";
                lblDescripcion.Text = "Categoría";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (tbDescripcion.Text.Length > 0) { 
                try
                {
                    if (Text == "Agregar Marca")
                    {
                        MarcaService service = new MarcaService();
                        Marca marca = new Marca();
                        marca.Descripcion = tbDescripcion.Text;
                        service.agregar(marca);
                        MessageBox.Show("Marca agregada existosamente");
                    }
                    else if (Text == "Agregar Categoría")
                    {
                        CategoriaService service = new CategoriaService();
                        Categoria categoria = new Categoria();
                        categoria.Descripcion = tbDescripcion.Text;
                        service.agregar(categoria);
                        MessageBox.Show("Categoría agregada existosamente");
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

        private void tbDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            btnAgregar.Enabled = validateText();
        }

        private void formAltaMarca_Load(object sender, EventArgs e)
        {

            btnAgregar.Enabled = validateText();

        }

        private bool validateText()
        {
            if (!(tbDescripcion.Text != ""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
