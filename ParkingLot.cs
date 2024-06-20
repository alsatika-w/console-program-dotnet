using System;
using System.Collections.Generic;
using System.Linq;

public class ParkingLot
{
    private int totalSlots;
    private Dictionary<int, Vehicle> slots;

    public ParkingLot(int totalSlots)
    {
        this.totalSlots = totalSlots;
        slots = new Dictionary<int, Vehicle>();
    }

    public void ParkVehicle(Vehicle vehicle)
    {
        for (int i = 1; i <= totalSlots; i++)
        {
            if (!slots.ContainsKey(i))
            {
                slots[i] = vehicle;
                Console.WriteLine($"Allocated slot number: {i}");
                return;
            }
        }
        Console.WriteLine("Sorry, parking lot is full");
    }

    public void Leave(int slotNumber)
    {
        if (slots.ContainsKey(slotNumber))
        {
            var vehicle = slots[slotNumber];
            var hoursParked = (DateTime.Now - vehicle.CheckInTime).TotalHours;
            var parkingFee = Math.Ceiling(hoursParked) * 10; // Example fee calculation
            Console.WriteLine($"Slot number {slotNumber} is free");
            Console.WriteLine($"Total parking fee: ${parkingFee}");
            slots.Remove(slotNumber);
        }
        else
        {
            Console.WriteLine($"Slot number {slotNumber} is already free");
        }
    }

    public void Status()
    {
        Console.WriteLine("Slot\tNo.\t\tType\tRegistration No\tColour");
        foreach (var slot in slots)
        {
            var vehicle = slot.Value;
            Console.WriteLine($"{slot.Key}\t{vehicle.RegistrationNumber}\t{vehicle.Type}\t{vehicle.RegistrationNumber}\t{vehicle.Colour}");
        }
    }

    public void ReportOccupiedSlots()
    {
        Console.WriteLine($"Occupied slots: {slots.Count}");
    }

    public void ReportAvailableSlots()
    {
        Console.WriteLine($"Available slots: {totalSlots - slots.Count}");
    }

    public void ReportVehiclesByOddEvenPlate(bool isOdd)
    {
        var vehicles = slots.Values.Where(v =>
        {
            var lastDigit = v.RegistrationNumber.LastOrDefault(char.IsDigit);
            return (lastDigit - '0') % 2 == (isOdd ? 1 : 0);
        }).Select(v => v.RegistrationNumber);

        Console.WriteLine(string.Join(", ", vehicles));
    }

    public void ReportVehiclesByType(string type)
    {
        var count = slots.Values.Count(v => v.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine(count);
    }

    public void ReportVehiclesByColour(string colour)
    {
        var vehicles = slots.Values.Where(v => v.Colour.Equals(colour, StringComparison.OrdinalIgnoreCase));
        var registrationNumbers = vehicles.Select(v => v.RegistrationNumber);
        var slotNumbers = vehicles.Select(v => slots.First(s => s.Value == v).Key);

        Console.WriteLine(string.Join(", ", registrationNumbers));
        Console.WriteLine(string.Join(", ", slotNumbers));
    }

    public void SlotNumberForRegistrationNumber(string registrationNumber)
    {
        var slot = slots.FirstOrDefault(s => s.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
        if (slot.Value != null)
        {
            Console.WriteLine(slot.Key);
        }
        else
        {
            Console.WriteLine("Not found");
        }
    }
}