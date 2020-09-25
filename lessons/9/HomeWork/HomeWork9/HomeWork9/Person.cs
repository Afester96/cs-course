using System;

namespace HomeWork9
{
    class Person
    {
        private string _name;
        private int _age;

        public string Name //Имя
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name must been not null");
                }
                else
                {
                    _name = value;
                }
            }
        }
        
        public int Age //Возраст
        {
            get { return _age; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Age must been more than 0");
                }
                else
                {
                    _age = value;
                }
            }
        }

        public int Age4Years => Age + 4; //Считает возраст через 4 года
        
        public string Info => $"Name: {Name}, Age after 4 years: {Age4Years}"; //Выводит данные
    }
}
