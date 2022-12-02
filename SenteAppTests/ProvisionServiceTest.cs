using SenteApp.Interfaces;
using SenteApp.Models;
using SenteApp.Processing;
using Xunit;

namespace SenteAppTests
{
    public class ProvisionServiceTest
    {
        private readonly IProvisionService _provisionService;

        public ProvisionServiceTest()
        {
            _provisionService = new ProvisionService();
        }

        [Fact]
        public void Scenario1ShouldPass()
        {
            var structure = new Structure
            {
                Participant = new Participant()
                {
                    Id = 1,
                    Depth = 0,
                    Subordinates = new[]
                    {
                        new Participant()
                        {
                            Id = 2,
                            Depth = 1
                        },
                        new Participant()
                        {
                            Id = 3,
                            Depth = 1,
                            Subordinates = new[]
                            {
                                new Participant()
                                {
                                    Id = 4,
                                    Depth = 2
                                }
                            }
                        }
                    }
                }
            };

            var transfers = new Transfers()
            {
                AllTransfers = new[]
                {
                    new Transfer()
                    {
                        From = 2,
                        Amount = 100
                    },
                    new Transfer()
                    {
                        From = 3,
                        Amount = 50
                    },
                    new Transfer()
                    {
                        From = 4,
                        Amount = 100
                    },
                    new Transfer()
                    {
                        From = 4,
                        Amount = 200
                    }
                }
            };

            _provisionService.Calculate(structure, transfers);
            var result = _provisionService.GetParticipants();

            var expectedId1Money = 300;
            var expectedId2Money = 0;
            var expectedId3Money = 150;
            var expectedId4Money = 0;

            var expectedId1NotLinkedSubordinates = 2;
            var expectedId2NotLinkedSubordinates = 0;
            var expectedId3NotLinkedSubordinates = 1;
            var expectedId4NotLinkedSubordinates = 0;


            Assert.Equal(expectedId1Money, result[1].Money);
            Assert.Equal(expectedId2Money, result[2].Money);
            Assert.Equal(expectedId3Money, result[3].Money);
            Assert.Equal(expectedId4Money, result[4].Money);


            Assert.Equal(expectedId1NotLinkedSubordinates, result[1].NotLinkedSubordinates);
            Assert.Equal(expectedId2NotLinkedSubordinates, result[2].NotLinkedSubordinates);
            Assert.Equal(expectedId3NotLinkedSubordinates, result[3].NotLinkedSubordinates);
            Assert.Equal(expectedId4NotLinkedSubordinates, result[4].NotLinkedSubordinates);
        }


        [Fact]
        public void Scenario2ShouldPass()
        {
            var structure = new Structure
            {
                Participant = new Participant()
                {
                    Id = 1,
                    Depth = 0,
                    Subordinates = new[]
                    {
                        new Participant()
                        {
                            Id = 2,
                            Depth = 1
                        },
                        new Participant()
                        {
                            Id = 3,
                            Depth = 1,
                            Subordinates = new[]
                            {
                                new Participant()
                                {
                                    Id = 4,
                                    Depth = 2
                                }
                            }
                        },
                        new Participant()
                        {
                            Id = 5,
                            Depth = 1,
                            Subordinates = new[]
                            {
                                new Participant()
                                {
                                    Id = 6,
                                    Depth = 2
                                },
                                new Participant()
                                {
                                    Id = 7,
                                    Depth = 2,
                                    Subordinates = new[]
                                    {
                                        new Participant()
                                        {
                                            Id = 8,
                                            Depth = 3
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var transfers = new Transfers()
            {
                AllTransfers = new[]
                {
                    new Transfer()
                    {
                        From = 1,
                        Amount = 500
                    },
                    new Transfer()
                    {
                        From = 2,
                        Amount = 100
                    },
                    new Transfer()
                    {
                        From = 3,
                        Amount = 50
                    },
                    new Transfer()
                    {
                        From = 4,
                        Amount = 80
                    },
                    new Transfer()
                    {
                        From = 5,
                        Amount = 100
                    },
                    new Transfer()
                    {
                        From = 6,
                        Amount = 199
                    },
                    new Transfer()
                    {
                        From = 7,
                        Amount = 120
                    },
                    new Transfer()
                    {
                        From = 8,
                        Amount = 100
                    },
                }
            };

            _provisionService.Calculate(structure, transfers);
            var result = _provisionService.GetParticipants();

            var expectedId1Money = 999;
            var expectedId2Money = 0;
            var expectedId3Money = 40;
            var expectedId4Money = 0;
            var expectedId5Money = 184;
            var expectedId6Money = 0;
            var expectedId7Money = 25;

            var expectedId1NotLinkedSubordinates = 3;
            var expectedId2NotLinkedSubordinates = 0;
            var expectedId3NotLinkedSubordinates = 1;
            var expectedId4NotLinkedSubordinates = 0;
            var expectedId5NotLinkedSubordinates = 2;
            var expectedId6NotLinkedSubordinates = 0;
            var expectedId7NotLinkedSubordinates = 1;


            Assert.Equal(expectedId1Money, result[1].Money);
            Assert.Equal(expectedId2Money, result[2].Money);
            Assert.Equal(expectedId3Money, result[3].Money);
            Assert.Equal(expectedId4Money, result[4].Money);
            Assert.Equal(expectedId5Money, result[5].Money);
            Assert.Equal(expectedId6Money, result[6].Money);
            Assert.Equal(expectedId7Money, result[7].Money);


            Assert.Equal(expectedId1NotLinkedSubordinates, result[1].NotLinkedSubordinates);
            Assert.Equal(expectedId2NotLinkedSubordinates, result[2].NotLinkedSubordinates);
            Assert.Equal(expectedId3NotLinkedSubordinates, result[3].NotLinkedSubordinates);
            Assert.Equal(expectedId4NotLinkedSubordinates, result[4].NotLinkedSubordinates);
            Assert.Equal(expectedId5NotLinkedSubordinates, result[5].NotLinkedSubordinates);
            Assert.Equal(expectedId6NotLinkedSubordinates, result[6].NotLinkedSubordinates);
            Assert.Equal(expectedId7NotLinkedSubordinates, result[7].NotLinkedSubordinates);
        }
    }
}
