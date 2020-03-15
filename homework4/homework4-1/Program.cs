using System;

namespace homework4_1
{
    class Program
    {
        public class Node<T>
        {
            public Node<T> Next { get; set; }
            public T Data { get; set; }
            public Node(T t)
            {
                Next = null;
                Data = t;
            }
        }
        public class List<T>
        {
            private Node<T> head;
            private Node<T> tail;
            public List()
            {
                tail = head = null;
            }
            public Node<T> Head
            {
                get => head;
            }
            public void Add(T t)
            {
                Node<T> n = new Node<T>(t);
                if (tail == null)
                {
                    head = tail = n;
                }
                else
                {
                    tail.Next = n;
                    tail = n;
                }
            }
            public void ForEach(Action<T> action)
            {
                for (Node<T> node = head; node != null; node = node.Next)
                {
                    action(node.Data);
                }
            }

        }
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            for (int x = 0; x < 10; x++)
            {
                list.Add(x);
            }
            list.ForEach(x => Console.Write(x + " "));
            Console.WriteLine();
            int sum = 0, min = 0x7fffffff, max = 0;
            list.ForEach(x => sum += x);
            list.ForEach(x => { if (min > x) min = x; });
            list.ForEach(x => { if (max < x) max = x; });
            Console.WriteLine(sum);
            Console.WriteLine(min);
            Console.WriteLine(max);
        }
    }
}