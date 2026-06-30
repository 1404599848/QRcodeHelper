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
                            IsPassed INTEGER,
                            AlertType INTEGER DEFAULT 0
                        )");
            }
        }

        public static void MigrateDB()
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    conn.Execute(@"ALTER TABLE QRCodeRecords ADD COLUMN AlertType INTEGER DEFAULT 0");
                }
            }
            catch
            {
                // 列已存在则忽略
            }
        }

        public static void TestData()
        {
            using (var conn = CreateConnection())
            {
                // 正常连续数据
                for (int i = 1; i <= 8; i++)
                {
                    conn.Execute(@"
                        INSERT INTO QRCodeRecords(QRCode, Level, CreationTime, IsPassed, AlertType) VALUES(
                            @QRCode, @Level, @CreationTime, @IsPassed, @AlertType
                        )", new QRCodeRecord
                        {
                            QRCode = $@"TESTPARTSUP001250101A{i:D4}",
                            Level = i % 3 == 0 ? "B" : "A",
                            CreationTime = DateTime.Now.AddSeconds(-60 + i),
                            AlertType = 0
                        });
                }

                // 漏码：9直接跳到15
                for (int i = 9; i <= 16; i++)
                {
                    var alertType = (i == 9) ? 2 : 0; // 9号标记漏码（中间缺了若干号）
                    conn.Execute(@"
                        INSERT INTO QRCodeRecords(QRCode, Level, CreationTime, IsPassed, AlertType) VALUES(
                            @QRCode, @Level, @CreationTime, @IsPassed, @AlertType
                        )", new QRCodeRecord
                        {
                            QRCode = $@"TESTPARTSUP001250101A{i:D4}",
                            Level = "A",
                            CreationTime = DateTime.Now.AddSeconds(-30 + i),
                            AlertType = alertType
                        });
                }

                // 重码：17出现两次
                conn.Execute(@"
                    INSERT INTO QRCodeRecords(QRCode, Level, CreationTime, IsPassed, AlertType) VALUES(
                        @QRCode, @Level, @CreationTime, @IsPassed, @AlertType
                    )", new QRCodeRecord
                    {
                        QRCode = "TESTPARTSUP001250101A0017",
                        Level = "A",
                        CreationTime = DateTime.Now.AddSeconds(-10),
                        AlertType = 1  // 重码
                    });

                // 另一批次正常数据
                for (int i = 1; i <= 5; i++)
                {
                    conn.Execute(@"
                        INSERT INTO QRCodeRecords(QRCode, Level, CreationTime, IsPassed, AlertType) VALUES(
                            @QRCode, @Level, @CreationTime, @IsPassed, @AlertType
                        )", new QRCodeRecord
                        {
                            QRCode = $@"TESTPARTSUP001250101B{i:D4}",
                            Level = "A",
                            CreationTime = DateTime.Now.AddSeconds(-20 + i),
                            AlertType = 0
                        });
                }

                // 不合格品
                conn.Execute(@"
                    INSERT INTO QRCodeRecords(QRCode, Level, CreationTime, IsPassed, AlertType) VALUES(
                        @QRCode, @Level, @CreationTime, @IsPassed, @AlertType
                    )", new QRCodeRecord
                    {
                        QRCode = "TESTPARTSUP001250101A0018",
                        Level = "F",
                        CreationTime = DateTime.Now,
                        AlertType = 0
                    });
            }
        }
    }
}
