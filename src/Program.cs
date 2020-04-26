using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruferCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Node root = new Node(1);
            Tree t = new Tree(root);
            root.AddChild(4);
            root.AddChild(6);
            root.AddChild(3);
            root.Children[0].AddChild(1);
            root.Children[0].AddChild(2);
            root.Children[0].AddChild(7);
            root.Children[0].Children[1].AddChild(5);
            root.Children[0].Children[1].Children[0].DisplayData();

        }
    }
}
