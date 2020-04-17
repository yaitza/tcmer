using System.Collections.Generic;
using System.Linq;
using TCMER.Model;

namespace TCMER.Controller
{
    public class TreeController
    {
        private List<TreeNodeModel> TnmList;

        public TreeController(List<TreeNodeModel> tnmList)
        {
            this.TnmList = tnmList;
        }


        public void DeleteTreeNode(TreeNodeModel tnm)
        {
            foreach (TreeNodeModel nodeModel in TnmList)
            {
                if (tnm.Id.Equals(nodeModel.Id))
                {
                    TnmList.Remove(nodeModel);
                }
                else
                {
                    DelTreeNode(nodeModel, tnm);
                }
            }
        }

        private void DelTreeNode(TreeNodeModel source, TreeNodeModel target)
        {
            if (source.Nodes.Count != 0)
            {
                int index = -1;
                foreach (TreeNodeModel node in source.Nodes)
                {
                    if (node.Id.Equals(target.Id))
                    {
                        index = source.Nodes.IndexOf(node);
                        //source.Nodes.Remove(node);
                    }
                    else
                    {
                        DelTreeNode(node, target);
                    }
                }

                if (index != -1)
                {
                    source.Nodes.RemoveAt(index);
                }
            }
            else
            {
                return;
            }
        }
    }
}