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
    public class Test2 : IEnumerable
    {
        private readonly int _numbers;
        public Test2(int numbers)
        {
            _numbers = numbers;
        }
        private int _first;
        private int _second;
        private int _count;
        public bool MoveNext()
        {
            return ++_count <= _numbers;
        }
        public object Current 
        {
            get
            {
                _first = 0;
                _second = 1;
                var firstTry = _first;
                _first = _second;
                _second = firstTry + _second;

                return _second - _first;
            } 
        }
        public void Reset()
        {
            _count = 0;
        }
        public IEnumerator GetEnumerator()
        {
            return new Test2(_numbers);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
