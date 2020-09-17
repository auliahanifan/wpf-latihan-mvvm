using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLatihan.Data;

namespace WpfLatihan.Models
{
    class PelangganService
    {
        private static List<Pelanggan> ObjPelanggansList;

        SqlConnection ObjSqlConnection;
        SqlCommand ObjSqlCommand;

        public PelangganService()
        {
            ObjSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["LatihanConnection"].ConnectionString);
            ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandType = CommandType.StoredProcedure;
            ObjPelanggansList = new List<Pelanggan>()
            {
                new Pelanggan() {Id=1, Name="Abdul", Age=20}
            };       
        }

        public List<Pelanggan> GetAll()
        {
            List<Pelanggan> PelangganList = new List<Pelanggan>();
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectAllPelanggan";

                ObjSqlConnection.Open();

                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    Pelanggan ObjPelanggan = null;
                    while(ObjSqlDataReader.Read())
                    {
                        ObjPelanggan = new Pelanggan();
                        ObjPelanggan.Id = ObjSqlDataReader.GetInt32(0);
                        ObjPelanggan.Name = ObjSqlDataReader.GetString(1);
                        ObjPelanggan.Age = ObjSqlDataReader.GetInt32(2);

                        PelangganList.Add(ObjPelanggan);
                    }
                }

            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return PelangganList;
        }

        public bool AddPelanggan(Pelanggan newPelanggan)
        {
            bool IsAdded = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_InsertPelanggan";
                ObjSqlCommand.Parameters.AddWithValue("@Id", newPelanggan.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", newPelanggan.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Age", newPelanggan.Age);

                ObjSqlConnection.Open();
                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsAdded = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return IsAdded;
        }

        public bool UpdatePelanggan(Pelanggan updatedPelanggan)
        {
            bool IsUpdated = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_UpdatePelanggan";
                ObjSqlCommand.Parameters.AddWithValue("@Id", updatedPelanggan.Id);
                ObjSqlCommand.Parameters.AddWithValue("@Name", updatedPelanggan.Name);
                ObjSqlCommand.Parameters.AddWithValue("@Age", updatedPelanggan.Age);

                ObjSqlConnection.Open();
                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsUpdated = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return IsUpdated;
        }
        public bool DeletePelanggan(int id)
        {
            bool IsDeleted = false;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_DeletePelanggan";
                ObjSqlCommand.Parameters.AddWithValue("@Id", id);

                ObjSqlConnection.Open();
                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();
                IsDeleted = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }

            return IsDeleted;
        }
        public Pelanggan Search(int id)
        {
            Pelanggan pelanggan = null;
            try
            {
                ObjSqlCommand.Parameters.Clear();
                ObjSqlCommand.CommandText = "udp_SelectPelangganById";
                ObjSqlCommand.Parameters.AddWithValue("@Id", id);

                ObjSqlConnection.Open();
                var ObjSqlDataReader = ObjSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    ObjSqlDataReader.Read();
                    pelanggan = new Pelanggan();
                    pelanggan.Id = ObjSqlDataReader.GetInt32(0);
                    pelanggan.Name = ObjSqlDataReader.GetString(1);
                    pelanggan.Age = ObjSqlDataReader.GetInt32(2);
                }

            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                ObjSqlConnection.Close();
            }
            return pelanggan;
        }
    }
}
