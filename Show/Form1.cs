using System;
using System.Drawing;
using System.Windows.Forms;
using DiskDispatchLibrary;
using System.Collections.Generic;
using System.Threading;

namespace Show
{
    public partial class Form1 : Form
    {

        private Font TextFont = new Font("宋体", 17);
        private Brush TextBrush = new System.Drawing.SolidBrush(Color.Black);
        private Image DiskArmImage = global::Show.Properties.Resources.DiskArm;
        private Image DiskImage = global::Show.Properties.Resources.BackGround;

        private PointF DiskArmPosPointsLeftOn;
        private PointF DiskArmPosPointsRightDown;
        private PointF TextPos;
        private PointF DiskCenterPos;
        private PointF DiskArmCenterInDisImage;
        private PointF DiskArmCenterPos;
        private double DiskArmLength;
        private double MinDiskRadius;
        private double MaxDiskRadius;
        private double ArmCenterToDiskCenterLength;
        private int TrackNum;
        private double BaseAngle;

        Disk _disk = new Disk();

        private int BackBufferCount = 2;
        private int CurrutBackBuffer = 0;
        private Bitmap[][] BackBuffer;

        private int QueueSize = 300;
        private int CurrutQueueBegin = 0;
        private int[][] DiskArmPosQueue;

        private IEnumerator<DiskState>[] DiskStatesIterator = new IEnumerator<DiskState>[4];

        private PictureBox[] PictureBoxs;

        public Form1()
        {
            InitializeComponent();
            InitDatas();
            CreateBuffer();
        }

        private void CreateBuffer()
        {
            PictureBoxs = new PictureBox[4];
            PictureBoxs[0] = PictureBoxFCFS;
            PictureBoxs[1] = pictureBoxLOOK;
            PictureBoxs[2] = pictureBoxSCAN;
            PictureBoxs[3] = pictureBoxSSTF;

            BackBuffer = new Bitmap[4][];
            DiskArmPosQueue = new int[4][];

            for (int i = 0; i != 4; ++i)
            {
                BackBuffer[i] = new Bitmap[2];

                for (int j = 0; j != 2; ++j)
                {
                    BackBuffer[i][j] = new Bitmap(DiskImage.Width, DiskImage.Height);
                    BackBuffer[i][j].SetResolution(72, 72);
                }

                DiskArmPosQueue[i] = new int[QueueSize];
            }
        }

        private void InitDatas()
        {
            DiskCenterPos = new PointF(181.5f, 184.0f);
            DiskArmCenterInDisImage = new PointF(388.0f, 227.5f);
            DiskArmCenterPos = new PointF(175.0f, 37.5f);
            DiskArmLength = 173.0;
            MinDiskRadius = 63.12289600454022;
            TrackNum = 200;
            MaxDiskRadius = 169.8035629779305;
            BaseAngle = MathHelper.GetAngleOfTwoVector(new PointF(-1.0f, 0), new PointF(DiskCenterPos.X - DiskArmCenterInDisImage.X, DiskCenterPos.Y - DiskArmCenterInDisImage.Y));
            ArmCenterToDiskCenterLength = MathHelper.GetVectorLength(new PointF(DiskCenterPos.X - DiskArmCenterInDisImage.X, DiskCenterPos.Y - DiskArmCenterInDisImage.Y));
            TextPos = new PointF(525.0f, 220.0f);
            DiskArmPosPointsLeftOn = new PointF(525, 10);
            DiskArmPosPointsRightDown = new PointF(670, 210);

            List<KeyValuePair<int, int>> S = Disk.GetS(20);
            IEnumerable<DiskState>[] DiskStates = new IEnumerable<DiskState>[4];

            //测试用代码
            //DiskStates[0] = new Disk().Test(S);
            //DiskStates[1] = new Disk().Test(S);
            //DiskStates[2] = new Disk().Test(S);
            //DiskStates[3] = new Disk().Test(S);


            DiskStates[0] = new Disk().FCFS(S);
            DiskStates[1] = new Disk().LOOK(S);
            DiskStates[2] = new Disk().SCAN(S);
            DiskStates[3] = new Disk().SSTF(S);



            for (int i = 0; i != 4; ++i)
            {
                DiskStatesIterator[i] = DiskStates[i].GetEnumerator();
            }
        }

