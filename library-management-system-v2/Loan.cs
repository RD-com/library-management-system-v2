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
    public partial class Loan : Form
    {
        private models.Member _member;
        public Loan()
        {
            InitializeComponent();
            LoadBooks();
            btnIssue.Enabled = false;
        }

        void LoadBooks()
        {
            var books = models.Book.GetAll();
            foreach (var book in books)
            {
                cmbBook.Items.Add(book.Id.ToString() + "-" + book.Title);
            }
        }

        void SearchMember()
        {
            var userid = txtSearchUserId.Text;
            _member = models.Member.GetOneWithUserID(userid);

            if(_member == null )
            {
                MessageBox.Show("Member not found");
                btnIssue.Enabled = false;
                return;
            }

            txtName.Text = _member.Name;
            txtGender.Text = _member.Gender;
            btnIssue.Enabled = true;

        }

        void IssueBook()
        {
            var date = dateDate.Value.ToString();
            var bookid = Convert.ToInt32(cmbBook.Text.ToString().Split('-')[0]);

            var borrowing = new models.Borrowing(_member.UserID, bookid, date, false, "");

            int arc = borrowing.Save();

            if( arc > 0)
            {
                MessageBox.Show("Record added");
                btnIssue.Enabled = false;
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

            LoadBooks();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMember();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchUserId.Text = string.Empty;
            _member = null;
            btnIssue.Enabled = false;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            IssueBook();
        }
    }
}
