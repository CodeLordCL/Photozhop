using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PhotoZgop.Entitys
{
    public class LayerEntitys
    {
        //--------------------------------------------------------------------------------
        public int hight { get; set; }

        public int width { get; set; }

        public string fileName { get; set; }

        public Operation operation { get; set; }

        public ChoseRgb choseRgb { get; set; }

        public string filePath { get; set; }

        public Bitmap bitmap { get; set; }
        //--------------------------------------------------------------------------------

        public LayerEntitys(string filePath, int hight, int width, Operation operation = Operation.Non, ChoseRgb choseRgb = ChoseRgb.NON)
        {
            this.filePath = filePath;

            this.hight = hight;

            this.width = width;

            this.operation = operation;

            this.choseRgb = choseRgb;

            this.bitmap = new Bitmap(new Bitmap(filePath), width, hight);
        }

        public LayerEntitys(Bitmap bitmap, int num, int hight, int width, Operation operation = Operation.Non, ChoseRgb choseRgb = ChoseRgb.NON)
        {
            this.filePath = "Собранная картинка" + num;

            this.bitmap = bitmap;

            this.hight = hight;

            this.width = width;

            this.operation = operation;

            this.choseRgb = choseRgb;
        }

        //--------------------------------------------------------------------------------

        private int Claim(int val, int min = 0, int max = 255)
        {
            if (val >= max)
            {
                return max;
            }
            else if (val <= min)
            {
                return min;
            }
            else
            {
                return val;
            }
        }

        //--------------------------------------------------------------------------------

        private Bitmap picxelOperation(Bitmap oldBitmap, Func<int, int, int> func)
        {

            for (int i = 0; i < width; i += 1)
            {
                for (int j = 0; j < hight; j += 1)
                {
                    var picel = oldBitmap.GetPixel(i, j);

                    var newPicel = bitmap.GetPixel(i, j);

                    int r = picel.R;

                    int g = picel.G;

                    int b = picel.B;

                    if ((choseRgb != ChoseRgb.GB) && (choseRgb != ChoseRgb.G) && (choseRgb != ChoseRgb.B) && (choseRgb != ChoseRgb.NON))
                    {
                        r = Claim(func(picel.R, newPicel.R));
                    }

                    if ((choseRgb != ChoseRgb.RB) && (choseRgb != ChoseRgb.R) && (choseRgb != ChoseRgb.B) && (choseRgb != ChoseRgb.NON))
                    {
                        g = Claim(func(picel.G, newPicel.G));
                    }

                    if ((choseRgb != ChoseRgb.RG) && (choseRgb != ChoseRgb.R) && (choseRgb != ChoseRgb.G) && (choseRgb != ChoseRgb.NON))
                    {
                        b = Claim(func(picel.B, newPicel.B));
                    }

                    picel = Color.FromArgb(r, g, b);

                    oldBitmap.SetPixel(i, j, picel);

                }
            }

            return oldBitmap;
        }

        //--------------------------------------------------------------------------------

        private Bitmap picxelOperation(Bitmap oldBitmap, Func<int, int, bool> func)
        {

            for (int i = 0; i < width; i += 1)
            {
                for (int j = 0; j < hight; j += 1)
                {
                    var picel = oldBitmap.GetPixel(i, j);

                    var newPicel = bitmap.GetPixel(i, j);

                    int r = picel.R;

                    int g = picel.G;

                    int b = picel.B;

                    if(!func((r + g + b), (newPicel.R + newPicel.G + newPicel.B)))

                    if ((choseRgb != ChoseRgb.GB) && (choseRgb != ChoseRgb.G) && (choseRgb != ChoseRgb.B) && (choseRgb != ChoseRgb.NON))
                    {
                        r = newPicel.R;
                    }

                    if ((choseRgb != ChoseRgb.RB) && (choseRgb != ChoseRgb.R) && (choseRgb != ChoseRgb.B) && (choseRgb != ChoseRgb.NON))
                    {
                        g = newPicel.G;
                    }

                    if ((choseRgb != ChoseRgb.RG) && (choseRgb != ChoseRgb.R) && (choseRgb != ChoseRgb.G) && (choseRgb != ChoseRgb.NON))
                    {
                        b = newPicel.B;
                    }

                    picel = Color.FromArgb(r, g, b);

                    oldBitmap.SetPixel(i, j, picel);

                }
            }

            return oldBitmap;
        }

        //--------------------------------------------------------------------------------

        public Bitmap renderingNew(Bitmap oldBitmap)
        {
            switch (operation)
            {
                case Operation.Non:
                    {
                        return bitmap;
                    }
                case Operation.Sum:
                    {
                        return picxelOperation(oldBitmap, (x, y) => x + y);
                    }
                case Operation.Multiply:
                    {
                        return picxelOperation(oldBitmap, (x, y) => x * y);
                    }
                case Operation.Mid:
                    {
                        return picxelOperation(oldBitmap, (x, y) => (x + y) / 2);
                    }
                case Operation.Min:
                    {
                        return picxelOperation(oldBitmap, (x, y) => x < y);
                    }
                case Operation.Max:
                    {
                        return picxelOperation(oldBitmap, (x, y) => x > y);
                    }
                default:
                    {
                        return oldBitmap;
                    }
            }
        }

        //--------------------------------------------------------------------------------
    }

    public enum Operation
    {
        Non = -1,
        Sum,
        Multiply,
        Mid, 
        Min,
        Max,
        MaskNotElon
    }

    public enum ChoseRgb
    {
        NON = -1,
        RGB,
        RG,
        GB,
        RB,
        R,
        G,
        B
    }

}
