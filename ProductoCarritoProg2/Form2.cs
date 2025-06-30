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
    public partial class Form2 : Form
    {
        public Producto objEntProducto = new Producto();

        public Carrito objEntCarrito = new Carrito();
        public NegocioCarrito objNegCarrito = new NegocioCarrito();
        public NegocioProducto objNegProducto = new NegocioProducto();

        public Form2()
        {
            InitializeComponent();
            LlenarDGVProductos();
            LlenarDGVCarrito();
        }

        /// <summary>
        /// Llena el DGV de productos disponibles
        /// </summary>
        private void LlenarDGVProductos()
        {
            DataSet ds = objNegProducto.CargarProductos();
            dgvProductos.DataSource = ds.Tables[0];

            if (dgvProductos.Columns.Contains("IdProducto"))
            {
                dgvProductos.Columns["IdProducto"].Visible = false;
            }
        }

        /// <summary>
        /// Llena el DGV del carrito con los datos relacionados
        /// </summary>
        private void LlenarDGVCarrito()
        {
            DataSet ds = objNegCarrito.CargarCarrito();
            dgvCarrito.DataSource = ds.Tables[0];

            if (dgvCarrito.Columns.Contains("IdCarrito"))
            {
                dgvCarrito.Columns["IdCarrito"].Visible = false;
            }
        }

        /// <summary>
        /// Agrega el producto seleccionado al carrito
        /// </summary>
        private void AgregarProductoAlCarrito()
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int idProducto = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value);

                bool habilitado = false;

                if (dgvProductos.SelectedRows[0].Cells["Habilitado"].Value != DBNull.Value)
                {
                    habilitado = Convert.ToBoolean(dgvProductos.SelectedRows[0].Cells["Habilitado"].Value);
                }

                if (!habilitado) 
                {
                    MessageBox.Show("El producto no esta disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                objEntCarrito.Id = idProducto;
                objEntCarrito.Cantidad = 1; // Siempre 1 unidad

                int resultado = objNegCarrito.abmCarrito("Alta", objEntCarrito);

                if (resultado <= 0)
                {
                    MessageBox.Show("Error al agregar el producto al carrito.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else 
                {
                    LlenarDGVCarrito();
                    CalcularTotalCarrito();

                }

            }
            else
            {
                MessageBox.Show("Seleccione un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Elimina el producto seleccionado del carrito
        /// </summary>
        private void EliminarProductoDelCarrito()
        {
            if (dgvCarrito.SelectedRows.Count > 0)
            {
                int idCarrito = Convert.ToInt32(dgvCarrito.SelectedRows[0].Cells["IdCarrito"].Value);

                objEntCarrito.Id = idCarrito;

                int resultado = objNegCarrito.abmCarrito("Baja", objEntCarrito);

                if (resultado <= 0)
                {
                    MessageBox.Show("Error al eliminar el producto del carrito.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Producto eliminado del carrito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenarDGVCarrito();
                    CalcularTotalCarrito();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto del carrito.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Eventos

        private void Form2_Load(object sender, EventArgs e)
        {
            LlenarDGVProductos();
            LlenarDGVCarrito();
            CalcularTotalCarrito();
        }

        private void CalcularTotalCarrito()
        {
            decimal sumaTotal = 0;

            foreach (DataGridViewRow row in dgvCarrito.Rows)
            {
                // Saltea la fila nueva si AllowUserToAddRows está en true
                if (row.IsNewRow)
                    continue;

                // Verifica que no sea null
                if (row.Cells["Total"].Value != DBNull.Value && row.Cells["Total"].Value != null)
                {
                    sumaTotal += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }

            // Mostrar en el label con formato moneda
            lblTotal.Text = "Total: $" + sumaTotal.ToString("N2");
        }



        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProductoAlCarrito();
        }

        private void btnEliminarDelCarrito_Click_1(object sender, EventArgs e)
        {
            EliminarProductoDelCarrito();
        }

        private void btnVaciarCarrito_Click(object sender, EventArgs e)
        {


            if (dgvCarrito.Rows.Count == 0 ||
        (dgvCarrito.Rows.Count == 1 && dgvCarrito.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay nada en el carrito, Ingrese un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult confirm = MessageBox.Show(
            "¿Está seguro que desea vaciar el carrito?",
            "Confirmación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

                if (confirm == DialogResult.Yes)
                {
                    int resultado = objNegCarrito.VaciarCarrito();

                    if (resultado >= 0)
                    {
                        MessageBox.Show("Carrito vaciado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LlenarDGVCarrito();
                        CalcularTotalCarrito();
                    }
                    else
                    {
                        MessageBox.Show("Error al vaciar el carrito.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.Rows.Count == 0 ||
        (dgvCarrito.Rows.Count == 1 && dgvCarrito.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay nada en el carrito, Ingrese un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                DialogResult confirm = MessageBox.Show(
        "Confirmar Compra",
        "Confirmación",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

                if (confirm == DialogResult.Yes)
                {
                    int resultado = objNegCarrito.VaciarCarrito();

                    if (resultado >= 0)
                    {
                        MessageBox.Show("Compra realizada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LlenarDGVCarrito();
                        CalcularTotalCarrito();
                    }
                    else
                    {
                        MessageBox.Show("Error al comprar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


             
        }
    }
    #endregion
}
