// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System;
using System.Collections.Generic;
public class Program
{
    public static void Main(string[] args)
    {
        ParkingLot? parkingLot = null;

        // Create a dictionary to map commands to actions
        var commandActions = new Dictionary<string, Action<string[]>>
        {
            { "create_parking_lot", cmd => 
                {
                    int totalSlots = int.Parse(cmd[1]);
                    parkingLot = new ParkingLot(totalSlots);
                    Console.WriteLine($"Created a parking lot with {totalSlots} slots");
                } 
            },
            { "park", cmd => 
                {
                    if (cmd.Length >= 4)
                    {
                        var vehicle = new Vehicle(cmd[1], cmd[2], cmd[3]);
                        parkingLot?.ParkVehicle(vehicle);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command parameters for park");
                    }
                } 
            },
            { "leave", cmd => 
                {
                    if (cmd.Length >= 2 && int.TryParse(cmd[1], out int slotNumber))
                    {
                        parkingLot?.Leave(slotNumber);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command parameters for leave");
                    }
                } 
            },
            { "status", cmd => parkingLot?.Status() },
            { "occupied_slots", cmd => parkingLot?.ReportOccupiedSlots() },
            { "available_slots", cmd => parkingLot?.ReportAvailableSlots() },
            { "type_of_vehicles", cmd => 
                {
                    if (cmd.Length >= 2)
                    {
                        parkingLot?.ReportVehiclesByType(cmd[1]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command parameters for type_of_vehicles");
                    }
                } 
            },
            { "registration_numbers_for_vehicles_with_odd_plate", cmd => parkingLot?.ReportVehiclesByOddEvenPlate(true) },
            { "registration_numbers_for_vehicles_with_even_plate", cmd => parkingLot?.ReportVehiclesByOddEvenPlate(false) },
            { "registration_numbers_for_vehicles_with_colour", cmd => 
                {
                    if (cmd.Length >= 2)
                    {
                        parkingLot?.ReportVehiclesByColour(cmd[1]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command parameters for registration_numbers_for_vehicles_with_colour");
                    }
                } 
            },
            { "slot_numbers_for_vehicles_with_colour", cmd => 
                {
                    if (cmd.Length >= 2)
                    {
                        parkingLot?.ReportVehiclesByColour(cmd[1]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command parameters for slot_numbers_for_vehicles_with_colour");
                    }
                } 
            },
            { "slot_number_for_registration_number", cmd => 
                {
                    if (cmd.Length >= 2)
                    {
                        parkingLot?.SlotNumberForRegistrationNumber(cmd[1]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command parameters for slot_number_for_registration_number");
                    }
                } 
            },
            { "exit", cmd => Environment.Exit(0) }
        };

        while (true)
        {
            var input = Console.ReadLine();
            if (input == null)
            {
                continue;
            }

            var command = input.Split(' ');

            if (commandActions.ContainsKey(command[0]))
            {
                commandActions[command[0]](command);
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        }
    }
}