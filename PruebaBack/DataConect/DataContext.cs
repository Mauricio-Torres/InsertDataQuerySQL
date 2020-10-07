
using DataConect;
using DataConect.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace PruebaTecnica.DataBase
{
    public class DataContext
    {
        Settings settings = new Settings();
        private SqlConnection CnSQL = null;
        private SqlCommand CmSQL = null;
        public DataContext()
        {
        }
        private bool Connect()
        {
            try
            {
                CnSQL = new SqlConnection();
                CnSQL.ConnectionString = settings.ConnectionDataBase; 
                CnSQL.Open();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CloseConnection()
        {
            if (CnSQL != null)
            {
                CnSQL.Close();
                CnSQL.Dispose();
                CnSQL = null;
            }
        }

        public DataTable GetEntity(string sCommand)
        {
            DataTable dtResultado = new DataTable();
            try
            {
                Connect();
                if (CnSQL != null)
                {
                    SqlDataAdapter daResultado = new SqlDataAdapter();
                    SqlCommand CmSQL = new SqlCommand(sCommand, CnSQL);
                    daResultado.SelectCommand = CmSQL;
                    daResultado.Fill(dtResultado);
                    daResultado.Dispose();
                    daResultado = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            catch (SqlException x1)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "SqlException ", x1.Message);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return dtResultado;
        }

        public bool ExecuteQuery(List<Tuple<string, object>> nombres, string comandSQL)
        {
            try
            {
                Connect();
                CmSQL = new SqlCommand(comandSQL, CnSQL);
                
                foreach (var item in nombres) 
                {
                    CmSQL.Parameters.AddWithValue("@"+item.Item1, item.Item2);
                }

                int nRows = CmSQL.ExecuteNonQuery();

                if (nRows <= 0)
                {
                    Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "nRows <= 0", "No se pudo insertar el registro.");
                    return false;
                }

                return true;
            }
            catch (SqlException x1)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "SqlException ", x1.Message);
                return false;
            }
            catch (Exception x)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool ExecuteQuery(string comandSQL)
        {
            try
            {
                Connect();
                CmSQL = new SqlCommand(comandSQL, CnSQL);

                int nRows = CmSQL.ExecuteNonQuery();

                if (nRows <= 0)
                {
                    Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "nRows <= 0", "No se pudo insertar el registro.");
                    return false;
                }

                return true;
            }
            catch (SqlException x1)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "SqlException ", x1.Message);
                return false;
            }
            catch (Exception x)
            {
                throw;

            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
