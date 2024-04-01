﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Ping
{
    public class NetworkService
    {
        public string SendPing()
        {
            return "Success: Ping sent.";
        }

        public int PingTimeOut(int a, int b)
        {
            return a + b;
        }

        public DateTime LastPingDate()
        {
            return DateTime.Now;
        }

        public PingOptions GetPingOptions()
        {
            return new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
        }

        public IEnumerable<PingOptions> MostRecentPings()
        {
            IEnumerable<PingOptions> pingOptions = new[]
            {
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
