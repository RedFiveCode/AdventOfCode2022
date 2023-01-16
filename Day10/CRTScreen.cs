using System.Text;

namespace Day10
{
    internal class CRTScreen
    {
        private const int Width = 40;
        private const int Height = 6;

        private const char LitPixel = '#';
        private const char DarkPixel = '.';

        private char[,] pixels = new char[Height, Width];

        public CRTScreen()
        {
            ClearScreen();
        }


        public void Draw(Registers registers)
        {
            var cycleZeroIndex = registers.Cycles - 1;
            var col = cycleZeroIndex % Width;
            var row = cycleZeroIndex / Width;

            // sprite is 3 pixels wide, centered on X register
            var spriteLeft   = registers.X - 1;
            var spriteCentre = registers.X;
            var spriteRight  = registers.X + 1;

            if (col == spriteLeft ||
                col == spriteCentre ||
                col == spriteRight)
            {
                pixels[row, col] = LitPixel;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    builder.Append(pixels[row, col]);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        private void ClearScreen()
        {
            // fill all with DarkPixel (.)
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    pixels[row, col] = DarkPixel;
                }
            }
        }
    }
}