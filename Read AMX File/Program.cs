using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string filePath = "1.GEN";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        byte[] bytes = File.ReadAllBytes(filePath);

        if (bytes.Length < 48)
        {
            Console.WriteLine("File too short to contain timestamp at offset 44.");
            return;
        }

        // Read timestamp from offset 44
        int timestamp = BitConverter.ToInt32(bytes, 44); // assumes little-endian
        DateTimeOffset utc = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        DateTimeOffset local = utc.ToLocalTime();

        Console.WriteLine($"Timestamp (raw): {timestamp}");
        Console.WriteLine($"UTC:   {utc}");
        Console.WriteLine($"Local: {local}");
    }
}
