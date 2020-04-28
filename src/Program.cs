using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruferCode
{
    class Program
    {
        static List<Tuple<int, int>> InputParser()
        {
            List<Tuple<int, int>> child_parent_table = new List<Tuple<int, int>>();
            String input = Console.ReadLine();
            int index_end = input.IndexOf(";");
            int child;
            int parent;
            bool root_set = false;
            while (true)
            {
                string parent_child = input.Substring(0, index_end);
                while(parent_child[0] == ' ')
                {
                    parent_child = parent_child.Substring(1);
                }
                while (parent_child[parent_child.Length-1] == ' ')
                {
                    parent_child = parent_child.Substring(0, parent_child.Length - 1);
                }
                child = Int32.Parse(parent_child.Substring(0, parent_child.IndexOf(" ")));
                parent = Int32.Parse(parent_child.Substring(parent_child.IndexOf(" ") + 1));
                if (!root_set)
                {
                    if(parent != 0)
                    {
                        Console.WriteLine("Root node is not [0].");
                        return new List<Tuple<int, int>>();
                    }
                    child_parent_table.Add(Tuple.Create(parent, parent));
                    root_set = true;
                }
                for(int i = 0; i < child_parent_table.Count; i++)
                {
                    if(child == child_parent_table[i].Item1)
                    {
                        Console.WriteLine(child + " is duplicated.");
                        return new List<Tuple<int, int>>();
                    }
                }
                child_parent_table.Add(Tuple.Create(child, parent));
                input = input.Substring(index_end + 1);
                index_end = input.IndexOf(";");
                if (index_end == -1)
                {
                    break;
                }
            }
            for (int i = 1; i < child_parent_table.Count; i++)
            {
                for (int j = 1; j < child_parent_table.Count; j++)
                {
                    if (child_parent_table[j].Item1 == i)
                    {
                        break;
                    }
                    if (j == child_parent_table.Count - 1)
                    {
                        Console.WriteLine("Sequence incomplete. Node [" + i + "] is missing.");
                        return new List<Tuple<int, int>>();
                    }
                    if (child_parent_table[j].Item1 >= child_parent_table.Count)
                    {
                        Console.WriteLine("Node [" + child_parent_table[j].Item1 + "] is equal or greater than the upper bound for [" + child_parent_table.Count + "].");
                        return new List<Tuple<int, int>>();
                    }
                }
            }
            return child_parent_table;
        }
        static void Main(string[] args)
        {
            Tree t = new Tree(new Node(0));
            List<Tuple<int, int>> input_table = InputParser();
            List<int> data = t.BFTraversal();
            Tree t2 = new Tree(input_table);
            Node smallest = t2.MinLeaf();
        }
    }
}
 