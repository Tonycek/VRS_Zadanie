using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRS_Zadanie
{
    public partial class Form1 : Form
    {
        private String line;
        private System.IO.StreamReader sr;
        private string[] files;
        private int rezimKreslenia = 1;         // ci sa kresli od svetovych suradnic (0), od aktuanej pozicie (1),...
        private double aktualX = 4, aktualY = 4, aktualZ = 0;   // aktualne suradnice
        private double svetX = 100, svetY = 100, svetZ = 0;
        private Bitmap DrawArea;
        private Pen mypen;
        private Graphics g;
        private int nasobic = 15;
        private double ARMLENGTH = 5;
        private double ARMLENGTH1 = 3;
        private const double radTodegree = 180 / Math.PI;
        private const double degToRadian = Math.PI / 180;
        private SerialPort seriovyPort;
        private double polomerKruznice = 0;
        private int predchadzajuciX = 0;
        private int predchadzajuciY = 0;

        public Form1()
        {
            InitializeComponent();

   //         DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            DrawArea = new Bitmap(300, 300);
            // Set the size of the PictureBox control.
            //         this.pictureBox1.Size = new System.Drawing.Size(20, 20);

            //Set the SizeMode to center the image.
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Image = DrawArea;
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                files = (string[])e.Data.GetData(DataFormats.FileDrop, false);     // cesta k dropnutemu suboru
                sr = new System.IO.StreamReader(files[0]);           // citanie dropnuteho suboru
                line = sr.ReadToEnd();                                               // ulozenie obsahu suboru do premennej
                textBox1.Text = line;                                                       // vypis do text boxu
            }

            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSpracujKod_Click(object sender, EventArgs e)    // ked je nacitany txt file, tak po stlaceni tohoto buttonu rozseka GCODE po riadkoch
        {
            try
            {
                string port = listBoxZoznamPortov.GetItemText(listBoxZoznamPortov.SelectedItem);
                seriovyPort = new SerialPort(port, 4800, Parity.None, 8, StopBits.One);
                sr = new System.IO.StreamReader(files[0]);          // znovu idem citat GCODE ktory uz mam ulozeny v premennej files[0]

                ARMLENGTH = Double.Parse(tbRameno1.Text);// Int32.Parse(tbRameno1.Text);
                ARMLENGTH1 = Double.Parse(tbRameno2.Text);// Int32.Parse(tbRameno2.Text);

                string riadok;

                while ((riadok = sr.ReadLine()) != null)            // citam GCODE po riadkoch
                {
                    if (riadok == "")
                        continue;

                    spracujKod(riadok);
                }
            }

            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)         // vykona sa na zaciatku pri nacitani programu
        {
            g = Graphics.FromImage(DrawArea);
            Pen mypen1 = new Pen(Brushes.Black);
            g.DrawLine(mypen1, 0, 0, 200, 200);
            g.Clear(Color.White);
            g.Dispose();
            mypen = new Pen(Color.Black);
            //aktualY = 0;// this.DrawArea.Height - aktualY;
        }

        private void spracujKod(string riadok)
        {
            if(riadok.Length == 3)
            {
                switch (riadok)
                {
                    case "G90":                 
                        rezimKreslenia = 0;
                        return;

                    case "G91":
                        rezimKreslenia = 1;
                        return;

                    //case "G92":
                    //    rezimKreslenia = 2;
                    //    break;
                    default:
                        return;
                }
            }

            string prveDvePismena = riadok.Substring(0, 2);     // v pripade ze riadok gcode obsahuje pohyb, do tohoto 
            //  stringu uloz prve dve pismena, ktore hovoria o druhu pohybu (rapid, linear, kruznicovy...)

            switch (prveDvePismena)
            {
                case "G0":
                    rapidPohyb(riadok.Substring(2, riadok.Length - 2));
                    break;

                case "G1":
                    linearPohyb(riadok.Substring(2, riadok.Length - 2));
                    break;

                case "G2":              // pohyb po hodinovych rudickach
                    kruznicovyPohyb(riadok.Substring(2, riadok.Length - 2), true);
                    break;

                case "G3":              // pohyb proti hodinovym rudickam
                    kruznicovyPohyb(riadok.Substring(2, riadok.Length - 2), false);
                    break;

                case "G4":
                    string time = riadok.Substring(3,riadok.Length - 3);
                    int timeToDelay = Int32.Parse(time);
                    cakaj(timeToDelay);
                    break;
            }

        }

        private void rapidPohyb(string suradnice)
        {
            //int indexY = suradnice.IndexOf('Y');
            //int indexZ = suradnice.IndexOf('Z');

            //string posunX = suradnice.Substring(0 + 1, (indexY - 1));
            //string posunY = suradnice.Substring(indexY + 1, indexZ - indexY - 1);
            //string posunZ = suradnice.Substring(indexZ + 1, 4);

            //double posX = double.Parse(posunX, System.Globalization.CultureInfo.InvariantCulture);
            //double posY = double.Parse(posunY, System.Globalization.CultureInfo.InvariantCulture);
            //double posZ = double.Parse(posunZ, System.Globalization.CultureInfo.InvariantCulture);

            ////posX *= nasobic;
            ////posY *= nasobic;

            //if (posZ != 0) {                            // nema sa kreslit, len sa presunut
            //    if (rezimKreslenia == 0) {
            //        aktualX = posX + svetX;
            //        aktualY = posY + svetY;
            //    }

            //    else if (rezimKreslenia == 1)
            //    {
            //        aktualX = posX + aktualX;
            //        aktualY = posY + aktualY;                  
            //    }
            //    //armTo(aktualX, DrawArea.Height - aktualY);
            //    return;
            //}

            //if (rezimKreslenia == 0)            // ked plati podmienka, kresli od svetoveho suradnicoveho systemu
            //{
            //    g = Graphics.FromImage(DrawArea);
            //    //Pen mypen = new Pen(Color.Black);
            //    g.DrawLine(mypen, (float)aktualX, (float)aktualY, (float)posX + (float)svetX, (float)posY + (float)svetY);
            //    pictureBox1.Image = DrawArea;
            //    g.Dispose();

            //    aktualX = posX + svetX;
            //    aktualY = posY + svetY;
            //}

            //else if (rezimKreslenia == 1)       // ked plati podmienka, kresli ikrementalne
            //{
            //    g = Graphics.FromImage(DrawArea);
            //    //Pen mypen = new Pen(Color.Black);
            //    g.DrawLine(mypen, (float)aktualX, (float)aktualY, (float)posX + (float)aktualX, (float)posY + (float)aktualY);
            //    pictureBox1.Image = DrawArea;
            //    g.Dispose();

            //    aktualX = posX + aktualX;
            //    aktualY = posY + aktualY;
            //}

            //armTo(aktualX, DrawArea.Height - aktualY);
        }

        private void linearPohyb(string suradnice)
        {

            int indexY = suradnice.IndexOf('Y');
            int indexZ = suradnice.IndexOf('Z');

            string posunX = suradnice.Substring(0 + 1, (indexY - 1));
            string posunY = suradnice.Substring(indexY + 1, indexZ - indexY - 1);
            string posunZ = suradnice.Substring(indexZ + 1, 4);

            double posX = double.Parse(posunX, System.Globalization.CultureInfo.InvariantCulture);
            double posY = double.Parse(posunY, System.Globalization.CultureInfo.InvariantCulture);
            double posZ = double.Parse(posunZ, System.Globalization.CultureInfo.InvariantCulture);

            //posY = posY * (-1);

            predchadzajuciX = (int)aktualX;
            predchadzajuciY = (int)aktualY;

            if (posZ != 0)                               // nema sa kreslit, len sa presunut
            {                           
                if (rezimKreslenia == 0)
                {
                    aktualX = posX + svetX;
                    aktualY = posY + svetY;
                }

                else if (rezimKreslenia == 1)
                {
                    aktualX = posX + aktualX;
                    aktualY = posY + aktualY;
                }

                armTo(aktualX, aktualY, 'L', 1);
                return;
            }

            if (rezimKreslenia == 0)            // ked plati podmienka, kresli od svetoveho suradnicoveho systemu
            {
                g = Graphics.FromImage(DrawArea);
                g.DrawLine(mypen, (float)aktualX, (float)aktualY, (float)posX + (float)svetX, (float)posY + (float)svetY);
                pictureBox1.Image = DrawArea;
                g.Dispose();

                aktualX = posX + svetX;
                aktualY = posY + svetY;
            }

            else if (rezimKreslenia == 1)       // ked plati podmienka, kresli ikrementalne
            {
                g = Graphics.FromImage(DrawArea);
                g.DrawLine(mypen, (float)aktualX, (float)aktualY, (float)posX + (float)aktualX, (float)posY + (float)aktualY);
                pictureBox1.Image = DrawArea;
                g.Dispose();

                aktualX = posX + aktualX;
                aktualY = posY + aktualY;
            }


            double posuvacX = (aktualX - predchadzajuciX) / 14;
            double posuvacY = (aktualY - predchadzajuciY) / 14;

            for (int q = 0; q < 15; q++)
            {
                double x1 = (predchadzajuciX) +posuvacX * q;
                double y1 = (predchadzajuciY) + posuvacY * q;
              //  armTo((aktualX- predchadzajuciX) +posuvacX*q,((300-aktualY)- predchadzajuciY) +posuvacY*q,'L',0);
                armTo(x1,y1, 'L', 0);
            }
        }

        private void kruznica(string suradnice) {
            int indexX = suradnice.IndexOf('X');
            int indexY = suradnice.IndexOf('Y');
            int indexR = suradnice.IndexOf('R');

            string posunX = suradnice.Substring(1, indexY - indexX - 1);
            string posunY = suradnice.Substring(indexY + 1, indexR - indexY - 1);
            string radius = suradnice.Substring(indexR + 1, suradnice.Length - indexR - 1);

            double posX = double.Parse(posunX, System.Globalization.CultureInfo.InvariantCulture);
            double posY = double.Parse(posunY, System.Globalization.CultureInfo.InvariantCulture);
            double polomer = double.Parse(radius, System.Globalization.CultureInfo.InvariantCulture);
            polomerKruznice = polomer;

            double bodX = aktualX + posX;
            double bodY =  DrawArea.Height - aktualY + posY;

            double iujf = DrawArea.Height;
            double fiune = DrawArea.Width;

            //aktualX = aktualX + posX;
            //double positionY = aktualY;
            //aktualY = aktualY + posY;
            //positionY = (300 - positionY) + posY;
            aktualX = aktualX + posX;
            aktualY = aktualY + posY;

            double xStart = aktualX - polomerKruznice;
            double xFinish = polomerKruznice + aktualX;
            double y = 0.0;
            double r_sq = polomerKruznice * polomerKruznice;
        //    double Xpo;

            for (double x = xStart; x <= xFinish; x += 0.02)
            {
                y = aktualY + Math.Sqrt(r_sq - ((x - aktualX) * (x - aktualX)));
                armTo(x,y,'K',0);
            }

            for (double x = xFinish; x >= xStart; x -= 0.02)
            {
                y = aktualY - Math.Sqrt(r_sq - ((x - aktualX) * (x - aktualX)));
              //  Xpo = x;
                armTo(x, y, 'K', 0);              
            }

            armTo(aktualX, aktualY, 'L', 1);
            //aktualX = aktualX - polomer / 2;
            //aktualY = aktualY - polomer / 2;
        }

        private void kruznicovyPohyb(string suradnice, bool pohybVsmereRuciciek)
        {
            int indexR;
            if ((indexR = suradnice.IndexOf('R')) != -1)
            {
                kruznica(suradnice);
                return;
            }

            int indexY = suradnice.IndexOf('Y');
            int indexI = suradnice.IndexOf('I');
            int indexJ = suradnice.IndexOf('J');

            string posunX = suradnice.Substring(0 + 1, (indexY - 1));
            string posunY = suradnice.Substring(indexY + 1, indexI - indexY - 1);
            string centerX = suradnice.Substring(indexI + 1, 4);
            string centerY = suradnice.Substring(indexJ + 1, 4);

            double posX = double.Parse(posunX, System.Globalization.CultureInfo.InvariantCulture);
            double posY = double.Parse(posunY, System.Globalization.CultureInfo.InvariantCulture);
            double centX = double.Parse(centerX, System.Globalization.CultureInfo.InvariantCulture);
            double centY = double.Parse(centerY, System.Globalization.CultureInfo.InvariantCulture);

            posY = (-1) * posY;          // prepocitavanie osi y, pretoze v bitmape je v tejto osi opacne + a -
            centY = DrawArea.Height - centY;

            g = Graphics.FromImage(DrawArea);
            
            if (pohybVsmereRuciciek == true)
            {
                //int indexY = suradnice.IndexOf('Y');
                //int indexI = suradnice.IndexOf('I');
                //int indexJ = suradnice.IndexOf('J');

                //string posunX = suradnice.Substring(0 + 1, (indexY - 1));
                //string posunY = suradnice.Substring(indexY + 1, indexI - indexY - 1);
                //string centerX = suradnice.Substring(indexI + 1, 4);
                //string centerY = suradnice.Substring(indexJ + 1, 4);

                //double posX = double.Parse(posunX, System.Globalization.CultureInfo.InvariantCulture);
                //double posY = double.Parse(posunY, System.Globalization.CultureInfo.InvariantCulture);
                //double centX = double.Parse(centerX, System.Globalization.CultureInfo.InvariantCulture);
                //double centY = double.Parse(centerY, System.Globalization.CultureInfo.InvariantCulture);

                //posY = (-1) * posY;          // prepocitavanie osi y, pretoze v bitmape je v tejto osi opacne + a -
                //centY = DrawArea.Height - centY;


                //posY = DrawArea.Height - posY;
                //aktualY = DrawArea.Height - aktualY;
                //centY = DrawArea.Height - centY;
                //      centY = pictureBox1.Height - centY + ()
                //*************************************************************************
                //Rectangle obdl = new Rectangle(100, 100, 100, 150);
                //g = Graphics.FromImage(DrawArea);

                double r = (double)Math.Sqrt((aktualX - centX) * (aktualX - centX) + (aktualY - centY) * (aktualY - centY));
                double x = centX - r;
                double y = centY - r;
                double width = 2 * r;
                double height = 2 * r;
                double startAngle = (double)(180 / Math.PI * Math.Atan2(aktualY - centY, aktualX - centX));
                double endAngle = (double)(180 / Math.PI * Math.Atan2((aktualY - posY) - centY, (aktualX + posX) - centX));
                g.DrawArc(mypen, (int)x, (int)y, (int)width, (int)height, (int)startAngle, (int)Math.Abs(startAngle - endAngle)); //


                //g.DrawLine(mypen, (float)0, (float)0, (float)50, (float)150);
                //g.DrawArc(mypen, obdl, -180, 90);    // predposledny parametere hovori ze kde sa ma s kruznicou zacat
                // posledny parameter hovori, ze aky velky obluk sa ma vykreslit
                //g.DrawRectangle(mypen, obdl);
                pictureBox1.Image = DrawArea;
                g.Dispose();
            }

            else
            {
                double r = (double)Math.Sqrt(((aktualX + posX) - centX) * ((aktualX + posX) - centX) + ((aktualY - posY) - centY) * ((aktualY - posY) - centY));
                double x = centX - r;
                double y = centY - r;
                double width = 2 * r;
                double height = 2 * r;
                double startAngle = (double)(180 / Math.PI * Math.Atan2((aktualY + posY) - centY, (aktualX + posX) - centX));
                double endAngle = (double)(180 / Math.PI * Math.Atan2(aktualY - centY, aktualX - centX));
                g.DrawArc(mypen, (int)x, (int)y, (int)width, (int)height, (int)startAngle, (int)Math.Abs(startAngle - endAngle)); //


                //g.DrawLine(mypen, (float)0, (float)0, (float)50, (float)150);
                //g.DrawArc(mypen, obdl, -180, 90);    // predposledny parametere hovori ze kde sa ma s kruznicou zacat
                // posledny parameter hovori, ze aky velky obluk sa ma vykreslit
                //g.DrawRectangle(mypen, obdl);
                pictureBox1.Image = DrawArea;
                g.Dispose();
            }

            aktualX += posX;
            aktualY = aktualY + posY;

            armTo(aktualX, DrawArea.Height - aktualY, 'O',0);
        }

        double getAngle(double a, double b, double c)
        {
            return Math.Acos((Math.Pow(b, 2) + Math.Pow(c, 2) - Math.Pow(a, 2)) / (2 * b * c));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            aktualX = 4;
            aktualY = 4;
            aktualZ = 0;
            textBox1.Text = "";
            textBox1.Refresh();
            textBoxVypisUhly.Text = "";
            textBoxVypisUhly.Refresh();
        }

        double constrain(double value, double min, double max)
        {
            double result = 0;
            if (value <= min)
            {
                result = min;
            }
            if (value >= max)
            {
                result = max;
            }
            else
            {
                result = value;
            }
            return result;
        }

        private void moveServo(double a1, double a2, char symbol, int pise)
        {

            double deg1 = constrain((Math.PI - a1) * radTodegree, 0, 150);
            double deg2 = 180 - constrain(a2 * radTodegree, 0, 150);

            double uholNaVypisanie = 180 - deg1;

            if (uholNaVypisanie > 145 || deg2 > 150)
            {
                Console.WriteLine("ahoh");
            }

            double prepocetZoStupnov1 = uholNaVypisanie * 100/145;      // prepocet pre velky motor
            //prepocetZoStupnov1 = prepocetZoStupnov1 * 263 / 100 + 147;
            prepocetZoStupnov1 = prepocetZoStupnov1 * 1250 / 100 + 800;

            double prepocetZoStupnov2 = deg2 * 100 / 150;               // prepocet pre maly motor
            prepocetZoStupnov2 = prepocetZoStupnov2 * 1500 / 100 + 600;

            textBoxVypisUhly.AppendText("Uhol 1: " + uholNaVypisanie.ToString() + "   " + "Uhol 2: " + deg2.ToString()); // = "Uhol 1: " + deg1.ToString() + "   " + "Uhol 2: " + deg2.ToString();
            textBoxVypisUhly.AppendText(Environment.NewLine + Environment.NewLine);

            //string s = string.Format("{0:N2}%", x);

            if (symbol == 'K')
            {
                string temp = ((int)prepocetZoStupnov1).ToString().PadLeft(4, '0');
                string temp2 = ((int)prepocetZoStupnov2).ToString().PadLeft(4, '0');
                string posliCOM = symbol.ToString() + ",R=" + polomerKruznice + "=>" + pise.ToString() + ";" + temp + "," + temp2;
                seriovyPort.Open();
                for (int i = 0; i < posliCOM.Length; i++)
                {
                    seriovyPort.Write(posliCOM.ElementAt(i).ToString());
                    System.Threading.Thread.Sleep(10);
                }
                seriovyPort.Write(Environment.NewLine);
                seriovyPort.Close();
            }

            else
            {
                string temp = ((int)prepocetZoStupnov1).ToString().PadLeft(4, '0');
                string temp2 = ((int)prepocetZoStupnov2).ToString().PadLeft(4, '0');
                string posliCOM = symbol.ToString() + "=>" + pise.ToString() + ";" + temp + "," + temp2;
                seriovyPort.Open();
                for (int i = 0; i < posliCOM.Length; i++) {
                    seriovyPort.Write(posliCOM.ElementAt(i).ToString());
                    System.Threading.Thread.Sleep(10);
                }
                seriovyPort.Write(Environment.NewLine);
                seriovyPort.Close();
            }
            
        }

        private void armTo(double x, double y, char symbol, int pise)
        {
            double h = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            //calculates angles based on desired point
            double a1 = (getAngle(ARMLENGTH1, ARMLENGTH, h) + Math.Atan2(y, x));
            double a2 = (getAngle(h, ARMLENGTH, ARMLENGTH1));

            moveServo(a1, a2, symbol, pise);
        }

        private void cakaj(int time)
        {
            System.Threading.Thread.Sleep(time * 1000);
        }
    }
}
// SerialPort _serialPort = new SerialPort();