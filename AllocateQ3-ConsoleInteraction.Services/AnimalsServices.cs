using AllocateQ3_ConsoleInteraction.Common.Exceptions;
using AllocateQ3_ConsoleInteraction.Common.Validators;
using AllocateQ3_ConsoleInteraction.Models;
using AllocateQ3_ConsoleInteraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllocateQ3_ConsoleInteraction.Services
{
    public class AnimalsServices // Name should be Singular AnimalService, not that important
    {
        // public AnimalsServices()
        // {
        //     _animalsRepository = new AnimalsRepository();// Inject in constructor not use new is not testable
        // }

        //private AnimalsRepository _animalsRepository { get; set; } // Use Readonly field instead of property
        private readonly AnimalsRepository _animalsRepository;

        // correct constructor, constructor should be bellow private fields, In c# 12 we got primary constructor so we can remove this constructor :D
        public AnimalsServices(AnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public void PlayAllVerses()
        {
            var animals = _animalsRepository.GetAll();
            //animals.ForEach(x => x.MakeSound()); // Dont use LINQ foreach is less performant then standard foreach and more readable not js style :D
            foreach (var animal in animals)
            {
                animal.MakeSound();
            }
        }

        //public void ShowAllAnimals() // can be made private
        private void ShowAllAnimals()
        {
            var animals = _animalsRepository.GetAll();

            //if (animals.Count <= 0) // It can be simplified
            if (animals.Count == 0)
            {
                // Should service layer be aware that this is console app? should return result and let caller decide what to do with it
                Console.WriteLine("Animals List is empty");
            }
            else
            {
                animals.ForEach(x => Console.WriteLine($"{x.Id} - {x.Type}"));
            }
        }

        // public void PlayVerseByAnimalId(string showAllAnimalsInput) // can be made private
        private void PlayVerseByAnimalId(string showAllAnimalsInput)
        {
            var animalId = showAllAnimalsInput.ValidatePositiveInteger();

            var animal = _animalsRepository.GetById(animalId);

            if (animal == null)
            {
                Console.WriteLine($"Wrong input, there is not Animal with Id: {animalId}");
            }
            else
            {
                Console.WriteLine($"Playing Verse for {animal.Type}");
                Console.WriteLine("");
                animal.MakeSound();
            }
        }

        public void PlaySingleVerse()
        {
            ShowAllAnimals();
            var showAllAnimalsInput = Console.ReadLine();
            PlayVerseByAnimalId(showAllAnimalsInput);
        }

        public void AddAnimal()
        {
            Console.WriteLine("Please enter Animal Type");
            var animalType = Console.ReadLine().CheckNullOrEmpty();

            CheckIfAnimalAllreadyExsist(animalType);

            Console.WriteLine("Please enter Animal's sound");
            var animalSound = Console.ReadLine().CheckNullOrEmpty();

            var newAnimal = new Animal();
            newAnimal.Type = animalType;
            newAnimal.Sound = animalSound;

            _animalsRepository.Create(newAnimal);
            _animalsRepository.SaveChanges();
            Console.WriteLine("Animal successfully Created");
        }

        public void RemoveAnimal()
        {
            Console.WriteLine("Please enter Animal Id to delete it ");
            ShowAllAnimals();
            var animalToDeleteId = Console.ReadLine().ValidatePositiveInteger();

            var animalToDelete = _animalsRepository.GetById(animalToDeleteId);

            if (animalToDelete == null)
            {
                throw new InputException("invalid Input");
            }

            _animalsRepository.Delete(animalToDelete);
            _animalsRepository.SaveChanges();
            Console.WriteLine("Animal successfully Deleted");
        }

        private void CheckIfAnimalAllreadyExsist(string animalType)
        {
            var animals = _animalsRepository.GetAll();

            var animalToCheck = animals.FirstOrDefault(x => x.Type.ToLower() == animalType.ToLower());

            if (animals.Count > 0 && animalToCheck != null)
            {
                throw new InputException($"The Animal:{animalToCheck.Type} allready exsist");
            }
        }
    }
}