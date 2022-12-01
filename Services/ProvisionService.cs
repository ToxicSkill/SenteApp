using SenteApp.Models;
using System.Collections.Generic;

namespace SenteApp.Processing
{
    public class ProvisionService
    {
        private Dictionary<int, Participant> _participantdDictionary;

        public ProvisionService()
        {
            _participantdDictionary = new Dictionary<int, Participant>();
        }

        public Dictionary<int, Participant> Calculate(Structure structure, Transfers transfers)
        {
            if (transfers == null || structure == null)
            {
                return default;
            }

            structure.Participant.NotLinkedSubordinates = GetNumberOfNotLinkedSubordinates(structure.Participant);
            _participantdDictionary.Add(structure.Participant.Id, structure.Participant);
            FillSupervisors(structure.Participant, 0);
            ApplyTransfers(transfers);
            return _participantdDictionary;
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
            var initialAmount = transfer.Amount;
            if (transfer.From == 1)
            {
                participant.Money += initialAmount;
                return;
            }

            while (participant.Supervisor != null)
            {
                if (participant.Supervisor.Supervisor != null)
                {
                    initialAmount /= 2;
                }
                participant.Supervisor.Money += initialAmount;
                participant = participant.Supervisor;
            }
        }

        private int GetNumberOfNotLinkedSubordinates(Participant participant)
        {
            var number = 0;
            if (participant.Subordinates == null)
            {
                return number;
            }
            foreach (var subordinate in participant.Subordinates)
            {
                if (subordinate.Subordinates == null)
                {
                    number++;
                }
            }
            return number;
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
