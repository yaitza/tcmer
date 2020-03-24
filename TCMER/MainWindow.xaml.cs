using System.Windows;
using HandyControl.Tools.Extension;
using TCMER.Dao;
using TCMER.Model;

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
            this.TreeView.Items.Add(td);

        }


        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.TreeView.SelectedItem != null)
            {
                this.ShowData((TreeNodeModel)this.TreeView.SelectedItem);
            }
        }

        [System.Obsolete]
        private void ShowData(TreeNodeModel node)
        {
            if (node.NodeType == NodeType.TestSuite)
            {
                this.TestcaseId.Text = node.Id;
                this.TestcaseName.Text = node.DataBody;
                this.TestCaseCreator.Name = node.CreateBy;
                this.TestCaseCreateTime.Name = node.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                this.TestCaseModifier.Name = node.UpdateBy;
                this.TestCaseModifyTime.Name = node.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (node.NodeType == NodeType.TestCase)
            {
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TreeNodeModel tnmss = new TreeNodeModel();
            tnmss.Id = "tc_00001";
            tnmss.NodeType = NodeType.TestCase;
            this.ShowData(tnmss);
        }
    }
}
