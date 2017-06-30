using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Data.SQLite;
using System.Data;

namespace Project4
{
    public class Database
    {
        public string sql;
        public void Data()
        {
            SQLiteConnection.CreateFile("HotPinkPong.sqlite");
            sql = "create table WinsLosses (ID INTEGER PRIMARY KEY, Wins INTEGER, Losses INTEGER)";
            DataBaseQueryExcecute(sql);

            sql = "insert into WinsLosses (ID, Wins, Losses) values (1,0,0)";
            DataBaseQueryExcecute(sql);

            sql = "select * from WinsLosses";
            DataBaseQueryRead(sql);
        }
        public void DataBaseQueryExcecute(string sql)
        {
            SQLiteConnection Conn = DBOpen();
            SQLiteCommand command = new SQLiteCommand(sql, Conn);
            command.ExecuteNonQuery();
            DBClose(Conn);
        }
        public void DataBaseQueryRead(string sql)
        {
            SQLiteConnection Conn = DBOpen();
            SQLiteCommand command = new SQLiteCommand(sql, Conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                System.Console.WriteLine("ID:" + reader.GetInt32(0) + " WIN: " + reader.GetInt32(1) + " LOSE: " + reader.GetInt32(2));
            DBClose(Conn);
        }
        public SQLiteConnection DBOpen()
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=HotPinkPong.sqlite;Version=3");
            m_dbConnection.Open();
            return m_dbConnection;
        }
        public void DBClose(SQLiteConnection m_dbConnection)
        {
            m_dbConnection.Close();
        }
    }
}
