using LiteDB;
using System;
using System.Collections.Generic;
using TCMER.Model;

namespace TCMER.Utils
{
    public static class ConfigHelper
    {
        private const string EncryptStr =
            "D4OlDJKfXz1Jcpn2fBbfqCfsiU4mb6TDwBL/AtkADfRmmGHra4zkSfcCnesA87vP5CXi6fgnzBYKJsKtEgx4dcG+mWpYIEvFEC4eGiDAPYL95Sxku+xCcINP4eeg4Bbk";

        private static readonly ConnectionString ConnectionString = new ConnectionString(@"Resource\Config.db")
        {
            Password = StringCipher.Decrypt(EncryptStr, "password")
        };

        public static IEnumerable<ConfigModel> GetAllConfigData()
        {
            IEnumerable<ConfigModel> configModelsList = null;
            using (var db = new LiteDatabase(ConnectionString))
            {
                var configData = db.GetCollection<ConfigModel>("Config");
                configModelsList = configData.FindAll();
            }

            return configModelsList;
        }

        public static String GetSingleConfigData(String configName)
        {
            ConfigModel singleConfigData;
            using (var db = new LiteDatabase(ConnectionString))
            {
                var col = db.GetCollection<ConfigModel>("Config");
                singleConfigData = col.FindOne(x => x.ConfigName == configName);
            }

            return singleConfigData.ConfigValue;
        }

        public static void UpdateSingeConfigData(string configName, string configValue)
        {
            List<ConfigModel> updateData = new List<ConfigModel>();
            ConfigModel cm = new ConfigModel
            {
                Id = 1,
                ConfigName = configName,
                ConfigValue = configValue
            };
            updateData.Add(cm);

            using (var db = new LiteDatabase(ConnectionString))
            {
                var configData = db.GetCollection<ConfigModel>("Config");
                configData.Update(updateData);
            }
        }

        public static void InitData()
        {
            List<ConfigModel> initData = new List<ConfigModel>();
            ConfigModel cm = new ConfigModel
            {
                Id = 1,
                ConfigName = @"MySqlConnectionString",
                ConfigValue = @"Server=52.83.196.98;user id=admin;password=admin@zh;Database=TCMER;Port=3306;charset=utf8;"
            };
            initData.Add(cm);

            using (var db = new LiteDatabase(ConnectionString))
            {
                var configData = db.GetCollection<ConfigModel>("Config");
                configData.Insert(initData);
            }
        }


    }
}