﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruferCode
{
    class Node
    {
        private Node parent;
        private List<Node> children;
        private int data;
        public Node()
        {
            this.children = new List<Node>();
        }
        public Node(int data_)
        {
            this.parent = null;
            this.data = data_;
            this.children = new List<Node>();
        }
        public Node(Node parent_, int data_)
        {
            this.parent = parent_;
            this.data = data_;
            this.children = new List<Node>();
        }
        public int Data { get => this.data; set => this.data = value; }
        public Node Parent { get => this.parent; set => this.parent = value; }
        public List<Node> Children { get => this.children; set => this.children = value; }
        public void AddChild(Node new_node)
        {
            this.children.Add(new_node);
        }
        public void DisplayData()
        {
            Console.WriteLine(this.data); 
        }
        public void SortChildren()
        {
            if(this.Children.Count == 0)
            {
                return;
            }
            //Selection Sort
            for (int i = 0; i < this.Children.Count; i++)
            {
                int min_index = i;
                for (int j = this.Children.Count - 1; j > i; j--)
                {
                    if (this.Children[j].Data < this.Children[min_index].Data)
                    {
                        min_index = j;
                    }
                }
                Node temp = this.Children[i];
                this.Children[i] = this.Children[min_index];
                this.Children[min_index] = temp;
            }
        }

        public override string ToString()
        {
            return this.data.ToString();
        }
    }
}
