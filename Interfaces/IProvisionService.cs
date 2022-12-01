using SenteApp.Models;
using System.Collections.Generic;

namespace SenteApp.Interfaces
{
    public interface IProvisionService
    {
        void Calculate(Structure structure, Transfers transfers);

        void Display();

        Dictionary<int, Participant> GetParticipants();
    }
}