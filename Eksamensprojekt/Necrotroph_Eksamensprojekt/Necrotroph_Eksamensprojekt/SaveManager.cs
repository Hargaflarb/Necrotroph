using Microsoft.Data.SqlClient;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    /// <summary>
    /// Echo
    /// </summary>
    public static class SaveManager
    {
        #region Fields
        private static string connectionString;
        private static SqlConnection connection;
        #endregion
        #region Properties
        public static string ConnectionString { get => connectionString; set => connectionString = value; }
        public static SqlConnection Connection { get => connection; set => connection = value; }
        #endregion
        #region Constructor
        #endregion
        #region Methods

        public static void SaveGame()
        {
            Thread saveThread = new Thread(Save);

            saveThread.Start();
        }
        public static void LoadGame()
        {
            Thread loadThread = new Thread(Load);

            loadThread.Start();
        }
        private static void Save()
        {
            ConnectionString =
                "Server = localhost\\SQLEXPRESS; Database = GhostGame; Trusted_Connection = True; TrustServerCertificate = True";

            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            string playerPosX = Player.Instance.Transform.WorldPosition.X.ToString().Replace(',', '.');
            string playerPosY = Player.Instance.Transform.WorldPosition.Y.ToString().Replace(',', '.');
            string hunterPosX = HunterEnemy.Instance.Transform.WorldPosition.X.ToString().Replace(',', '.');
            string hunterPosY = HunterEnemy.Instance.Transform.WorldPosition.Y.ToString().Replace(',', '.');
            string insertQuery = $"INSERT INTO Saves (Light, PlayerPosX, PlayerPosY, HunterPosX, HunterPosY) VALUES ({Player.Instance.Life}, {playerPosX}, {playerPosY}, {hunterPosX}, {hunterPosY})";
            SqlCommand insertCommand = new SqlCommand(insertQuery, Connection);
            insertCommand.ExecuteNonQuery();
            Connection.Close();
        }
        private static void Load()
        {
            ConnectionString =
                "Server = localhost\\SQLEXPRESS; Database = GhostGame; Trusted_Connection = True; TrustServerCertificate = True";

            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT TOP (1) Light, PlayerPosX, PlayerPosY, HunterPosX, HunterPosY FROM Saves ORDER BY SaveID DESC;", Connection);
            SqlDataReader reader = selectCommand.ExecuteReader();

            bool rowExists = reader.Read();

            if (rowExists)
            {
                Player.Instance.Life = reader.GetInt32(0);
                Player.Instance.Transform.WorldPosition = new Vector2((float)reader.GetDouble(1), (float)reader.GetDouble(2));
                HunterEnemy.Instance.Transform.WorldPosition = new Vector2((float)reader.GetDouble(3), (float)reader.GetDouble(4));
            }
            reader.Close();
            Connection.Close();
        }
        #endregion
    }
}
