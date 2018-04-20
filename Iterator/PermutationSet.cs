using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PermutationSet
{
    public partial class PermutationSet
    {
        private List<int> basePermutation;
        private Dictionary<int, int> translator;
        private Node root;

        private struct Transposition
        {
            public int i1;
            public int i2;
        }

        private class Node
        {
            public List<Node> Children { get; }
            public Transposition Value { get; }
            public bool End { get; set; }

            public Node(Transposition value)
            {
                Children = new List<Node>();
                Value = value;
            }
        }

        public IEnumerator<List<int>> GetEnumerator()
        {
            return new PermutationEnumerator(root, new List<int>(basePermutation));
        }

        private class PermutationEnumerator : IEnumerator<List<int>>
        {
            Node root;
            List<int> permutation;
            List<int> basePermutation;
            List<int> result;
            IEnumerator<List<int>> enumerator;
            int s;

            public PermutationEnumerator(Node root, List<int> basePermutation)
            {
                this.root = root;
                this.basePermutation = basePermutation;
                this.permutation = new List<int>(basePermutation);
                s = -1;
            }

            public object Current => result;

            List<int> IEnumerator<List<int>>.Current => result;

            public void Dispose()
            {
                return;
            }

            public bool MoveNext()
            {

                if (s == -1)
                {
                    Switch(root.Value);
                    result = permutation;
                    s = 0;
                    if (s < root.Children.Count)
                        enumerator = new PermutationEnumerator(root.Children[s], new List<int> (permutation));
                    if (!root.End)
                    {
                        return MoveNext();
                    }
                    else
                        return true;
                }
                else if (s < root.Children.Count)
                {
                    if (enumerator.MoveNext())
                    {
                        result = enumerator.Current;
                        return true;
                    }
                    else
                    {
                        s++;
                        if (s < root.Children.Count)
                        {
                            enumerator = new PermutationEnumerator(root.Children[s], new List<int>(permutation));      
                        }
                        return MoveNext();
                    }
                }
                else
                    return false;
            }

            public void Reset()
            {
                this.permutation = new List<int>(basePermutation);
                s = -1;
            }

            private void Switch(Transposition trans)
            {
                int tmp = permutation[trans.i1 ];
                permutation[trans.i1 ] = permutation[trans.i2 ];
                permutation[trans.i2 ] = tmp;
            }
        }
    }
}
