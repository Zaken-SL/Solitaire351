
//Author: Jacob Slee
//Adapted from  https://www.youtube.com/watch?v=iQ2w4cQzekk


using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Runtime.InteropServices;

public class DBHighScore : MonoBehaviour
{


    public InputField nameInput;
    public InputField scoreInput;
   
    public Text ScoreList;


    private string dbName = "URI=file:ScoreLog.db";


    // Start is called before the first frame update
    void Start()
    {
        CreateDB();

     //   DisplayScores();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Creates a Database Connection and generates a Table for High Scores if one does not exist
    public void CreateDB()
    {

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {

                command.CommandText = "CREATE TABLE IF NOT EXISTS HighScore (name VARCHAR(20), score INT);";
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }

    //attempt at making the scores appear on screen
    /*
        public void DisplayScores()
        {

            ScoreList.text = "";

            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM HighScore;";


                    using (IDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            ScoreList.text += reader["name"] + "\t\t" + reader["score"] + "\n";



                        }
                        reader.Close();
                    }

                }
                connection.Close();


            }
    */

    //Attached to Submit Button, add text values of Labels into DB

    public void AddScore()
    {
     //   print("test");
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO HighScore (name,score) VALUES ('" + nameInput.text + "','" + scoreInput.text + "');";
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }


}


