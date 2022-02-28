using System;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                using (var conn = new SQLiteConnection("Data Source=Memo.db"))
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


        private void button2_Click(object sender, EventArgs e)
        {
            InsertRecord(202101, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "佐藤浩二", 85);
            InsertRecord(202102, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "山田孝雄", 70);
            InsertRecord(202103, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "鈴木健司", 50);
            InsertRecord(202104, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "吉田将司", 65);

        }
    }

}
