﻿using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args) {
            var animalTemplates = new Dictionary<string, AviaryConfig>

            {
                { "Lion", new AviaryConfig("Лев", "Рев", "Вольер со львами") },
                { "Elephant", new AviaryConfig("Слон", "Труба", "Вольер со слонами") },
                { "Monkey", new AviaryConfig("Обезьяна", "Визг", "Вольер с обезьянами") },
                { "Giraffe", new AviaryConfig("Жираф", "Гул", "Вольер с жирафами") }
            };

            EnclosureFactory enclosureFactory = new EnclosureFactory();
            ZooFactory zooFactory = new ZooFactory(enclosureFactory);

            Zoo zoo = zooFactory.Create(animalTemplates);
            zoo.Work();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public Zoo(List<Aviary> aviaries)
            => _aviaries = aviaries;

        public void Work() {
            bool isOpen = true;

            while (isOpen){
                Console.Clear();
                Console.WriteLine("Вы в зоопарке. Какой вольер хотите посетить?\n");
                ShowAviaries();
                isOpen = IsContinueAfterAviaryChoice();
            }
        }

        private bool IsContinueAfterAviaryChoice() {
            Console.Write("\n\nВаш выбор: ");
            if (int.TryParse(Console.ReadLine(), out int choice)){
                if (choice >= 1 && choice <= _aviaries.Count){
                    _aviaries[choice - 1].ShowInfo();
                    Console.ReadKey();
                    return true;
                }
                if (choice == _aviaries.Count + 1){
                    return false;
                }
            }

            Console.WriteLine("Введены некорректные данные!");
            Console.ReadKey();
            return true;
        }

        private void ShowAviaries() {
            for (int i = 0; i < _aviaries.Count; i++)
                Console.WriteLine($"{i + 1}. {_aviaries[i].Name}");

            Console.WriteLine($"\n{_aviaries.Count + 1}. Выход");
        }
    }

    class Aviary
    {
        private string _name;
        private List<Animal> _animals;

        public Aviary(string name, List<Animal> animals) {
            _name = name;
            _animals = animals;
        }

        public string Name => _name;

        public void ShowInfo() {
            Console.WriteLine($"Вольер: {_name}");
            Console.WriteLine($"Количество животных: {_animals.Count}");

            for (int i = 0; i < _animals.Count; i++){
                Console.WriteLine($"{i + 1}. {_animals[i].Gender} - {_animals[i].Sound}");
            }
        }
    }

    class Animal
    {
        public Animal(string name, string gender, string sound) {
            Name = name;
            Gender = gender;
            Sound = sound;
        }

        public string Name { get; }
        public string Gender { get; }
        public string Sound { get; }

        public Animal Clone()
            => new Animal(Name, Gender, Sound);
    }

    class EnclosureFactory
    {
        private const int MinNumberAnimals = 1;
        private const int MaxNumberAnimals = 10;

        public Aviary Create(Animal baseAnimal, string enclosureName) {
            var animals = new List<Animal>();
            int cloneCount = UserUtils.GenerateRandomNumber(MinNumberAnimals, MaxNumberAnimals + 1);

            for (int i = 0; i < cloneCount; i++){
                animals.Add(baseAnimal.Clone());
            }

            return new Aviary(enclosureName, animals);
        }
    }

    class ZooFactory
    {
        private readonly EnclosureFactory _enclosureFactory;
        private readonly List<string> _genders;

        public ZooFactory(EnclosureFactory enclosureFactory) {
            _enclosureFactory = enclosureFactory;
            _genders = ["Мужской", "Женский"];
        }

        public Zoo Create(Dictionary<string, AviaryConfig> animalTemplates) {
            var aviaries = new List<Aviary>();

            foreach (var template in animalTemplates.Values){
                string gender = _genders[UserUtils.GenerateRandomNumber(0, _genders.Count)];
                var baseAnimal = new Animal(template.Name, gender, template.Sound);
                aviaries.Add(_enclosureFactory.Create(baseAnimal, template.EnclosureName));
            }

            return new Zoo(aviaries);
        }
    }

    class AviaryConfig
    {
        public AviaryConfig(string name, string sound, string enclosureName) {
            Name = name;
            Sound = sound;
            EnclosureName = enclosureName;
        }

        public string Name { get; }
        public string Sound { get; }
        public string EnclosureName { get; }
    }

    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max) {
            return s_random.Next(min, max);
        }
    }
}
