﻿using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Project1.Data
{
    public class AzureSQLAccess
    {
        private readonly string connectionString;

        public AzureSQLAccess()
        {
            // Connection string with SQL authentication
            connectionString = "Server=tcp:dts.database.windows.net,1433;Initial Catalog=DB_DriveTracker;Persist Security Info=False;User ID=dts-admin;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        //Email and Password login check
        public async Task<bool> IsDriverValidAsync(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT COUNT(1) FROM Drivers WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                int count = (int)await cmd.ExecuteScalarAsync();
                return count > 0;
            }
        }
        //Fetch feilds.
        public async Task<Driver> GetDriverByEmailAsync(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT FirstName, LastName, Email, Address, PostalCode, LicenseNumber, OverallScore FROM Drivers WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var driver = new Driver(
                            reader.GetString(0), // FirstName
                            reader.GetString(1), // LastName
                            reader.GetString(2), // Email
                            reader.GetString(3), // Address
                            reader.GetString(4), // PostalCode
                            reader.GetString(5), // LicenseNumber
                            reader.GetInt32(6)   // OverallScore
                        );
                        return driver;
                    }
                    return null;
                }
            }
        }
        
        public async Task<bool> IsDuplicateEmailAsync(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT Email FROM Drivers WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    return await reader.ReadAsync(); // If a row is found, email is a duplicate
                }
            }
        }

        public async Task InsertDriverAsync(Driver driver, Account account)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO Drivers (FirstName, LastName, Email, Password, Address, PostalCode, LicenseNumber, OverallScore) VALUES (@FirstName, @LastName, @Email, @Password, @Address, @PostalCode, @LicenseNumber, @OverallScore)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@FirstName", driver.GetName());
                cmd.Parameters.AddWithValue("@LastName", driver.getLastName());
                cmd.Parameters.AddWithValue("@Password", account.GetPassword());
                cmd.Parameters.AddWithValue("@Email", account.GetEmail());
                cmd.Parameters.AddWithValue("@Address", driver.GetAddress());
                cmd.Parameters.AddWithValue("@PostalCode", driver.GetPostalCode());
                cmd.Parameters.AddWithValue("@LicenseNumber", driver.GetDriverLicenseNumber());
                cmd.Parameters.AddWithValue("@OverallScore", driver.GetOverallScore());

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateDriverOverallScoreAsync(string email, int newOverallScore)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE Drivers SET OverallScore = @OverallScore WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OverallScore", newOverallScore);
                cmd.Parameters.AddWithValue("@Email", email);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteDriverAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = "DELETE FROM Drivers WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
