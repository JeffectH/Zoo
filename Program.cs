using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EnclosureFactory enclosureFactory = new EnclosureFactory();
            ZooFactory zooFactory = new ZooFactory();

            Zoo zoo = zooFactory.CreateZoo(enclosureFactory);

            zoo.Work();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public Zoo(List<Aviary> aviaries)
        {
            _aviaries = aviaries;
        }

        public void Work()
        {
            bool _isOpen = true;

            while (_isOpen)
            {
                Console.Clear();

                Console.WriteLine("Вы в зоопарке. Какой вальер хотите посетить?\n");
                ShowAviary();
                ChoiceAviary(ref _isOpen);
            }
        }

        private void ChoiceAviary(ref bool isOpen)
        {
            Console.Write("\n\nВаш выбор:");

            if (int.TryParse(Console.ReadLine(), out int choiceUser))
            {
                if (choiceUser >= 1 && choiceUser <= _aviaries.Count)
                {
                    _aviaries[choiceUser - 1].ShowInfo();
                    Console.ReadKey();
                }
                else if (choiceUser == _aviaries.Count + 1)
                {
                    isOpen = false;
                }
                else
                {
                    PrintIncorrectDataMessage();
                }
            }
            else
            {
                PrintIncorrectDataMessage();
            }
        }

        private void PrintIncorrectDataMessage()
        {
            Console.WriteLine("Введены некорректные данные!");
            Console.ReadKey();
        }

        private void ShowAviary()
        {
            for (int i = 0; i < _aviaries.Count; i++)
                Console.WriteLine($"{i + 1}.Вальер");

            Console.Write($"\n{_aviaries.Count + 1}.Выход");
        }
    }

    class Aviary
    {
        private string _name;
        private List<Animal> _animals = new List<Animal>();

        public Aviary(string name, List<Animal> animals)
        {
            _name = name;
            _animals = animals;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Вальер: {_name}");
            Console.WriteLine($"Колличество животных: {_animals.Count}");

            for (int i = 0; i < _animals.Count; i++)
            {
                Console.WriteLine(i + 1 + " " + _animals[i].Gender + "-" + _animals[i].Sound);
            }
        }
    }

    class Animal
    {
        public Animal(string name, string gender, string sound)
        {
            Name = name;
            Gender = gender;
            Sound = sound;
        }

        public string Name { get; private set; }
        public string Gender { get; private set; }
        public string Sound { get; private set; }

        public Animal Clone()
        {
            return new Animal(Name, Gender, Sound);
        }
    }

    class EnclosureFactory
    {
        private int _minNumberAnimals = 1;
        private int _maxNumberAnimals = 10;
        private Random _random = new Random();

        public Aviary CreateLionEnclosure(string gender)
        {
            var animal = new Animal("Лев", gender, "Рев");
            return CreateEnclosure("Вольер со львами", animal);
        }

        public Aviary CreateElephantEnclosure(string gender)
        {
            var animal = new Animal("Слон", gender, "Труба");
            return CreateEnclosure("Вольер со слонами", animal);
        }

        public Aviary CreateMonkeyEnclosure(string gender)
        {
            var animal = new Animal("Обезьяна", gender, "У-у");
            return CreateEnclosure("Вольер с обезьянами", animal);
        }

        public Aviary CreateGiraffeEnclosure(string gender)
        {
            var animal = new Animal("Жираф", gender, "Гул");
            return CreateEnclosure("Вольер с жирафами", animal);
        }

        private Aviary CreateEnclosure(string enclosureName, Animal baseAnimal)
        {
            var animals = new List<Animal>();

            int cloneCount = _random.Next(_minNumberAnimals, _maxNumberAnimals + 1);
            for (int i = 0; i < cloneCount; i++)
            {
                animals.Add(baseAnimal.Clone());
            }

            return new Aviary(enclosureName, animals);
        }
    }

    class ZooFactory
    {
        public Zoo CreateZoo(EnclosureFactory enclosureFactory)
        {
            var enclosures = new List<Aviary>
        {
            enclosureFactory.CreateLionEnclosure("Мужской"),
            enclosureFactory.CreateElephantEnclosure("Мужской"),
            enclosureFactory.CreateMonkeyEnclosure("Женский"),
            enclosureFactory.CreateGiraffeEnclosure("Мужской"),
        };
            return new Zoo(enclosures);
        }
    }

}
