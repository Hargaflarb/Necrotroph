using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    public static class SaveManager
    {
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Constructor
        #endregion
        #region Methods
        public static void Execute()
        {
            Thread saveThread = new Thread(SaveGame);

            saveThread.Start();
        }
        private static void SaveGame()
        {
            try
            {
                GameWorld.Instance.Connection.Open();
                string insertQuery = "INSERT INTO Saves (Light, PlayerPosX, PlayerPosY) VALUES (2, 10, 50)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, GameWorld.Instance.Connection);
                insertCommand.ExecuteNonQuery();
                GameWorld.Instance.Connection.Close();
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                //in case the user does not have the database
            }
        }
        #endregion
    }
}
