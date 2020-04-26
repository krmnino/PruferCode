using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruferCode
{
    class Tree
    {
        private Node root;
        public Tree()
        {
            this.root = new Node();
        }
        public Tree(int data)
        {
            this.root = new Node(data);
        }
        public Tree(Node root_)
        {
            this.root = root_;
        }
    }
}

