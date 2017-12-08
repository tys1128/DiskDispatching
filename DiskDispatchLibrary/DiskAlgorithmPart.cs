using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskDispatchLibrary
{
    partial class Disk
    {
        /// <summary>
        /// 将S中的请求，加载到Track[]中
        /// </summary>
        /// <param name="S"></param>
        void LoadRequest(List<KeyValuePair<int, int>> S)
        {
            foreach (var i in S)
            {
                Track[i.Key] = i.Value;
            }
            S.Clear();
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
            for (; i >= 0 && j < Track.Length; i--, j++)
            {
                if (Track[i] != 0)
                {
                    return i;
                }
                if (Track[j] != 0)
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
            DiskState.MoveIn = DiskState.Now - target > 0 ? true : false;
            DiskState.Target = target;
            //移动
            for (int i = DiskState.Now; i <= target; i++)
            {
                DiskState.Now++;

                DiskState.TotalSeekTime += TimePerTrack;
                DiskState.TotalRunTime += TimePerTrack;

                yield return DiskState;
            }
        }
        /// <summary>
        /// 读取当前磁道的数据
        /// </summary>
        /// <returns>磁盘状态</returns>
        IEnumerable<DiskState> Read()
        {
            int byteNum = Track[DiskState.Now]; //取出要读取的字节数
            Track[DiskState.Now] = 0;           //删除请求
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
            throw new NotImplementedException();
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
            while (mostNearTrack > 0)
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
                mostNearTrack = GetMostNearTrack(DiskState.Now);

            }
        }
        /// <summary>
        /// 扫描算法（SCAN）
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// 当前：读一个扇区的时间 = TimePerTrack = 1，
        /// </returns>
        public IEnumerable<DiskState> LOOK(List<KeyValuePair<int, int>> S)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 电梯算法（LOOK）
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// 当前：读一个扇区的时间 = TimePerTrack = 1，
        /// </returns>
        public IEnumerable<DiskState> SCAN(List<KeyValuePair<int, int>> S)
        {
            throw new NotImplementedException();
        }


    }
}
