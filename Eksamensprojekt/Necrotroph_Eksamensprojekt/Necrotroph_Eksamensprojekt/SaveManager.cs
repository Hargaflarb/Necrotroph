using Microsoft.Data.SqlClient;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.Menu;
using Necrotroph_Eksamensprojekt.ObjectPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Necrotroph_Eksamensprojekt
{
    //Echo
    public static class SaveManager
    {
        #region Fields
        private static string connectionString;
        private static SqlConnection connection;
        private static int value = 0;
        #endregion
        #region Properties
        public static string ConnectionString { get => connectionString; set => connectionString = value; }
        public static SqlConnection Connection { get => connection; set => connection = value; }
        #endregion
        #region Constructor
        #endregion
        #region Methods
        /// <summary>
        /// Runs the Save thread
        /// </summary>
        public static void SaveGame()
        {
            Thread saveThread = new Thread(Save);

            saveThread.Start();
        }

        /// <summary>
        /// Method for Saving the game, does a lot
        /// </summary>
        private static void Save()
        {
            try
            {
                ConnectionString =
                    "Server = localhost\\SQLEXPRESS; Database = GhostGame; Trusted_Connection = True; TrustServerCertificate = True";

                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                string playerLight = Player.Instance.Life.ToString().Replace(',', '.');
                string playerPosX = Player.Instance.Transform.WorldPosition.X.ToString().Replace(',', '.');
                string playerPosY = Player.Instance.Transform.WorldPosition.Y.ToString().Replace(',', '.');
                string hunterPosX = HunterEnemy.Instance.Transform.WorldPosition.X.ToString().Replace(',', '.');
                string hunterPosY = HunterEnemy.Instance.Transform.WorldPosition.Y.ToString().Replace(',', '.');



                for (int i = 0; i < InGame.Instance.ActiveMemorabilia.Count; i++)
                {
                    int id = (int)Math.Pow(2, i);
                    if (InGame.Instance.ActiveMemorabilia.ContainsKey(id))
                    {
                        value += id;
                    }
                }

                string insertQuery = $"INSERT INTO Saves (Light, ItemsCollected, PlayerPosX, PlayerPosY, HunterPosX, HunterPosY, MapSeed) " +
                    $"VALUES ({playerLight}, {GameWorld.MemProgress}, {playerPosX}, {playerPosY}, {hunterPosX}, {hunterPosY}, {GameWorld.Seed})";
                SqlCommand insertCommand = new SqlCommand(insertQuery, Connection);
                insertCommand.ExecuteNonQuery();
                Connection.Close();
            }
            catch(Microsoft.Data.SqlClient.SqlException)
            {
                //in case there is no sql server found
            }
        }
        /// <summary>
        /// Method for loading the game, also does a lot
        /// </summary>
        public static void Load()
        {
            try
            {
                ConnectionString =
                    "Server = localhost\\SQLEXPRESS; Database = GhostGame; Trusted_Connection = True; TrustServerCertificate = True";

                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                SqlCommand selectCommand = new SqlCommand("SELECT TOP (1) Light, ItemsCollected, PlayerPosX, PlayerPosY, HunterPosX, HunterPosY, MapSeed FROM Saves ORDER BY SaveID DESC;", Connection);
                SqlDataReader reader = selectCommand.ExecuteReader();

                bool rowExists = reader.Read();

                if (rowExists)
                {
                    Player.Instance.Life = (float)reader.GetDouble(0);
                    InGame.Instance.ItemsCollected = reader.GetInt32(1);
                    Player.Instance.Transform.WorldPosition = new Vector2((float)reader.GetDouble(2), (float)reader.GetDouble(3));
                    HunterEnemy.Instance.Transform.WorldPosition = new Vector2((float)reader.GetDouble(4), (float)reader.GetDouble(5));
                    GameWorld.Seed = reader.GetInt32(6);
                    GameWorld.Rnd = new Random(GameWorld.Seed);
                    Map.Rnd = new Random(GameWorld.Seed);
                }
                reader.Close();

                TreePool.Instance.Clear();
                Map.Clear();
                Map.GenerateMap();

                InGame.Instance.ActiveMemorabilia.Clear();
                if (((MemorabiliaProgress)value).HasFlag(MemorabiliaProgress.mem1))
                {
                    InGame.Instance.ActiveMemorabilia.Add((int)MemorabiliaProgress.mem1, InGame.Instance.Mem1);
                }
                if (((MemorabiliaProgress)value).HasFlag(MemorabiliaProgress.mem2))
                {
                    InGame.Instance.ActiveMemorabilia.Add((int)MemorabiliaProgress.mem2, InGame.Instance.Mem2);
                }
                if (((MemorabiliaProgress)value).HasFlag(MemorabiliaProgress.mem3))
                {
                    InGame.Instance.ActiveMemorabilia.Add((int)MemorabiliaProgress.mem3, InGame.Instance.Mem3);
                }
                if (((MemorabiliaProgress)value).HasFlag(MemorabiliaProgress.mem4))
                {
                    InGame.Instance.ActiveMemorabilia.Add((int)MemorabiliaProgress.mem4, InGame.Instance.Mem4);
                }
                if (((MemorabiliaProgress)value).HasFlag(MemorabiliaProgress.mem5))
                {
                    InGame.Instance.ActiveMemorabilia.Add((int)MemorabiliaProgress.mem5, InGame.Instance.Mem5);
                }

                Connection.Close();
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                //in case there is no sql server found
            }
        }
        #endregion
    }
}
