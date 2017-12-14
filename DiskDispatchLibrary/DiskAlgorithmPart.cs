using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskDispatchLibrary
{
    public partial class Disk
    {
        public IEnumerable<DiskState> Test(List<KeyValuePair<int, int>> S)
        {
            yield return DiskState;
        }

        /// <summary>
        /// 将S中的请求，加载到Track[]中
        /// </summary>
        /// <param name="S"></param>
        void LoadRequest(List<KeyValuePair<int, int>> S)
        {
            foreach (var i in S)
            {
                DiskState.Track[i.Key] = i.Value;
            }
            //S.Clear();
        }
        /// <summary>
        /// 从Track[]中选出离Track[now]最近的磁道
        /// </summary>
        /// <param name="now">当前磁道</param>
        /// <returns>离Track[now]最近的磁道的下标，无请求时返回-1</returns>
        int GetMostNearTrack(int now)
        {
            int i = now - 1;
            int j = now + 1;
            for (; i >= 0 || j < DiskState.Track.Length; i--, j++)
            {
                //
                if (i >= 0 && DiskState.Track[i] != 0)
                {
                    return i;
                }
                if (j < DiskState.Track.Length && DiskState.Track[j] != 0)
                {
                    return j;
                }
            }
            //无请求
            return -1;
        }
        /// <summary>
        /// 进行磁臂移动
        /// </summary>
        /// <param name="target">目标磁道的下标</param>
        /// <returns>磁盘状态</returns>
        IEnumerable<DiskState> Move(int target)
        {
            DiskState.MoveIn = target - DiskState.Now > 0 ? true : false;
            DiskState.Target = target;
            if (DiskState.MoveIn)
            {
                //向内移动
                for (int i = DiskState.Now; i <= target; i++)
                {
                    DiskState.Now++;

                    DiskState.TotalSeekTime += TimePerTrack;
                    DiskState.TotalRunTime += TimePerTrack;

                    yield return DiskState;
                }
                DiskState.Now--;
            }
            else
            {
                //向外移动
                for (int i = DiskState.Now; i >= target; i--)
                {
                    DiskState.Now--;

                    DiskState.TotalSeekTime += TimePerTrack;
                    DiskState.TotalRunTime += TimePerTrack;

                    yield return DiskState;
                }
                DiskState.Now++;
            }
        }
        /// <summary>
        /// 读取当前磁道的数据
        /// </summary>
        /// <returns>磁盘状态</returns>
        IEnumerable<DiskState> Read()
        {
            int byteNum = DiskState.Track[DiskState.Now]; //取出要读取的字节数
            bool[] sector = new bool[SectorNum];//每个磁道的扇区,有要读的数据为true

            //按要读取的字节数初始化磁道状态
            for (int i = 0; i < byteNum / BytePerSector; i++)
            {
                sector[i] = true;
            }
            //随机生成磁头位置
            int head = random.Next(0, sector.Length);
            //空转/读取
            while (true)
            {
                if (head == 0)//开始读取
                {
                    for (; sector[head]; head++)
                    {
                        DiskState.TotalAccessTime += TimePerSector;
                        DiskState.TotalRunTime += TimePerSector;

                        yield return DiskState;
                    }
                    break;//读取完毕
                }
                else//空转
                {
                    DiskState.TotalRunTime += TimePerSector;
                    head++;
                    if (head == sector.Length)//磁道为环形
                    {
                        head = 0;
                    }
                    yield return DiskState;
                }
            }

        }


        /// <summary>
        /// 先到先服务（FCFS）算法
        /// </summary>
        /// <param name="S">磁道I/O访问序列S, 参数为磁道编号，访问字节数</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// 当前：读一个扇区的时间 = TimePerTrack = 1，
        /// </returns>
        public IEnumerable<DiskState> FCFS(List<KeyValuePair<int, int>> S)
        {
            //Track[]中记录请求数量
            foreach (var i in S)
            {
                DiskState.Track[i.Key]++;
            }
            
            //启动，初次返回状态
            DiskState.TotalRunTime += TimeToStart;
            yield return DiskState;

            foreach (var i in S)
            {
                //移动
                foreach (var item in Move(i.Key))
                {
                    yield return item;
                }
                //读取
                foreach (var item in Read())
                {
                    yield return item;
                }
                DiskState.Track[i.Key]--;
            }
            yield return DiskState;

        }
        /// <summary>
        /// 最短查找时间优先（SSTF）算法
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// 当前：读一个扇区的时间 = TimePerTrack = 1，
        /// </returns>
        public IEnumerable<DiskState> SSTF(List<KeyValuePair<int, int>> S)
        {
            LoadRequest(S);
            int mostNearTrack = GetMostNearTrack(DiskState.Now);

            //启动，初次返回状态
            DiskState.TotalRunTime += TimeToStart;
            yield return DiskState;

            //运行
            //仍存在需要处理的数据
            while (mostNearTrack >= 0)
            {
                //移动
                foreach (var item in Move(mostNearTrack))
                {
                    yield return item;
                }
                //读取
                foreach (var item in Read())
                {
                    yield return item;
                }
                //读取完清空请求
                DiskState.Track[mostNearTrack] = 0;
                mostNearTrack = GetMostNearTrack(DiskState.Now);
            }
            yield return DiskState;
        }
        /// <summary>
        /// 扫描算法（SCAN）
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// 当前：读一个扇区的时间 = TimePerTrack = 1，
        /// </returns>
        public IEnumerable<DiskState> SCAN(List<KeyValuePair<int, int>> S)
        {
            LoadRequest(S);

            //启动，初次返回状态
            DiskState.TotalRunTime += TimeToStart;
            yield return DiskState;

            if (DiskState.MoveIn == true)
            {
                for (int i = DiskState.Now; i <= 199; i++)
                {
                    if (DiskState.Track[i] != 0)
                    {
                        //移动
                        foreach (var item in Move(i))
                        {
                            yield return item;
                        }
                        //读取
                        foreach (var item in Read())
                        {
                            yield return item;
                        }
                        DiskState.Track[i] = 0;
                    }

                }
                for (int i = 199; i >= 0; i--)
                {
                    if (DiskState.Track[i] != 0)
                    {
                        //移动
                        foreach (var item in Move(i))
                        {
                            yield return item;
                        }
                        //读取
                        foreach (var item in Read())
                        {
                            yield return item;
                        }
                        DiskState.Track[i] = 0;
                    }

                }
            }
            else
            {
                for (int i = DiskState.Now; i >= 0; i--)
                {
                    if (DiskState.Track[i] != 0)
                    {
                        //移动
                        foreach (var item in Move(i))
                        {
                            yield return item;
                        }
                        //读取
                        foreach (var item in Read())
                        {
                            yield return item;
                        }
                        DiskState.Track[i] = 0;
                    }

                }
                for (int i = 0; i <= 199; i++)
                {
                    if (DiskState.Track[i] != 0)
                    {
                        //移动
                        foreach (var item in Move(i))
                        {
                            yield return item;
                        }
                        //读取
                        foreach (var item in Read())
                        {
                            yield return item;
                        }
                        DiskState.Track[i] = 0;
                    }

                }
            }
            yield return DiskState;
        }
        /// <summary>
        /// 电梯算法（LOOK）
        /// 
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// 当前：读一个扇区的时间 = TimePerTrack = 1，
        /// </returns>
        public IEnumerable<DiskState> LOOK(List<KeyValuePair<int, int>> S)
        {
            LoadRequest(S);
            List<int> TrackAdd = new List<int>();//装入比当前磁道大的含有请求的磁道下标
            List<int> TrackSub = new List<int>();//装入比当前磁道小或等于当前磁道的含有请求的磁道下标

            //启动，初次返回状态
            DiskState.TotalRunTime += TimeToStart;
            yield return DiskState;

            //初始化TrackSub
            for (int i = 0; i <= DiskState.Now; i++)
            {
                if (DiskState.Track[i] != 0)
                {
                    TrackSub.Add(i);
                }
            }
            //初始化TrackAdd
            for (int i = DiskState.Now; i < trackNum; i++)
            {
                if (DiskState.Track[i] != 0)
                {
                    TrackAdd.Add(i);
                }
            }
            //向内，先读取TrackAdd，再读取TrackSub
            if (DiskState.MoveIn == true)
            {
                //
                for (int i = 0; i < TrackAdd.Count; i++)
                {
                    //移动
                    foreach (var item in Move(TrackAdd[i]))
                    {
                        yield return item;
                    }
                    //读取
                    foreach (var item in Read())
                    {
                        yield return item;
                     DiskState.Track[item.Now] = 0;
                   }
                }
                //回转
                for (int i = TrackSub.Count - 1; i >= 0; i--)
                {
                    //移动
                    foreach (var item in Move(TrackSub[i]))
                    {
                        yield return item;
                    }
                    //读取
                    foreach (var item in Read())
                    {
                        yield return item;
                        DiskState.Track[item.Now] = 0;
                    }
                }



            }
            //向外，先读取TrackSub，再读取TrackAdd
            else
            {
                for (int i = TrackSub.Count - 1; i >= 0; i--)
                {
                    //移动
                    foreach (var item in Move(TrackSub[i]))
                    {
                        yield return item;
                    }
                    //读取
                    foreach (var item in Read())
                    {
                        yield return item;
                        DiskState.Track[item.Now] = 0;
                    }
                }
                //回转
                for (int i = 0; i < TrackAdd.Count; i++)
                {
                    //移动
                    foreach (var item in Move(TrackAdd[i]))
                    {
                        yield return item;
                    }
                    //读取
                    foreach (var item in Read())
                    {
                        yield return item;
                        DiskState.Track[item.Now] = 0;
                    }
                }
            }
            yield return DiskState;
        }
    }
}
