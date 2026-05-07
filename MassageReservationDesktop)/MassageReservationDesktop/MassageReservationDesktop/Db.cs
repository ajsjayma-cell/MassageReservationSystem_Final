using System.Data;
using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public static class Db
{
    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(AppSettings.ConnectionString);
    }

    public static DataTable Query(string sql, params MySqlParameter[] parameters)
    {
        using var conn = GetConnection();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddRange(parameters);
        using var adapter = new MySqlDataAdapter(cmd);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static int Execute(string sql, params MySqlParameter[] parameters)
    {
        using var conn = GetConnection();
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteNonQuery();
    }

    public static object? Scalar(string sql, params MySqlParameter[] parameters)
    {
        using var conn = GetConnection();
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteScalar();
    }
}
