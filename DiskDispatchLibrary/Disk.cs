using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskDispatchLibrary
{
    /// <summary>
    /// 存储磁盘当前的状态
    /// </summary>
    public class DiskState
    {
        /// <summary>
        /// 移动方向(是否向内移动)
        /// </summary>
        public bool MoveIn { get; set; } 
        /// <summary>
        /// 目标磁道编号
        /// </summary>
        public int Target { get; set; }
        /// <summary>
        /// 当前磁道
        /// </summary>
        public int Now { get; set; }

        /// <summary>
        /// 总寻道时间(ms)
        /// </summary>
        public int TotalSeekTime { get; set; }
        /// <summary>
        /// 总传输（访问）时间(ms)
        /// </summary>
        public int TotalAccessTime { get; set; }
        /// <summary>
        /// 总运行时间(ms)
        /// </summary>
        public int TotalRunTime { get; set; }

    }

    /// <summary>
    /// 模拟的磁盘
    /// </summary>
    public partial class Disk : IDiskArgument, IDispatchAlgorithm
    {
        /// <summary>
        /// 磁道数
        /// </summary>
        const int trackNum = 200;
        static Random random = new Random();

        public int TimePerTrack { get; set; }
        public int TimePerSector { get; set; }
        public int TimeToStart { get; set; }
        public int Rpm { get; set; }
        public int SectorNum { get; set; }
        public int BytePerSector { get; set; }
        public int ArgAccessDelay { get; set; }
        public int[] Track { get; set; }

        DiskState DiskState { get; set; }


        /// <summary>
        /// 随机产生磁道I/O访问序列S
        /// KeyValuePair<int, int>为磁道编号，访问字节数
        /// </summary>
        static public List<KeyValuePair<int, int>> S
        {
            get
            {
                List<KeyValuePair<int, int>> s = new List<KeyValuePair<int, int>>(trackNum);

                Random random = new Random();
                for (int i = 0; i < trackNum; i++)
                {
                    s.Add(new KeyValuePair<int, int>(random.Next(0, trackNum), random.Next(1024)));
                }
                return s;
            }
        }
        /// <summary>
        /// 获取随机产生的磁道I/O访问序列S
        /// KeyValuePair<int, int>为磁道编号，访问字节数
        /// </summary>
        /// <param name="n">序列的长度</param>
        /// <returns></returns>
        static public List<KeyValuePair<int, int>> GetS(int n)
        {
            List<KeyValuePair<int, int>> s = new List<KeyValuePair<int, int>>(n);

            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                s.Add(new KeyValuePair<int, int>(random.Next(0, trackNum), random.Next(1024)));
            }
            return s;
        }


        public Disk()
        {
            TimePerTrack = 1;
            TimeToStart = 200;
            Rpm = 5000;         //Rmp*SectorNum == 60*1000  时，
            SectorNum = 12;     //读一个扇区的时间为1ms
            BytePerSector = 128;
            Track = new int[trackNum];
            ArgAccessDelay = 60 * 1000 / (Rpm * 2);

            TimePerSector = 60 * 1000 / (Rpm * SectorNum);
            DiskState = new DiskState()
            {
                Now = random.Next(70, trackNum - 70),
                MoveIn = Convert.ToBoolean(random.Next(0, 2)),
            };
        }
        //public Disk(int timePerTrack, int timeToStart, int rpm, int sectorNum, int bytePerSector)
        //{
        //    TimePerTrack = timePerTrack;
        //    TimeToStart = timeToStart;
        //    Rpm = rpm;
        //    SectorNum = sectorNum;
        //    BytePerSector = bytePerSector;
        //    Track = new int[trackNum];
        //}


    }
}
