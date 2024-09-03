using NetworkUtility.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestCourse.Class
{
    public class NetworkService
    {
        private readonly IDNS _dns;
        public NetworkService(IDNS dns)
        {
            _dns = dns;
        }
        public string SendPing() {
            //SearchDNS();
            //BuildPacket();
            var dnsSucces = _dns.SendDNS();
            if (dnsSucces)
            {
                return "Success: Ping Sent";
            }
            else {
                return "Failed: Ping not sent";
            }
            
        }
    
        public int PingTimeout(int a, int b) {
            if (_dns.getDNS()){
                return a + b;
            }
            else {
                return 0;
            }
            
        }
        public DateTime LastPingDate() {
            return DateTime.Now;
        }
        public PingOptions GetPingOptions() {
            return new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            
        }
        
        public IEnumerable<PingOptions> MostRecentPings()
        {
            IEnumerable<PingOptions> pingOptions = new[]{
                
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1
                },
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1
                },
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1
                },
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1
                }
            };
            return pingOptions;
        }
    }
}
