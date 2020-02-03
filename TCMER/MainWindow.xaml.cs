using System.Windows;
using TCMER.Dao;

namespace TCMER
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TreeNodeMapper tnm = new TreeNodeMapper();
            var td = tnm.GetAllNodes();
        }
    }
}
