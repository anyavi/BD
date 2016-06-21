using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newdivandcon
{
    class Node
    {
        public string name;
        public List<Node> children = new List<Node>();

        public Node(string name) { this.name = name; }
    }
}
