using CarRace;
using System.ComponentModel.DataAnnotations;

namespace CarRace
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // create cars
            Car car1 = new Car
            {
                Id = 1,
                Name = "Tesla"
            };

            Car car2 = new Car
            {
                Id = 2,
                Name = "Mercedes benz"
            };

            // Check competation status
            Task t = Task.Run(() => CompetationStatus(new List<Car> { car1, car2 }));

            while (true)
            {
                //Start competation
                var tesla = await Car.StartCompetation(car1);
                var mercedesbenz = await Car.StartCompetation(car2);

                // Check  if race has completed or not
                if (tesla.Finished && mercedesbenz.Finished)
                {
                    if (tesla.ElapseTime > mercedesbenz.ElapseTime)
                    {
                        Console.WriteLine($"Mercedesbenz has won the race with margin  of {Math.Round(tesla.ElapseTime - mercedesbenz.ElapseTime)} seconds");
                    }
                    else if (tesla.ElapseTime < mercedesbenz.ElapseTime)
                    {
                        Console.WriteLine($"Tesla  own the race with  margin of {Math.Round(mercedesbenz.ElapseTime - tesla.ElapseTime)} seconds");

                    }
                    else
                    {
                        Console.WriteLine("Racee tied!!!Both car has reached at same time");
                    }
                    return;
                }

                await Wait30Seconds();

            }
        }

        public async static Task Wait30Seconds(int tick = 2)
        {
            Task.Delay(TimeSpan.FromSeconds(tick)).Wait(); //  Force wait for 30 seconds
        }

        public static async Task CompetationStatus(List<Car> cars)
        {
            Console.WriteLine("Press  enter if you want to know competation status");
            while (true)
            {
                DateTime start = DateTime.Now;
                bool gotKey = false;

                while ((DateTime.Now - start).TotalSeconds < 2)
                {
                    if (Console.KeyAvailable)
                    {
                        gotKey = true;
                        break;
                    }
                }

                if (gotKey)
                {
                    Console.ReadKey();
                    Console.Clear();
                    cars.ForEach(car =>
                    {
                        Console.WriteLine($"Name: {car.Name} DistanceLeft: {car.DistanceLeft}  ElapseTime : {car.ElapseTime} TimeToFinish : {car.TimeToFinish}");
                    });
                }

                gotKey = false;

                var isFinishedRace = cars.All(item => item.Finished);
                if (isFinishedRace)
                {
                    return;
                }

            }

        }
    }
}