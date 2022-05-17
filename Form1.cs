using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_Exercicio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Enable();

            string con = @"Data Source=DESKTOP-HN7QPII\SQLEXPRESS;Initial Catalog=Comissao;Integrated Security=True";

            SqlConnection objCon = new SqlConnection(con);
            objCon.Open();

            SqlCommand cmd = new SqlCommand("select * from Vendas", objCon);
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter();

            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds); 

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].TableName;

            objCon.Close();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            string con = @"Data Source=DESKTOP-HN7QPII\SQLEXPRESS;Initial Catalog=Comissao;Integrated Security=True";
            SqlConnection objCon = new SqlConnection(con);

            //Comando SQL para inserir dados na tabela
            string sql = @"INSERT INTO Vendas(VendedorID, ItemID, DtVenda, Valor) 
                           VALUES ('"+ int.Parse(txtVendedorID.Text) + "', '" + int.Parse(txtItemID.Text) + "', '"+dateTimePicker1.Value+"', '" + decimal.Parse(txtValor.Text) + "')";

            SqlCommand cmd = new SqlCommand(sql, objCon);
            cmd.CommandType = CommandType.Text;
            objCon.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Dado Cadatrado com Sucesso");
                    txtVendedorID.Focus();
                    Form1_Load(sender, e);
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não Cadastrou, Algo Deu Ruim " + ex.Message);
                throw;
            }
            finally
            {
                objCon.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtVendedorID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtItemID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtValor.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            btnDeletar.Enabled = true;
            btnAtualizar.Enabled = true;
            btnInserir.Enabled = false;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string connection = @"Data Source=DESKTOP-HN7QPII\SQLEXPRESS;Initial Catalog=Comissao;Integrated Security=True";
            SqlConnection objCon = new SqlConnection(connection);

            string sql = @"UPDATE Vendas 
                           set VendedorID = '" + int.Parse(txtVendedorID.Text) + "', ItemID = '"+int.Parse(txtItemID.Text)+ "', DtVenda = '"+ dateTimePicker1.Value + "', Valor = '"+decimal.Parse(txtValor.Text)+"' WHERE VendaID = '" + txtID.Text + "' ";

            SqlCommand cmd = new SqlCommand(sql, objCon);
            cmd.CommandType = CommandType.Text;
            objCon.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Registro atualizado");
                    Form1_Load(sender, e);
                    LimparCampos();
                    Enable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deu ruim na atualização de dados\n" +
                    "" + ex.Message);
                throw;
            }
            finally
            {
                objCon.Close();
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            string connection = @"Data Source=DESKTOP-HN7QPII\SQLEXPRESS;Initial Catalog=Comissao;Integrated Security=True";
            SqlConnection objCon = new SqlConnection(connection);

            //Comando SQL para Deletar dados na tabela
            string sql = @"DELETE FROM Vendas WHERE VendaID = '" + txtID.Text + "' ";

            SqlCommand cmd = new SqlCommand(sql, objCon);
            cmd.CommandType = CommandType.Text;
            objCon.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Registro deletado");
                    Form1_Load(sender, e);
                    LimparCampos();
                    Enable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deu ruim " + ex.Message);
                throw;
            }
            finally
            {
                objCon.Close();

            }
        }

        private void btnLimparCampos_Click(object sender, EventArgs e)
        {
            LimparCampos();
            Enable();
        }

        private void LimparCampos()
        {
            txtID.Text = null;
            txtVendedorID.Text = null;
            txtItemID.Text = null;
            txtValor.Text = null;
        }

        private void Enable()
        {
            btnInserir.Enabled = true;
            btnDeletar.Enabled = false;
            btnAtualizar.Enabled = false;
        }
    }
}
