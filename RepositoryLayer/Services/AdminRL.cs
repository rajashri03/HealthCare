using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private IConfiguration config;
        NpgsqlConnection sqlConnection;
        string ConnString = "Server=localhost;Port=5432;Database=health;Username=postgres; Password=rajashri@315;Integrated Security=True;";
        public AdminRL(IConfiguration config)
        {
            this.config = config;
        }
        public UsersModel Registration(UsersModel users)
        {
            sqlConnection = new NpgsqlConnection(ConnString);
            using (sqlConnection)
            {
                try
                {
                    //var password = this.EncryptPassword(registrationModel.Password);
                    //var password = registrationModel.Password;
                    NpgsqlCommand sqlCommand = new NpgsqlCommand("Call SP_Register(:username,:useraddress,:phonenumber,:email,:password,:usertype)", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.Text;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("username", DbType.String).Value = users.Username;
                    sqlCommand.Parameters.AddWithValue("useraddress", DbType.String).Value = users.UserAddress;
                    sqlCommand.Parameters.AddWithValue("phonenumber", DbType.Int32).Value = users.PhoneNumber;
                    sqlCommand.Parameters.AddWithValue("email", DbType.String).Value = users.Email;
                    sqlCommand.Parameters.AddWithValue("password", DbType.String).Value = users.Password;
                    sqlCommand.Parameters.AddWithValue("usertype", DbType.String).Value = users.UserType;

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != null)
                        return users;
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

        public string Login(LoginModel model)
        {
            throw new NotImplementedException();
        }

    }
}
