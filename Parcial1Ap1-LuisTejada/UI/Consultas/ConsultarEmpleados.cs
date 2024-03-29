﻿using Parcial1Ap1_LuisTejada.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial1Ap1_LuisTejada.UI.Consultas
{
    public partial class ConsultarEmpleados : Form
    {
        public ConsultarEmpleados()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            using (var context = new DAL.Repositorio<Empleados>())
            {

                if (FiltrarCheckBox.Checked == false) DataGridView.DataSource = context.GetListAll();
                else
                {
                    if (FechaCheckBox.Checked == true && NombreCheckBox.Checked == true)
                    {
                        if (Utils.NoWhiteNoSpace(SearchTextBox.Text))
                        {
                            DataGridView.DataSource = context.GetList(x => x.FechaNacimientos >= DesdeDateTimePicker.Value.Date && x.FechaNacimientos <= HastaDateTimePicker.Value.Date && x.Nombres == SearchTextBox.Text);
                            ErrorProvider.Clear();
                        }
                        else ErrorProvider.SetError(SearchTextBox, "No puede estar vacio!");
                    }
                    else
                    {
                        if (FechaCheckBox.Checked == true)
                        {
                            DataGridView.DataSource = context.GetList(x => x.FechaNacimientos >= DesdeDateTimePicker.Value.Date && x.FechaNacimientos <= HastaDateTimePicker.Value.Date);
                        }
                        else if (NombreCheckBox.Checked == true)
                        {
                            if (Utils.NoWhiteNoSpace(SearchTextBox.Text))
                            {
                                DataGridView.DataSource = context.GetList(x => x.Nombres == SearchTextBox.Text);

                                ErrorProvider.Clear();
                            }
                            else ErrorProvider.SetError(SearchTextBox, "No puede estar vacio!");
                        }
                    }
                }
            }
        }

        private void FiltrarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(FiltrarCheckBox.Checked == true)
            {
                FechaCheckBox.Checked = true;
                NombreCheckBox.Checked = true;
                FechaCheckBox.Enabled = true;
                NombreCheckBox.Enabled = true;
                SearchTextBox.Enabled = true;
            }
            else
            {
                FechaCheckBox.Checked = false;
                NombreCheckBox.Checked = false;
                FechaCheckBox.Enabled = false;
                NombreCheckBox.Enabled = false;
            }
        }

        private void ConsultarEmpleados_Load(object sender, EventArgs e)
        {
            FechaCheckBox.Enabled = false;
            NombreCheckBox.Enabled = false;
            SearchTextBox.Enabled = false;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
