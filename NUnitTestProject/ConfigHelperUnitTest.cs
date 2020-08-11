using System;
using TCMER.Utils;
using NUnit.Framework;

namespace Tests
{
    public class ConfigHelperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllConfigDataTest()
        {
            var value = ConfigHelper.GetAllConfigData();

            string ConfigName = @"MySqlConnectionString";
            string ConfigValue = @"Server=localhost;user id=admin;password=admin@zh;Database=TCMER;Port=3306;charset=utf8;";
            ConfigHelper.UpdateSingeConfigData(ConfigName, ConfigValue);

            var endValue = ConfigHelper.GetAllConfigData();
            Assert.Pass();
        }
    }
}