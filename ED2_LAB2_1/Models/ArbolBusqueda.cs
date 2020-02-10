using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_LAB2_1.Models
{
    public class ArbolBusqueda
    {
        public NodoBusqueda Root { get; set; }
        public ArbolBusqueda()
        {
            Root = null;
        }
        public void Insert(string v)
        {
            if(Root == null)
            {
                Root = new NodoBusqueda(v);
                return;
            }
            NodoBusqueda actual = Root;
            NodoBusqueda father = null;
            while(actual != null)
            {
                if (actual.keys.Count == 5)
                {
                    if(father == null)
                    {
                        string x = actual.Pop(1);
                        NodoBusqueda NewRoot = new NodoBusqueda(x);
                        NodoBusqueda[] newNodos = actual.Split();
                        NewRoot.InsertEdge(newNodos[0]);
                        NewRoot.InsertEdge(newNodos[1]);
                        NewRoot.InsertEdge(newNodos[2]);
                        NewRoot.InsertEdge(newNodos[3]);
                        Root = NewRoot;
                        actual = NewRoot;
                    }
                    else
                    {
                        string x = actual.Pop(1);
                        if(x != null)
                        {
                            father.Push(x);
                        }
                        NodoBusqueda[] nNodos = actual.Split();
                        int pos1 = father.FindEdgePosition(nNodos[1].keys[0]);
                        father.InsertEdge(nNodos[1]);

                        int Actualpos = father.FindEdgePosition(v);
                        actual = father.GetEdge(Actualpos);
                    }
                }
                father = actual;
                actual = actual.Traverse(v);
                if(actual == null)
                {
                    father.Push(v);
                }
            }
        }
        public NodoBusqueda Find(string x)
        {
            NodoBusqueda c = Root;
            while(c != null)
            {
                if(c.Key(x) >= 0)
                {
                    return x;
                }
                else
                {
                    int p = c.FindEdgePosition(x);
                    c = c.GetEdge(p);
                }
            }
            return null;
        }
        public NodoBusqueda Min(NodoBusqueda n = null)
        {
            if(n == null)
            {
                n = Root;
            }

            NodoBusqueda c = n;
            if(c != null)
            {
                while(c.edges.Count > 0)
                {
                    c = c.edges[0];
                }
            }
            return c;
        }
        public string[] Inorder(NodoBusqueda n = null)
        {
            if(n == null)
            {
                n = Root;
            }
            int a = 0;
            List<string> items = new List<string>();
            Tuple<NodoBusqueda, int> c = new Tuple<NodoBusqueda, int>(n, a);
            Stack<Tuple<NodoBusqueda, int>> stack = new Stack<Tuple<NodoBusqueda, int>>();
            while(stack.Count > 0 || c.Item1 != null)
            {
                if(c.Item1 != null)
                {
                    stack.Push(c);
                    NodoBusqueda left_child = c.Item1.GetEdge(c.Item2);
                    c = new Tuple<NodoBusqueda, int>(left_child, a);
                }
                else
                {
                    c = stack.Pop();
                    NodoBusqueda cNodo = c.Item1;

                    if(c.Item2 < cNodo.keys.Count)
                    {
                        items.Add(cNodo.keys[0]);
                        c = new Tuple<NodoBusqueda, int>(cNodo, c.Item2 + 1);
                    }
                    else
                    {
                        NodoBusqueda right_child = cNodo.GetEdge(c.Item2 + 1);
                        c = new Tuple<NodoBusqueda, int>(right_child, c.Item2 + 1);
                    }
                }
            }
            return items.ToArray();
        }
    }

}
