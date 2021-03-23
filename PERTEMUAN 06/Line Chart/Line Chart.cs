using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerGraphics.Class_Meeting.PERTEMUAN_06
{
    public partial class FormStatistics : Form
    {
        // INTERNAL VARIABLES
        internal Bitmap bitmapPicture_Statistics;
        internal int TopCoordinate, LeftCoordinate;
        internal int[] ValueArray;
        private int dataTableCount;
        private int DrawingWidth;
        private int DrawingHeight;
        private int ScaleWidth, ScaleHeight;
        private Point[] ChartPoints;

        public FormStatistics()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            InitializeAttributes();
            
            LetsDraw();
        }

        private void InitializeAttributes()
        {
            bitmapPicture_Statistics = new Bitmap(pbStatistics.Width, pbStatistics.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            dataTableCount = 5;
            TopCoordinate = LeftCoordinate = 50;

            ValueArray = new int[dataTableCount];
            ValueArray[0] = 8;
            ValueArray[1] = 10;
            ValueArray[2] = 7;
            ValueArray[3] = 6;
            ValueArray[4] = 4;

            DrawingWidth = pbStatistics.Width - (2 * LeftCoordinate);
            DrawingHeight = pbStatistics.Height - (2 * TopCoordinate);

            ChartPoints = new Point[dataTableCount];
        }

        internal void LetsDraw()
        {
            DrawVerticalLine(pbStatistics, bitmapPicture_Statistics, LeftCoordinate, TopCoordinate, pbStatistics.Height - TopCoordinate, 0xff, 0x00, 0xff);
            DrawHorizontalLine(pbStatistics, bitmapPicture_Statistics, LeftCoordinate, pbStatistics.Width - LeftCoordinate, pbStatistics.Height - TopCoordinate, 0xff, 0x00, 0xff);
            DrawLineChart(pbStatistics, bitmapPicture_Statistics);
        }

        private void DrawLine(PictureBox pictureBox, Bitmap bitmapPicture, int xStart, int yStart, int xEnd, int yEnd, byte RedColor, byte GreenColor, byte BlueColor)
        {
            /**
             * Buat Line antar 2 titik
             * Dengan sudut yang fleksibel (tidak hanya 45 derajat)
             */

            int xDistance, yDistance, Count, countOrder;
            float xOrder, yOrder, xRatio, yRatio, lineWidth, lineLength;
            // NB: bila lineWidth = integer, nantinya: Int32/ Int32 = Int32 (tidak bisa ke float)
            // Sehingga lineWidth dan lineLength harus berformat desimal (float)

            xOrder = xStart;
            yOrder = yStart;

            xDistance = xEnd - xStart; // Bisa positif, bisa negatif
            yDistance = yEnd - yStart; // Bisa positif, bisa negatif

            lineLength = Math.Abs(xDistance); // panjang koordinat x
            lineWidth = Math.Abs(yDistance); // panjang koordinat y

            // Bila panjang > lebar
            if (lineLength > lineWidth)
            {
                xRatio = 1;
                yRatio = lineWidth / lineLength;
                Count = Convert.ToInt32(lineLength);
            }
            // Bila panjang < lebar
            else
            {
                yRatio = 1;
                xRatio = lineLength / lineWidth;
                Count = Convert.ToInt32(lineWidth);
            }

            for (countOrder = 0; countOrder <= Count; countOrder++)
            {
                int R = Convert.ToInt32(RedColor);
                int G = Convert.ToInt32(GreenColor);
                int B = Convert.ToInt32(BlueColor);

                int xOrderInt = Convert.ToInt32(xOrder);
                int yOrderInt = Convert.ToInt32(yOrder);

                bitmapPicture.SetPixel(xOrderInt, yOrderInt, Color.FromArgb(R, G, B));

                if (xDistance > 0 && yDistance >= 0) // Kuadran 1
                {
                    xOrder += xRatio;
                    yOrder += yRatio;
                }
                else if (xDistance > 0 && yDistance < 0) // Kuadran 4
                {
                    xOrder += xRatio;
                    yOrder -= yRatio;
                }
                else if (xDistance < 0 && yDistance > 0) // Kuadran 2
                {
                    xOrder -= xRatio;
                    yOrder += yRatio;
                }
                else if (xDistance < 0 && yDistance < 0) // Kuadran 3
                {
                    xOrder -= xRatio;
                    yOrder -= yRatio;
                }
            }
        }

        private void FillRectangle(PictureBox pictureBox, Bitmap bitmapPicture, int xStart, int yStart, int xEnd, int yEnd, byte RedColor, byte GreenColor, byte BlueColor)
        {
            /**
             * Isi bangun datar secara titik per titik
             * Hingga titiknya menutupi bangun datar tersebut
             * 
             * Kelemahan: cuma berlaku di Rectangle saja
             */

            int xOrder, yOrder;
            for (xOrder = xStart + 1; xOrder < xEnd; xOrder++)
            {
                for (yOrder = yStart + 1; yOrder < yEnd; yOrder++)
                {
                    int R = Convert.ToInt32(RedColor);
                    int G = Convert.ToInt32(GreenColor);
                    int B = Convert.ToInt32(BlueColor);

                    bitmapPicture.SetPixel(xOrder, yOrder, Color.FromArgb(R, G, B));
                    pictureBox.Image = bitmapPicture;
                }
            }
        }

        private void DrawHorizontalLine(PictureBox pictureBox, Bitmap bitmapPicture, int xStart, int xEnd, int y, byte RedColor, byte GreenColor, byte BlueColor)
        {
            /**
             * Horizontal Line berasal dari 2 titik yang berbeda
             * Tapi nilai y sama
             */

            int xOrder;
            for (xOrder = xStart; xOrder <= xEnd; xOrder++)
            {
                int R = Convert.ToInt32(RedColor);
                int G = Convert.ToInt32(GreenColor);
                int B = Convert.ToInt32(BlueColor);

                bitmapPicture.SetPixel(xOrder, y, Color.FromArgb(R, G, B));
                pbStatistics.Image = bitmapPicture;
            }
        }

        private void DrawVerticalLine(PictureBox pictureBox, Bitmap bitmapPicture, int x, int yStart, int yEnd, byte RedColor, byte GreenColor, byte BlueColor)
        {
            /**
             * Vertical Line berasal dari 2 titik yang berbeda
             * Tapi nilai x sama
             */

            int yOrder;

            for (yOrder = yStart; yOrder <= yEnd; yOrder++)
            {
                int R = Convert.ToInt32(RedColor);
                int G = Convert.ToInt32(GreenColor);
                int B = Convert.ToInt32(BlueColor);

                bitmapPicture.SetPixel(x, yOrder, Color.FromArgb(R, G, B));
                pictureBox.Image = bitmapPicture;
            }
        }

        private void DrawRectangle(PictureBox pictureBox, Bitmap bitmapPicture, int xStart, int yStart, int xEnd, int yEnd, byte RedColor, byte GreenColor, byte BlueColor)
        {
            /**
             * Rectangle dibangun dari 4 garis
             * 2 garis horizontal, 2 garis vertikal
             * 
             * Kelemahan: hanya berlaku untuk rectangle dengan posisi TEGAK atau TELENTANG
             */

            DrawHorizontalLine(pictureBox, bitmapPicture, xStart, xEnd, yStart, RedColor, GreenColor, BlueColor); // Buat garis horizontal 1
            DrawHorizontalLine(pictureBox, bitmapPicture, xStart, xEnd, yEnd, RedColor, GreenColor, BlueColor); // Buat garis horizontal 2
            DrawVerticalLine(pictureBox, bitmapPicture, xStart, yStart, yEnd, RedColor, GreenColor, BlueColor); // Buat garis vertikal 1
            DrawVerticalLine(pictureBox, bitmapPicture, xEnd, yStart, yEnd, RedColor, GreenColor, BlueColor); // Buat garis vertikal 2
        }

        private int GetMaximumValue()
        {
            int i, SavedMaxValue;

            i = 1; SavedMaxValue = ValueArray[0];
            while (i < dataTableCount)
            {
                if (SavedMaxValue < ValueArray[i])
                    SavedMaxValue = ValueArray[i];
                i++;
            }
            return SavedMaxValue;
        }

        private void DrawLineChart(PictureBox pictureBox, Bitmap bitmapPicture)
        {
            // this.labelSS = new System.Windows.Forms.Label();

            int MaximumRangeAxis;
            Point ZeroPoint = new Point();
            Point StartPoint = new Point();
            Point EndPoint = new Point();
            int currentStartPointX;
            int DataOrder;

            MaximumRangeAxis = GetMaximumValue();
            ScaleHeight = DrawingHeight / MaximumRangeAxis;
            ScaleWidth = DrawingWidth / dataTableCount;

            // Titik 0
            ZeroPoint.X = LeftCoordinate;
            ZeroPoint.Y = pbStatistics.Height - TopCoordinate;
            currentStartPointX = ZeroPoint.X;

            // Buat sebuah point LINE CHART
            for (DataOrder = 0; DataOrder < dataTableCount; DataOrder++)
            {
                currentStartPointX += ScaleWidth;
                ChartPoints[DataOrder].X = currentStartPointX;
                ChartPoints[DataOrder].Y = ZeroPoint.Y - (ValueArray[DataOrder] * ScaleHeight);
            }

            // Tarik sebuah garis untuk menghubungkan point2 tersebut
            for (DataOrder = 0; DataOrder < dataTableCount; DataOrder++)
            {
                if (DataOrder < dataTableCount - 1)
                {
                    // Titik awal
                    StartPoint.X = ChartPoints[DataOrder].X;
                    StartPoint.Y = ChartPoints[DataOrder].Y;

                    // Titik akhir
                    EndPoint.X = ChartPoints[DataOrder + 1].X;
                    EndPoint.Y = ChartPoints[DataOrder + 1].Y;

                    DrawLine(pbStatistics, bitmapPicture_Statistics, StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y, 0xff, 0x00, 0x00);
                }
            }
        }

        private void DrawBarChart()
        {

        }

        private void Question()
        {
            /**
             * 1. Apakah c#, mesti buat bitmapPictrue.exit?
             * 
             */
        }
    }
}