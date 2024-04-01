using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _pingService;
        private readonly IDNS _dns;
        public NetworkServiceTests() 
        {
            _dns = A.Fake<IDNS>();
            _pingService = new NetworkService(_dns);


            //SUT (System under test)
            //_pingService = new NetworkService();

        }

        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            //Arrenge
            //var pingService = new NetworkService();
            A.CallTo(() => _dns.SendDNS()).Returns(true);

            //Act
            var result = _pingService.SendPing();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping sent.");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]

        public void NetworkService_PingTimeOut_ReturnInt(int a, int b, int expected)
        {
            //Arrenge
            // var pingService = new NetworkService();

            //Act
            var result = _pingService.PingTimeOut(a, b);

            //Assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-1000, 0);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            //Arrenge

            //Act
            var result = _pingService.LastPingDate();

            //Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnObject()
        {
            //Arrenge
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var result = _pingService.GetPingOptions();

            //Assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkService_MostRecentPings_ReturnObject()
        {
            //Arrenge
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var result = _pingService.MostRecentPings();

            //Assert
            //result.Should().BeOfType<IEnumerable<PingOptions>>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }

    }
}
