using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var zoo = ZooFactory.CreateZoo();
            zoo.Work();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;
        private bool _isOpen = true;

        public Zoo(List<Aviary> aviaries)
        {
            _aviaries = aviaries;
        }

        public void Work()
        {
            while (_isOpen)
            {
                Console.Clear();

                Console.WriteLine("Вы в зоопарке. Какой вальер хотите посетить?\n");
                ShowAviary();
                ChoiceAviary();
            }
        }

        private void ChoiceAviary()
        {
            Console.Write("\nВаш выбор:");

            if (int.TryParse(Console.ReadLine(), out int choiceUser))
            {
                if (choiceUser >= 1 && choiceUser <= _aviaries.Count)
                {
                    _aviaries[choiceUser - 1].ShowInfo();
                    Console.ReadKey();
                }
                else if (choiceUser == _aviaries.Count + 1)
                {
                    _isOpen = false;
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
                Console.WriteLine($"{i + 1}.Вальер " + (i + 1));

            Console.Write($"{_aviaries.Count + 1}. Выход");

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
    }

    class EnclosureFactory
    {
        public static Aviary CreateLionEnclosure()
        {
            var animals = new List<Animal>
        {
            new Animal("Лев", "Мужской", "Рев"),
            new Animal("Лев", "Мужской", "Рев"),
            new Animal("Львица", "Женский", "Рев"),
            new Animal("Львица", "Женский", "Рев")
        };
            return new Aviary("Вольер со львами", animals);
        }

        public static Aviary CreateElephantEnclosure()
        {
            var animals = new List<Animal>
        {
            new Animal("Слон", "Мужской", "Труба"),
        };
            return new Aviary("Вольер со слонами", animals);
        }

        public static Aviary CreateMonkeyEnclosure()
        {
            var animals = new List<Animal>
        {
            new Animal("Обезьяна", "Мужской", "У-у"),
            new Animal("Обезьяна", "Мужской", "У-у"),
            new Animal("Обезьяна", "Женский", "А-а")
        };
            return new Aviary("Вольер с обезьянами", animals);
        }

        public static Aviary CreateGiraffeEnclosure()
        {
            var animals = new List<Animal>
        {
            new Animal("Жираф", "Мужской", "Гул"),
            new Animal("Жирафа", "Женский", "Гул"),
            new Animal("Жирафа", "Женский", "Гул"),
            new Animal("Жирафа", "Женский", "Гул")
        };
            return new Aviary("Вольер с жирафами", animals);
        }
    }


    class ZooFactory
    {
        public static Zoo CreateZoo()
        {
            var enclosures = new List<Aviary>
        {
            EnclosureFactory.CreateLionEnclosure(),
            EnclosureFactory.CreateElephantEnclosure(),
            EnclosureFactory.CreateMonkeyEnclosure(),
            EnclosureFactory.CreateGiraffeEnclosure()
        };
            return new Zoo(enclosures);
        }
    }

}
