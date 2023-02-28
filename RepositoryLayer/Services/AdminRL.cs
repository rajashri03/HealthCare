using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
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
                    NpgsqlCommand sqlCommand = new NpgsqlCommand("Call sp_register1(:username,:useraddress,:phonenumber,:email,:password,:usertype,:approve)", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("username", users.username);
                    sqlCommand.Parameters.AddWithValue("useraddress", users.useraddress);
                    sqlCommand.Parameters.AddWithValue("phonenumber", users.phonenumber);
                    sqlCommand.Parameters.AddWithValue("email", users.email);
                    sqlCommand.Parameters.AddWithValue("password", users.password);
                    sqlCommand.Parameters.AddWithValue("usertype", users.usertype);
                    sqlCommand.Parameters.AddWithValue("approve", users.approve);
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
        private string GenerateSecurityToken(string Email, string UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["AppSettings:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Email,Email),
                new Claim("userid",UserId.ToString())
            };
            var token = new JwtSecurityToken(config["AppSettings:Key"],
              config["AppSettings:key"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string Login(LoginModel model)
        {
            using (sqlConnection = new NpgsqlConnection(ConnString))
                try
                {
                    string query = "select * from Users where email='" + model.Email + "' and password='" + model.Password + "' and  usertype='Admin';";
                    NpgsqlCommand sqlCommand = new NpgsqlCommand(query, sqlConnection);

                    sqlConnection.Open();


                    var result = sqlCommand.ExecuteScalar();
                    if (result != null)
                    {
                        string query1 = "SELECT userid FROM Users WHERE username = '" + result + "'";
                        NpgsqlCommand cmd = new NpgsqlCommand(query1, sqlConnection);
                        var Id = cmd.ExecuteScalar();
                        var token = GenerateSecurityToken(model.Email, Id.ToString());
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally { sqlConnection.Close(); }
        }
        public MedicineModel AddMedicine(MedicineModel medicine, long userid)
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
        public IEnumerable<GetAllMedicine> GetAllMedicines()
        {
            sqlConnection = new NpgsqlConnection(ConnString);
            List<GetAllMedicine> getAddressModels = new List<GetAllMedicine>();
            using (sqlConnection)
            {
                try
                {
                    NpgsqlCommand sqlCommand = new NpgsqlCommand("select * from medicines;", sqlConnection);
                    sqlConnection.Open();
                    NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            getAddressModels.Add(new GetAllMedicine()
                            {
                                MedicineId = Convert.ToInt32(reader["medicinid"]),
                                MedicineName = reader["medicinename"].ToString(),
                                MedicineDescription = reader["medicin_description"].ToString()
                            });
                        }
                        return getAddressModels;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }

            }
        }
        public IEnumerable<GetDoctors> GetAllDoctors()
        {
            sqlConnection = new NpgsqlConnection(ConnString);
            List<GetDoctors> getAddressModels = new List<GetDoctors>();
            using (sqlConnection)
            {
                try
                {
                    NpgsqlCommand sqlCommand = new NpgsqlCommand("select * from users where usertype='Doctors';", sqlConnection);
                    sqlConnection.Open();
                    NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            getAddressModels.Add(new GetDoctors()
                            {
                                Doctorname = reader["username"].ToString(),
                                Phonenumber = reader["phonenumber"].ToString(),
                                emailaddress = reader["email"].ToString(),
                                Address = reader["useraddress"].ToString(),
                                approve = Convert.ToInt16(reader["approve"]),
                                userid = Convert.ToInt16(reader["userid"])
                            });
                        }
                        return getAddressModels;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }

            }

        }
        public IEnumerable<GetAllMedicine> GetAllMedicinesByid(long userid)
        {
            sqlConnection = new NpgsqlConnection(ConnString);
            List<GetAllMedicine> getAddressModels = new List<GetAllMedicine>();
            using (sqlConnection)
            {
                try
                {
                    NpgsqlCommand sqlCommand = new NpgsqlCommand("select * from medicines where medicinesadded_bywhom='" + userid + "'; ", sqlConnection);
                    sqlConnection.Open();
                    NpgsqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            getAddressModels.Add(new GetAllMedicine()
                            {
                                MedicineId = Convert.ToInt32(reader["medicinid"]),
                                medicinesadded_bywhom = Convert.ToInt32(reader["medicinesadded_bywhom"]),
                                MedicineName = reader["medicinename"].ToString(),
                                MedicineDescription = reader["medicin_description"].ToString()
                            });
                        }
                        return getAddressModels;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public bool ApproveORNot(int userid, int approve)
        {
            sqlConnection = new NpgsqlConnection(ConnString);
            using (sqlConnection)
            {
                try
                {
                    NpgsqlCommand sqlCommand = new NpgsqlCommand("Call sp_approve(:uid,:approve)", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("uid", userid);
                    sqlCommand.Parameters.AddWithValue("approve", approve);
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != null)
                        return true;
                    else
                        return false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}