namespace throughStars
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private Star[] stars = new Star[10000];
        private Random rnd = new Random();
        public class Star
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
            public Brush color { get; set; }

        }
        public Form1()
        {
            InitializeComponent();
        }
        private float Mapping(float n, float start1, float stop1, float start2, float stop2)
        {
            return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            picBox.Image = new Bitmap(picBox.Width, picBox.Height);
            graphics = Graphics.FromImage(picBox.Image);
            
            for(int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star()
                {
                    X = rnd.Next(-picBox.Width, picBox.Width),
                    Y = rnd.Next(-picBox.Height, picBox.Height),
                    Z = rnd.Next(1, picBox.Width),
                    color = brushes[rnd.Next(0, brushes.Length - 1)]

                };
            }
            
            timer1.Start();
        }
        Brush[] brushes = new Brush[]
            {
                Brushes.Pink,
                Brushes.Plum,
                Brushes.PapayaWhip,
                Brushes.Yellow,
                Brushes.Tomato,
                Brushes.Tan,
                Brushes.Green,
                Brushes.Goldenrod,
                Brushes.Fuchsia,
                Brushes.Khaki,
                Brushes.Gray,
                Brushes.Magenta,
                Brushes.Silver,
                Brushes.Crimson,
                Brushes.Aqua,
                Brushes.Blue,
                Brushes.Cyan,
                Brushes.Lime,
                Brushes.Azure,
                Brushes.DeepPink,
                Brushes.LightPink,
                Brushes.MistyRose,
                Brushes.White,
                Brushes.SeaShell

                //
            };
        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            foreach(var star in stars)
            {
                DrawStar(star);
                MoveStar(star);
            }
            picBox.Refresh();
            
        }

        private void MoveStar(Star star)
        {
            star.Z -= 10;

            if (star.Z < 1)
            {
                star.Z = rnd.Next(1, picBox.Width);
                star.X = rnd.Next(-picBox.Width, picBox.Width);
                star.Y = rnd.Next(-picBox.Height, picBox.Height);
            }
        }

        private void DrawStar(Star star)
        {
            float sizeOfStar = Mapping(star.Z, 0, picBox.Width, 10, 0);
            float x = Mapping(star.X / star.Z, 0, 1, 0, picBox.Width) + picBox.Width / 2;
            float y = Mapping(star.Y / star.Z, 0, 1, 0, picBox.Height) + picBox.Height / 2;
            
            graphics.FillEllipse(star.color, x, y, sizeOfStar, sizeOfStar);
        }

        
    }
}