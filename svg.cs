/* HTML       C# Klassenbibliothek zum Generieren von SVG-Code
 * ------------------------------------------------------------
 * Datei:     svg.cs
 * Version:   30.04.2015
 * Besitzer:  Mathias Rentsch (rentsch@online.de)
 * Lizenz:    GPL
 *
 * Die Anwendung und die Quelltextdateien sind freie Software und stehen unter der
 * GNU General Public License. Der Originaltext dieser Lizenz kann eingesehen werden
 * unter http://www.gnu.org/licenses/gpl.html.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web;
using System.Globalization;
using HTML;


namespace HTML.SVG
{
    public class SvgGraphic : HtmlObject
    {
        public Point Origin = new Point(0, 0);
        
        public int Width = 100;
        public int Height = 100;
        public Color BackgroundColor;

        private Single zoomFactor = 1;
        public Single ZoomFactor
        {
            set
            {
                if (value>0) zoomFactor = value;
            }
            get
            {
                return zoomFactor;
            }
        }

        public SvgGraphic()
        {
        }

        public SvgGraphic(int width, int height)
        {
            Width = width;
            Height = height;
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Parameters.Add("width", GetString(Width));
            tag.Parameters.Add("height", GetString(Height));
            tag.Parameters.Add("viewbox", GetString(Origin, Width, Height, ZoomFactor));
            tag.Styles.Add("background-color", GetString(BackgroundColor));
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "svg";

            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd();
            return s;
        }

        private string GetString(Point origin,int width,int height,Single zoomfaktor)
        {
            return origin.X.ToString() + "," + origin.Y.ToString() + "," + (Width / ZoomFactor).ToString("G", CultureInfo.InvariantCulture) + "," + (Height / ZoomFactor).ToString("G", CultureInfo.InvariantCulture);
        }
    }

    public class SvgElement:HtmlObject
    {
        public Point Location = new Point(0, 0);
        public bool Filled = false;
        public int LineWidth = 1;
        public Color Color = Color.Black;

        public SvgElement()
        {
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Styles.Add("stroke", GetString(Color));
            tag.Styles.Add("stroke-width", GetString(LineWidth));
            if (Filled)
            {
                tag.Parameters.Add("fill", GetString(Color));
            }
            else
            {
                tag.Parameters.Add("fill", "none");
            }
            return tag;
        }
    }

    public class SvgLine : SvgElement
    {
        public int X1 = 0;
        public int Y1 = 0;
        public int X2 = 0;
        public int Y2 = 0;

        public SvgLine()
        {
        }

        public SvgLine(int x1, int y1,int x2,int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public SvgLine(Point start, Point ende)
        {
            X1 = start.X;
            Y1 = start.Y;
            X2 = ende.X;
            Y2 = ende.Y;
        }

        public SvgLine(Point start, Point ende, Color color,int width):this(start,ende)
        {
            Color = color;
            LineWidth = width;  
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Parameters.Add("x1", X1.ToString());
            tag.Parameters.Add("y1", Y1.ToString());
            tag.Parameters.Add("x2", X2.ToString());
            tag.Parameters.Add("y2", Y2.ToString());
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "line";
            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd(); Console.WriteLine("Line GetHtml: " + s);
            return s;
        }

        public new void Add(HtmlObject element)
        {
            Add("");
        }

        public new void Add(string text)
        {
            throw new Exception("Das Einfügen von SVG-Elementen ist nicht gestattet."); 
        }
    }

    public class SvgCircle : SvgElement
    {
        public int Radius = 2;

        public SvgCircle()
        {
        }

        public SvgCircle(int x, int y)
        {
            Location = new Point(x,y);
        }

        public SvgCircle(Point location)
        {
            Location = location;
        }

        public SvgCircle(Point location, Color color, int lineWidth)
            : this(location)
        {
            Color = color;
            LineWidth = lineWidth;
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Parameters.Add("cx", Location.X.ToString());
            tag.Parameters.Add("cy", Location.Y.ToString());
            tag.Parameters.Add("r", GetString(Radius));
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "circle";
            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd(); Console.WriteLine("Circle GetHtml: " + s);
            return s;
        }
    }

    public class SvgRectangle : SvgElement
    {
        public int Width = 0;
        public int Height = 0;

        public SvgRectangle()
        {
        }

        public SvgRectangle(int x, int y)
        {
            Location = new Point(x,y);
        }

        public SvgRectangle(Point location)
        {
            Location = location;
        }

        public SvgRectangle(int x, int y, int width, int height)
            : this(x,y)
        {
            Width = width;
            Height = height;
        }

        public SvgRectangle(Point location,int width,int height)
            : this(location)
        {
            Width = width;
            Height = height;
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Parameters.Add("x", (Location.X-Width/2).ToString());
            tag.Parameters.Add("y", (Location.Y-Height/2).ToString());
            tag.Parameters.Add("width", GetString(Width));
            tag.Parameters.Add("height", GetString(Height));
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "rect";
            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd(); Console.WriteLine("Rect GetHtml: "+s);
            return s;
        }
    }

    public class SvgText : SvgElement
    {
        public FontFamilyTyp FontFamily = FontFamilyTyp.None;
        public int FontSize = 0;
        public FontWeight FontWeight = FontWeight.None;
        public FontStyle FontStyle = FontStyle.None;
        public HorizontalAlignment HorizontalAlign = HorizontalAlignment.None;
        public VerticalAlignment VerticalAlign = VerticalAlignment.None;
        
        public int Rotation = 0;
        public string Text = "";

        //  Vorerst keine Implementierung
        //----------------------------------------------------------------
        //public Color BackgroundColor;
        //public TextDecoration TextDecoration = TextDecoration.None;
        //----------------------------------------------------------------

        public SvgText()
        {
            Filled = true;
            LineWidth = 0;
        }

        public SvgText(int x, int y):this()
        {
            Location = new Point(x,y);
        }

        public SvgText(string text,int x, int y):this(x,y)
        {
            Text = text;
        }

        public SvgText(Point location):this(location.X,location.Y)
        {
        }

        public SvgText(string text,Point location):this(text,location.X,location.Y)
        {
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Parameters.Add("x", Location.X.ToString());
            tag.Parameters.Add("y", Location.Y.ToString());
            tag.Parameters.Add("font-family", GetString(FontFamily));
            tag.Parameters.Add("font-size", GetString(FontSize));
            tag.Parameters.Add("font-weight", GetString(FontWeight));
            tag.Parameters.Add("font-style", GetString(FontStyle));
            tag.Parameters.Add("transform", GetStringRotate(Rotation));
            tag.Parameters.Add("text-anchor", GetString(HorizontalAlign));
            tag.Parameters.Add("alignment-baseline", GetString(VerticalAlign));
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "text";
            string s = "";
            if (Text.Length > 0)
            {
                s += tag.GetHtml();
                s += base.GetHtml();
                s += HttpUtility.HtmlEncode(Text);
                s += tag.GetHtmlEnd();
            }
            return s;
        }

        private string GetStringRotate(int degree)
        {
            string s = "";
            if (Rotation>0)
            {
                s = "rotate(-" + GetString(degree) + " " + Location.X + "," + Location.Y + ")"; 
            }
            return s;
        }

        private new string GetString(HorizontalAlignment align)
        {
            string p = "";
            switch (align)
            {
                case HorizontalAlignment.Left:
                    p += "start";
                    break;
                case HorizontalAlignment.Right:
                    p += "end";
                    break;
                case HorizontalAlignment.Center:
                    p += "middle";
                    break;
                default:
                    break;
            }
            return p;
        }

        private string GetString(VerticalAlignment valign)
        {
            string p = "";
            switch (valign)
            {
                case VerticalAlignment.Baseline:
                    p = "baseline";
                    break;
                case VerticalAlignment.Middle:
                    p = "middle";
                    break;
                case VerticalAlignment.Bottom:
                    p = "hanging";
                    break;
            }
            return p;
        }

        public static HTML.FontFamilyTyp ConvertFontFamiliy(Font font)
        {
            HTML.FontFamilyTyp erg = FontFamilyTyp.Verdana;
            switch (font.FontFamily.Name)
            {
                case "Arial":
                    erg = FontFamilyTyp.Arial;
                    break;
                case "Verdana":
                    erg = FontFamilyTyp.Verdana;
                    break;
                case "Courier New":
                    erg = FontFamilyTyp.Courier;
                    break;
            }
            return erg;
        }
    }

    public class SvgString : SvgElement
    {
        public string String;

        public SvgString()
        {
        }

        public SvgString(string s)
        {
            String = s;
        }

        public override string GetHtml()
        {
            return String;
        }
    }
}
