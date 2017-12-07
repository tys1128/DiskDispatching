using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskDispatchLibrary
{
    /// <summary>
    /// 模拟的磁盘
    /// </summary>
    public class Disk : IDiskArgument, IDispatchAlgorithm
    {
        public int TimePerTrack { get; set; }
        public int TimeToStart { get; set; }
        public int Rpm { get; set; }
        public int SectorNum { get; set; }
        public int BytePerSector { get; set; }
        public int[] Track { get; set; }

        public DiskState DiskState { get; }

        public Disk()
        {
            TimePerTrack = 10;
            TimeToStart = 200;
            Rpm = 5400;
            SectorNum = 16;
            BytePerSector = 4092;
            Track = new int[200];
        }
        public Disk(int timePerTrack, int timeToStart, int rpm, int sectorNum, int bytePerSector, int[] track)
        {
            TimePerTrack = timePerTrack;
            TimeToStart = timeToStart;
            Rpm = rpm;
            SectorNum = sectorNum;
            BytePerSector = bytePerSector;
            Track = new int[200];
        }

        public IEnumerable<DiskState> FCFS(List<KeyValuePair<int, int>> S)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DiskState> LOOK(List<KeyValuePair<int, int>> S)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DiskState> SCAN(List<KeyValuePair<int, int>> S)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DiskState> SSTF(List<KeyValuePair<int, int>> S)
        {
            throw new NotImplementedException();
        }

    }
}
