using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

using SharedLogic;


using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;

namespace MonoGameLogic
{
    public class Database
    {
        public string sql;
        public void ExecuteData()
        {
            SqliteConnection.CreateFile("HotPinkPong.sqlite");
            sql = "create table WinsLosses (ID INTEGER PRIMARY KEY, Wins INTEGER, Losses INTEGER)";
            DataBaseQueryExcecute(sql);

            sql = "insert into WinsLosses (ID, Wins, Losses) values (1,0,0)";
            DataBaseQueryExcecute(sql);

            sql = "select * from WinsLosses";
            DataBaseQueryRead(sql);
        }
        public void ReadData(string Query)
        {

        }
        public void DataBaseQueryExcecute(string sql)
        {
            SqliteConnection Conn = DBOpen();
            SqliteCommand command = new SqliteCommand(sql, Conn);
            command.ExecuteNonQuery();
            DBClose(Conn);
        }
        public void DataBaseQueryRead(string sql)
        {
            SqliteConnection Conn = DBOpen();
            SqliteCommand command = new SqliteCommand(sql, Conn);
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                System.Diagnostics.Debug.WriteLine("ID:" + reader.GetInt32(0) + " WIN: " + reader.GetInt32(1) + " LOSE: " + reader.GetInt32(2));
            DBClose(Conn);
        }
        public SqliteConnection DBOpen()
        {
            SqliteConnection m_dbConnection;
            m_dbConnection = new SqliteConnection("Data Source=HotPinkPong.sqlite;Version=3");
            m_dbConnection.Open();
            return m_dbConnection;
        }
        public void DBClose(SqliteConnection m_dbConnection)
        {
            m_dbConnection.Close();
        }
    }
}
