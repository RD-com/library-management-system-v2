using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library_management_system_v2
{
    public partial class Borrowing : Form
    {
        private models.Borrowing _borrowing = null;

        public Borrowing()
        {
            InitializeComponent();
            LoadData(); 
        }

        void LoadData()
        {
            dgvBorrowings.Rows.Clear(); 
            var borrowings = models.Borrowing.GetAll();
            foreach (var borrowing in borrowings)
            {
                var member = models.Member.GetOneWithUserID(borrowing.UserID);
                var book = models.Book.GetOne(borrowing.BookID);

                var row = new object[]
                {
                    borrowing.Id,
                    book.Title,
                    member.Name,
                    member.UserID,
                    borrowing.IssuedDate,
                    borrowing.ReturnedDate,
                    borrowing.Returned ? "Returned" : "Pending"
                };

                dgvBorrowings.Rows.Add(row);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(_borrowing == null)
            {
                MessageBox.Show("Plese select a record");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                int arc = _borrowing.Delete();
                if(arc > 0)
                {
                    MessageBox.Show("Record deleted");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void dgvBorrowings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(dgvBorrowings.Rows[e.RowIndex].Cells[0].Value);
            _borrowing = models.Borrowing.GetOne(id);
        }
    }
}
