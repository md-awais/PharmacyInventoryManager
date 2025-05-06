using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyInventoryManager
{
    public partial class Form1 : Form
    {
        MedicineService service = new MedicineService(); // Logic layer instance

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Medicine med = new Medicine
                {
                    Name = txtName.Text,
                    Category = txtCategory.Text,
                    Quantity = int.Parse(txtQuantity.Text),
                    Supplier = txtSupplier.Text,
                    Price = decimal.Parse(txtPrice.Text)
                };

                service.AddMedicine(med);
                MessageBox.Show("Medicine added successfully!");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            dgvMedicines.DataSource = null;
            dgvMedicines.DataSource = service.GetAllMedicines();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a row to update.");
                return;
            }

            try
            {
                int selectedId = ((Medicine)dgvMedicines.SelectedRows[0].DataBoundItem).Id;

                Medicine updated = new Medicine
                {
                    Id = selectedId,
                    Name = txtName.Text,
                    Category = txtCategory.Text,
                    Quantity = int.Parse(txtQuantity.Text),
                    Supplier = txtSupplier.Text,
                    Price = decimal.Parse(txtPrice.Text)
                };

                bool result = service.UpdateMedicine(updated);

                if (result)
                {
                    MessageBox.Show("Updated successfully!");
                    btnView.PerformClick();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a row to delete.");
                return;
            }

            int selectedId = ((Medicine)dgvMedicines.SelectedRows[0].DataBoundItem).Id;
            bool deleted = service.DeleteMedicine(selectedId);

            if (deleted)
            {
                MessageBox.Show("Deleted successfully.");
                btnView.PerformClick();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Delete failed.");
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtCategory.Clear();
            txtQuantity.Clear();
            txtSupplier.Clear();
            txtPrice.Clear();
        }

        private void dgvMedicines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvMedicines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMedicines.Rows[e.RowIndex];

                txtName.Text = row.Cells["Name"].Value?.ToString();
                txtCategory.Text = row.Cells["Category"].Value?.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value?.ToString();
                txtSupplier.Text = row.Cells["Supplier"].Value?.ToString();
                txtPrice.Text = row.Cells["Price"].Value?.ToString();
            }
        }
    }
}
