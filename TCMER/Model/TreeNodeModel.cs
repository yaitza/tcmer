﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TCMER.Annotations;

namespace TCMER.Model
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNodeModel:INotifyPropertyChanged
    {
        public TreeNodeModel()
        {
            this.Nodes = new ObservableCollection<TreeNodeModel>();
        }
        /// <summary>
        /// 树节点ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 排序ID
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 树节点内容
        /// </summary>
        public string DataBody { get; set; }

        /// <summary>
        /// 根节点ID
        /// </summary>
        public string RootId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 节点层级，顶级节点Depth=0，依次增加
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 节点类型，测试套或者测试用例
        /// </summary>
        public NodeType NodeType { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public ObservableCollection<TreeNodeModel> Nodes { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
