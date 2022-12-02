using SenteApp.Interfaces;
using SenteApp.Models;
using SenteApp.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SenteApp.Processing
{
    public class ProvisionService : IProvisionService
    {
        private Dictionary<int, Participant> _participantdDictionary;

        private readonly Display _display;

        public ProvisionService()
        {
            _display = new Display();
            _participantdDictionary = new Dictionary<int, Participant>();
        }

        public void Calculate(Structure structure, Transfers transfers)
        {
            if (transfers == null || structure == null)
            {
                return;
            }

            structure.Participant.NotLinkedSubordinates = GetNumberOfNotLinkedSubordinates(structure.Participant);
            _participantdDictionary.Add(structure.Participant.Id, structure.Participant);
            FillSupervisors(structure.Participant, 0);
            ApplyTransfers(transfers);
        }

        public Dictionary<int, Participant> GetParticipants()
        {
            return _participantdDictionary;
        }

        public void Display()
        {
            _display.Show(_participantdDictionary);
        }

        private void ApplyTransfers(Transfers transfers)
        {
            foreach (var transfer in transfers.AllTransfers)
            {
                TransferMoney(transfer);
            }
        }

        private void TransferMoney(Transfer transfer)
        {
            var participant = _participantdDictionary[transfer.From];
            var initialDepth = participant.Depth;

            if (participant.Supervisor == null)
            {
                participant.Money = transfer.Amount;
                return;
            }

            while (participant.Supervisor != null)
            {
                participant.Supervisor.Money += transfer.Amount / (int)(Math.Pow(2, participant.Depth == initialDepth ? participant.Supervisor.Depth : participant.Depth));
                participant = participant.Supervisor;
            }
        }

        private int GetNumberOfNotLinkedSubordinates(Participant participant)
        {
            if (participant.Subordinates == null) return 0;
            return (from subordinate in participant.Subordinates
                    where subordinate.Subordinates == null
                    select subordinate).Count();
        }

        private void FillSupervisors(Participant participant, int depth)
        {
            if (participant.Subordinates == null)
            {
                return;
            }

            depth++;
            var numberOfNotLinkedSubordinates = 0;
            foreach (var subordinate in participant.Subordinates)
            {
                subordinate.Supervisor = participant;
                subordinate.Depth = depth;
                subordinate.NotLinkedSubordinates = GetNumberOfNotLinkedSubordinates(subordinate);
                numberOfNotLinkedSubordinates += subordinate.NotLinkedSubordinates;
                _participantdDictionary.Add(subordinate.Id, subordinate);
                FillSupervisors(subordinate, depth);
            }

            participant.NotLinkedSubordinates += numberOfNotLinkedSubordinates;

            return;
        }
    }
}