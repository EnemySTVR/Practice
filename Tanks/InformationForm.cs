using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public partial class InformationForm : Form
    {
        public InformationForm(DataTable source)
        {
            InitializeComponent();
            dataGridView1.DataSource = source;
            dataGridView1.Columns["Id"].Visible = false;
            ControlBox = false;

        }
    }
}
