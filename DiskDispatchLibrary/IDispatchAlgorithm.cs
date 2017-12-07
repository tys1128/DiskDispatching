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
        bool MoveIn { get; set; }
        /// <summary>
        /// 目标磁道编号
        /// </summary>
        int Target { get; set; }
        /// <summary>
        /// 当前磁道
        /// </summary>
        int Now { get; set; }


        /// <summary>
        /// 寻道时间(ms)
        /// </summary>
        int TotalSeekTime { get; set; }
        /// <summary>
        /// 总传输（访问）时间(ms)
        /// </summary>
        int TotalAccessTime { get; set; }
        /// <summary>
        /// 平均旋转延迟时间(ms),
        /// 读取时，一个磁道未读取的扇区/SectorNum
        /// </summary>
        int ArgAccessDelay { get; set; }
        /// <summary>
        /// 所有访问处理时间(ms)
        /// </summary>
        int TotalRunTime { get; set; }

    }
    /// <summary>
    /// 磁盘参数设置接口
    /// </summary>
    interface IDiskArgument
    {
        /// <summary>
        /// 跨越1个磁道所用时间（单位：毫秒）
        /// </summary>
        int TimePerTrack { get; set; }
        /// <summary>
        /// 启动时间（单位：毫秒）
        /// </summary>
        int TimeToStart { get; set; }
        /// <summary>
        /// 磁盘转速（单位：转/分钟）
        /// </summary>
        int Rpm { get; set; }
        /// <summary>
        /// 每磁道扇区（块）数
        /// </summary>
        int SectorNum { get; set; }
        /// <summary>
        /// 每扇区（块）字节数
        /// </summary>
        int BytePerSector { get; set; }
        /// <summary>
        /// 盘面的磁道数（由外向内）固定为：0,1,2，……，198,199
        /// 每个单元存储磁道的I/O请求数量（也许是请求的字节数的意思 =_=）
        /// </summary>
        int[] Track { get; set; }
        /// <summary>
        /// 磁盘当前状态
        /// </summary>
        DiskState DiskState { get; }
    }


    /// <summary>
    /// 磁盘调度算法接口
    /// </summary>
    interface IDispatchAlgorithm
    {
        /// <summary>
        /// 先到先服务（FCFS）算法
        /// 使用与实现方法见：https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/yield
        /// </summary>
        /// <param name="S">磁道I/O访问序列S, 参数为磁道编号，访问字节数</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后每经过读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 返回一次
        /// </returns>
        IEnumerable<DiskState> FCFS(List<KeyValuePair<int, int>> S);
        /// <summary>
        /// 最短查找时间优先（SSTF）算法
        /// 使用与实现方法见：https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/yield
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后每经过读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 返回一次
        /// </returns>
        IEnumerable<DiskState> SSTF(List<KeyValuePair<int, int>> S);
        /// <summary>
        /// 扫描算法（SCAN）
        /// 使用与实现方法见：https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/yield
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后每经过读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 返回一次
        /// </returns>
        IEnumerable<DiskState> SCAN(List<KeyValuePair<int, int>> S);
        /// <summary>
        /// 电梯算法（LOOK）
        /// 使用与实现方法见：https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/yield
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后每经过读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 返回一次
        /// </returns>
        IEnumerable<DiskState> LOOK(List<KeyValuePair<int, int>> S);

    }
}
