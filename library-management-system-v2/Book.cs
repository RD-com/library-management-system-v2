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
    public partial class Book : Form
    {

        private DataSet _dataSet;
        private models.Book _book = null;

        public Book()
        {
            InitializeComponent();
            RetriveBooks();
        }

        void RetriveBooks()
        {
            _dataSet = models.Book.GetDataSet();
            dgvBooks.DataSource = _dataSet.Tables[0];
        }

        void InserBook()
        {
            var title = txtTitle.Text;
            var author = txtAuthor.Text;
            var classificationCode = txtClsCode.Text;
            var quantity = Convert.ToInt32(txtQty.Text);
            var date = dateDate.Value.ToString();
            var borrowableCount = Convert.ToInt32(txtBorrowable.Text);
            var category = txtCategory.Text;
            var availableCount = borrowableCount;

            models.Book book = new models.Book(title, author, classificationCode, quantity, date, borrowableCount, category, availableCount);

            int arc = book.Save();

            if(arc > 0)
            {
                MessageBox.Show("New book added");
                Clear();
                RetriveBooks();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        void UpdateBook()
        {
            if( _book == null)
            {
                MessageBox.Show("Plese select a book");
                return;
            }

            var title = txtTitle.Text;
            var author = txtAuthor.Text;
            var classificationCode = txtClsCode.Text;
            var quantity = Convert.ToInt32(txtQty.Text);
            var date = dateDate.Value.ToString();
            var borrowableCount = Convert.ToInt32(txtBorrowable.Text);
            var category = txtCategory.Text;

            _book.Title = title;
            _book.Author = author;
            _book.ClassificationCode = classificationCode;
            _book.Quantity = quantity;
            _book.Date = date;
            _book.BorrowableCount = borrowableCount;
            _book.Category = category;

            int arc = _book.Update();
            if(arc > 0)
            {
                MessageBox.Show("Book updated");
                RetriveBooks();
                Clear();
            }
            else { MessageBox.Show("Error"); }
        }

        void DeleteBook()
        {
            if (_book == null)
            {
                MessageBox.Show("Plese select a book");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                int arc = _book.Delete();
                if (arc > 0)
                {
                    MessageBox.Show("Book deleted");
                    RetriveBooks();
                    Clear();
                }
                else { MessageBox.Show("Error"); }
            }
        }

        void Draw()
        {
            if (_book == null) return;

            txtTitle.Text = _book.Title;
            txtAuthor.Text = _book.Author;
            txtClsCode.Text = _book.ClassificationCode;
            txtQty.Text = _book.Quantity.ToString();
            dateDate.Text = _book.Date.ToString();
            txtBorrowable.Text = _book.BorrowableCount.ToString();
            txtCategory.Text = _book.Category;
        }

        void Clear()
        {
            _book = null;
            txtTitle.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtClsCode.Text = string.Empty;
            txtQty.Text = string.Empty;
            dateDate.Text = string.Empty;
            txtBorrowable.Text = string.Empty;
            txtCategory.Text = string.Empty;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            InserBook();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dgvBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _book = models.Book.GetOne(_dataSet.Tables[0].Rows[e.RowIndex].Field<int>("Id"));
            Draw();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateBook();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteBook();
        }
    }
}
