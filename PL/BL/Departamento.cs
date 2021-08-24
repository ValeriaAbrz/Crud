using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Departamento
    {
        public static ML.Result Add(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(DL.Conexion.GetConexion()))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        string Query = "INSERT INTO [Departamento]([Descripcion]) VALUES(?)";
                        cmd.CommandText = Query;
                        cmd.Connection = context;

                        OleDbParameter[] collection = new OleDbParameter[1];

                        //collection[0] = new OleDbParameter("DepartamentoID", SqlDbType.VarChar);
                        //collection[0].Value = departamento.DepartamentoID;

                        collection[0] = new OleDbParameter("Descripcion", SqlDbType.Int);
                        collection[0].Value = departamento.Descripcion;

                        cmd.Parameters.AddRange(collection);

                        context.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un Error al insertar el departamento";
                        }//else
                    }//using
                }//using
            }//try
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }//catch
            return result;
        }//Add
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(DL.Conexion.GetConexion()))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        string Query = "SELECT DepartamentoID, Descripcion FROM Departamento";
                        cmd.CommandText = Query;
                        cmd.Connection = context;
                        context.Open();

                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable tableDepartamento = new DataTable();

                        da.Fill(tableDepartamento);

                        if (tableDepartamento.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableDepartamento.Rows)
                            {
                                ML.Departamento departamento = new ML.Departamento();

                                departamento.DepartamentoID = int.Parse(row[0].ToString());
                                departamento.Descripcion = row[1].ToString();

                                result.Objects.Add(departamento);
                            }//foreach
                            result.Correct = true;
                        }//if
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla";
                        }//else
                    }//using
                }//using
            }//try
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }//catch
            return result;
        }//GetAllSP
        public static ML.Result Delete(int DepartamentoID)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(DL.Conexion.GetConexion()))
                {

                    using (OleDbCommand cmd = new OleDbCommand())
                    {

                        string Query = "DELETE FROM [Departamento] WHERE DepartamentoID = ?";

                        cmd.CommandText = Query;
                        cmd.Connection = context;
                        cmd.Parameters.AddWithValue("DepartamentoID", DepartamentoID);

                        context.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error";
                        }//else
                    }//using 2
                }//using 1
            }//try

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }//catch

            return result;
        }//Delete
    }
}
