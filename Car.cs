using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Speed { get; set; }
        public double DistanceLeft { get; set; }
        public double ElapseTime { get; set; }
        public double Penalty { get; set; }
        public bool Finished { get; set; }
        public double TimeToFinish { get; set; }

        public Car()
        {
            //default values
            DistanceLeft = 10000; //meter
            Speed = 120; //km/h
            ElapseTime = 0;
            Penalty = 0;
            Finished = false;
        }

        public async static Task<Car> StartCompetation(Car car)
        {
            if (car.Finished == false)
            {
                //Calculation time to finish 
                // Time  = Distance /  speed
                car.TimeToFinish = (car.DistanceLeft / (car.Speed / 3.6));
                Console.WriteLine($"Name: {car.Name} DistanceLeft: {car.DistanceLeft}");

                //Check if race is finished or going to finish ssoon
                if (car.DistanceLeft <= 0)
                {
                    Console.WriteLine($"{car.Name} : {car.ElapseTime}");
                    car.Finished = true;
                }
                else if (car.TimeToFinish <= 30)
                {
                    car.ElapseTime += car.TimeToFinish;
                    car.DistanceLeft = 0;
                    Console.WriteLine($"{car.Name} : {car.ElapseTime}");
                    car.Finished = true;
                    return car;
                }

                await EventOccurrence(car);
                car.ElapseTime += 30;
                //Calculate distance left after every 30
                car.DistanceLeft = car.DistanceLeft - ((car.Speed / 3.6) * (30 - car.Penalty));

                // reset penalty after every 30s
                car.Penalty = 0;
            }
            return car;
        }

        // Make async?
        public static async Task EventOccurrence(Car car)
        {
            // Make a list of evens based on probability to occur
            //Refuel 1/50
            //Puncture 2/50
            //EventOccurrence 5/50
            //EngineFailure 10/50
            var events = new List<string> { "Refuel", "Puncture", "Puncture", "WashWindSheild", "WashWindSheild", "WashWindSheild", "WashWindSheild", "WashWindSheild", "EngineFailure", "EngineFailure", "EngineFailure", "EngineFailure", "EngineFailure", "EngineFailure", "EngineFailure", "EngineFailure", "EngineFailure", "EngineFailure" };
            var rand = new Random();
            var eventIndex = rand.Next(0, 50);

            if (events.ElementAtOrDefault(eventIndex) != null)
            {
                Console.WriteLine($"Event occured in {car.Name}: {events[eventIndex]} <-------");

                if (events[eventIndex] == "Refuel")
                {
                    car.Penalty = 30;
                }
                else if (events[eventIndex] == "Puncture")
                {
                    car.Penalty = 20;
                }
                else if (events[eventIndex] == "WashWindSheild")
                {
                    car.Penalty = 10;
                }
                else if (events[eventIndex] == "EngineFailure")
                {
                    car.Speed = car.Speed - 1;
                }
            }
            else
            {
                car.Penalty = 0;
            }

            return;
        }

    }

}
