using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMER.Model
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNodeModel
    {
        public TreeNodeModel()
        {
            this.Nodes = new ObservableCollection<TreeNodeModel>();
        }
        /// <summary>
        /// 树节点ID
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// 树节点内容
        /// </summary>
        public String DataBody { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public String UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 节点层级，顶级节点Depth=0，依次增加
        /// </summary>
        public int Depth { get; set; }

        public ObservableCollection<TreeNodeModel> Nodes { get; set; }
    }
}
