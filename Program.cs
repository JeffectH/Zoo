using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Work();
        }
    }

    class Zoo
    {
        private const int ComandNextWalkToZoo = 1;
        private const int ComandExit = 2;

        private List<Aviary> _aviaries;
        private bool _isOpen = true;

        public Zoo()
        {
            _aviaries = new List<Aviary>()
            {
                new Aviary(5, new Animal("Тигр", "Мужской", "Ррррр")),
                new Aviary(7, new Animal("Утка", "Мужской", "Кря-кря")),
                new Aviary(7, new Animal("Овечка", "Женский", "Бе-е-е")),
                new Aviary(3, new Animal("Лощадь", "Мужской", "Иго-го"))
            };
        }

        public void Work()
        {
            while (_isOpen)
            {
                Console.Clear();

                Console.WriteLine("Вы в зоопарке. Какой вальер хотите посетить?\n");
                ShowAviary();
                ChoiceAviary();
                ProcessUserInput();
            }
        }
        private void ProcessUserInput() 
        {
            Console.Write("\nХотите дальше ходить по зоопарку или уйти? 1 - ДА 2 - НЕТ ");

            if (int.TryParse(Console.ReadLine(), out int inputUser))
            {
                if (inputUser == ComandNextWalkToZoo)
                {
                    return;
                }
                else if (inputUser == ComandExit)
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

        private void ChoiceAviary() 
        {
            Console.Write("\nВаш выбор:");

            if (int.TryParse(Console.ReadLine(), out int choiceUser))
            {
                if (choiceUser >= 1 && choiceUser <= _aviaries.Count)
                {
                    _aviaries[choiceUser - 1].ShowInfo();
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
                Console.WriteLine("Вальер " + (i + 1));
        }
    }

    class Aviary
    {
        private int _numberOfAnimals;
        private Animal _animal;
        private List<Animal> _animals = new List<Animal>();

        public Aviary(int numberOfAnimals, Animal animal)
        {
            _numberOfAnimals = numberOfAnimals;
            _animal = animal;

            PlaceAnimals();
        }

        public void ShowInfo() =>
            Console.WriteLine($"\nВ вальере {_numberOfAnimals} животных. {_animal.Name}. Все они имеют пол {_animal.Gender} и издают звук {_animal.Sound}");


        private void PlaceAnimals()
        {
            for (int i = 0; i < _numberOfAnimals; i++)
                _animals.Add((Animal)_animal.Clone());
        }
    }

    class Animal : ICloneable
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

        public object Clone() => MemberwiseClone();
    }
}
