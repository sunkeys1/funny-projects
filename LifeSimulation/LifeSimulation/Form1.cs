namespace LifeSimulation
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private GameLogic gameLogic;
        private Color _bgColor = Color.Black;
        private Brush _pixelColor = Brushes.Green;

        public Form1()
        {
            InitializeComponent();
            
        }
        private void startLife()
        {
            if (time.Enabled) return;


            resolution = (int)numResolution.Value;
            numResolution.Enabled = false;
            numDensity.Enabled = false;
            gameLogic = new GameLogic
            (
                rows: pictureBox1.Height / resolution,
                cols: pictureBox1.Width / resolution,
                density: (int)numDensity.Minimum + (int)numDensity.Maximum - (int)numDensity.Value
            );
            Text = $"Current generation: {gameLogic.currentGen}";


            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            time.Start();
        }
        public void DrawNewGen()
        {
            graphics.Clear(_bgColor);

            var gameField = gameLogic.GetCurrentGen();
            for (int x = 0; x < gameField.GetLength(0); x++)
            {
                for (int y = 0; y < gameField.GetLength(1); y++)
                {
                    if (gameField[x, y])
                    {
                        graphics.FillRectangle(_pixelColor, x * resolution, y * resolution, resolution - 1, resolution - 1);
                    }
                    
                }
            }


            pictureBox1.Refresh();
            Text = $"Current generation: {gameLogic.currentGen}";

            gameLogic.Generations();
        }
        private void stopLife()
        {
            if (!time.Enabled)
            {
                return;
            }
            else
            {
                time.Stop();
                numResolution.Enabled = true;
                numDensity.Enabled = true;
            }
        }
        private void bStart_Click(object sender, EventArgs e)
        {
            startLife();

        }

        private void bStop_Click(object sender, EventArgs e)
        {
            stopLife();
        }



        private void time_Tick(object sender, EventArgs e)
        {
            DrawNewGen();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!time.Enabled) return;

            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameLogic.addCell(x, y);

            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameLogic.removeCell(x, y);
            }
        }
        
        private void selectColor_Click(object sender, EventArgs e)
        {
            Brush clr = pickColor();
            _pixelColor = clr;
            
        }
        private void selectColorBg_Click(object sender, EventArgs e)
        {
            Color clr = pickColorBG();
            _bgColor = clr;
        }
        private Brush pickColor()
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получить выбранный цвет
                Color selectedColor = colorDialog.Color;
                Brush selectedBrush = new SolidBrush(selectedColor);

                return selectedBrush;
            }
            return _pixelColor;
        }
        private Color pickColorBG()
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получить выбранный цвет
                Color selectedColor = colorDialog.Color;

                return selectedColor;
            }
            return _bgColor;
        }
        

    }
}