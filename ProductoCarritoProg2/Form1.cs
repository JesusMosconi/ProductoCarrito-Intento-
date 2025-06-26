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


        public Carrito objEntCarrito = new Carrito();
        public NegocioCarrito objNegCarrito = new NegocioCarrito();


        public Form1()
        {

            InitializeComponent();
            dgvProducto.ColumnCount = 5;
            dgvProducto.Columns[0].HeaderText = "Id";
            dgvProducto.Columns[1].HeaderText = "Nombre";
            dgvProducto.Columns[2].HeaderText = "Desripcion";
            dgvProducto.Columns[3].HeaderText = "Precio";
            dgvProducto.Columns[4].HeaderText = "Habilitado";

            dgvProducto.Columns[0].Width = 60;
            dgvProducto.Columns[1].Width = 89;
            dgvProducto.Columns[2].Width = 89;
            dgvProducto.Columns[3].Width = 89;
            dgvProducto.Columns[4].Width = 80;
            LlenarDGV();
        }

        private void LlenarDGV()
        {
            dgvProducto.Rows.Clear();
            DataSet ds = new DataSet();
            ds = objNegProducto.ListadoProducto("Todos");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Lo que quieres mostrar esta en dr[0].ToString(), dr[1].ToString(),
                    dgvProducto.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                }
            }
            else
                lblMensaje.Text = "No hay profesionales cargados en el sistema";
        }

        private void TxtBox_A_Obj()
        {
            objEntProducto.Id = int.Parse(txtId.Text);
            objEntProducto.Nombre = txtNombre.Text;
            objEntProducto.Descripcion = txtDescripcion.Text;
            objEntProducto.Precio = int.Parse(txtPrecio.Text);
            objEntProducto.habilitado = txtTotal.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int nGrabados = -1;
            TxtBox_A_Obj();

            nGrabados = objNegProducto.abmProducto("Alta", objEntProducto);

            if (nGrabados == -1)
            {
                MessageBox.Show("Error al grabar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Producto grabado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarDGV();

            }
        }

        private void dgvProducto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet ds = new DataSet();

            objEntProducto.Id = Convert.ToInt32(dgvProducto.CurrentRow.Cells[0].Value);
            ds = objNegProducto.ListadoProducto(objEntProducto.Id.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds_A_TxtBNox(ds);
                btnAgregar.Visible = false;

            }
        }

        private void ds_A_TxtBNox(DataSet ds)
        {
            txtId.Text = ds.Tables[0].Rows[0]["IdProducto"].ToString();
            txtNombre.Text = ds.Tables[0].Rows[0]["Nombre"].ToString();
            txtDescripcion.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
            txtPrecio.Text = ds.Tables[0].Rows[0]["Precio"].ToString();
            txtTotal.Text = ds.Tables[0].Rows[0]["Habilitado"].ToString();
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            int nResultado = -1;
            TxtBox_A_Obj();
            nResultado = objNegProducto.abmProducto("Modificar", objEntProducto);

            if (nResultado == -1)
            {
                MessageBox.Show("Error al modificar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Producto modificado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Actualizar el DataGridView
                LlenarDGV();
                btnAgregar.Visible = true;

                txtNombre.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                txtPrecio.Text = string.Empty;
                txtTotal.Text = string.Empty;





            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int nResultado = -1;
            TxtBox_A_Obj();
            nResultado = objNegProducto.abmProducto("Baja", objEntProducto);
            if (nResultado == -1)
            {
                MessageBox.Show("Error al eliminar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Producto eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Actualizar el DataGridView
                LlenarDGV();
                btnAgregar.Visible = true;
                txtNombre.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                txtPrecio.Text = string.Empty;
                txtTotal.Text = string.Empty;
            }
        }


  

        private void btnIrCarrito_Click(object sender, EventArgs e)
        {
            Form2 nuevoForm = new Form2();
            nuevoForm.Show(); // Abre Form2 sin cerrar Form1
            this.Hide(); // Oculta Form1 si es necesario
        }
    }
}
