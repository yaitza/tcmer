using System.Windows;
using HandyControl.Tools.Extension;
using TCMER.Dao;
using TCMER.Model;
using System;
using System.Windows.Controls;

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
                TreeNodeModel tnm = this.TreeView.SelectedItem as TreeNodeModel;
                this.ShowData(tnm);

                if (tnm.NodeType == NodeType.TestCase)
                {
                    ContextMenu treeViewContextMenu = this.TreeView.ContextMenu;

                    foreach (var item in treeViewContextMenu.Items)
                    {
                        if (item is MenuItem miItem && miItem.Name.Equals("AddTestSuite"))
                        {
                            miItem.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                else
                {
                    ContextMenu treeViewContextMenu = this.TreeView.ContextMenu;

                    foreach (var item in treeViewContextMenu.Items)
                    {
                        if (item is MenuItem miItem && miItem.Name.Equals("AddTestSuite"))
                        {
                            miItem.Visibility = Visibility.Visible;
                        }
                        
                    }
                }
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/yaitza/TCMer/wiki");
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.Show();
        }

        private void MenuItem_Click_DataBase(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            settings.Show();
        }

        private void AddTestSuite_Click(object sender, RoutedEventArgs e)
        {
            TreeNodeModel stnm = this.TreeView.SelectedItem as TreeNodeModel;
            TreeNodeModel tnm = new TreeNodeModel();
            tnm.Id = System.Guid.NewGuid().ToString();
            tnm.Depth = stnm.Depth + 1;
            tnm.NodeType = NodeType.TestSuite;
            
            stnm.Nodes.Add(tnm);
            
        }

        private void AddTestCase_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
