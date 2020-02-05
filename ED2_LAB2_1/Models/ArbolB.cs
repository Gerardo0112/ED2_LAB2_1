using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_LAB2_1.Models
{
    public class ArbolB
    {
        public Nodo Root { get; set; }
        public ArbolB()
        {
            Root = null;
        }
        public void Insert(int value)
        {
            if (Root == null)
            {
                Root = new Nodo(value);
                return;
            }
            Nodo actual = Root;
            Nodo father = null;

            while (actual != null)
            {
                if (actual.keys.Count == 5)
                {
                    if (father == null)
                    {
                        int x = actual.Pop(1).Value;
                        Nodo NewRoot = new Nodo(x);
                        Nodo[] newNodos = actual.Split();
                        NewRoot.InsertEdge(newNodos[0]);
                        NewRoot.InsertEdge(newNodos[1]);
                        NewRoot.InsertEdge(newNodos[2]);
                        NewRoot.InsertEdge(newNodos[3]);
                        Root = NewRoot;
                    }
                    else
                    {
                        int? x = actual.Pop(1);
                        if (x != null)
                        {
                            father.Push(x.Value);
                        }
                        Nodo[] nNodos = actual.Split();
                        int pos1 = FindEdgePosition(nNodos[1].keys[0]);
                        father.InsertEdge(nNodos[1]);

                        int Actualpos = father.FindEdgeosition(value);
                        actual = father.GetEdge(Actualpos);
                    }
                }
                father = actual;
                actual = actual.Traverse(value);
                if (actual == null)
                {
                    father.Push(value);
                }
            }
        }
        public Nodo Find(int x)
        {
            Nodo c = Root;

            while (c != null)
            {
                if (c.Key(x) >= 0)
                {
                    return c;
                }
                else
                {
                    int p = c.FindEdgePosition(x);
                    c = c.GetEdge(p);

                }
            }
            return null;
        }
        public Nodo Min(Nodo n = null)
        {
            if (n == null)
            {
                n = Root;
            }
            Nodo c = n;
            if (c != null)
            {
                while (c.edges.Count > 0)
                {
                    c = c.edges[0];
                }
            }
            return c;
        }
        public int[] Inorder(Nodo n = null)
        {
            if (n == null)
            {
                n = Root;
            }
            List<int> items = new List<int>();
            Tuple<Nodo, int> c = new Tuple<Nodo, int>(n, 0);
            Stack<Tuple<Nodo, int>> stack = new Stack<Tuple<Nodo, int>>();
            while (stack.Count > 0 || c.Item1 != null)
            {
                if (c.Item1 != null)
                {
                    stack.Push(c);
                    //Mueve a la izquierda
                    Nodo left_child = c.Item1.GetEdge(c.Item2);
                    c = new Tuple<Nodo, int>(left_child, 0);
                }
                else
                {
                    c = stack.Pop();
                    Nodo cNodo = c.Item1;

                    //Cada nodo puede tener mas hijos, en el indice siempre se querrá agregar la llave a la lista.
                    ///De lo contrario solo pasar por los hijos.
                    if (c.Item2 < cNodo.keys.Count)
                    {
                        items.Add(cNodo.keys[c.Item2]);
                        c = new Tuple<Nodo, int>(cNodo, c.Item2 + 1);
                    }
                    else
                    {
                        //Obtencion del hijo derecho.
                        Nodo right_child = cNodo.GetEdge(c.Item2 + 1);

                        c = new Tuple<Nodo, int>(right_child, c.Item2 + 1);
                    }
                }
            }
            return items.ToArray();
        }
    }
}
