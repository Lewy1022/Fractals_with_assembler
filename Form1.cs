using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;


namespace Mandelbroat_set
{
    unsafe public partial class Form1 : Form
    {
       
        [DllImport("MojaDLL.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern double tran_x(double  x, double  y, double a, double b, double c);
        [DllImport("MojaDLL.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern double tran_y(double x, double y, double d, double e, double f);
        [DllImport("MojaDLL.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern double xe(double x,double width,double x_min,double x_max);
        [DllImport("MojaDLL.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern double ye(double y, double height,double y_min,double y_max);
        [DllImport("MojaDLL.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern double generator(double los, int a, int b, int m);
        [DllImport("MojaDLL.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern double normalizuj(double los);
        


        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "500000";
            // button5.Visible = false;\
            button6.Visible = false;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int x = 0; x < pictureBox1.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    double a = (double)(x - (pictureBox1.Width / 2)) / (double)(pictureBox1.Width / 4);
                    //Console.WriteLine("a = " + a);
                    double b = (double)(y - (pictureBox1.Height / 2)) / (double)(pictureBox1.Height / 4);
                    // Console.WriteLine("b = " + b);
                    Complex C = new Complex(a, b);
                    // Console.WriteLine("C = " + C);
                    Complex Z = new Complex(0, 0);
                    // Console.WriteLine("Z = " + Z);
                    int it = 0;
                    do
                    {
                        it++;
                        Z = Complex.Pow(Z, 2);
                        //Complex.Add(Z, C);
                        //Console.WriteLine("{0} + {1} = {2}", Z, C,
                        //     Complex.Add(Z, C));
                        Z = Complex.Add(Z, C);
                        if (Z.Magnitude > 2.0) break;

                    }
                    while (it < 100);
                    //  Console.WriteLine(it);
                    //  bm.SetPixel(x, y, Color.Black);
                    if (it < 100) bm.SetPixel(x, y, Color.Black);
                    else bm.SetPixel(x, y, Color.White);
                }

            }
            
            pictureBox1.Image = bm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = 1400;
            pictureBox1.Height = 900;
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            double[,] dragon = {{-.4, 0.0, -1.0, -.0, -.4, .1},
                  { .76, -.4, .0, .4, 0.76, 0 } };
            
            double x1=0.0, y1=0.0,xe,ye;


            int n = 500000;
            Int32.TryParse(textBox1.Text, out n);
            Random rnd = new Random();
            while (n-->0)
            {
               
                int los = rnd.Next(1, 100);
               
                
                if (los > 18)
                {
                    
                    transform(ref x1, ref y1, 1);
                  

                }
                else
                {
                    transform(ref x1, ref y1, 0);
                }
                xe = ((x1 + 1.3) / (0.6 + 1.3)) * pictureBox1.Width;
                ye = ((y1 + 0.9) / (0.45 + 0.9)) * -1.0*pictureBox1.Height+ pictureBox1.Height;

              
                if (n > 200000) bm.SetPixel((int)xe, (int)ye, Color.Red);
                else if (n > 50000) bm.SetPixel((int)xe, (int)ye, Color.Orange);
                else bm.SetPixel((int)xe, (int)ye, Color.Yellow);
                // bm.SetPixel((int)xe, (int)ye, Color.Black);

                
            }

            
            pictureBox1.Image = bm;


             void transform(ref double x, ref double y, int firstDimension)
            {
                double x3, x4;
                x3 = x * dragon[firstDimension, 0] + y * dragon[firstDimension, 1] + dragon[firstDimension, 2];
                x4 = x * dragon[firstDimension, 3] + y * dragon[firstDimension, 4] + dragon[firstDimension, 5];
                x = x3;
                y = x4;
                

            }   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        unsafe private void button3_Click(object sender, EventArgs e)
        {

           

            pictureBox1.Width = 1400;
            pictureBox1.Height = 900;
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            double[,] dragon = {{-.4, 0.0, -1.0, -.0, -.4, .1},
                  { .76, -.4, .0, .4, 0.76, 0 } };
            double a1 = -.4, b1 = .0, c1 = -1.0, d1 = -.0, e1 = -.4, f1 = .1;
            double a2 = .76, b2 = -.4, c2 = .0, d2 = .4, e2 = .76, f2 = .0;

            double x1 = 0.0, y1 = 0.0, xe1, ye1;

            
            int n = 500000;
            Int32.TryParse(textBox1.Text,out n);
           
           
            var losowa = DateTime.Now;
            double tmp,tmp2= losowa.Second + losowa.Hour * 3600;
            int los;
            while (n-- > 0)
            {

              

               tmp2= (generator(tmp2, 13100233, 11040857, 9999991));
               // Console.WriteLine("tmp2 = {0}", tmp2);
                los = (int)(normalizuj(tmp2));
               // Console.WriteLine("Losowa = {0}",los);

                //Console.WriteLine("x={0}, y={1} los = {2}", x1, y1,los);
                if (los > 18)
                {
                   
                    tmp = x1;
                    x1 = tran_x(x1, y1, dragon[1, 0], dragon[1, 1], dragon[1, 2]);
                    
                    y1 = tran_y(tmp,y1,dragon[1,3], dragon[1, 4], dragon[1, 5]);
                    

                   // Console.WriteLine(x1);
                }
                else

                {
                    tmp = x1;
                    x1 = tran_x(x1, y1, dragon[0,0], dragon[0, 1], dragon[0, 2]);

                    y1 = tran_y(tmp, y1, dragon[0, 3], dragon[0, 4], dragon[0, 5]);
                }
                // Console.WriteLine("x={0} y={1}",x1,y1);
                xe1 = xe(x1, pictureBox1.Width,-1.3,0.6);
                ye1 = ye(y1,pictureBox1.Height,-0.9,0.45);


               
                bm.SetPixel((int)xe1, (int)ye1, Color.Black);

                
            }

            
            pictureBox1.Image = bm;


           


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = 1400;
            pictureBox1.Height = 900;
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            double[,] choinka = {{-.67, -0.02, .0, -.18, .81, 10},
                                 { .4, .4, .0, -.1, 0.4, 0 },
                                 { -.4, -.4, .0, -.1, 0.4, 0 },
                                 { -.1, .0, .0, .44, 0.44, -2 }};
            

            double x1 = 0.0, y1 = 0.0, xe1, ye1;


            int n = 500000;
            Int32.TryParse(textBox1.Text, out n);


            var losowa = DateTime.Now;
            double tmp, tmp2 = losowa.Second + losowa.Hour * 3600;
            int los;
            while (n-- > 0)
            {



                tmp2 = (generator(tmp2, 13100233, 11040857, 9999991));
                // Console.WriteLine("tmp2 = {0}", tmp2);
                los = (int)(normalizuj(tmp2));
                // Console.WriteLine("Losowa = {0}",los);

                //Console.WriteLine("x={0}, y={1} los = {2}", x1, y1,los);
                if (los < 54)
                {

                    tmp = x1;
                    x1 = tran_x(x1, y1, choinka[0, 0], choinka[0, 1], choinka[0, 2]);

                    y1 = tran_y(tmp, y1, choinka[0, 3], choinka[0, 4], choinka[0, 5]);


                    //// Console.WriteLine(x1);
                }
                else if (los < 74)

                {
                    tmp = x1;
                    x1 = tran_x(x1, y1, choinka[1, 0], choinka[1, 1], choinka[1, 2]);

                    y1 = tran_y(tmp, y1, choinka[1, 3], choinka[1, 4], choinka[1, 5]);
                }
                else if (los < 94)

                {
                    tmp = x1;
                    x1 = tran_x(x1, y1, choinka[2, 0], choinka[2, 1], choinka[2, 2]);

                    y1 = tran_y(tmp, y1, choinka[2, 3], choinka[2, 4], choinka[2, 5]);
                }
                else

                {
                    tmp = x1;
                    x1 = tran_x(x1, y1, choinka[3, 0], choinka[3, 1], choinka[3, 2]);

                    y1 = tran_y(tmp, y1, choinka[3, 3], choinka[3, 4], choinka[3, 5]);
                }
                // Console.WriteLine("x={0} y={1}",x1,y1);
                xe1 = xe(x1, pictureBox1.Width, -30, 30);
                ye1 = ye(y1, pictureBox1.Height, -10, 60);



                bm.SetPixel((int)xe1, (int)ye1, Color.Black);


            }


            pictureBox1.Image = bm;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = 1500;
            pictureBox1.Height = 800;
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            double[,] paprotka = {{0, 0, .0, 0, .16, 0},
                                 { .85, .04, .0, -.04, 0.85, 1.6 },
                                 { .2, -.26, .0, .23, 0.22, 1.6 },
                                 { -.15, .28, .0, .26, 0.24, 0.44 }};


            double x1 = 0.0, y1 = 0.0, xe1, ye1;


            int n = 500000;
            Int32.TryParse(textBox1.Text, out n);

            //Random rnd = new Random();
            //while (n-- > 0)
            //{

            //    int los = rnd.Next(1, 100);


            //    if (los > 99)
            //    {

            //        transform(ref x1, ref y1, 0);


            //    }
            //    else if(los > 21)
            //    {
            //        transform(ref x1, ref y1, 1);
            //    }
            //    else if (los > 10)
            //    {
            //        transform(ref x1, ref y1, 2);
            //    }
            //    else
            //    {
            //        transform(ref x1, ref y1, 3);
            //    }
            //    xe1 = ((y1) / (10.5)) * pictureBox1.Width;
            //    ye1 = ((-x1 + 3.0) / (2.8 + 3.0)) * -1.0 * pictureBox1.Height + pictureBox1.Height;


            //    if (n > 200000) bm.SetPixel((int)xe1, (int)ye1, Color.Green);
            //    else if (n > 50000) bm.SetPixel((int)xe1, (int)ye1, Color.DarkGreen);
            //    else bm.SetPixel((int)xe1, (int)ye1, Color.Yellow);
            //    // bm.SetPixel((int)xe, (int)ye, Color.Black);


            //} 


            //pictureBox1.Image = bm;


            //void transform(ref double x, ref double y, int firstDimension)
            //{
            //    double x3, x4;
            //    x3 = x * paprotka[firstDimension, 0] + y * paprotka[firstDimension, 1] + paprotka[firstDimension, 2];
            //    x4 = x * paprotka[firstDimension, 3] + y * paprotka[firstDimension, 4] + paprotka[firstDimension, 5];
            //    x = x3;
            //    y = x4;


            //}


            var losowa = DateTime.Now;
            double tmp, tmp2 = losowa.Second + losowa.Hour * 3600;
            tmp2 = 0;
            int los;
            while (n-- > 0)
            {



                tmp2 = (generator(tmp2, 13100233, 11040857, 9999991));
                // Console.WriteLine("tmp2 = {0}", tmp2);
                los = (int)(normalizuj(tmp2));
                // Console.WriteLine("Losowa = {0}",los);

                //Console.WriteLine("x={0}, y={1} los = {2}", x1, y1,los);
                if (los>98 )
                {

                    tmp = x1;
                    x1 = tran_x(x1, y1, paprotka[0, 0], paprotka[0, 1], paprotka[0, 2]);

                    y1 = tran_y(tmp, y1, paprotka[0, 3], paprotka[0, 4], paprotka[0, 5]);


                    // Console.WriteLine(x1);
                }
                else if (los > 20)

                {
                    tmp = x1;
                    x1 = tran_x(x1, y1, paprotka[1, 0], paprotka[1, 1], paprotka[1, 2]);

                    y1 = tran_y(tmp, y1, paprotka[1, 3], paprotka[1, 4], paprotka[1, 5]);
                }
                else if (los > 10)

                {
                    tmp = x1;
                    x1 = tran_x(x1, y1, paprotka[2, 0], paprotka[2, 1], paprotka[2, 2]);

                    y1 = tran_y(tmp, y1, paprotka[2, 3], paprotka[2, 4], paprotka[2, 5]);
                }
                else

                {
                    tmp = x1;
                    x1 = tran_x(x1, y1, paprotka[3, 0], paprotka[3, 1], paprotka[3, 2]);

                    y1 = tran_y(tmp, y1, paprotka[3, 3], paprotka[3, 4], paprotka[3, 5]);
                }
                // Console.WriteLine("x={0} y={1}",x1,y1);
                xe1 = xe(y1, pictureBox1.Width, 0, 10.5);
                ye1 = ye(-x1, pictureBox1.Height, -3, 2.8);
                xe(-1 * x1, pictureBox1.Width, 0, 10.5);
                //Console.WriteLine("Los = {0} x = {1} xe = {2} y= {3} ye = {4}", los, x1, xe1, y1, ye1);


                // bm.SetPixel((int)xe1, (int)ye1, Color.Black);
                if (n > 200000) bm.SetPixel((int)xe1, (int)ye1, Color.GreenYellow);
                else if (n > 50000) bm.SetPixel((int)xe1, (int)ye1, Color.DarkGreen);
                else bm.SetPixel((int)xe1, (int)ye1, Color.ForestGreen);


            }


            pictureBox1.Image = bm;
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
                pictureBox1.Width = 1500;
                pictureBox1.Height = 800;
                Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);

                double[,] paprotka = {{.14, .01, -1.31, .0, .51, .1},
                                 { .43, .52, 1.49, -0.45, 0.5, -0.75 },
                                 { .45, -.49, -1.62, .47, 0.47, -0.74 },
                                 { .49, .0, .02, .0, 0.51, 1.62 }};


                double x1 = 0.0, y1 = 0.0, xe1, ye1;


                int n = 500000;
                Int32.TryParse(textBox1.Text, out n);

                //Random rnd = new Random();
                //while (n-- > 0)
                //{

                //    int los = rnd.Next(1, 100);


                //    if (los > 99)
                //    {

                //        transform(ref x1, ref y1, 0);


                //    }
                //    else if(los > 21)
                //    {
                //        transform(ref x1, ref y1, 1);
                //    }
                //    else if (los > 10)
                //    {
                //        transform(ref x1, ref y1, 2);
                //    }
                //    else
                //    {
                //        transform(ref x1, ref y1, 3);
                //    }
                //    xe1 = ((y1) / (10.5)) * pictureBox1.Width;
                //    ye1 = ((-x1 + 3.0) / (2.8 + 3.0)) * -1.0 * pictureBox1.Height + pictureBox1.Height;


                //    if (n > 200000) bm.SetPixel((int)xe1, (int)ye1, Color.Green);
                //    else if (n > 50000) bm.SetPixel((int)xe1, (int)ye1, Color.DarkGreen);
                //    else bm.SetPixel((int)xe1, (int)ye1, Color.Yellow);
                //    // bm.SetPixel((int)xe, (int)ye, Color.Black);


                //} 


                //pictureBox1.Image = bm;


                //void transform(ref double x, ref double y, int firstDimension)
                //{
                //    double x3, x4;
                //    x3 = x * paprotka[firstDimension, 0] + y * paprotka[firstDimension, 1] + paprotka[firstDimension, 2];
                //    x4 = x * paprotka[firstDimension, 3] + y * paprotka[firstDimension, 4] + paprotka[firstDimension, 5];
                //    x = x3;
                //    y = x4;


                //}


                var losowa = DateTime.Now;
                double tmp, tmp2 = losowa.Second + losowa.Hour * 3600;
                tmp2 = 0;
                int los;
                while (n-- > 0)
                {



                    tmp2 = (generator(tmp2, 13100233, 11040857, 9999991));
                    // Console.WriteLine("tmp2 = {0}", tmp2);
                    los = (int)(normalizuj(tmp2));
                    // Console.WriteLine("Losowa = {0}",los);

                    //Console.WriteLine("x={0}, y={1} los = {2}", x1, y1,los);
                    if (los > 98)
                    {

                        tmp = x1;
                        x1 = tran_x(x1, y1, paprotka[0, 0], paprotka[0, 1], paprotka[0, 2]);

                        y1 = tran_y(tmp, y1, paprotka[0, 3], paprotka[0, 4], paprotka[0, 5]);


                        // Console.WriteLine(x1);
                    }
                    else if (los > 20)

                    {
                        tmp = x1;
                        x1 = tran_x(x1, y1, paprotka[1, 0], paprotka[1, 1], paprotka[1, 2]);

                        y1 = tran_y(tmp, y1, paprotka[1, 3], paprotka[1, 4], paprotka[1, 5]);
                    }
                    else if (los > 10)

                    {
                        tmp = x1;
                        x1 = tran_x(x1, y1, paprotka[2, 0], paprotka[2, 1], paprotka[2, 2]);

                        y1 = tran_y(tmp, y1, paprotka[2, 3], paprotka[2, 4], paprotka[2, 5]);
                    }
                    else

                    {
                        tmp = x1;
                        x1 = tran_x(x1, y1, paprotka[3, 0], paprotka[3, 1], paprotka[3, 2]);

                        y1 = tran_y(tmp, y1, paprotka[3, 3], paprotka[3, 4], paprotka[3, 5]);
                    }
                    // Console.WriteLine("x={0} y={1}",x1,y1);
                    xe1 = xe(y1, pictureBox1.Width, 0, 10.5);
                    ye1 = ye(-x1, pictureBox1.Height, -3, 2.8);
                    xe(-1 * x1, pictureBox1.Width, 0, 10.5);
                    //Console.WriteLine("Los = {0} x = {1} xe = {2} y= {3} ye = {4}", los, x1, xe1, y1, ye1);


                    // bm.SetPixel((int)xe1, (int)ye1, Color.Black);
                    if (n > 200000) bm.SetPixel((int)xe1, (int)ye1, Color.Blue);
                    else if (n > 50000) bm.SetPixel((int)xe1, (int)ye1, Color.DarkGreen);
                    else bm.SetPixel((int)xe1, (int)ye1, Color.ForestGreen);


                }


                pictureBox1.Image = bm;
            

        }
    }
}
