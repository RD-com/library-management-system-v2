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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            var home = new Home();
            home.ShowDialog();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            var book = new Book();
            book.ShowDialog();
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            var member = new Member();
            member.ShowDialog();
        }

        private void btnLoan_Click(object sender, EventArgs e)
        {
            var loan = new Loan();
            loan.ShowDialog();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            var _return = new Return();
            _return.ShowDialog();
        }

        private void btnBorrowing_Click(object sender, EventArgs e)
        {
            var borrowing = new Borrowing();
            borrowing.ShowDialog();
        }
    }
}
