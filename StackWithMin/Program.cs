using System;

namespace StackWithMin
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] data = new int[] { 5, 6, 3, 2, 5, 5, 7, 8, 1 };
            int[] expectedMins = new int[] { 5, 5, 3, 2, 2, 2, 2, 2, 1 };

            Console.WriteLine("Testing single stack with min data");
            IStack<int> stack = new StackWithMin<int>();
            Test(stack, data, expectedMins);
            Console.WriteLine("Success");

            Console.WriteLine("Testing double stack");
            IStack<int> stack2 = new DoubleStack<int>();
            Test(stack2, data, expectedMins);
            Console.WriteLine("Success");

            Console.WriteLine("Code completed successfully");
        }

        static void Test<T>(IStack<T> stack, T[] data, T[] expectedMins) where T : IComparable<T>
        {
            Console.WriteLine("Testing push");
            for (int i = 0; i < data.Length; i++)
            {
                stack.Push(data[i]);
                if (!stack.Min.Equals(expectedMins[i])) throw new Exception($"Calculated wrong min. Expected {expectedMins[i]}, Actual {stack.Min}. i = {i}");
            }

            Console.WriteLine("Testing pop");
            for (int i = expectedMins.Length-1; i >= 0; i--)
            {
                if (!stack.Min.Equals(expectedMins[i])) throw new Exception($"Calculated wrong min. Expected {expectedMins[i]}, Actual {stack.Min}. i = {i}");
                var tmp = stack.Pop();
            }
        }
    }

    interface IStack<T> where T : IComparable<T>
    {
        void Push(T value);
        T Pop();
        T Min { get; }
    }

    class StackWithMin<T> : IStack<T> where T : IComparable<T>
    {
        Node head;
        public void Push(T value)
        {
            if (head == null)
            {
                head = new Node(value, value);
                return;
            }

            var newMin = value.CompareTo(head.Min) < 0 ? value : head.Min;
            var node = new Node(value, newMin);

            node.Next = head;
            head = node;
        }

        public T Pop()
        {
            var tmp = head.Data;
            head = head.Next;

            return tmp;
        }

        public T Min => head.Min;

        class Node
        {
            public T Min { get; }
            public T Data { get; }

            public Node Next { get; set; }

            public Node(T data, T min)
            {
                this.Data = data;
                this.Min = min;
            }
        }
    }

    class DoubleStack<T> : IStack<T> where T : IComparable<T>
    {
        Node head;
        MinNode minHead;

        public T Min => minHead.Min;

        public T Pop()
        {
            if (--minHead.Count == 0)
            {
                minHead = minHead.Next;
            }

            var tmp = head.Data;
            head = head.Next;

            return tmp;
        }

        public void Push(T value)
        {
            if (head == null)
            {
                head = new Node(value);
                minHead = new MinNode(value);
                return;
            }

            var comparison = minHead.Min.CompareTo(value);

            if (comparison <= 0)
            {
                minHead.Count++;
            }
            else if (comparison > 0)
            {
                var newMin = new MinNode(value);
                newMin.Next = minHead;
                minHead = newMin;
            }

            var node = new Node(value);
            node.Next = head;
            head = node;
        }

        class Node
        {
            public T Data { get; }
            public Node Next { get; set; }
            public Node(T data)
            {
                this.Data = data;
            }
        }

        class MinNode
        {
            public int Count { get; set; } = 1;
            public T Min { get; set; }

            public MinNode Next { get; set; }

            public MinNode(T min)
            {
                this.Min = min;
            }
        }
    }
}
