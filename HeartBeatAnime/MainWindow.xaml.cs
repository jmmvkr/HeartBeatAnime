using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ColorAnime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Action<int> actColor;
        bool bStop = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bStop = true;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            actColor = setColor;
            new Thread(animeProc).Start();
        }

        const int MAX_FRAME = 13;
        const int MAX_TIMELINE = 20;
        //int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //int[] arr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] arr = { 70, 192, 255, 240, 235, 215, 195, 175, 155, 135, 115, 95, 75 };

        // syntax in C
        // const int MAX_FRAME = 10;
        // int arr[MAX_FRAME] = { 0 };


        void fillLinearAnime(int nMax, int nDirection, int nStep)
        {
            int i;
            int curValue = nMax;

            for (i = 0; i < nStep; i++)
            {
                arr[i] = curValue;
                curValue += nDirection;
            }
        }

        void fillSinAnime()
        {
            int i;
            int nDuration = MAX_TIMELINE;
            int nFillEnd = MAX_TIMELINE;
            arr = new int[nDuration]; // C# only, if C used, declare int arr[MAX_FRAME] = { 0 };

            double halfMax = (0.5f * 255f);
            double frame2Phase = (Math.PI * 2) / nDuration;
            for (i = 0; i < nFillEnd; i++)
            {
                double v = halfMax + (halfMax * Math.Cos(frame2Phase * i));
                arr[i] = ((int)Math.Floor(v));
            }
        }

        void printAnime()
        {
            int i;

            Console.WriteLine("");
            Console.Write("int arr[] = { ");
            for (i = 0; i < MAX_FRAME; i++)
            {
                // C# only
                if (i > 0)
                {
                    Console.Write(", ");
                }
                Console.Write(arr[i]);
            }
            Console.Write(" }");
            Console.WriteLine();
        }

        int opt = 0;

        void showHeartLight()
        {
            int color;
            if ((iFrame >= MAX_FRAME) || (heartOnOff <= 0))
            {
                color = 0;
                if (ON == heartLaterShutDown)
                {
                    heartLaterShutDown = OFF;
                    if (ON == heartOnOff)
                    {
                        updateHeartPulse(0);
                    }
                }
            }
            else
            {
                color = arr[iFrame];
                //color = (255 - iFrame * 20);
            }

            setPwmColor(color);
            //iFrame = (1 + iFrame) % MAX_TIMELINE;
            if (iFrame < MAX_TIMELINE)
            {
                iFrame++;
            }
            else
            {
                iFrame = 0;
            }
        }

        void animeProc()
        {
            if (1 == opt)
            {
                fillLinearAnime(255, -20, 10);
                printAnime();
            }
            else if (2 == opt)
            {
                fillSinAnime();
                printAnime();
            }
            else
            {
                printAnime();
            }

            while (true)
            {
                showHeartLight();

                if (bStop)
                {
                    break;
                }
                System.Threading.Thread.Sleep(50);
            }
        }

        void setPwmColor(int color)
        {
            //analogWrite(4 * color);  // convert from (0,255) to (0,1023)
            Dispatcher.Invoke(actColor, color);
        }

        const int ON = 1;
        const int OFF = 0;
        int iFrame = 0;
        int heartOnOff = ON;
        int heartLaterShutDown = OFF;


        void setColor(int v)
        {
            Color c = Color.FromRgb((byte) v, 0, 0);
            btnColor.Background = new SolidColorBrush(c);
        }

        private void BtnTrigOff_Click(object sender, RoutedEventArgs e)
        {
            heartLaterShutDown = ON;
            //updateHeartPulse(0);
        }

        private void BtnTrigOn_Click(object sender, RoutedEventArgs e)
        {
            updateHeartPulse(1);
        }

        void updateHeartPulse(int nOnOff)
        {
            heartOnOff = nOnOff;
            iFrame = 0;
        }

    } // end - class MainWindow
}
