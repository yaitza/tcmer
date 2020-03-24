using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Org.BouncyCastle.Math.EC;
using TCMER.Utils;

namespace TCMER
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DataBaseSettingView.Visibility = Visibility.Visible;
            string sqlConnector = ConfigHelper.GetSingleConfigData("MySqlConnectionString");
            string[] stringArrays = sqlConnector.Split(';');

            this.tbIP.Text = stringArrays[0].Split('=')[1];
            this.tbUser.Text = stringArrays[1].Split('=')[1];
            this.tbPwd.Text = stringArrays[2].Split('=')[1];
            this.tbDb.Text = stringArrays[3].Split('=')[1];
            this.tbPort.Text = stringArrays[4].Split('=')[1];

        }

        private void TextBlock_MouseDown_Other(object sender, MouseButtonEventArgs e)
        {
            this.DataBaseSettingView.Visibility = Visibility.Hidden;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlConnectString = @"Server={0};user id={1};password={2};Database={3};Port={4};charset=utf8;";

            sqlConnectString = string.Format(sqlConnectString, tbIP.Text.Trim(), tbUser.Text.Trim(), tbPwd.Text.Trim(), tbDb.Text.Trim(),
                tbPort.Text.Trim());

            ConfigHelper.UpdateSingeConfigData("MySqlConnectionString", sqlConnectString);
        }
    }
}
