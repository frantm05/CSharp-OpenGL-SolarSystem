using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using OpenGL;

namespace SolarSystemProject
{
    public static class TextRenderer
    {
        public static void DrawText(Control control, string text, int x, int y, Font font, Color color)
        {
            // Vytvoření bitmapy s textem - použijeme Graphics.MeasureString
            SizeF size;
            using (var tempBitmap = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(tempBitmap))
            {
                size = g.MeasureString(text, font);
            }

            using (var bitmap = new Bitmap((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height)))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawString(text, font, new SolidBrush(color), 0, 0);

                // Převod bitmapy na texturu
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                var data = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                // Vytvoření OpenGL textury
                int texture = gl.GenTexture();
                gl.BindTexture(gl.TEXTURE_2D, texture);
                gl.TexImage2D(
                    gl.TEXTURE_2D, 0, gl.RGBA,
                    bitmap.Width, bitmap.Height, 0,
                    gl.BGRA, gl.UNSIGNED_BYTE, data.Scan0);

                bitmap.UnlockBits(data);

                // Nastavení parametrů textury
                gl.TexParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
                gl.TexParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);

                // Uložení stavu
                gl.PushAttrib(gl.ENABLE_BIT | gl.COLOR_BUFFER_BIT | gl.TEXTURE_BIT);
                gl.MatrixMode(gl.PROJECTION);
                gl.PushMatrix();
                gl.LoadIdentity();
                glu.Ortho2D(0, control.Width, control.Height, 0);
                gl.MatrixMode(gl.MODELVIEW);
                gl.PushMatrix();
                gl.LoadIdentity();

                // Vykreslení textury
                gl.Enable(gl.TEXTURE_2D);
                gl.BindTexture(gl.TEXTURE_2D, texture);
                gl.Begin(gl.QUADS);
                gl.TexCoord2f(0, 0); gl.Vertex2f(x, y + bitmap.Height);
                gl.TexCoord2f(1, 0); gl.Vertex2f(x + bitmap.Width, y + bitmap.Height);
                gl.TexCoord2f(1, 1); gl.Vertex2f(x + bitmap.Width, y);
                gl.TexCoord2f(0, 1); gl.Vertex2f(x, y);
                gl.End();

                // Obnovení stavu
                gl.PopMatrix();
                gl.MatrixMode(gl.PROJECTION);
                gl.PopMatrix();
                gl.MatrixMode(gl.MODELVIEW);
                gl.PopAttrib();

                // Uvolnění textury
                gl.DeleteTextures(1, new int[] { texture });
            }
        }
    }
}