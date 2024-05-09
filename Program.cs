﻿using System;
using System.Collections.Generic;
using System.Net;

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
                for (int i = 0; i < _aviaries.Count; i++)
                    Console.WriteLine("Вальер " + (i + 1));

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
                        continue;
                    }
                }
                else
                {
                    PrintIncorrectDataMessage();
                    continue;
                }

                Console.Write("\nХотите дальше ходить по зоопарку или уйти? 1 - ДА 2 - НЕТ ");

                if (int.TryParse(Console.ReadLine(), out int inputUser))
                {
                    if (inputUser == 1)
                    {
                        continue;
                    }
                    else if (inputUser == 2)
                    {
                        _isOpen = false;
                    }
                    else
                    {
                        PrintIncorrectDataMessage();
                        continue;
                    }
                }
                else
                {
                    PrintIncorrectDataMessage();
                    continue;
                }
            }
        }

        private void PrintIncorrectDataMessage()
        {
            Console.WriteLine("Введены некорректные данные!");
            Console.ReadKey();
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
        private string _name;
        private string _gender;
        private string _sound;

        public Animal(string name, string gender, string sound)
        {
            _name = name;
            _gender = gender;
            _sound = sound;
        }

        public string Name => _name;
        public string Gender => _gender;
        public string Sound => _sound;

        public object Clone() => MemberwiseClone();
    }
}