using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using snap7_to_prometheus.Models;

namespace snap7_to_prometheus.Services
{
    public class PLCService
    {
        private readonly ConfigService configService;
        private S7Client client;
        private Timer periodicRead;

        public PLCService(ConfigService configService)
        {
            client = new S7Client();
            this.configService = configService;

            // Connect to the PLC
            Connect();

            // Read Periodically
            periodicRead = new Timer(TimerCallback, null, 0, configService.ActiveConfig.ReadRateMs);
        }

        private void Connect()
        {
            Console.WriteLine("Connecting...");

            var ret = client.ConnectTo(configService.ActiveConfig.PLCAddress, (ushort)configService.ActiveConfig.PLCRack, (ushort)configService.ActiveConfig.PLCSlot);
            var errorText = client.ErrorText(ret);

            // Ok, or fail
            if (ret == 0)
            {
                Console.WriteLine("Connected.");
            }
            else
            {
                Console.WriteLine($"Connection failed, error {ret}:, {errorText}");
            }

        }

        private void TimerCallback(object state)
        {
            ReadPLC();
        }

        private void ReadPLC()
        {
            // Connect if disconnected
            if (!client.Connected)
            {
                client.Connect();
            }

            //Do this for each DB in the configuration
            foreach (var db in configService.ActiveConfig.DBReads)
            {
                Byte[] buffer = new byte[db.DBLengthByte];
                var ret = client.DBRead(db.DBNumber, db.DBOffsetByte, db.DBLengthByte, buffer);
                var errorText = client.ErrorText(ret);
                if (ret == 0)
                {
                    Console.WriteLine($"Reading DB{db.DBNumber} success.");
                }
                else
                {
                    Console.WriteLine($"Reading DB{db.DBNumber} failed, error {ret}:, {errorText}");
                }

                //Parse the db into parts
                foreach(var tag in db.Tags)
                {
                    if (tag.Type == "Int64")
                    {
                        tag.Data = S7.GetLIntAt(buffer, tag.OffsetByte);
                        Console.WriteLine($"Test data {tag.Data}");
                    }
                    else
                    {
                        Console.WriteLine($"Unknown type for tag {tag.Name} success.");
                    }
                }
            }



        }
    }
}
