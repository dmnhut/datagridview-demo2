using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo_2
{
    public partial class Frm : Form
    {
        /// <summary>
        /// Frm
        /// </summary>
        public Frm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Frm_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void Frm_Load(object sender, EventArgs e)
        {
            Containers.DATA_TABLE = new DataTable();
            Containers.DATA_TABLE.Columns.Add(Member.fillable[0]);
            Containers.DATA_TABLE.Columns.Add(Member.fillable[1]);
            Containers.DATA_TABLE.Columns.Add(Member.fillable[2]);
            this.dgvMembers.DataSource = Containers.DATA_TABLE;
            this.dgvMembers.Columns[0].Width = 100;
            this.dgvMembers.Columns[1].Width = 300;
            this.dgvMembers.Columns[2].Width = 300;
            PutData();
            FormClear();
        }

        /// <summary>
        /// btnClose_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// dgvMembers_CellClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">DataGridViewCellEventArgs</param>
        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Containers.ROW_INDEX = e.RowIndex;
            if (Containers.ROW_INDEX == -1)
            {
                return;
            }
            else
            {
                this.dgvMembers.Rows[Containers.ROW_INDEX].Selected = true;
                this.txtRow.Text = (Containers.ROW_INDEX + 1).ToString();
                this.txtId.Text = this.dgvMembers.Rows[Containers.ROW_INDEX].Cells[0].Value.ToString();
                this.txtName.Text = this.dgvMembers.Rows[Containers.ROW_INDEX].Cells[1].Value.ToString();
                this.txtEmail.Text = this.dgvMembers.Rows[Containers.ROW_INDEX].Cells[2].Value.ToString();
            }
        }

        /// <summary>
        /// btnAdd_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtName.Text != "" && this.txtEmail.Text != "")
                {
                    Containers.CONNECTION = DBA.Connect();
                    Containers.CONNECTION.Open();
                    Containers.DATA_TABLE = DBA.Fill(SQL.Find(), Containers.CONNECTION, this.txtId.Text);
                    if (Containers.DATA_TABLE.Rows.Count > 0)
                    {
                        // rows init
                        IList<int> rows = new List<int>();
                        for (int index = 0; index < dgvMembers.Rows.Count - 1; index++)
                        {
                            if (Containers.DATA_TABLE.Rows[0][0].ToString() == dgvMembers.Rows[index].Cells[0].Value.ToString())
                            {
                                rows.Add(index + 1);
                            }
                        }
                        MessageBox.Show(String.Format("Exist member at row: {0}.", String.Join(",", rows.ToArray())), 
                            Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Containers.HASH_TABLE = new Hashtable();
                        Containers.HASH_TABLE.Add(Member.fillable[0], this.txtId.Text);
                        Containers.HASH_TABLE.Add(Member.fillable[1], this.txtName.Text);
                        Containers.HASH_TABLE.Add(Member.fillable[2], this.txtEmail.Text);
                        DBA.ExecuteNonQuery(SQL.Insert(), Containers.CONNECTION, Containers.HASH_TABLE);
                        MessageBox.Show("Add member success.", 
                            Containers.SUCCESS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Add member fail.", 
                    Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Containers.CONNECTION.Close();
                PutData();
                FormClear();
            }
        }

        /// <summary>
        /// btnUpdate_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtId.Text != "" && this.txtId.Text != "" && this.txtEmail.Text != "")
                {
                    if (this.dgvMembers.Rows[Containers.ROW_INDEX].Cells[0].Value.ToString() != this.txtId.Text)
                    {
                        MessageBox.Show("Don't change Id\nId is primary key.", 
                            Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Containers.CONNECTION = DBA.Connect();
                        Containers.CONNECTION.Open();
                        Containers.DATA_TABLE = DBA.Fill(SQL.Find(), Containers.CONNECTION, this.txtId.Text);
                        if (Containers.DATA_TABLE.Rows.Count == 0)
                        {
                            MessageBox.Show(String.Format("Not member has Id = {0}.", this.txtId.Text), 
                                Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Containers.HASH_TABLE = new Hashtable();
                            Containers.HASH_TABLE.Add(Member.fillable[0], this.txtId.Text);
                            Containers.HASH_TABLE.Add(Member.fillable[1], this.txtName.Text);
                            Containers.HASH_TABLE.Add(Member.fillable[2], this.txtEmail.Text);
                            Containers.CONNECTION = DBA.Connect();
                            Containers.CONNECTION.Open();
                            DBA.ExecuteNonQuery(SQL.Update(), Containers.CONNECTION, Containers.HASH_TABLE);
                            MessageBox.Show("Update member success.", 
                                Containers.SUCCESS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Update member fail.", 
                    Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Containers.CONNECTION.Close();
                PutData();
                FormClear();
            }
        }

        /// <summary>
        /// btnDelete_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtId.Text != "" && this.txtId.Text != "" && this.txtEmail.Text != "")
                {
                    if (this.dgvMembers.Rows[Containers.ROW_INDEX].Cells[0].Value.ToString() != this.txtId.Text)
                    {
                        MessageBox.Show("Don't change Id\nId is primary key.", 
                            Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Containers.CONNECTION = DBA.Connect();
                        Containers.CONNECTION.Open();
                        Containers.DATA_TABLE = DBA.Fill(SQL.Find(), Containers.CONNECTION, this.txtId.Text);
                        if (Containers.DATA_TABLE.Rows.Count == 0)
                        {
                            MessageBox.Show(String.Format("Not member has Id = {0}.", this.txtId.Text), 
                                Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            Containers.HASH_TABLE = new Hashtable();
                            Containers.HASH_TABLE.Add(Member.fillable[0], this.txtId.Text);
                            Containers.CONNECTION = DBA.Connect();
                            Containers.CONNECTION.Open();
                            DBA.ExecuteNonQuery(SQL.Delete(), Containers.CONNECTION, Containers.HASH_TABLE);
                            MessageBox.Show("Delete member success.", 
                                Containers.SUCCESS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Delete member fail.", 
                    Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Containers.CONNECTION.Close();
                PutData();
                FormClear();
            }
        }

        /// <summary>
        /// btnReload_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            PutData();
            FormClear();
        }

        /// <summary>
        /// PutData
        /// </summary>
        public void PutData()
        {
            try
            {
                Containers.CONNECTION = DBA.Connect();
                Containers.CONNECTION.Open();
                Containers.DATA_TABLE = DBA.Fill(SQL.Query(), Containers.CONNECTION, String.Empty);
                this.dgvMembers.DataSource = Containers.DATA_TABLE;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Query member fail.", 
                    Containers.ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Containers.CONNECTION.Close();
            }
        }

        /// <summary>
        /// FormClear
        /// </summary>
        public void FormClear()
        {
            this.txtEmail.Text = "";
            this.txtId.Text = "";
            this.txtName.Text = "";
            this.txtRow.Text = "";
            this.ActiveControl = null;
            this.btnClose.Focus();
            if (this.dgvMembers.Rows.Count > 1)
            {
                this.dgvMembers.CurrentCell.Selected = false;
            }
        }
    }
}
