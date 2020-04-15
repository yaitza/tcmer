﻿using System;
using System.Windows;
using System.Windows.Controls;
using TCMER.Dao;
using TCMER.Model;

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

        [Obsolete]
        private void AddTestSuite_Click(object sender, RoutedEventArgs e)
        {
            TreeNodeModel stnm = this.TreeView.SelectedItem as TreeNodeModel;

            if (stnm != null)
            {
                TreeNodeModel tnm = new TreeNodeModel();
                string guid = System.Guid.NewGuid().ToString();
                tnm.Id = guid;
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
            tcm.Name = guid;

            TreeNodeModel tnm = new TreeNodeModel();
            tnm.Id = guid;
            tnm.DataBody = guid;
            tnm.NodeType = NodeType.TestCase;
            tnm.Depth = stnm.Depth + 1;

            stnm.Nodes.Add(tnm);

            TestCaseMapper tcmer = new TestCaseMapper();
            tcmer.InsertTestCase(tcm, stnm);

            this.TreeView.Items.Refresh();
        }
    }
}
