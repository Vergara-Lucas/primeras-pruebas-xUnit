using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using NetworkUtility.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UnitTestCourse.Class;


namespace UnitTest.Class
{
    public class NetworkServiceTest
    {
        private readonly NetworkService _networkService;
        private readonly IDNS _dns;
        //private readonly Mock mockDns;


        public NetworkServiceTest()
        { 
            _dns = A.Fake<IDNS>();
            //Mock<IDNS> mockDns = new Mock<IDNS>();
            _networkService = new NetworkService(_dns);
        }
        [Fact]
        public void NetworkService_SendPing_ReturnString() {
            //Arrange
            //Usamos FakeItEasy para hacer el mock del metodo
            //burla el metodo SendDNS y retorna un true
            A.CallTo(()=>_dns.SendDNS()).Returns(true);
            //usamos mock para mockear el metodo del servicio
            //Mock<IDNS> mockDns = new Mock<IDNS>();
            //mockDns.Setup(a=>a.getDNS()).Returns(true);
            //mockDns.Setup(b=>b.SendDNS()).Returns(true);
           
            //Act
            var result= _networkService.SendPing();
            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent");
            result.Should().Contain("Success",Exactly.Once());
            //Assert.Equal();
            //mockDns.Verify(a => a.getDNS());
            //mockDns.Verify(a => a.SendDNS());
        }

        [Theory]
        [InlineData(10, 20, 30)]
        [InlineData(10, 25, 35)]
        [InlineData(10, 30, 40)]
        [InlineData(10, 10, 20)]
        public void NetworkService_PingTimeOut_ReturnInt(int a, int b,int expected) {
            //arrange
            //var pingService = new NetworkService();
            //Mock<IDNS> mockDns = new Mock<IDNS>();
            //mockDns.Setup(a=>a.getDNS()).Returns(true);
            A.CallTo(()=>_dns.getDNS()).Returns(true);
            //act
            var result = _networkService.PingTimeout(a,b);
            //assert
            result.Should().Be(expected);
            result.Should().BeInRange(1, 40);
            result.Should().BeLessThanOrEqualTo(50);
            result.Should().BePositive();
            //MustNotHaveHappened afirma que la llamada NO se realizo
            //A.CallTo(()=>_dns.getDNS()).MustNotHaveHappened();
            //MustHaveHappened afirma que la llamada SI se realizo
            A.CallTo(() => _dns.getDNS()).MustHaveHappened();
            //ejemplo con parametros
            //A.CallTo(() => _dns.getDNS()).MustHaveHappened(4, Times.Exactly);
            //A.CallTo(() => foo.Bar()).MustHaveHappened(4, Times.Exactly);
            //A.CallTo(() => foo.Bar()).MustHaveHappened(6, Times.OrMore);
            //A.CallTo(() => foo.Bar()).MustHaveHappened(7, Times.OrLess);
        }
        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            //Arrange
            //var pingService = new NetworkService();
            //Act
            var result = _networkService.LastPingDate();
            //Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));
        }
        [Fact]
        public void NetworkService_GetPingOptions_ReturnsObject() {
            //arrange
            var expectation = new PingOptions() { 
                DontFragment = true,
                Ttl = 1
            };
            //act
            var result = _networkService.GetPingOptions();
            //assert Warning "be"
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expectation);
        }
        [Fact]
        public void NetworkService_MostRecentPings_ReturnsIEnumerableObject()
        {
            //arrange
            var expectation = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            //act
            var result = _networkService.MostRecentPings();
            //assert Warning "be"
            //result.Should().BeOfType<IEnumerable<PingOptions>>();
            result.Should().ContainEquivalentOf(expectation);
            result.Should().Contain(x=>x.DontFragment==true);
        }
    }
}
