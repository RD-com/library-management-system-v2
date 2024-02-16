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
    public partial class Member : Form
    {
        private DataSet _dataSet;
        private models.Member _member = null;

        public Member()
        {
            InitializeComponent();
            RetriveMembers();
        }

        void RetriveMembers()
        {
            _dataSet = models.Member.GetDataSet();
            dgvMembers.DataSource = _dataSet.Tables[0];
        }

        void InsertMember()
        {
            var userid = txtUserID.Text;
            var name = txtName.Text;
            var nic = txtNic.Text;
            var address = txtAddress.Text;
            var gender = rbMale.Checked ? "Male" : "Female";

            var member = new models.Member(userid, name, nic, address, gender);
            int arc = member.Save();

            if(arc > 0)
            {
                MessageBox.Show("Member added");
                RetriveMembers();
                Clear();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        void UpdateMember()
        {
            if( _member == null )
            {
                MessageBox.Show("Plese select a member");
                return;
            }
            var userid = txtUserID.Text;
            var name = txtName.Text;
            var nic = txtNic.Text;
            var address = txtAddress.Text;
            var gender = rbMale.Checked ? "Male" : "Female";

            _member.UserID = userid;
            _member.Name = name;
            _member.Address = address;
            _member.Gender = gender;
            _member.Address = address;

            DialogResult result = MessageBox.Show("Are you sure you want to update this member?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if( result == DialogResult.Yes )
            {
                int arc = _member.Update();
                if( arc > 0 )
                {
                    MessageBox.Show("Member details updated");
                    RetriveMembers();
                    Clear();
                }else { MessageBox.Show("Error"); }
            }
        }

        void DeleteMember()
        {
            if( _member == null )
            {
                MessageBox.Show("Plese select a member");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this member?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if( result == DialogResult.Yes )
            {
                int arc = _member.Delete();
                if( arc > 0)
                {
                    MessageBox.Show("Member Deleted");
                    RetriveMembers();
                    Clear();
                }
            }

        }

        void Draw()
        {
            if (_member == null) return;

            txtUserID.Text = _member.UserID;
            txtName.Text = _member.Name;
            txtAddress.Text = _member.Address;
            txtNic.Text = _member.NIC;

            rbFemale.Checked = _member.Gender == "Female";
            rbMale.Checked = _member.Gender == "Male";
        }

        void Clear()
        {
            _member = null;
            txtUserID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtNic.Text = string.Empty;
            rbFemale.Checked = false;
            rbMale.Checked = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertMember();
        }

        private void dgvMembers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _member = models.Member.GetOne(_dataSet.Tables[0].Rows[e.RowIndex].Field<int>("Id"));
            Draw();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteMember();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateMember();
        }
    }
}