        protected void Draw(Bitmap _RenderTarget, DiskState _In, int[] _DiskArmPosQueue)
        {
            float extraAngle = (float)(MathHelper.GetAngleOfTrigle(ArmCenterToDiskCenterLength, DiskArmLength, MathHelper.Lerp((double)MaxDiskRadius, (double)MinDiskRadius, (double)_In.Now / TrackNum))[2]);
            Graphics g = Graphics.FromImage(_RenderTarget);
            g.Clear(Color.FromArgb(0));
            g.TranslateTransform(DiskArmCenterInDisImage.X, DiskArmCenterInDisImage.Y);
            g.RotateTransform((float)BaseAngle + extraAngle);
            g.TranslateTransform(-DiskArmCenterInDisImage.X, -DiskArmCenterInDisImage.Y);
            g.DrawImage(DiskArmImage, new PointF(DiskArmCenterInDisImage.X - DiskArmCenterPos.X, DiskArmCenterInDisImage.Y - DiskArmCenterPos.Y));
            g.ResetTransform();
            g.DrawString(string.Format(
                    "移动方向:{0}\n目标磁道编号:{1}\n当前磁道:{2}\n总寻道时间:{3}\n总传输时间:{4}\n平均传输时间:{5}\n总运行时间:{6}",
                    _In.MoveIn ? "向内" : "向外", _In.Target, _In.Now, _In.TotalSeekTime, _In.TotalAccessTime, _disk.ArgAccessDelay, _In.TotalRunTime),
                     TextFont, TextBrush, TextPos.X, TextPos.Y);

            for (float i = DiskArmPosPointsLeftOn.X; i <= DiskArmPosPointsRightDown.X; i += 1)
            {
                _RenderTarget.SetPixel((int)i, (int)DiskArmPosPointsLeftOn.Y, Color.BlanchedAlmond);
                _RenderTarget.SetPixel((int)i, (int)DiskArmPosPointsRightDown.Y, Color.BlanchedAlmond);
            }

            for (int i = 0; i != QueueSize; ++i)
            {
                _RenderTarget.SetPixel((int)MathHelper.Lerp(DiskArmPosPointsLeftOn.X, DiskArmPosPointsRightDown.X, (double)i / QueueSize),
                    (int)MathHelper.Lerp(DiskArmPosPointsRightDown.Y, DiskArmPosPointsLeftOn.Y, (double)(_DiskArmPosQueue[(CurrutQueueBegin + i) % QueueSize]) / TrackNum),
                    Color.Black);
            }

            for (int i = 0; i != TrackNum; ++i)
                if (_In.Track[i] != 0)
                {
                    _RenderTarget.SetPixel((int)DiskArmPosPointsRightDown.X, (int)MathHelper.Lerp(DiskArmPosPointsRightDown.Y, DiskArmPosPointsLeftOn.Y, (double)i / TrackNum), Color.Red);
                    _RenderTarget.SetPixel((int)DiskArmPosPointsRightDown.X, (int)MathHelper.Lerp(DiskArmPosPointsRightDown.Y, DiskArmPosPointsLeftOn.Y, (double)i / TrackNum) + 1, Color.Red);
                    _RenderTarget.SetPixel((int)DiskArmPosPointsRightDown.X + 1, (int)MathHelper.Lerp(DiskArmPosPointsRightDown.Y, DiskArmPosPointsLeftOn.Y, (double)i / TrackNum), Color.Red);
                    _RenderTarget.SetPixel((int)DiskArmPosPointsRightDown.X + 1, (int)MathHelper.Lerp(DiskArmPosPointsRightDown.Y, DiskArmPosPointsLeftOn.Y, (double)i / TrackNum) + 1, Color.Red);
                }

            g.Dispose();
        }

        private void Update(object sender, EventArgs e)
        {
            for (int i = 0; i != 4; ++i)
            {
                if (DiskStatesIterator[i].MoveNext())
                {
                    DiskArmPosQueue[i][(CurrutQueueBegin + QueueSize - 1) % QueueSize] = DiskStatesIterator[i].Current.Now;

                    Draw(BackBuffer[i][CurrutBackBuffer], DiskStatesIterator[i].Current, DiskArmPosQueue[i]);
                    PictureBoxs[i].Image = BackBuffer[i][CurrutBackBuffer];
                }
            }

            CurrutBackBuffer = (CurrutBackBuffer + 1) % BackBufferCount;
            CurrutQueueBegin = (CurrutQueueBegin + 1) % QueueSize;
        }

        private void OnRebot(object sender, EventArgs e)
        {
            List<KeyValuePair<int, int>> S = Disk.GetS(20);
            IEnumerable<DiskState>[] DiskStates = new IEnumerable<DiskState>[4];

            DiskStates[0] = new Disk().FCFS(S);
            DiskStates[1] = new Disk().LOOK(S);
            DiskStates[2] = new Disk().SCAN(S);
            DiskStates[3] = new Disk().SSTF(S);

            for (int i = 0; i != 4; ++i)
            {
                DiskStatesIterator[i] = DiskStates[i].GetEnumerator();
                for (int j = 0; j != QueueSize; ++j)
                    DiskArmPosQueue[i][j] = 0;
            }
        }
    }
}
