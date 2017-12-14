using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskDispatchLibrary
{
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
        /// 跨越1个扇区所用时间（单位：毫秒）
        /// </summary>
        int TimePerSector { get; set; }
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
        /// 平均旋转延迟时间(ms),
        /// 通常使用磁盘旋转一周所需时间的1/2表示
        /// </summary>
        int ArgAccessDelay { get; set; }
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
        /// 启动后返回一次磁盘的状态，之后每个状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 返回一次
        /// </returns>
        IEnumerable<DiskState> FCFS(List<KeyValuePair<int, int>> S);
        /// <summary>
        /// 最短查找时间优先（SSTF）算法
        /// 使用与实现方法见：https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/yield
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// </returns>
        IEnumerable<DiskState> SSTF(List<KeyValuePair<int, int>> S);
        /// <summary>
        /// 扫描算法（SCAN）
        /// 使用与实现方法见：https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/yield
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// </returns>
        IEnumerable<DiskState> SCAN(List<KeyValuePair<int, int>> S);
        /// <summary>
        /// 电梯算法（LOOK）
        /// 使用与实现方法见：https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/yield
        /// </summary>
        /// <param name="S">磁道I/O访问序列S</param>
        /// <returns>
        /// 启动后返回一次磁盘的状态，之后返回的每个状态，状态间的间隔为读一个扇区的时间 (60*1000/(Rmp*SectorNum))ms 
        /// </returns>
        IEnumerable<DiskState> LOOK(List<KeyValuePair<int, int>> S);

    }
}
