using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruferCode
{
    class TreeContainer
    {
        private List<Tree> collection;
        private int min_empty_spot;
        public List<Tree> Collection { get => collection; }

        public TreeContainer()
        {
            this.min_empty_spot = 0;
            this.collection = new List<Tree>();
            for(int i = 0; i < 10; i++)
            {
                collection.Add(null);
            }
        }

        public TreeContainer(int size)
        {
            this.min_empty_spot = 0;
            this.collection = new List<Tree>();
            for (int i = 0; i < size; i++)
            {
                collection.Add(null);
            }
        }
        public int AddTree(Tree t)
        {
            int empty_spot = this.min_empty_spot;
            this.collection[empty_spot] = t;
            this.min_empty_spot++;
            int i = this.min_empty_spot;
            while(true)
            {
                if (i >= this.collection.Count || this.collection[i] == null)
                {
                    break;
                }
                this.min_empty_spot++;
                i++;
            }
            return empty_spot;
        }
        public Tree DeleteTree(int position)
        {
            Tree out_tree = null;
            if(position < collection.Count)
            {
                out_tree = collection[position];
                collection[position] = null;
                if(position < this.min_empty_spot)
                {
                    this.min_empty_spot = position;
                }
            }
            return out_tree;
        }

        public void EmptyCollection()
        {
            for(int i = 0; i < Collection.Count; i++)
            {
                this.Collection[i] = null;
            }
        }

        public String DisplayTree(int position)
        {
            string out_str = "";
            if(this.collection[position] != null)
            {
                out_str += this.collection[position].ToString();
            }
            return out_str;
        }
        public override string ToString()
        {
            string out_str = "";
            for(int i = 0; i < collection.Count; i++)
            {
                if(collection[i] == null)
                {
                    out_str += "[NULL]";
                }
                else
                {
                    out_str += "[TREE_" + i + "]";
                }
                if(i != collection.Count - 1)
                {
                    out_str += ",";
                }
            }
            return out_str;
        }
    }
}
