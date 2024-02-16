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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            int bookCount = models.Book.GetAll().Count;
            int memberCount = models.Member.GetAll().Count;
            int borrowingCount = models.Borrowing.GetAll().Count;
            int pendignReturns = models.Borrowing.GetAllPending().Count;

            labelBooks.Text = bookCount.ToString();
            labelMembers.Text = memberCount.ToString();
            labelBorrowings.Text = borrowingCount.ToString();
            labelReturns.Text = pendignReturns.ToString();

        }

    }
}
