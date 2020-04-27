using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using TCMER.Controller;
using TCMER.Dao;
using TCMER.Model;
using TCMER.Utils;

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
            DisplayHelper.ShowMethod += this.OutputRichTextBox;
        }

        private TreeViewItem item;

        [System.Obsolete]
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.TreeView.SelectedItem != null && this.TreeView.SelectedItem.GetType().IsGenericType)
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

                        if (item is MenuItem mtItem && mtItem.Name.Equals("AddTestCase"))
                        {
                            mtItem.Visibility = Visibility.Collapsed;
                        }

                        if (item is MenuItem reItem && reItem.Name.Equals("reNameItem"))
                        {
                            reItem.Visibility = Visibility.Visible;
                        }

                        if (item is MenuItem cpItem && cpItem.Name.Equals("CopyItem"))
                        {
                            cpItem.Visibility = Visibility.Visible;
                        }

                        if (item is MenuItem cItem && cItem.Name.Equals("CutItem"))
                        {
                            cItem.Visibility = Visibility.Visible;
                        }

                        if (item is MenuItem deItem && deItem.Name.Equals("DeleteItem"))
                        {
                            deItem.Visibility = Visibility.Visible;
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

                        if (item is MenuItem mtItem && mtItem.Name.Equals("AddTestCase"))
                        {
                            mtItem.Visibility = Visibility.Visible;
                        }

                        if (item is MenuItem reItem && reItem.Name.Equals("reNameItem"))
                        {
                            reItem.Visibility = Visibility.Visible;
                        }

                        if (item is MenuItem cpItem && cpItem.Name.Equals("CopyItem"))
                        {
                            cpItem.Visibility = Visibility.Visible;
                        }

                        if (item is MenuItem cItem && cItem.Name.Equals("CutItem"))
                        {
                            cItem.Visibility = Visibility.Visible;
                        }

                        if (item is MenuItem deItem && deItem.Name.Equals("DeleteItem"))
                        {
                            deItem.Visibility = Visibility.Visible;
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
                this.TestsuiteId.Text = node.OrderId;
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
                this.TestcaseId.Text = tcModel.OrderId;
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

                ExecuteResult erModel = tcm.QueryResultById(node.Id, node.RootId);
                if(erModel != null)
                {
                    this.TestCaseExecuteResult.SelectedItem = erModel.result;
                    this.TestCaseExecutor.Content = erModel.CreateBy;
                    this.TestCaseExecuteTime.Content = erModel.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
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

        [Obsolete]
        private void AddTestSuite_Click(object sender, RoutedEventArgs e)
        {
            TreeNodeModel stnm = this.TreeView.SelectedItem as TreeNodeModel;

            if (stnm != null)
            {
                TreeNodeModel tnm = new TreeNodeModel();
                string guid = System.Guid.NewGuid().ToString();
                tnm.Id = guid;
                tnm.OrderId = guid;
                tnm.Depth = stnm.Depth + 1;
                tnm.NodeType = NodeType.TestSuite;
                tnm.CreateTime = DateTime.Now;
                tnm.UpdateTime = DateTime.Now;
                tnm.DataBody = guid;

                stnm.Nodes.Add(tnm);

                TreeNodeMapper tcm = new TreeNodeMapper();
                tcm.InsertTreeNode(tnm, stnm);
            }
            else
            {
                TreeNodeModel tnm = new TreeNodeModel();
                string guid = System.Guid.NewGuid().ToString();
                tnm.Id = guid;
                tnm.OrderId = guid;
                tnm.Depth = 0;
                tnm.NodeType = NodeType.TestSuite;
                tnm.CreateTime = DateTime.Now;
                tnm.UpdateTime = DateTime.Now;
                tnm.DataBody = guid;
                
                TreeNodeMapper tcm = new TreeNodeMapper();
                tcm.InsertTreeNode(tnm);

                this.TreeView.Items.Add(tcm);
            }
            this.TreeView.Items.Refresh();
        }

        [Obsolete]
        private void AddTestCase_Click(object sender, RoutedEventArgs e)
        {
            TreeNodeModel stnm = this.TreeView.SelectedItem as TreeNodeModel;
            
            TestCaseModel tcm = new TestCaseModel();
            string guid = System.Guid.NewGuid().ToString();
            tcm.Id = guid;
            tcm.OrderId = guid;
            tcm.Name = guid;

            TreeNodeModel tnm = new TreeNodeModel();
            tnm.Id = guid;
            tnm.OrderId = guid;
            tnm.DataBody = guid;
            tnm.NodeType = NodeType.TestCase;
            tnm.Depth = stnm.Depth + 1;

            stnm.Nodes.Add(tnm);

            TestCaseMapper tcmer = new TestCaseMapper();
            tcmer.InsertTestCase(tcm, stnm);

            this.TreeView.Items.Refresh();
        }

        private Dictionary<string, string> PorpertiesMap = new Dictionary<string, string>
        {
            {"TestsuiteId", "ORDERID"},
            {"TestcaseId", "ORDERID"},
            {"TestsuiteName", "DATA_BODY"},
            {"TestsuiteNote", "REMARK"},
            {"TestcaseName", "NAME"},
            {"TestcaseSummary", "SUMMARY"},
            {"TestcasePrecondition", "PRECONDITION"},
            {"TestCaseImportance", "IMPORTANCE"},
            {"TestCaseType", "TYPE"}
        };

        /// <summary>
        /// 临时存储Id
        /// </summary>
        private string _stayId;

        [Obsolete]
        private void Property_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (string.IsNullOrEmpty(tb.Text))
            {
                return;
            }
            TreeNodeModel tnm = this.TreeView.SelectedItem as TreeNodeModel;
            if (tnm != null)
            {
                // 为了当修改属性后，树刷新后，选中的节点没有；不能再次继续编辑；股临时存储对应id
                _stayId = tnm.Id;
                switch (tb.Name)
                {
                    case "TestsuiteName":
                        tnm.DataBody = tb.Text;
                        break;
                    case "TestcaseName":
                        tnm.DataBody = tb.Text;
                        break;
                    default:
                        break;
                }
            }

            if (tb.Name.Equals("TestsuiteId") || tb.Name.Equals("TestsuiteName"))
            {
                TreeNodeMapper tnmm = new TreeNodeMapper();
                tnmm.UpdateTreeNodeProperty(PorpertiesMap[tb.Name], tb.Text, _stayId);
            }
            else
            {
                TestCaseMapper tcm = new TestCaseMapper();
                tcm.UpdateTestCaseProperty(PorpertiesMap[tb.Name], tb.Text, _stayId);
            }
            this.TreeView.Items.Refresh();
            
        }

        [Obsolete]
        private void DeleteItem_OnClick(object sender, RoutedEventArgs e)
        {
            TreeNodeModel tnm = this.TreeView.SelectedItem as TreeNodeModel;
            
            if (tnm != null && tnm.NodeType == NodeType.TestSuite)
            {
                TreeNodeMapper tnmm = new TreeNodeMapper();
                tnmm.DeleteTreeNode(tnm.Id);
            }

            if (tnm != null && tnm.NodeType == NodeType.TestCase)
            {
                TestCaseMapper testCaseMapper = new TestCaseMapper();
                testCaseMapper.DeleteTestCase(tnm.Id);
            }

            List<TreeNodeModel> tnmList = (List<TreeNodeModel>) this.TreeView.ItemsSource;
            TreeController tc = new TreeController(tnmList);
            tc.DeleteTreeNode(tnm);
        }

        [Obsolete]
        private void ReNameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            var tempTB = FindVisualChild<TextBox>(this.item as DependencyObject);
            var tempTxB = FindVisualChild<TextBlock>(this.item as DependencyObject);
            tempTxB.Text = tempTB.Text;
            tempTB.Visibility = Visibility.Collapsed;
            tempTxB.Visibility = Visibility.Visible;

            TreeNodeModel tnm = this.TreeView.SelectedItem as TreeNodeModel;
            if (tnm.NodeType == NodeType.TestSuite)
            {
                TreeNodeMapper tnmm = new TreeNodeMapper();
                tnmm.UpdateTreeNodeProperty("DATA_BODY", tempTB.Text, tnm.Id);
            }
            else
            {
                TestCaseMapper tcm = new TestCaseMapper();
                tcm.UpdateTestCaseProperty("NAME", tempTB.Text, tnm.Id);
            }


        }

        private void ReNameItem_Click(object sender, RoutedEventArgs e)
        {
            var tempTB = FindVisualChild<TextBox>(this.item as DependencyObject);
            var tempTxB = FindVisualChild<TextBlock>(this.item as DependencyObject);
            tempTxB.Visibility = Visibility.Collapsed;
            tempTB.Visibility = Visibility.Visible;
        }

        //获取ItemTemplate内部的各种控件
        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        //获取当前TreeView的TreeViewItem
        public TreeViewItem GetParentObjectEx<TreeViewItem>(DependencyObject obj) where TreeViewItem : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent is TreeViewItem)
                {
                    return (TreeViewItem)parent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        private void TreeView_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //此处item定义的是一个类的成员变量，是一个TreeViewItem类型
            this.item = GetParentObjectEx<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (this.item != null)
            {
                //使当前节点获得焦点
                this.item.Focus();

                //系统不再处理该操作
                e.Handled = true;
            }
        }

        private delegate void OutputMsg(string msg, Color col);

        private void OutputRichTextBox(string msg, Color color)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new OutputMsg(OutputRichTextBox), new object[]{msg, color});
            }
            else
            {
                Run appendText = new Run {Text = $"{DateTime.Now.ToString("u")} {msg} {Environment.NewLine}", Foreground = new SolidColorBrush(color) };
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(appendText);

                this.outputRTB.Document.Blocks.Add(paragraph);
                this.outputRTB.UpdateLayout();
            }
        }

        [Obsolete]
        private void TestCaseExecuteResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 树节点选中变更，会触发该事件
            if (e.RemovedItems.Count > 0 && e.Source is ComboBox)
            {
                TreeNodeModel tnModel = this.TreeView.SelectedItem as TreeNodeModel;

                ExecuteResult erModel = new ExecuteResult();
                erModel.result = (ExecuteResultType)TestCaseExecuteResult.SelectedIndex;
                // TODO 未创建用户权限
                erModel.CreateBy = "TODO";

                TestCaseMapper tcm = new TestCaseMapper();
                tcm.InsertTestCaseExecuteResult(tnModel, erModel);
                
            }
        }
    }
}
