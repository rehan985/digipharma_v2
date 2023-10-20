﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDAL
{
    public class DALCRUD
    {
        public static async Task CRUD(string ProcedureName, SqlParameter[] sqlParameters)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    await con.OpenAsync();
                   
                    using (SqlCommand cmd = new SqlCommand(ProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(sqlParameters);
                        await cmd.ExecuteNonQueryAsync();
                        await con.CloseAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }
        }
        public static async Task<DataTable> ReadTable(string ProcedureName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureName, conn))
                    {
                        await conn.OpenAsync();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                        await conn.CloseAsync();

                        if (dt.Rows.Count > 0)
                        {

                            return dt;

                        }
                        else { return new DataTable(); }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
                return new DataTable();

            }

        }
        public static async Task<DataTable> ReadDataSpecific(string ProcedureName, SqlParameter[] sqlParameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(ProcedureName, conn))
                    {
                        await conn.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(sqlParameters);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                        await conn.CloseAsync();

                        if (dt.Rows.Count > 0)
                        {

                            return dt;

                        }
                        else { return new DataTable(); }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }
            return new DataTable();

        }


    }
}