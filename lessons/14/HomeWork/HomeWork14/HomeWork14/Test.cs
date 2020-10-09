using System.Collections;

namespace HomeWork14
{
    public class Test : IEnumerable
    {
        private readonly int _numbers;
        public Test(int numbers)
        {
            _numbers = numbers;
        }
        public IEnumerator GetEnumerator()
        {
            int first = 0;
            int second = 1;
            int count = 0;

            while (count <= _numbers)
            {
                var firstTry = first;
                first = second;
                second = firstTry + second;
                ++count;
                yield return second - first;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
