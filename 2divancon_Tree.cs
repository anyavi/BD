using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newdivandcon
{
    class Tree
    {
        Node root;
        Node current;
        Node father;

        public void Add(string name, List<string> chnames)
        {
            if (root == null)
            {
                root = new Node(name);
                foreach (string a in chnames)
                {
                    root.children.Add(new Node(a));
                }
                current = root.children[0];
                father = root;
            }
            else if ((name == "yes") || (name == "no"))
            {
                current.children.Add(new Node(name));
                current.children[0].children = null;
                foreach (Node a in father.children)
                {
                    if (a.children.Count == 0)
                    {
                        current = a;
                        break;
                    }
                }
            }
            else
            {
                current.children.Add(new Node(name));
                father = current;
                current = father.children[0];
                foreach (string a in chnames)
                    current.children.Add(new Node(a));
                father = current;
                current = current.children[0];
            }
        }


        public void Show()
        {
            Node cur = root;
            if (cur == null) Console.WriteLine("error");
            else
            {
                Round(cur);
                Console.WriteLine();
            }
        }

        private void Round(Node current)
        {

            Console.WriteLine(current.name);
            if (current.children != null)
                foreach (Node a in current.children)
                {
                    if (a != null) Round(a);
                }
        }

    }
}
