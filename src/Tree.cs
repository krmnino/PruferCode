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
        public Node Root{get => root; set => root = value; }
        
        public List<int> BFS_traversal()
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

