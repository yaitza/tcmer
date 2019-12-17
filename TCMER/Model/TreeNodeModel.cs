using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMER.Model
{
    public class TreeNodeModel
    {
        public String Id { get; set; }

        public String DataBody { get; set; }

        public String CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public String UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }

        public List<TreeNodeModel> Children { get; set; }
    }
}
