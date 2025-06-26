using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductoCarritoProg2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 nuevoForm = new Form1();
            nuevoForm.Show(); // Abre Form1 sin cerrar Form2
            this.Hide(); // Oculta Form2
        }
    }
}
