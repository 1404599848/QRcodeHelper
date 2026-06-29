using Dapper;
using Microsoft.Data.Sqlite;
using QRcodeHelper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRcodeHelper.Services
{
    public class DatabaseService
    {
        private static string dbPath = @"./MyDb.sqlite";
        public static string connStr = $@"data source={dbPath}";
        public static IDbConnection CreateConnection()
        {
            return new SqliteConnection(connStr);
        }

        public static void InitSQLiteDb()
        {
            //数据库存在则跳过
            if (File.Exists(dbPath))
            {
                return;
            }

            using (var conn = CreateConnection())
            {
                conn.Execute(@"
                        CREATE TABLE QRCodeRecords (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            QRCode TEXT,
                            Level TEXT,
                            CreationTime DATETIME,
                            IsPassed INTEGER
                        )");
            }
        }

        public static void TestData()
        {
            using (var conn = CreateConnection())
            {
                var record = new QRCodeRecord()
                {
                    QRCode = "asdqwe",
                    Level = "A"
                };
                for (int i = 0; i < 100; i++)
                {
                    conn.Execute(@"
                        INSERT INTO QRCodeRecords VALUES(
                            NULL, @QRCode, @Level, @CreationTime, @IsPassed
                        )", record);
                }

                var result = conn.Query<QRCodeRecord>($@"SELECT * FROM QRCodeRecords");
            }
        }
    }
}
