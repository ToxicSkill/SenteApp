using SenteApp.Models;
using System;
using System.Collections.Generic;

namespace SenteApp.Modules
{
    public class Display
    {
        private const string Separator = " ";

        public void Show(Dictionary<int, Participant> participants)
        {
            foreach (var item in participants)
            {
                Console.Write(item.Value.Id + Separator);
                Console.Write(item.Value.Depth + Separator);
                Console.Write(item.Value.NotLinkedSubordinates + Separator);
                Console.WriteLine(item.Value.Money + Separator);
            }
        }
    }
}
