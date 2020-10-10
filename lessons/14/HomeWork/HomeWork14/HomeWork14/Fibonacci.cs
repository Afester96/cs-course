using System.Collections;
using System.Collections.Generic;

namespace HomeWork14
{
    public class Fibonacci : IEnumerable<int>
    {
        private readonly int _numbers;
        
        public Fibonacci(int numbers)
        {
            _numbers = numbers;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new Test2Enumerator(_numbers);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    class Test2Enumerator : IEnumerator<int>
    {
        private readonly int _numbers;
        private int _first;
        private int _second;
        private int _count;
        public Test2Enumerator(int numbers)
        {
            _numbers = numbers;
            Reset();
        }
        public bool MoveNext()
        {
            return ++_count <= _numbers;
        }

        public void Reset()
        {
            _count = 0;
            _first = 0;
            _second = 1;
        }

        public void Dispose()
        {
        }

        public int Current
        {
            get
            {
                var firstTry = _first;
                _first = _second;
                _second = firstTry + _second;

                return _second - _first;
            }
        }

        object IEnumerator.Current => Current;
    }
}

