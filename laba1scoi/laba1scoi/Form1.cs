using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Numerics;
using Microsoft.VisualBasic.Logging;

namespace laba1scoi
{
    public partial class Form1 : Form
    {

        static Bitmap imgS1 = new Bitmap("..\\..\\..\\in.jpg");
        static Bitmap imgS2 = new Bitmap("..\\..\\..\\in2.jpg");

        static int h = imgS1.Height;
        static int w = imgS1.Width;

        Bitmap img1 = new Bitmap(imgS1, w, h);
        Bitmap img2 = new Bitmap(imgS2, w, h);
        Bitmap img_out = new Bitmap(w, h);
        public Form1()
        {
            InitializeComponent();

            var canvas1 = new MyCanvas();
            canvas1.Dock = DockStyle.Fill;
            canvas1.img = pictureBox1.Image;

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    var pix1 = img1.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i);

                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;


                    var pix = Color.FromArgb(r, g, b);

                    img_out.SetPixel(j, i, pix);

                }
            }

            pictureBox1.Image = img_out;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = img1;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Image = img2;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.Image = makeHistogram(new Bitmap(pictureBox1.Image));


        } //static void Main(string[] args)

        static Complex[] DFT(Complex[] x, double n = 1)
        {
            int N = x.Length;
            Complex[] G = new Complex[N]; // 

            for (int u = 0; u < N; ++u)
            {
                double _fi = -2.0 * Math.PI * u / N;
                for (int k = 0; k < N; ++k)
                {
                    double fi = _fi * k;
                    G[u] += (new Complex(Math.Cos(fi), Math.Sin(fi)) * x[k]);
                }
                G[u] = n * G[u]; //для умножения на 1/N для прямого преобразования I
            }

            return G;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = furie(imgS1);
        }
        double color(double c)
        {
            double ty = c;
            if (c > 255) { ty = 255; }
            if (c < 0) ty = 0;
            return ty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2(furie(imgS1), imgS1);
            frm2.Show();
        }

        private Bitmap furie(Bitmap bitmap)
        {
            var w = bitmap.Width;
            var h = bitmap.Height;

            double[,] iznr = new double[w, h];
            double[,] izng = new double[w, h];
            double[,] iznb = new double[w, h];

            Complex[,] fiznr = new Complex[w, h];
            Complex[,] fizng = new Complex[w, h];
            Complex[,] fiznb = new Complex[w, h];

            Complex[,] fiznr1 = new Complex[h, w];
            Complex[,] fizng1 = new Complex[h, w];
            Complex[,] fiznb1 = new Complex[h, w];

            var fr = new Complex[w * h];
            var fb = new Complex[w * h];
            var fg = new Complex[w * h];
            var fr1 = new Complex[h, w];
            var fb1 = new Complex[h, w];
            var fg1 = new Complex[h, w];
            var start = new Bitmap(w, h);
            var finish = new Bitmap(w, h);
            var Fstart = new Bitmap(w, h);
            var Ffinish = new Bitmap(w, h);

            //заполнение черным
            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Fstart.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                    Ffinish.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                }
            }

            //массив чисел

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    var pix = bitmap.GetPixel(j, i);
                    int r = pix.R;
                    int g = pix.G;
                    int b = pix.B;
                    iznr[j, i] = r * Math.Pow(-1, i + j);
                    izng[j, i] = g * Math.Pow(-1, i + j);
                    iznb[j, i] = b * Math.Pow(-1, i + j);
                    start.SetPixel(j, i, pix);

                }
            }


            for (int i = 0; i < h; ++i)
            {
                double[] diznr = new double[w];
                double[] dizng = new double[w];
                double[] diznb = new double[w];
                for (int j = 0; j < w; ++j)
                {
                    diznr[j] = iznr[j, i];
                    dizng[j] = izng[j, i];
                    diznb[j] = iznb[j, i];
                }

                var c_iznr = diznr.Select(x => new Complex(x, 0)).ToArray();
                var c_iznb = diznb.Select(x => new Complex(x, 0)).ToArray();
                var c_izng = dizng.Select(x => new Complex(x, 0)).ToArray();
                var fur = DFT(c_iznr, 1.0 / w);
                var fub = DFT(c_iznb, 1.0 / w);
                var fug = DFT(c_izng, 1.0 / w);

                for (int j = 0; j < w; ++j)
                {
                    fiznr[j, i] = fur[j];
                    fizng[j, i] = fug[j];
                    fiznb[j, i] = fub[j];
                }
            }


            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    fiznr1[i, j] = fiznr[j, i];
                    fiznb1[i, j] = fiznb[j, i];
                    fizng1[i, j] = fizng[j, i];
                }
            }

            for (int i = 0; i < w; ++i)
            {
                Complex[] diznr = new Complex[h];
                Complex[] diznb = new Complex[h];
                Complex[] dizng = new Complex[h];
                for (int j = 0; j < h; ++j)
                {
                    diznr[j] = fiznr1[j, i];
                    diznb[j] = fiznb1[j, i];
                    dizng[j] = fizng1[j, i];
                }

                var fur = DFT(diznr, 1.0 / h);
                var fub = DFT(diznb, 1.0 / h);
                var fug = DFT(dizng, 1.0 / h);


                for (int j = 0; j < h; ++j)
                {
                    fr1[j, i] = fur[j];
                    fb1[j, i] = fub[j];
                    fg1[j, i] = fug[j];
                    fr[i + j * w] = fur[j];
                    fb[i + j * w] = fub[j];
                    fg[i + j * w] = fug[j];
                }
            }


            int[] yr = new int[w * h];
            int[] yb = new int[w * h];
            int[] yg = new int[w * h];
            int cg = 0;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    fr[cg] = fr1[i, j];
                    fb[cg] = fb1[i, j];
                    fg[cg] = fg1[i, j];
                    cg++;
                }

            var Furie_Light = 4;

            double er = (int)(Furie_Light);
            var mar = fr[1].Real;
            var mag = fg[1].Real;
            var mab = fb[1].Real;
            //Чисто визуализация
            for (int i = 0; i < fr.Length; i++)
            {
                var gh = Math.Log(fr[i].Imaginary + 1);
                if (gh > mar) mar = gh;
                gh = Math.Log(fb[i].Imaginary + 1);
                if (gh > mab) mab = gh;
                gh = Math.Log(fg[i].Imaginary + 1);
                if (gh > mag) mag = gh;

            }
            var po = 255.0 / er;
            var gi = 1.0;
            if (po < 100) { gi = gi + 0.5 * er; }
            for (int i = 0; i < w * h; ++i)
            {
                yr[i] = (int)color(gi * Math.Log(fr[i].Magnitude + 1) * 255 / mar);
                yb[i] = (int)color(gi * Math.Log(fb[i].Magnitude + 1) * 255 / mab);
                yg[i] = (int)color(gi * Math.Log(fg[i].Magnitude + 1) * 255 / mag);

            }
            yr = yr.Select((x, i) => (x < po) ? (x = 0) : x).ToArray();
            yb = yb.Select((x, i) => (x < po) ? (x = 0) : x).ToArray();
            yg = yg.Select((x, i) => (x < po) ? (x = 0) : x).ToArray();

            for (int j = 0; j < h; ++j)
            {
                for (int i = 0; i < w; ++i)
                {
                    iznr[i, j] = yr[i + j * w];
                    iznb[i, j] = yb[i + j * w];
                    izng[i, j] = yg[i + j * w];
                }
            }

            for (int j = 0; j < w; ++j)
            {
                for (int i = 0; i < h; ++i)
                {
                    var pix = Fstart.GetPixel(j, i);
                    int r = (int)iznr[j, i];
                    int b = (int)iznb[j, i];
                    int g = (int)izng[j, i];
                    Ffinish.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            }

            return Ffinish;

        }

        static void LineFilter(byte[] bgrAValues, int[] size, double[] M, int a, int b, double[] out_test)
        {
            Queue<List<byte>> S = new Queue<List<byte>>();
            for (int i = 0; i < size[0]; ++i)
            {
                for (int j = 0; j < size[1]; ++j)
                {
                    if (i > (a - 1) / 2)
                    {
                        byte[] front = S.First().ToArray();
                        bgrAValues[4 * ((i - (a - 1) / 2 - 1) * size[1] + j)] = front[0];
                        bgrAValues[4 * ((i - (a - 1) / 2 - 1) * size[1] + j) + 1] = front[1];
                        bgrAValues[4 * ((i - (a - 1) / 2 - 1) * size[1] + j) + 2] = front[2];
                        S.Dequeue();
                    }
                    double[] sum = { 0, 0, 0 };
                    for (int it = 0; it < a; ++it)
                    {
                        for (int jt = 0; jt < b; ++jt)
                        {
                            int iter = i - (a - 1) / 2 + it; iter = (iter < 0) ? (0 - iter) : iter;
                            iter = (iter > (size[0] - 1)) ? (2 * (size[0] - 1) - iter) : iter;
                            int jter = j - (b - 1) / 2 + jt;
                            jter = (jter < 0) ? (0 - jter) : jter;
                            jter = (jter > (size[1] - 1)) ? (2 * (size[1] - 1) - jter) : jter;
                            sum[0] += (double)bgrAValues[4 * (iter * size[1] + jter)] * M[it * b + jt];
                            sum[1] += (double)bgrAValues[4 * (iter * size[1] + jter) + 1] * M[it * b + jt];
                            sum[2] += (double)bgrAValues[4 * (iter * size[1] + jter) + 2] * M[it * b + jt];
                        }
                    }
                    List<byte> tmpS = new List<byte> {
                                                        (byte)Math.Min(Math.Max(Math.Round(sum[0]), byte.MinValue), byte.MaxValue),
                                                        (byte)Math.Min(Math.Max(Math.Round(sum[1]), byte.MinValue), byte.MaxValue),
                                                        (byte)Math.Min(Math.Max(Math.Round(sum[2]), byte.MinValue), byte.MaxValue)
                                                                                                                                   };

                    out_test[i * size[1] + j] = tmpS[0];

                    S.Enqueue(tmpS);
                }
            }
            for (int i = size[0] - (a - 1) / 2 - 1; i < size[0]; ++i)
            {
                for (int j = 0; j < size[1]; ++j)
                {
                    byte[] front = S.First().ToArray();
                    bgrAValues[4 * (i * size[1] + j)] = front[0];
                    bgrAValues[4 * (i * size[1] + j) + 1] = front[1];
                    bgrAValues[4 * (i * size[1] + j) + 2] = front[2];
                    S.Dequeue();
                }
            }
        }

        public System.Drawing.Image MakeLineFilter(System.Drawing.Image image)
        {

            List<double> M = getCore();

            Bitmap Bimage = new Bitmap(image);
            Rectangle rect = new Rectangle(0, 0, Bimage.Width, Bimage.Height);
            BitmapData bmpData =
                Bimage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * Bimage.Height;
            byte[] bgrAValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, bgrAValues, 0, bytes);

            double[] test = new double[Bimage.Height * Bimage.Width];

            LineFilter(bgrAValues, new int[2] { Bimage.Height, Bimage.Width }, M.ToArray(), 3, 3, test);

            System.Runtime.InteropServices.Marshal.Copy(bgrAValues, 0, ptr, bytes);

            Bimage.UnlockBits(bmpData);

            return Bimage;
        }

        static Byte partition(Byte[] arr, int l, int r)
        {
            int x = arr[r], i = l;
            for (int j = l; j <= r - 1; j++)
            {
                if (arr[j] <= x)
                {
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    i++;
                }
            }
            (arr[i], arr[r]) = (arr[r], arr[i]);
            return Convert.ToByte(i);
        }

        static Byte kthSmallest(Byte[] arr, int l, int r, int k)
        {
            if (k > 0 && k <= r - l + 1)
            {

                int index = partition(arr, l, r);

                if (index - l == k - 1)
                    return arr[index];

                if (index - l > k - 1)
                    return kthSmallest(arr, l, index - 1, k);

                return kthSmallest(arr, index + 1, r,
                    k - index + l - 1);
            }

            return Convert.ToByte(int.MaxValue);
        }

        static void MedianFilter(Byte[] bgrAValues, int[] size, int a, int b, double[] out_test)
        {
            Queue<byte[]> S = new Queue<byte[]>();

            for (int i = 0; i < size[0]; ++i)
            {
                for (int j = 0; j < size[1]; ++j)
                {
                    if (i > (a - 1) / 2)
                    {
                        bgrAValues[4 * ((i - (a - 1) / 2 - 1) * size[1] + j)] = S.Peek()[0];
                        bgrAValues[4 * ((i - (a - 1) / 2 - 1) * size[1] + j) + 1] = S.Peek()[1];
                        bgrAValues[4 * ((i - (a - 1) / 2 - 1) * size[1] + j) + 2] = S.Peek()[2];
                        S.Dequeue();
                    }

                    List<byte> data1 = new List<byte>();
                    List<byte> data2 = new List<byte>();
                    List<byte> data3 = new List<byte>();

                    for (int it = 0; it < a; ++it)
                    {
                        for (int jt = 0; jt < b; ++jt)
                        {
                            int iter = i - (a - 1) / 2 + it;
                            iter = (iter < 0) ? (0 - iter) : iter;
                            iter = (iter > (size[0] - 1)) ? (2 * (size[0] - 1) - iter) : iter;

                            int jter = j - (b - 1) / 2 + jt;
                            jter = (jter < 0) ? (0 - jter) : jter;
                            jter = (jter > (size[1] - 1)) ? (2 * (size[1] - 1) - jter) : jter;

                            data1.Add(bgrAValues[4 * (iter * size[1] + jter)]);
                            data2.Add(bgrAValues[4 * (iter * size[1] + jter) + 1]);
                            data3.Add(bgrAValues[4 * (iter * size[1] + jter) + 2]);
                        }
                    }

                    kthSmallest(data1.ToArray(), 0, data1.Count - 1, (data1.Count - 1) / 2 + 1);
                    S.Enqueue(new byte[] {
            kthSmallest(data1.ToArray(), 0, data1.Count - 1, (data1.Count - 1) / 2 + 1),
            kthSmallest(data2.ToArray(), 0, data2.Count - 1, (data2.Count - 1) / 2 + 1),
            kthSmallest(data3.ToArray(), 0, data3.Count - 1, (data3.Count - 1) / 2 + 1) });

                    data1.Clear();
                    data1 = null;
                    data2.Clear();
                    data2 = null;
                    data3.Clear();
                    data3 = null;
                }
            }

            for (int i = size[0] - (a - 1) / 2 - 1; i < size[0]; ++i)
            {
                for (int j = 0; j < size[1]; ++j)
                {
                    bgrAValues[4 * (i * size[1] + j)] = S.Peek()[0];
                    bgrAValues[4 * (i * size[1] + j) + 1] = S.Peek()[1];
                    bgrAValues[4 * (i * size[1] + j) + 2] = S.Peek()[2];

                    S.Dequeue();
                }
            }
        }

        public static System.Drawing.Image CPP_MedianFilter(System.Drawing.Image image, int a, int b)
        {
            Bitmap Bimage = new Bitmap(image);
            Rectangle rect = new Rectangle(0, 0, Bimage.Width, Bimage.Height);
            BitmapData bmpData =
                Bimage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * Bimage.Height;
            byte[] bgrAValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, bgrAValues, 0, bytes);

            double[] test = new double[Bimage.Height * Bimage.Width];

            MedianFilter(bgrAValues, new int[2] { Bimage.Height, Bimage.Width }, a, b, test);

            System.Runtime.InteropServices.Marshal.Copy(bgrAValues, 0, ptr, bytes);

            Bimage.UnlockBits(bmpData);

            return Bimage;
        }

        public List<double> getCore()
        {
            List<double> Core = new List<double>();
            int a = richTextBox1.Lines.Length;
            int b = richTextBox1.Lines[0].Split(' ').Length;
            bool error = false;
            foreach (string str in richTextBox1.Lines)
            {
                var stringSplit = str.Split(' ');
                if (b != str.Split(' ').Length)
                    error = true;
                foreach (string item in stringSplit)
                {
                    Core.Add(Convert.ToDouble(item));
                }               
            }

            return Core;
        }

        void GetGauss(double[] out_val, double sig, int a, int b)
        {
            for (int i = 0; i < a; ++i)
            {
                int it = i - (a - 1) / 2;
                for (int j = 0; j < b; ++j)
                {
                    int jt = j - (b - 1) / 2;
                    out_val[i * b + j] = (1.0 / (2 * Math.PI * sig * sig)) * Math.Exp(-(it * it + jt * jt) / (2 * sig * sig));
                }
            }
        }

        public static Bitmap fura()
        {
            var rnd = new Random();
            //создадим входной массив из случайных чисел
            var hi = imgS1.Height;
            var wi = imgS1.Width;
            Bitmap outImg = new Bitmap(wi, hi);
            var input_arr = new double[wi, hi];

            for (int y = 0; y < hi; y++)
            {
                for (int x = 0; x < wi; x++)
                {
                    //var c = (inputImg.GetPixel(x, y).R + inputImg.GetPixel(x, y).G + inputImg.GetPixel(x, y).B) / 3;
                    var c = (int)(0.2125 * imgS1.GetPixel(x, y).R + 0.7154 * imgS1.GetPixel(x, y).G + 0.0721 * imgS1.GetPixel(x, y).B);
                    input_arr[y, x] = c;
                }
            }

            var N = 256;

            //Преобразуем наш входной массив в массив комплексных числел //с нулевой мнимой частью
            Complex[,] c_input_arr = new Complex[wi, hi];

            for (int y = 0; y < hi; y++)
            {
                for (int x = 0; x < wi; x++)
                {
                    c_input_arr[y, x] = new Complex(input_arr[y, x], 0);
                }
            }



            //выполняем ДПФ
            Complex[] tmp = new Complex[wi];
            for (int y = 0; y < hi; y++)
            {
                for (int x = 0; x < wi; x++)
                {
                    tmp[x] = c_input_arr[y, x];
                }
                tmp = DFT(tmp, 1.0 / N);
                for (int x = 0; x < wi; x++)
                {
                    c_input_arr[y, x] = tmp[x];
                }
            }

            for (int y = 0; y < hi; y++)
            {
                for (int x = 0; x < wi; x++)
                {
                    tmp[x] = c_input_arr[x, y];
                }
                tmp = DFT(tmp, 1.0 / N);
                for (int x = 0; x < wi; x++)
                {
                    c_input_arr[x, y] = tmp[x];
                }
            }

            //Закоментированный низкочастотный фильтр c полосой отсечения 5.
            //furier = furier.Select( (x,i) => (i>5 ? (new Complex(0,0)) : x ) ).ToArray();

            //Закоментированный высокочастотный фильтр c полосой отсечения 5.
            //furier = furier.Select( (x,i) => (i<5 ? (new Complex(0,0)) : x ) ).ToArray();

            //Закоментированный режекторный фильтр c полосой отсечения [5...9]
            //furier = furier.Select( (x,i) => ( (i>5)&&(i<9) ? (new Complex(0,0)) : x ) ).ToArray();

            //Закоментированный полосовой фильтр c полосой пропускания [5...9]
            //furier = furier.Select( (x,i) => ( (i<5)||(i>9) ? (new Complex(0,0)) : x ) ).ToArray();

            var m = 0;
            for (int y = 0; y < hi; y++)
            {
                for (int x = 0; x < wi; x++)
                {
                    if (Math.Log(c_input_arr[x, y].Magnitude + 1) > m)
                        m = (int)Math.Log(c_input_arr[x, y].Magnitude + 1);
                }
            }
            for (int y = 0; y < hi; y++)
            {
                for (int x = 0; x < wi; x++)
                {
                    var magn = (int)c_input_arr[x, y].Magnitude;
                    var pix = Color.FromArgb(255, (int)Math.Log(magn + 1) * m / 255, (int)Math.Log(magn + 1) * m / 255, (int)Math.Log(magn + 1) * m / 255);

                    outImg.SetPixel(x, y, pix);
                }
            }
            return outImg;
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        public static Bitmap fillCircle(Bitmap image1, Bitmap image2)
        {
            var imageOut = new Bitmap(image1.Width, image1.Height);
            var graphics = Graphics.FromImage(imageOut);
            graphics.DrawImage(image1, 0, 0);
            var path = new GraphicsPath();
            path.AddEllipse(0, 0, image2.Width, image2.Height);
            Region region = new Region(path);
            graphics.SetClip(region, CombineMode.Replace);
            graphics.DrawImage(image2, 0, 0);
            graphics.Dispose();
            region.Dispose();

            return imageOut;

        }

        public static Bitmap fillSquare(Bitmap image1, Bitmap image2)
        {
            var imageOut = new Bitmap(image1.Width, image1.Height);
            var graphics = Graphics.FromImage(imageOut);
            graphics.DrawImage(image1, 0, 0);
            var path = new GraphicsPath();
            path.AddRectangle(new Rectangle(image1.Width / 4, image1.Height / 4, 500, 500));
            Region region = new Region(path);
            graphics.SetClip(region, CombineMode.Replace);
            graphics.DrawImage(image2, 0, 0);
            graphics.Dispose();
            region.Dispose();

            return imageOut;
        }

        public static Bitmap fillRectangle(Bitmap image1, Bitmap image2)
        {
            var imageOut = new Bitmap(image1.Width, image1.Height);
            var graphics = Graphics.FromImage(imageOut);
            graphics.DrawImage(image1, 0, 0);
            var path = new GraphicsPath();
            path.AddRectangle(new Rectangle(image1.Width / 4, image1.Width / 4, image1.Width / 2, image1.Height / 2));
            Region region = new Region(path);
            graphics.SetClip(region, CombineMode.Replace);
            graphics.DrawImage(image2, 0, 0);
            graphics.Dispose();
            region.Dispose();

            return imageOut;

        }


        public Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();   // Releasing all resource used by graphics 
            return bmp;
        }
        private Bitmap combinePictures(string operation, Bitmap img1, Bitmap img2)
        {
            var img_out = new Bitmap(img1.Width, img1.Height);
            var h = img1.Height;
            var w = img1.Width;
            var stop = false;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    var pix1 = img1.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i);

                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;

                    if (!checkBox1.Checked)
                        r = 0;
                    if (!checkBox2.Checked)
                        g = 0;
                    if (!checkBox3.Checked)
                        b = 0;

                    var pix = Color.FromArgb(trackBar1.Value * 25, r, g, b);

                    img1.SetPixel(j, i, pix);
                }
            }
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    var pix1 = img1.GetPixel(j, i);
                    var pix2 = img2.GetPixel(j, i);


                    int r = pix1.R;
                    int g = pix1.G;
                    int b = pix1.B;


                    switch (operation)
                    {
                        case "Ничего":
                            break;
                        case "Сумма":
                            r = (int)Clamp(r + pix2.R, 0, 255);
                            g = (int)Clamp(g + pix2.G, 0, 255);
                            b = (int)Clamp(b + pix2.B, 0, 255);
                            break;
                        case "Умножение":
                            r = (int)Clamp(r * pix2.R, 0, 255);
                            g = (int)Clamp(g * pix2.G, 0, 255);
                            b = (int)Clamp(b * pix2.B, 0, 255);
                            break;
                        case "Вычитание":
                            r = (int)Clamp(r - pix2.R, 0, 255);
                            g = (int)Clamp(g - pix2.G, 0, 255);
                            b = (int)Clamp(b - pix2.B, 0, 255);
                            break;
                        case "Максимум":
                            r = (int)Clamp(Math.Max(r, pix2.R), 0, 255);
                            g = (int)Clamp(Math.Max(g, pix2.G), 0, 255);
                            b = (int)Clamp(Math.Max(b, pix2.B), 0, 255);
                            break;
                        case "Геом":
                            r = (int)Clamp(Math.Sqrt(r * pix2.R), 0, 255);
                            g = (int)Clamp(Math.Sqrt(g * pix2.G), 0, 255);
                            b = (int)Clamp(Math.Sqrt(b * pix2.B), 0, 255);
                            break;
                        case "Ср":
                            r = (int)Clamp((r + pix2.R) / 2, 0, 255);
                            g = (int)Clamp((g + pix2.G) / 2, 0, 255);
                            b = (int)Clamp((b + pix2.B) / 2, 0, 255);
                            break;
                        case "Маска прямоугольник":
                            img_out = fillRectangle(img2, img1);
                            stop = true;
                            break;
                        case "Маска квадрат":
                            img_out = fillSquare(img2, img1);
                            stop = true;
                            break;
                        case "Маска круг":
                            img_out = fillCircle(img2, img1);
                            stop = true;
                            break;
                        default:
                            break;
                    }

                    if (stop)
                        break;

                    var pix = Color.FromArgb(r, g, b);

                    img_out.SetPixel(j, i, pix);

                }
                if (stop)
                    break;
            }
            return img_out;
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void setData(int val)
        {
            double[] rez = new double[val * val];
            GetGauss(rez, 3, val, val);

            List<string> str = new List<string>();
            for (int i = 0; i < val; ++i)
            {
                string tmp = "";
                for (int j = 0; j < val; ++j)
                {
                    tmp += Math.Round(rez[i * val + j], 4).ToString();
                    if (j != val - 1)
                        tmp += " ";
                }
                str.Add(tmp);
            }

            richTextBox1.Clear();
            richTextBox1.Lines = str.ToArray();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            if (selectedState.Substring(0, 8) == "Критерий")
            {
                pictureBox1.Image = this.binarizationImage(img1, selectedState);
            }
            else if (selectedState == "Линейная фильтрация")
            {
        //        setData(3);
                pictureBox1.Image = MakeLineFilter(new Bitmap(imgS1));
                pictureBox4.Image = makeHistogram(new Bitmap(pictureBox1.Image));


            }
            else if (selectedState == "Медианная фильтрация")
            {
                var a = 5;
                pictureBox1.Image = CPP_MedianFilter(imgS1, a, a);
            }
            else
            {
                pictureBox1.Image = this.combinePictures(selectedState, img1, img2);
            }
        }

        int clamp(int a, int min, int max)
        {
            if (a < min) a = min;
            if (a > max) a = max;
            return a;
        }

        private Bitmap binarizationImage(Bitmap inputImg, string operation)
        {
            var img = inputImg;

            var arr = new int[256];
            for (int i = 0; i < 256; i++)
            {
                arr[i] = 0;
            }
            var hi = inputImg.Height;
            var wi = inputImg.Width;

            double threshold = 0;

            for (int y = 0; y < hi; y++)
            {
                for (int x = 0; x < wi; x++)
                {
                    var c = (inputImg.GetPixel(x, y).R + inputImg.GetPixel(x, y).G + inputImg.GetPixel(x, y).B) / 3;
                    arr[c] += 1;
                }
            }
            if (operation == "Критерий Гаврилова")
            {
                var sum = 0;

                for (int i = 0; i < arr.Length; i++)
                {
                    sum += arr[i] * i;
                }

                threshold = sum / (hi * wi);

            }
            else if (operation == "Критерий Отсу")
            {
                /*var normGist = new double[256];
                double sum = 0;
                double sigmab = 0;
                var indexMaxI = 0;
                double max = -1;
                for (int i = 0; i < arr.Length; i++)
                {
                    normGist[i] = (double)arr[i] / (wi * hi);
                    sum += normGist[i] * i;
                }
                for (int i = 0; i < normGist.Length; i++)
                {
                    if (normGist[i] > max)
                    {
                        max = normGist[i];
                        indexMaxI = i;
                    }
                }


                for (int t = 0; t < indexMaxI; t++)
                {
                    double omega1 = 0;
                    for (int i = 0; i < indexMaxI - 1; i++)
                    {
                        omega1 += normGist[i];
                    }
                    double omega2 = 1 - omega1;

                    double mu1 = sum / omega1;


                    double muT = 0;
                    for (int i = 0; i <= indexMaxI; i++)
                    {
                        muT += normGist[i] * i;
                    }

                    double mu2 = (muT - mu1 * omega1) / omega2;

                    double sigmaTemp = omega1 * omega2 * Math.Pow(mu1 - mu2, 2);

                    if (sigmaTemp > sigmab)
                        sigmab = (int)sigmaTemp;

                }
                threshold = sigmab;*/

                var monoImg = new double[wi, hi];
                Bitmap outImg = new Bitmap(wi, hi);
                var summ = 0;
                var t = 0;
                var N = new double[256];
                var max = -1;

                for (int y = 0; y < hi; y++)
                {
                    for (int x = 0; x < wi; x++)
                    {
                        var pixel = inputImg.GetPixel(x, y);
                        monoImg[x, y] = 0.2125 * pixel.R + 0.7154 * pixel.G + 0.0721 * pixel.B;
                        if (0.2125 * pixel.R + 0.7154 * pixel.G + 0.0721 * pixel.B > max)
                        {
                            max = (int)(0.2125 * pixel.R + 0.7154 * pixel.G + 0.0721 * pixel.B);
                        }
                    }
                }

                for (int i = 0; i < 256; i++)
                {
                    N[i] = 0;
                }

                for (int y = 0; y < hi; y++)
                {
                    for (int x = 0; x < wi; x++)
                    {
                        //var c = (inputImg.GetPixel(x, y).R + inputImg.GetPixel(x, y).G + inputImg.GetPixel(x, y).B) / 3;
                        var c = (int)(0.2125 * inputImg.GetPixel(x, y).R + 0.7154 * inputImg.GetPixel(x, y).G + 0.0721 * inputImg.GetPixel(x, y).B);
                        N[c] += 1;
                    }
                }

                for (int i = 0; i < 256; i++)
                {
                    N[i] /= (wi * hi);
                }

                var v1 = 0.0;
                var v2 = 0.0;
                var u1 = 0.0;
                var u2 = 0.0;
                var ut = 0.0;
                var sigma1 = -1.0;
                var sigma2 = -1.0;
                var sigmas = new double[max + 1];
                // var t = -1;

                for (int i = 0; i <= max; i++)
                {
                    for (int j = 0; j <= i - 1; j++)
                    {
                        v1 += N[j];
                    }

                    v2 = 1 - v1;

                    var summm = 0.0;
                    for (int j = 0; j <= i - 1; j++)
                    {
                        summm += (j * N[j]);

                    }

                    u1 = summm / v1;
                    if (summm == 0 && v1 == 0)
                    {
                        u1 = 0;
                    }

                    for (int j = 0; j <= max; j++)
                    {
                        ut += (j * N[j]);
                    }

                    u2 = (ut - u1 * v1) / v2;
                    sigma1 = v1 * v2 * (u1 - u2) * (u1 - u2);

                    // sigmas[i] = oldSigma;
                    if (sigma1 > sigma2)
                    {
                        sigma2 = sigma1;
                        threshold = i;
                    }

                    v1 = 0.0;
                    v2 = 0.0;
                    u1 = 0.0;
                    u2 = 0.0;
                    ut = 0.0;
                    sigma1 = -1.0;
                }

            }
            else if (operation == "Критерий Ниблека")
            {
                var imageMono = makeMono(inputImg);
                double sensitivity = -0.2;
                int n = 15;
                int[,] pix_matrix = new int[n, n];
                int n_forMatrix = (int)Math.Floor((double)n / 2);
                for (int i = 0; i < imageMono.Height; i++)
                    for (int j = 0; j < imageMono.Width; j++)
                    {

                        var pix = imageMono.GetPixel(j, i);
                        pix_matrix[n_forMatrix, n_forMatrix] = pix.R;

                        double math_expec = 0.0;
                        double math_expec_powered = 0.0;
                        double math_dispersion = 0.0;

                        for (int i_matrix = 0; i_matrix < n; i_matrix++)
                        {
                            for (int j_matrix = 0; j_matrix < n; j_matrix++)
                            {
                                if ((i - n_forMatrix + i_matrix) == n_forMatrix && (j - n_forMatrix + j_matrix) == n_forMatrix)
                                    continue;

                                if ((j - n_forMatrix + j_matrix) >= 0 && (j - n_forMatrix + j_matrix) < imageMono.Width)

                                    if ((i - n_forMatrix + i_matrix) >= 0 && (i - n_forMatrix + i_matrix) < imageMono.Height)

                                        pix_matrix[i_matrix, j_matrix] = imageMono.GetPixel(j - n_forMatrix + j_matrix, i - n_forMatrix + i_matrix).R;

                                    else { pix_matrix[i_matrix, j_matrix] = 0; }
                                else { pix_matrix[i_matrix, j_matrix] = 0; }


                                math_expec += pix_matrix[i_matrix, j_matrix];
                                math_expec_powered += Math.Pow(pix_matrix[i_matrix, j_matrix], 2);
                            }
                        }

                        math_expec /= (n * n);
                        math_expec_powered /= (n * n);
                        math_dispersion = math_expec_powered - Math.Pow(math_expec, 2);

                        double avg_deviation = Math.Sqrt(math_dispersion);

                        int local_threshold = clamp((int)(math_expec + sensitivity * avg_deviation), 0, 255);

                        img_out.SetPixel(j, i,
                           pix.R <= local_threshold ? Color.Black : Color.White);


                    }

                return img_out;
            }


            for (int i = 0; i < hi; i++)
            {
                for (int j = 0; j < wi; j++)
                {
                    var c = (inputImg.GetPixel(j, i).R + inputImg.GetPixel(j, i).G + inputImg.GetPixel(j, i).B) / 3;
                    var pix = Color.FromArgb(0, 0, 0);
                    if (c > threshold)
                        pix = Color.FromArgb(255, 255, 255, 255);

                    img_out.SetPixel(j, i, pix);

                }
            }

            return img_out;
        }

        Bitmap makeMono(Bitmap img)
        {
            Bitmap result = new Bitmap(img);

            int monoPx = 0;

            for (int i = 0; i < result.Width; i++)
            {
                for (int j = 0; j < result.Height; j++)
                {
                    var pix = result.GetPixel(i, j);

                    monoPx = (int)clamp((int)(pix.R * 0.2125 + pix.G * 0.7154 + pix.B * 0.0721), 0, 255);

                    result.SetPixel(i, j, Color.FromArgb(monoPx, monoPx, monoPx));
                }
            }

            return result;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Image = this.combinePictures(comboBox1.SelectedItem.ToString(), img1, img2);
            img1.Dispose();
            img1 = new Bitmap(imgS1, w, h);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = this.combinePictures(comboBox1.SelectedItem.ToString(), img1, img2);
            img1.Dispose();
            img1 = new Bitmap(imgS1, w, h);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = this.combinePictures(comboBox1.SelectedItem.ToString(), img1, img2);
            img1.Dispose();
            img1 = new Bitmap(imgS1, w, h);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = this.combinePictures(comboBox1.SelectedItem.ToString(), img1, img2);
            img1.Dispose();
            img1 = new Bitmap(imgS1, w, h);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileFialog = new SaveFileDialog();
            saveFileFialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileFialog.Filter = "Картинки (png, jpg, bmp, gif) |*.png;*.jpg;*.bmp;*.gif|All files (*.*)|*.*";
            saveFileFialog.RestoreDirectory = true;

            if (saveFileFialog.ShowDialog() == DialogResult.OK)
            {
                if (img_out != null)
                {
                    pictureBox1.Image.Save(saveFileFialog.FileName);
                }
            }
        }
        private void myCanvas1_MouseUp(object sender, MouseEventArgs e)
        {
            var image1 = new Bitmap(imgS1);
            var image2 = myCanvas1.DrawNewImage(image1);
            pictureBox1.Image = image2;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            var img = image2;
            var image = makeHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.Image = makeHistogram(new Bitmap(pictureBox1.Image));
            pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            //pictureBox5.SizeMode=PictureBoxSizeMode.
            pictureBox4.Refresh();
        }

        Bitmap makeHistogram(Bitmap inputImg)
        {

            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 3);

            var img = inputImg;
            var layer = Graphics.FromImage(img);


            var arr = new int[256];
            for (int i = 0; i < 256; i++)
            {
                arr[i] = 0;
            }
            var hi = inputImg.Height;
            var wi = inputImg.Width;
            Bitmap outImg = new Bitmap(765, pictureBox4.Height);
            var layer2 = Graphics.FromImage(outImg);

            for (int y = 0; y < hi; y++)
            {
                for (int x = 0; x < wi; x++)
                {
                    var c = (inputImg.GetPixel(x, y).R + inputImg.GetPixel(x, y).G + inputImg.GetPixel(x, y).B) / 3;
                    arr[c] += 1;
                }
            }
            Console.WriteLine(arr);

            var max = arr.Max();

            var k = (decimal)((decimal)pictureBox4.Height / max);
            //var k = 50;

            for (int i = 0; i < 255; i++)
            {
                Point A = new Point(i * 3, pictureBox4.Height - 1);
                Point B = new Point(i * 3, (int)(pictureBox4.Height - 1 - (decimal)arr[i] * k));
                layer2.DrawLine(pen, A, B);
            }

            return outImg;
        }


    }

    public class MyCanvas : Control
    {
        bool PaintComplete = false;
        public System.Drawing.Image img { get; set; }

        List<int> Values = new List<int>();
        public List<Point> points = new List<Point>() { new Point(0, 0), new Point(255, 255) };
        private Point currentPoint;

        //Таймер для ее обновления
        private Timer timer;

        //битмапы на которых будем рисовать в 2 слоя.
        //На первом будет само содержаение
        //На втором курсор.
        private Bitmap layer1;
        private Bitmap layer2;

        //Графиксы для этих битмапов
        private Graphics g_layer1;
        private Graphics g_layer2;

        private bool painting_mode = false;

        Pen pen = new Pen(Color.FromArgb(255, 0, 80, 0), 1);

        public MyCanvas()
        {
            //Включаем режим двойной буферизации, чтобы рисовка не мерцала.
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);

            //Опеределяем в нашей канве события
            this.Paint += MyCanvas_Paint;
            this.MouseDown += MyCanvas_MouseDown;
            this.MouseUp += MyCanvas_MouseUp;
            this.MouseMove += MyCanvas_MouseMove;

            this.SizeChanged += MyCanvas_SizeChanged;

            //Запускаем таймер на перерисовку
            timer = new Timer();
            timer.Interval = 25;
            timer.Tick += (s, a) => this.Refresh();
            timer.Start();
        }


        ~MyCanvas()
        {
            if (g_layer1 != null)
                layer1.Dispose();
            if (g_layer2 != null)
                layer2.Dispose();

            if (layer1 != null)
                layer1.Dispose();
            if (layer2 != null)
                layer2.Dispose();

            timer.Dispose();
            pen.Dispose();
        }

        public Bitmap DrawNewImage(Bitmap imageIn)
        {
            var h = imageIn.Height;
            var w = imageIn.Width;

            Bitmap imageOut = new Bitmap(w, h);



            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    var img_pix = imageIn.GetPixel(j, i);
                    var r = img_pix.R;
                    var g = img_pix.G;
                    var b = img_pix.B;
                    var currentBrightness = 0.0;
                    Console.WriteLine((float)currentBrightness);


                    var newR = 0;
                    var newB = 0;
                    var newG = 0;


                    if (r + Values[r] > 255)
                    {
                        newR = 255;
                    }
                    else if (r + Values[r] < 0)
                    {
                        newR = 0;
                    }
                    else
                    {
                        newR = r + Values[r];
                    }


                    if (g + Values[g] > 255)
                    {
                        newG = 255;
                    }
                    else if (g + Values[g] < 0)
                    {
                        newG = 0;
                    }
                    else
                    {
                        newG = g + Values[g];
                    }


                    if (b + Values[b] > 255)
                    {
                        newB = 255;
                    }
                    else if (b + Values[b] < 0)
                    {
                        newB = 0;
                    }
                    else
                    {
                        newB = b + Values[b];
                    }

                    imageOut.SetPixel(j, i, Color.FromArgb(newR, newG, newB));
                }
            }

            return imageOut;
        }

        private void MyCanvas_SizeChanged(object sender, EventArgs e)
        {
            var _sender = sender as MyCanvas;

            //При изменении размера у нас должны пересоздатся битмапы (так как нельзя изменить
            // размер битмапа во время работы)
            //По этому мы сначала создаем новые, если старые есть (при создании конвы их нет,
            //вот тут они и создадутся при первом отображении) - рисуем их содержимое на новых, удаляем старые.

            Bitmap new_layer1 = new Bitmap(_sender.Size.Width, _sender.Size.Height, PixelFormat.Format32bppArgb);
            Bitmap new_layer2 = new Bitmap(_sender.Size.Width, _sender.Size.Height, PixelFormat.Format32bppArgb);
            Graphics new_g_layer1 = Graphics.FromImage(new_layer1);
            Graphics new_g_layer2 = Graphics.FromImage(new_layer2);

            if (g_layer1 != null)
            {
                new_g_layer1.DrawImageUnscaled(layer1, 0, 0);
                layer1.Dispose();
            }
            if (layer1 != null)
                layer1.Dispose();


            if (g_layer2 != null)
                layer2.Dispose();
            if (layer2 != null)
                layer2.Dispose();

            layer1 = new_layer1;
            g_layer1 = new_g_layer1;
            layer2 = new_layer2;
            g_layer2 = new_g_layer2;

            LinearGradientBrush linGrBrushVert = new LinearGradientBrush(
               new Point(0, this.Size.Height),
               new Point(this.Size.Width, this.Size.Height - 5),
               Color.FromArgb(255, 0, 0, 0),
               Color.FromArgb(255, 255, 255, 255));

            LinearGradientBrush linGrBrushGor = new LinearGradientBrush(
                new Point(0, this.Size.Height),
                new Point(5, 0),
                Color.FromArgb(255, 0, 0, 0),
                Color.FromArgb(255, 255, 255, 255));

            g_layer1.FillRectangle(linGrBrushVert, new Rectangle(0, this.Size.Height - 5, this.Size.Width, 5));
            g_layer1.FillRectangle(linGrBrushGor, new Rectangle(0, 0, 5, this.Size.Height));
        }

        private void MyCanvas_Paint(object sender, PaintEventArgs e)
        {
            var mouse_pos = PointToClient(MousePosition);
            int r = 2;
            g_layer2.Clear(Color.FromArgb(0, 0, 0, 0));
            g_layer2.DrawEllipse(pen, mouse_pos.X - r / 2, mouse_pos.Y - r / 2, r, r);

            points = points.OrderBy(p => p.X).ToList();
            for (int i = 0; i < points.Count - 1; ++i)
                if (points[i].X == points[i + 1].X)
                {

                    if (currentPoint == points[i + 1])
                    {
                        Point tmp = new Point(points[i + 1].X + 1, points[i + 1].Y);
                        currentPoint = new Point(tmp.X, tmp.Y);
                        points[i + 1] = tmp;
                    }
                    else
                    {
                        Point tmp = new Point(points[i].X - 1, points[i].Y);
                        currentPoint = new Point(tmp.X, tmp.Y);
                        points[i] = tmp;
                    }

                }

            Values.Clear();

            for (int i = 0; i < points.Count - 1; ++i)
            {
                Point one = PointToMine(points[i]);
                Point two = PointToMine(points[i + 1]);
                g_layer2.DrawLine(pen, one, two);
            }
            for (int i = 0; i < points.Count - 1; ++i)
            {
                float dy = (float)(points[i + 1].Y - points[i].Y) / (points[i + 1].X - points[i].X);
                for (int v = 0; v < points[i + 1].X - points[i].X; ++v)
                {
                    Values.Add(points[i].Y + (int)(dy * v));
                }
            }
            Values.Add(points[points.Count - 1].Y);


            Size pointSize = new Size(15, 15);

            for (int i = 0; i < points.Count; ++i)
            {
                Point point = PointToMine(points[i]);
                g_layer2.FillRectangle(Brushes.Red, new Rectangle(new Point(point.X - pointSize.Width / 2, point.Y - pointSize.Height / 2), pointSize));
            }

            e.Graphics.DrawImageUnscaled(layer1, 0, 0);
            e.Graphics.DrawImageUnscaled(layer2, 0, 0);


        }

        public Point PointToMine(Point pnt)
        {
            return new Point(
                (int)(pnt.X * (float)this.Size.Width / 255),
                (int)((float)this.Size.Height - pnt.Y * (float)this.Size.Height / 255));
        }

        private void MyCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            //при отпускании ЛКМ отключаем режим рисования
            if (e.Button == MouseButtons.Left)
                painting_mode = false;


        }

        private static bool PointInRectangle(Point pnt1, Point pnt2, Size size)
        {
            bool result = false;
            if (pnt1.X < pnt2.X + size.Width / 2 && pnt1.X > pnt2.X - size.Width / 2 &&
                pnt1.Y < pnt2.Y + size.Height / 2 && pnt1.Y > pnt2.Y - size.Height / 2)
                result = true;
            return result;
        }

        private Point PointOutOfMine(Point pnt)
        {
            return new Point(
                (int)(((float)pnt.X / this.Size.Width) * 255),
                (int)(((float)(this.Size.Height - pnt.Y) / this.Size.Height) * 255));
        }

        private void MyCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            //при нажании ЛКМ включаем режим рисования
            if (e.Button == MouseButtons.Left)
            {
                PaintComplete = false;
                painting_mode = true;

                bool pointFinded = false;

                var mouse_pos = PointToClient(MousePosition);
                for (int i = 0; i < points.Count; ++i)
                {
                    if (PointInRectangle(mouse_pos, PointToMine(points[i]), new Size(20, 20)))
                    {
                        currentPoint = points[i];
                        pointFinded = true;
                        break;
                    }
                }
                if (!pointFinded && points.Count < 13)
                {
                    currentPoint = PointOutOfMine(mouse_pos);
                    points.Add(PointOutOfMine(mouse_pos));
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                PaintComplete = false;
                painting_mode = true;

                var mouse_pos = PointToClient(MousePosition);
                for (int i = 1; i < points.Count - 1; ++i)
                {
                    if (PointInRectangle(mouse_pos, PointToMine(points[i]), new Size(20, 20)))
                    {
                        points.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //Если есть режим рисования, то нарисовать красный круг под мышкой.
            //ф-ция вызывается при движении мыши по канве
            if (painting_mode && e.Button != MouseButtons.Right)
            {
                var mouse_pos = PointToClient(MousePosition);


                if (PointInRectangle(mouse_pos, new Point(173, 173), new Size(347, 348)))
                    if (points.IndexOf(currentPoint) == 0 || points.IndexOf(currentPoint) == points.Count - 1)
                    {
                        int index = points.IndexOf(currentPoint);
                        points[index] = new Point(points[index].X, PointOutOfMine(mouse_pos).Y);
                        currentPoint = new Point(points[index].X, PointOutOfMine(mouse_pos).Y);
                        Console.WriteLine("sdf");
                    }
                    else
                    {
                        int index = points.IndexOf(currentPoint);
                        points[index] = PointOutOfMine(mouse_pos);
                        currentPoint = PointOutOfMine(mouse_pos);
                        Console.WriteLine("sdf");
                    }
            }
        }
    }
}


