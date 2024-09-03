using AllocateQ3_ConsoleInteraction.Common.Exceptions;
using AllocateQ3_ConsoleInteraction.Services;
using System;
using AllocateQ3_ConsoleInteraction.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AllocateQ3_ConsoleInteraction
{
    class Program
    {
        static void Main(string[] args)
        {
            // Place this to method Intro Text
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("-------       Song Player      --------");
            Console.WriteLine("-----             for             -----");
            Console.WriteLine("------ Old MacDonald Had a Farm -------");
            Console.WriteLine("---------------------------------------");

            var proceed = "";
            var services = new ServiceCollection();
            services.AddSingleton<AnimalsServices>().AddSingleton<AnimalsRepository>();

            var serviceProvider = services.BuildServiceProvider();
            var animalsService = serviceProvider.GetRequiredService<AnimalsServices>();

            do
            {
                try
                {
                    // Do not create new instance of service in loop it is not needed
                    // var animalsService = new AnimalsServices(); // Use DI in real world scenario with interfaces

                    // This can be in separate method, Called ChooseOption
                    Console.WriteLine("Chose one of the following options");
                    Console.WriteLine("1. - Play verses for all Animals");
                    Console.WriteLine("2. - Choose Animal to play verse for");
                    Console.WriteLine("3. - Add new Animal");
                    Console.WriteLine("4. - Remove existing Animal");

                    var input = Console.ReadLine();
                    
                    // Handle option method, small thing
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("--- Play verses for all Animals ---");
                            animalsService.PlayAllVerses();
                            break;
                        case "2":
                            Console.WriteLine("--- Choose Animal to play verse for ----");
                            animalsService.PlaySingleVerse();
                            break;
                        case "3":
                            Console.WriteLine("--- Add new Animal ----");
                            animalsService.AddAnimal();
                            break;
                        case "4":
                            Console.WriteLine("--- Remove existing Animal ----");
                            animalsService.RemoveAnimal();
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
                catch (InputException ex)
                {
                    //Console.WriteLine($"{ex.Message}"); no need for interpolation
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong.");
                    Console.WriteLine($"Error message: {ex.Message}");
                }
                
                Console.WriteLine("Write no to stop , everything else to continue");
                //proceed = Console.ReadLine().Trim().ToLower(); // Use conditional access null is option
                proceed = Console.ReadLine()?.Trim().ToLower();
            } while (proceed != "no");
        }
    }
}