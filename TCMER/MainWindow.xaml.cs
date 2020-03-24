using System.Windows;
using HandyControl.Tools.Extension;
using TCMER.Dao;
using TCMER.Model;
using System;

namespace TCMER
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [Obsolete]
        public MainWindow()
        {
            InitializeComponent();

        }

        [System.Obsolete]
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.TreeView.SelectedItem.GetType().IsGenericType)
            {
                return;
            }
            if (this.TreeView.SelectedItem != null)
            {
                this.ShowData((TreeNodeModel)this.TreeView.SelectedItem);
            }
        }

        [Obsolete]
        private void ShowData(TreeNodeModel node)
        {
            if (node.NodeType == NodeType.TestSuite)
            {
                this.TestCaseDetails.Visibility = Visibility.Hidden;
                this.NodeDetails.Visibility = Visibility.Visible;
                this.TestsuiteId.Text = node.Id;
                this.TestsuiteName.Text = node.DataBody;
                this.TestsuiteCreator.Content = node.CreateBy;
                this.TestsuiteCreateTime.Content = node.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                this.TestsuiteModifier.Content = node.UpdateBy;
                this.TestsuiteModifyTime.Content = node.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (node.NodeType == NodeType.TestCase)
            {
                this.NodeDetails.Visibility = Visibility.Hidden;
                this.TestCaseDetails.Visibility = Visibility.Visible;
                TestCaseMapper tcm = new TestCaseMapper();
                TestCaseModel tcModel = tcm.QueryTestCaseById(node.Id);
                this.TestcaseId.Text = tcModel.Id;
                this.TestcaseName.Text = tcModel.Name;
                this.TestcaseSummary.Text = tcModel.Summary;
                this.TestcasePrecondition.Text = tcModel.Precondition;
                this.TestCaseImportance.SelectedItem = tcModel.Importance;
                this.TestCaseType.SelectedItem = tcModel.TestCaseType;
                this.TestCaseCreator.Content = tcModel.CreateBy;
                this.TestCaseCreateTime.Content = tcModel.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                this.TestCaseModifier.Content = tcModel.UpdateBy;
                this.TestCaseModifyTime.Content = tcModel.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                this.TestCaseSteps.ItemsSource = tcModel.TestSteps;
            }
        }

        [Obsolete]
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TreeNodeMapper tnm = new TreeNodeMapper();
            var td = tnm.GetAllNodes();
            this.TreeView.ItemsSource = td;
        }
    }
}
