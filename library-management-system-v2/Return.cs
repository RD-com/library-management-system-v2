using library_management_system_v2.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library_management_system_v2
{
    public partial class Return : Form
    {
        private models.Member _member = null;
        public Return()
        {
            InitializeComponent();
            btnReturn.Enabled = false;
        }

        void SearchMember()
        {
            var userid = txtSearchUserId.Text;
            _member = models.Member.GetOneWithUserID(userid);

            if (_member == null)
            {
                MessageBox.Show("Member not found");
                btnReturn.Enabled = false;
                return;
            }

            LoadBooks();

            txtName.Text = _member.Name;
            txtGender.Text = _member.Gender;
            btnReturn.Enabled = true;

        }

        void LoadBooks()
        {
            if (_member == null) return;

            var borrowings = models.Borrowing.GetAllWithUserID(_member.UserID);
            
            if(borrowings.Count == 0)
            {
                btnReturn.Enabled = false;
                return;
            }

            foreach (var borrowing in borrowings)
            {
                var book = models.Book.GetOne(borrowing.BookID);
                cmbBook.Items.Add(borrowing.Id.ToString() + "-" + book.Title + "-" + borrowing.IssuedDate);
            }
        }

        void ReturnBook()
        {
            var borrowingId = Convert.ToInt32(cmbBook.Text.Split('-')[0]);
            var borrowing = models.Borrowing.GetOne(borrowingId);

            var returnedDate = dateDate.Value.ToString();

            borrowing.ReturnedDate = returnedDate;
            borrowing.Returned = true;

            int arc = borrowing.Update();
            if(arc > 0)
            {
                MessageBox.Show("Book returned");
                btnReturn.Enabled = false;
                Clear();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        void Clear()
        {
            cmbBook.Items.Clear();
            cmbBook.Text = string.Empty;
            txtGender.Text = string.Empty;
            txtName.Text = string.Empty;
            dateDate.Text = string.Empty;
            txtSearchUserId.Text = string.Empty;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMember();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            ReturnBook();   
        }
    }
}
