using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruferCode
{
    class Tree
    {
        //TYree's root
        private Node root;
        //Default constructor method
        public Tree()
        {
            this.root = new Node();
            this.root.Parent = null;
        }
        //Constructor method, set Node parameter as root
        public Tree(Node root_)
        {
            this.root = root_;
        }
        //Copy constructor, replicates tree structure
        public Tree(Tree t)
        {
            this.root = new Node(t.Root.Data);
            this.root.Parent = null;
            Queue<Node> original_tree_queue = new Queue<Node>();
            Queue<Node> copy_tree_queue = new Queue<Node>();
            original_tree_queue.Enqueue(t.Root);
            copy_tree_queue.Enqueue(this.root);
            Node curr_original;
            Node curr_copy;
            while (original_tree_queue.Count != 0)
            {
                curr_original = original_tree_queue.Dequeue();
                curr_copy = copy_tree_queue.Dequeue();
                for (int i = 0; i < curr_original.Children.Count; i++)
                {
                    original_tree_queue.Enqueue(curr_original.Children[i]);
                    Node copy_child = new Node(curr_copy, curr_original.Children[i].Data);
                    curr_copy.AddChild(copy_child);
                    copy_tree_queue.Enqueue(copy_child);
                }
            }
        }
        //Constructor method, takes child-parent relationship parent to build tree
        public Tree(List<Tuple<int, int>> table)
        {
            //This function will not add any nodes with no direct path to the root
            this.root = new Node(table[0].Item2);
            this.root.Parent = null;
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
                        Node child = new Node(curr, table[i].Item1);
                        curr.AddChild(child);
                    }

                }
                if (curr.Children.Count != 0)
                {
                    //curr.SortChildren();
                    for (int i = 0; i < curr.Children.Count; i++)
                    {
                        node_queue.Enqueue(curr.Children[i]);
                    }
                }
            }
        }
        //Constructor Method, builds tree from list containing Prufer Code
        public Tree(List<int> prufer_code)
        {
            //Create child-parent relationship table (2D list)
            List<List<int>> child_parent_table = new List<List<int>>();
            //Resize table with the size of Prufer code list + 1 for the last node to the root
            for(int i = 0; i < prufer_code.Count + 1; i++) { child_parent_table.Add(new List<int>()); }
            //Add -1 initizer for child node and the parent node from Prufer code. Add 0 for the last child-parent node
            for(int i = 0; i < child_parent_table.Count; i++)
            {
                if (i == prufer_code.Count)
                {
                    child_parent_table[i].Add(-1);
                    child_parent_table[i].Add(0);
                }
                else
                {
                    child_parent_table[i].Add(-1);
                    child_parent_table[i].Add(prufer_code[i]);
                }
            }
            //Find the smallest available node value from the child-parent relationship table
            for (int i = 0; i < child_parent_table.Count; i++)
            {
                List<int> existing_nodes = new List<int>();
                int min_available;
                for (int j = 0; j < child_parent_table.Count; j++)
                {
                    if (j < i)
                    { 
                        if (!existing_nodes.Contains(child_parent_table[j][0]))
                        {
                            existing_nodes.Add(child_parent_table[j][0]);
                        }
                    }
                    else
                    {
                        if (!existing_nodes.Contains(child_parent_table[j][1]))
                        {
                            existing_nodes.Add(child_parent_table[j][1]);
                        }
                    }
                    
                }
                for (min_available = 0; min_available < child_parent_table.Count; min_available++)
                {
                    if (!existing_nodes.Contains(min_available))
                    {
                        break;
                    }
                }
                child_parent_table[i][0] = min_available;
            }
            //Build tree from child-parent relationship table. Use last pair to create root node.
            this.root = new Node(child_parent_table[child_parent_table.Count - 1][1]);
            this.root.Parent = null;
            Node curr;
            Queue<Node> node_queue = new Queue<Node>();
            node_queue.Enqueue(root);
            while (node_queue.Count != 0)
            {
                curr = node_queue.Dequeue();
                for (int i = 0; i < child_parent_table.Count; i++)
                {
                    if (child_parent_table[i][1] == curr.Data)
                    {
                        Node child = new Node(curr, child_parent_table[i][0]);
                        curr.AddChild(child);
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

        //Tree's root accessor method
        public Node Root{get => root; set => root = value; }
        //Breadth-first traversal of n-ary tree
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
        //Returns leaf in tree with the smallest value
        public Node MinLeaf()
        {
            Node min_node = null;
            Queue<Node> node_queue = new Queue<Node>();
            List<Node> curr_children;
            node_queue.Enqueue(this.root);
            while (node_queue.Count != 0)
            {
                curr_children = node_queue.Dequeue().Children;
                for (int i = 0; i < curr_children.Count; i++)
                {
                    node_queue.Enqueue(curr_children[i]);
                    if((min_node == null || min_node.Data > curr_children[i].Data) && curr_children[i].Children.Count == 0)
                    {
                        min_node = curr_children[i];
                    }
                }
            }
            return min_node;
        }
        //Generates Tree's Prufer code 
        public List<int> PruferCode()
        {
            Tree copy = new Tree(this);
            List<int> prufer_code = new List<int>();
            while (copy.Root.Children.Count != 0)
            {
                Node min_leaf = copy.MinLeaf();
                prufer_code.Add(min_leaf.Parent.Data);
                min_leaf.Parent.Children.Remove(min_leaf);
            }
            prufer_code.RemoveAt(prufer_code.Count - 1);
            return prufer_code;
        }

        public override string ToString()
        {
            return this.root.ToString();
        }
    }
}

