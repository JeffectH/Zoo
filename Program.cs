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

        public Aviary CreateEnclosure(string animalType, string gender)
        {
            var animalInfo = _animalDictionary[animalType];
            var baseAnimal = new Animal(animalInfo.name, gender, animalInfo.sound);
            return CreateEnclosure(animalInfo.enclosureName, baseAnimal);
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

        private readonly Dictionary<string, (string name, string sound, string enclosureName)> _animalDictionary =
            new Dictionary<string, (string name, string sound, string enclosureName)>
            {
                { "Lion", ("Лев", "Рев", "Вольер со львами") },
                { "Elephant", ("Слон", "Труба", "Вольер со слонами") },
                { "Monkey", ("Обезьяна", "Визг", "Вольер с обезьянами") },
                { "Giraffe", ("Жираф", "Гул", "Вольер с жирафами") }
            };
    }

    class ZooFactory
    {
        public Zoo CreateZoo(EnclosureFactory enclosureFactory)
        {
            var animalTypes = new List<string> { "Lion", "Elephant", "Monkey", "Giraffe" };
            var genders = new List<string> { "Мужской", "Женский" };

            var enclosures = new List<Aviary>();

            foreach (var animalType in animalTypes)
            {
                var gender = genders[new Random().Next(genders.Count)];
                enclosures.Add(enclosureFactory.CreateEnclosure(animalType, gender));
            }
            return new Zoo(enclosures);
        }
    }
}