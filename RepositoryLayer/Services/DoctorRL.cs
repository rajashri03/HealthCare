using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class DoctorRL : IDoctorRL
    {
        private IConfiguration config;
        NpgsqlConnection sqlConnection;
        string ConnString = "Server=localhost;Port=5432;Database=health;Username=postgres; Password=rajashri@315;Integrated Security=True;";
        public DoctorRL(IConfiguration config)
        {
            this.config = config;
        }
        public MedicineModel AddMedicine(MedicineModel medicine,long userid)
        {
            sqlConnection = new NpgsqlConnection(ConnString);
            using (sqlConnection)
            {
                try
                {
                    NpgsqlCommand sqlCommand = new NpgsqlCommand("Call SP_AddMedicine1(:medicinename,:medicin_description,:medicinesadded_bywhom)", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.Text;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("medicinename", DbType.String).Value = medicine.MedicineName;
                    sqlCommand.Parameters.AddWithValue("medicin_description", DbType.String).Value = medicine.MedicineDescription;
                    sqlCommand.Parameters.AddWithValue("medicinesadded_bywhom", DbType.Int16).Value = userid;


                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != null)
                        return medicine;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }
    }
}
