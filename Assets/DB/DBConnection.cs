using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class DBConnection : MonoBehaviour
{


    //   private string connectionString;
    //    private IDbConnection dbConnection;
    private string dbName = "URI=file:HighScore.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDB() {

        using (var connection = new SqliteConnection(dbName))
        { 
        connection.Open();

            using(var command = connection.CreateCommand())
            {

                command.CommandText = "CREATE TABLE IF NOT EXISTS HighScore (name VARCHAR(20), score INT);";
                command.ExecuteNonQuery();
            }
        connection.Close();
        
        }
    }


}
