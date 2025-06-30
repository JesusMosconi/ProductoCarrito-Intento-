using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocios;

namespace ProductoCarritoProg2
{
    public partial class Form1 : Form
    {

        public Producto objEntProducto = new Producto();

        public NegocioProducto objNegProducto = new NegocioProducto();





        public Form1()
        {

            InitializeComponent();
            LlenarDGV();
        }

        private void LlenarDGV()
        {
           
            NegocioProducto producto = new NegocioProducto();
            DataSet ds = producto.CargarProductos();
            dgvProducto.DataSource = ds.Tables[0];
            
            if (dgvProducto.Columns.Contains("IdProducto")) 
            {
                  dgvProducto.Columns["IdProducto"].Visible = false; // Oculta la columna IdProducto
            }
        
        }


        private void RegistrarProducto()
        {
            // Pasa los datos del formulario al objeto
            TxtBox_A_Obj();

            // Llamada a NegocioProducto para hacer el alta
            int resultado = objNegProducto.abmProducto("Alta", objEntProducto);

            if (resultado == -1)
            {
                MessageBox.Show("Error al registrar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Producto registrado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarDGV();
                Limpiar();
            }
        }

        private void TxtBox_A_Obj()
        {
            
            objEntProducto.Nombre = txtNombre.Text;
            objEntProducto.Descripcion = txtDescripcion.Text;
            objEntProducto.Precio = txtPrecio.Text;
            objEntProducto.habilitado = chkProducto.Checked;
        }

        private void Limpiar()
        {

            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            chkProducto.Checked = false;
        }

        #region Eventos
        private void Form1_Load(object sender, EventArgs e)
        {
            LlenarDGV();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            RegistrarProducto();
            LlenarDGV();
        }

  




        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProducto.SelectedRows.Count > 0)
            {
                int idProducto = Convert.ToInt32(dgvProducto.SelectedRows[0].Cells["IdProducto"].Value);
                // Cargar los datos del producto seleccionado en el formulario
                TxtBox_A_Obj();

                objEntProducto.Id = idProducto; // Asignar el Id del producto seleccionado
                int resultado = objNegProducto.abmProducto("Modificar", objEntProducto);

                if (resultado <= 0)
                {
                    MessageBox.Show("Error al modificar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Producto modificado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenarDGV();
                    Limpiar();
                }
            }

        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvProducto.SelectedRows.Count > 0)
            {
                // Obtener el Id del producto seleccionado
                int idProducto = Convert.ToInt32(dgvProducto.SelectedRows[0].Cells["IdProducto"].Value);
                // Llamada a NegocioProducto para hacer la baja
                int resultado = objNegProducto.abmProducto("Baja", new Producto { Id = idProducto });
                if (resultado == -1)
                {
                    MessageBox.Show("Error al eliminar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Producto eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenarDGV();
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnIrCarrito_Click(object sender, EventArgs e)
        {
            Form2 nuevoForm = new Form2();
            nuevoForm.Show(); // Abre Form2 sin cerrar Form1
            this.Hide(); // Oculta Form1 si es necesario
        }
        #endregion
    }
}
