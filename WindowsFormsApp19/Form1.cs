﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace WindowsFormsApp19
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // テーブル名が存在しなければ作成する
            StringBuilder query = new StringBuilder();
            query.Clear();
            query.Append("CREATE TABLE IF NOT EXISTS PURCHASELIST (");
            query.Append(" NO INTEGER NOT NULL");
            query.Append(" ,DATETIME TEXT NOT NULL");
            query.Append(" ,NAME TEXT NOT NULL");
            query.Append(" ,POINT INTEGER NOT NULL");
            query.Append(" ,primary key (NO)");
            query.Append(")");

            // クエリー実行
            ExecuteNonQuery(query.ToString());
        }



        private void ExecuteNonQuery(string query)
        {
            try
            {
                // 接続先を指定
                using (var conn = new SQLiteConnection("Data Source=DataBase.sqlite"))
                using (var command = conn.CreateCommand())
                {
                    // 接続
                    conn.Open();

                    // コマンドの実行処理
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    //var value = command.ExecuteNonQuery();
                    //MessageBox.Show($"更新されたレコード数は {value} です。");
                }
            }
            catch (Exception ex)
            {
                //例外が発生した時はメッセージボックスを表示
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertRecord(int no, string datetime, string name, int point)
        {
            // レコードの登録
            var query = "INSERT INTO PURCHASELIST (NO,DATETIME,NAME,POINT) VALUES (" +
                $"{no},'{datetime}','{name}',{point})";

            // クエリー実行
            ExecuteNonQuery(query.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertRecord(1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "佐藤浩二", 85);
            InsertRecord(2, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "山田孝雄", 70);
            InsertRecord(3, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "鈴木健司", 50);
            InsertRecord(4, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "吉田将司", 65);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SerachRecordData2("NAME", textBox2.Text);
            //textBox1.Text = result.ToString();

        }





        private void SerachRecordData2(string column, string word)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=DataBase.sqlite");
            con.Open();
            try
            {
                string sql = "SELECT * FROM PURCHASELIST WHERE " +
                $"{column} = '{word}' ORDER BY NO ASC";

                SQLiteCommand com = new SQLiteCommand(sql, con);
                SQLiteDataReader sdr = com.ExecuteReader();
                while (sdr.Read() == true)
                {
                    textBox1.Text = sdr["NO"].ToString();
                    textBox1.Text += sdr["DATETIME"].ToString();
                    textBox1.Text += sdr["NAME"].ToString();
                    textBox1.Text += sdr["POINT"].ToString();
                }
                sdr.Close();
            }
            finally
            {
                con.Close();
            }

        }
        private void SerachRecordDataAll(string column, string word)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=DataBase.sqlite");
            con.Open();
            try
            {
                string sql = "SELECT * FROM PURCHASELIST  ORDER BY NO ASC";

                SQLiteCommand com = new SQLiteCommand(sql, con);
                SQLiteDataReader sdr = com.ExecuteReader();
                while (sdr.Read() == true)
                {
                    textBox1.Text += sdr["NO"].ToString();
                    textBox1.Text += " : ";
                    textBox1.Text += sdr["DATETIME"].ToString();
                    textBox1.Text += " : ";
                    textBox1.Text += sdr["NAME"].ToString();
                    textBox1.Text += " : ";
                    textBox1.Text += sdr["POINT"].ToString();
                    textBox1.Text += "\r\n";
                }
                sdr.Close();
            }
            finally
            {
                con.Close();
            }

        }
        private void DeleteRecord(string column, string word)
        {
            // レコードの削除
            var query = "DELETE FROM PURCHASELIST WHERE " +
                $"{column} = '{word}'";

            // クエリー実行
            ExecuteNonQuery(query.ToString());
        }
        private void DropTable()
        {
            // テーブルの削除
            var query = "DROP TABLE PURCHASELIST";

            // クエリー実行
            ExecuteNonQuery(query.ToString());
        }

        private void UpdateRecord(int no, string datetime, string name, int point)
        {
            // レコードの登録
            var query = $"UPDATE PURCHASELIST SET DATETIME = '{datetime}', NAME = '{name}',POINT = '{point}' " +
                $"WHERE NO = {no};";

            // クエリー実行
            ExecuteNonQuery(query.ToString());
        }
        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DropTable();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DeleteRecord("NAME", textBox2.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UpdateRecord(int.Parse(textBox3.Text), DateTime.Now.ToString(), textBox4.Text, 100);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SerachRecordDataAll("NAME", textBox2.Text);

        }
    }
}


