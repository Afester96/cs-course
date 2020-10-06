using System;
using System.Collections;
using System.Collections.Generic;

namespace HomeWork14
{
    class Program
    {
        static void Main(string[] args)
        {
            //var test = new Test(20);
            var test = new Test2(10);

            foreach (var i in test)
            {
                Console.WriteLine(i);
            }
        }
    }
    //public class Test : IEnumerable
    //{
    //    private readonly int _numbers;
    //    public Test(int numbers)
    //    {
    //        _numbers = numbers;
    //    }
    //    public IEnumerator GetEnumerator()
    //    {
    //        int first = 0;
    //        int second = 1;
    //        int count = 0;

    //        while (count <= _numbers)
    //        {
    //            var firstTry = first;
    //            first = second;
    //            second = firstTry + second;
    //            ++count;

    //            yield return second - first;
    //        }
    //    }
    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
    public class Test2 : IEnumerable<int>
    {
        private readonly int _numbers;
        private int _first;
        private int _second;
        private int _count;
        public Test2(int numbers)
        {
            _numbers = numbers;
            Reset();
        }        
        public bool MoveNext()
        {
            return ++_count <= _numbers;
        }
        public object Current 
        {
            get
            {
                var firstTry = _first;
                _first = _second;
                _second = firstTry + _second;

                return _second - _first;
            } 
        }
        public void Reset()
        {
            _count = 0;
            _first = 0;
            _second = 1;
        }
        
        public IEnumerator<int> GetEnumerator()
        {
            return new Test2(this);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
