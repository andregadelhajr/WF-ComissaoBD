using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WF_Exercicio
{
    internal class Connection
    {
        static string con = @"Data Source=DESKTOP-HN7QPII\SQLEXPRESS;Initial Catalog=Comissao;Integrated Security=True";
        SqlConnection objCon = new SqlConnection(con);

        public DataSet BuscarDados()
        {
            objCon.Open();
            SqlCommand cmd = new SqlCommand("select * from Vendas", objCon);
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter();

            DataSet ds = new DataSet();

            da.SelectCommand = cmd;
            da.Fill(ds);
            objCon.Close();

            return ds;
        }

        public int InserirDado(int VendedorID, int ItemID, DateTime DtVenda, decimal Valor)
        {
            objCon.Open();
            string sql = $@"INSERT INTO Vendas(VendedorID, ItemID, DtVenda, Valor) VALUES ('{VendedorID}', '{ItemID}', '{DtVenda}', '{Valor}')";
            
            SqlCommand cmd = new SqlCommand(sql, objCon);

            cmd.CommandType = CommandType.Text;
            int i = cmd.ExecuteNonQuery();
            objCon.Close();

            return i;


        }

        public int DeletarDado(int ID)
        {
            objCon.Open();
            string sql = $"DELETE FROM Vendas WHERE VendaId = {ID} ";

            SqlCommand cmd = new SqlCommand(sql, objCon);

            cmd.CommandType = CommandType.Text;
            int i = cmd.ExecuteNonQuery();
            objCon.Close();

            return i;
        }

        public int AtualizarDado(int VendedorID, int ItemID, DateTime DtVenda, decimal Valor, int ID)
        {
            objCon.Open();
            string sql = $@"UPDATE Vendas set VendedorID = '{VendedorID}', ItemID = {ItemID}, DtVenda ='{DtVenda}', Valor = '{Valor}' WHERE VendaID = {ID}";
            
            SqlCommand cmd = new SqlCommand(sql, objCon);

            cmd.CommandType = CommandType.Text;
            int i = cmd.ExecuteNonQuery();
            objCon.Close();

            return i;
        }
    }
}
