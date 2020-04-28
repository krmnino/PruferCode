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
        public Tree(Node root_)
        {
            this.root = root_;
        }
        public Tree(List<Tuple<int, int>> table)
        {
            //This function will not add any nodes with no direct path to the root
            this.root = new Node(table[0].Item1);
            Node curr;
            Queue<Node> node_queue = new Queue<Node>();
            node_queue.Enqueue(root);
            while (node_queue.Count != 0)
            {
                curr = node_queue.Dequeue();
                for (int i = 1; i < table.Count; i++)
                {
                    if (table[i].Item2 == curr.Data)
                    {
                        curr.AddChild(table[i].Item1);
                    }

                }
                if (curr.Children.Count != 0)
                {
                    curr.SortChildren();
                    for (int i = 0; i < curr.Children.Count; i++)
                    {
                        node_queue.Enqueue(curr.Children[i]);
                    }
                }
            }
        }
        public Node Root{get => root; set => root = value; }
        
        public List<int> BFTraversal()
        {
            List<int> data_nodes = new List<int>();
            Queue<Node> node_queue = new Queue<Node>();
            List<Node> curr_children;
            node_queue.Enqueue(this.root);
            data_nodes.Add(this.root.Data);
            while(node_queue.Count != 0)
            {
                curr_children = node_queue.Dequeue().Children;
                for(int i = 0; i < curr_children.Count; i++)
                {
                    node_queue.Enqueue(curr_children[i]);
                    data_nodes.Add(curr_children[i].Data);
                }
            }
            return data_nodes;
        }
    }
}

