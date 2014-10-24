/* HTML       C# Klassenbibliothek zum Generieren von HTML-Code
 * ------------------------------------------------------------
 * Anwendung: Testanwendung
 * Datei:     html.cs
 * Version:   24.10.2014
 * Besitzer:  Mathias Rentsch (rentsch@online.de)
 * Lizenz:    GPL
 *
 * Die Anwendung und die Quelltextdateien sind freie Software und stehen unter der
 * GNU General Public License. Der Originaltext dieser Lizenz kann eingesehen werden
 * unter http://www.gnu.org/licenses/gpl.html.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Web;


namespace HTML
{
    public class Tag
    {
        public string Name;
        public ParameterList Parameters = new ParameterList();
        public ParameterList Styles = new ParameterList();

        public Tag()
        {
            Parameters.Add("style", ""); //dummy anlegen
        }

        public Tag(string name)
        {
            Name = name;
            Parameters.Add("style", ""); //dummy anlegen
        }

        public string GetHtml()
        {
            Parameters[0].Value = getHtmlStyles();
            string s = "";
            s += @"<" + Name + " ";
            s += getHtmlParameters();
            s += @">";
            return s;
        }

        public string GetHtmlEnd()
        {
            return @"</" + Name + ">";
        }

        private string getHtmlParameters()
        {
            string s = "";
            foreach (Parameter parameter in Parameters)
            {
                if (parameter.Value.Length > 0)
                    s += parameter.Name + @"=""" + parameter.Value + @""" ";
            }
            return s;
        }

        private string getHtmlStyles()
        {
            string s = "";
            foreach (Parameter style in Styles)
            {
                if (style.Value.Length > 0)
                    s += style.Name + ":" + style.Value + ";";
            }
            return s;
        }
    }

    public class HtmlDivision : HtmlElement
    {
        public HtmlDivision()
        {

        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "div";

            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd();
            return s;
        }
    }

    public class HtmlText : HtmlElement
    {
        public HtmlText()
        {

        }

        public HtmlText(string text)
        {
            Text = text;
        }

        public HtmlText(string text, FontFamily font, FontWeight weight)
        {
            Text = text;
            FontFamily = font;
            FontWeight = weight;
        }

        public HtmlText(string text, FontFamily font)
        {
            Text = text;
            FontFamily = font;
        }

        public HtmlText(string text, FontFamily font, int size)
        {
            Text = text;
            FontFamily = font;
            FontSize = size;
        }

        public HtmlText(string text, FontFamily font, int size, FontWeight weight)
        {
            Text = text;
            FontFamily = font;
            FontSize = size;
            FontWeight = weight;
        }

        public HtmlText(FontFamily font, int size, FontWeight weight)
        {
            FontFamily = font;
            FontSize = size;
            FontWeight = weight;
        }

        public HtmlText(int size, FontWeight weight)
        {
            FontSize = size;
            FontWeight = weight;
        }

        public HtmlText(FontWeight weight)
        {
            FontWeight = weight;
        }

        public HtmlText(FontFamily font, FontWeight weight)
        {
            FontFamily = font;
            FontWeight = weight;
        }

        public HtmlText(string text, int size)
        {
            Text = text;
            FontSize = size;
        }

        public HtmlText(int size)
        {
            FontSize = size;
        }

        public HtmlText(FontFamily font)
        {
            FontFamily = font;
        }

        public HtmlText(FontFamily font, int size)
        {
            FontFamily = font;
            FontSize = size;
        }

        public HtmlText(string text, FontWeight weight)
        {
            Text = text;
            FontWeight = weight;
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "span";

            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd();
            return s;
        }
    }



    public class HtmlImg : HtmlElement
    {

        public string Alternative = "";
        public int HorizontalSpace = 0;
        public int VerticalSpace = 0;
        public string Url = "http://rentsch.dashosting.de/ei.png";

        public HtmlImg()
        {
        }

        public HtmlImg(string url)
        {
            Url = url;
        }

        public HtmlImg(string url, string alternative)
        {
            Url = url;
            Alternative = alternative;
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Parameters.Add("src", Url);
            tag.Parameters.Add("alt", HttpUtility.HtmlEncode(Alternative));
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "img";
            tag.Parameters.Add("hspace", GetString(HorizontalSpace));
            tag.Parameters.Add("vspace", GetString(VerticalSpace));

            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            return s;
        }
    }



    public class HtmlParagraph : HtmlElement
    {
        public ParagraphTyp Typ = ParagraphTyp.Paragraph;

        public HtmlParagraph()
        {

        }

        public HtmlParagraph(string text)
        {
            Text = text;
        }

        public HtmlParagraph(HorizontalAlignment align)
        {
            HorizontalAlign = align;
        }

        public HtmlParagraph(string text, HorizontalAlignment align)
        {
            HorizontalAlign = align;
            Items.Add(new HtmlText(text));
        }

        public HtmlParagraph(string text, FontFamily font)
        {
            HtmlText htmltext = new HtmlText(text, font);
            Items.Add(htmltext);
        }

        public HtmlParagraph(string text, FontFamily font, HorizontalAlignment align)
        {
            HtmlText htmltext = new HtmlText(text, font);
            Items.Add(htmltext);
            HorizontalAlign = align;
        }

        public HtmlParagraph(string text, FontFamily font, int size)
        {
            HtmlText htmltext = new HtmlText(text, font, size);
            Items.Add(htmltext);

        }

        public HtmlParagraph(string text, int size)
        {
            HtmlText htmltext = new HtmlText(text, size);
            Items.Add(htmltext);
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            return tag;
        }


        public override string GetHtml()
        {
            string p = "";
            switch (Typ)
            {
                case ParagraphTyp.Paragraph:
                    p = "p";
                    break;
                case ParagraphTyp.HeadLine1:
                    p = "h1";
                    break;
                case ParagraphTyp.HeadLine2:
                    p = "h2";
                    break;
                case ParagraphTyp.HeadLine3:
                    p = "h3";
                    break;
                default:
                    p = "p";
                    break;
            }

            Tag tag = getTag();
            tag.Name = p;

            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd();
            return s;
        }
    }


    public class HtmlAnchor : HtmlElement
    {
        public string Url;
        public string FrameName;
        public LinkTarget Target = LinkTarget.None;

        public HtmlAnchor()
        {
        }

        public HtmlAnchor(string adresse)
        {
            Url = adresse;
        }

        public HtmlAnchor(string text, string adresse)
        {
            Url = adresse;
            Text = text;
        }

        public HtmlAnchor(HtmlText text, string adresse)
        {
            Url = adresse;
            Add(text);
        }

        public HtmlAnchor(string text, string adresse, LinkTarget target)
        {
            Url = adresse;
            Target = target;
            Text = text;
        }

        public HtmlAnchor(string adresse, LinkTarget target)
        {
            Url = adresse;
            Target = target;
        }

        public HtmlAnchor(HtmlText text, string adresse, LinkTarget target)
        {
            Url = adresse;
            Target = target;
            Add(text);
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Name = "a";
            tag.Parameters.Add("href", HttpUtility.HtmlEncode(Url));
            tag.Parameters.Add("target", GetString(Target, FrameName));

            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "a";
            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd();
            return s;
        }
    }



    public class HtmlBreak : HtmlElement
    {
        public HtmlBreak()
        {
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            return tag;
        }


        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "br";

            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd();
            return s;
        }
    }

    public class HtmlPage : HtmlElement
    {
        public string Title = "";
        public string Author = "";
        public string Description = "";
        public string Generator = "";
        public string Keywords = "";

        public HtmlPage()
            : base()
        {
        }

        public HtmlPage(string title)
            : base()
        {
            Title = title;
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "body";

            string s = "";
            s += getHeader();
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd();
            s += getFooder();
            return s;
        }


        private string getHeader()
        {
            string s = "";
            s += "<html>";
            s += "<head>";
            if (Title.Length > 0)
            {
                s += "<title>";
                s += HttpUtility.HtmlEncode(Title);
                s += "</title>";
            }
            
            if (Author.Length > 0)
            {
                s += @"<meta name=""author"" content=""";
                s += HttpUtility.HtmlEncode(Author);
                s += @""">";
            }

            if (Description.Length > 0)
            {
                s += @"<meta name=""description"" content=""";
                s += HttpUtility.HtmlEncode(Description);
                s += @""">";
            }

            if (Keywords.Length > 0)
            {
                s += @"<meta name=""keywords"" content=""";
                s += HttpUtility.HtmlEncode(Keywords);
                s += @""">";
            }
            s += "</head>";

            return s;
        }

        private string getFooder()
        {
            string s = "";
            s += "</html>";
            return s;
        }

        public void Save(string fileName)
        {
            StreamWriter w = new StreamWriter(fileName);
            w.WriteLine(GetHtml());
            w.Flush();
            w.Close();
        }
    }

    public class HtmlRow : HtmlElement
    {
        public List<HtmlCell> Cells = new List<HtmlCell>();

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "tr";

            string s = "";
            s += tag.GetHtml();
            foreach (HtmlCell cell in Cells)
            {
                s += cell.GetHtml();
            }
            s += tag.GetHtmlEnd();
            return s;
        }

       
    }



    public class HtmlCell : HtmlElement
    {

        public int ColSpan = 0;
        public int RowSpan = 0;


        public HtmlCell()
        {
        }        

        public HtmlCell(string text)
        {
            Items.Add(new HtmlText(text));
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Styles.Add("colspan", GetString(ColSpan));
            tag.Styles.Add("rowspan", GetString(RowSpan));
            return tag;
        }

        public override string GetHtml()
        {

            Tag tag = getTag();
            tag.Name = "td";

            string s = "";
            s += tag.GetHtml();
            s += base.GetHtml();
            s += tag.GetHtmlEnd();
            return s;
        }
    }

    public class HtmlTable : HtmlElement
    {
        private List<HtmlRow> Rows = new List<HtmlRow>();
        public TableAlignment Align = TableAlignment.None;
        public int CellPadding = 0;
        public int CellSpacing = 0;
        public TableFrame Frame = TableFrame.Box;
        public TableRules Rules = TableRules.All;

        public HtmlTable()
        {
            Rows.Add(new HtmlRow());   // für die Spaltenüberschriften
        }

        public HtmlCell GetNewColumn(string text)
        {
            if (Rows.Count > 1) 
                throw new Exception("Das Einfügen von Spalten ist nur möglich, wenn die Tabelle leer ist.");
            HtmlCell cell = new HtmlCell();
            HtmlText htmltext = new HtmlText(text, FontWeight.Bold);
            cell.Add(htmltext);
            cell.HorizontalAlign = HorizontalAlignment.Center;
            Rows[0].Cells.Add(cell);
            return cell;
        }

        public HtmlCell GetNewColumn(string text, int width)
        {
            HtmlCell cell = GetNewColumn(text);
            cell.Width = width;
            return cell;
        }

        public HtmlRow GetNewRow()
        {
            if (Rows[0].Cells.Count<1)
                throw new Exception("Das Einfügen von Zeilen ist nur möglich, wenn zuvor Spalten hinzugefügt wurden.");
            HtmlRow row = new HtmlRow();
            foreach (HtmlCell cell in Rows[0].Cells)
            {
                row.Cells.Add(new HtmlCell());
            }
            Rows.Add(row);
            return row;
        }

        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Parameters.Add("align", GetString(Align));   // wird nicht über Styles gemacht, da es unter float offenbar kein >center< gibt = Scheiße
            tag.Parameters.Add("cellpadding", GetString(CellPadding));  // wird über Parameter gemacht, da sonst alle Zellen über css margin gesetzt müßten
            tag.Parameters.Add("cellspacing", GetString(CellSpacing));  // wird über Parameter gemacht, wüsste sonst nicht wie
            tag.Parameters.Add("frame", GetString(Frame));
            tag.Parameters.Add("rules", GetString(Rules));
            return tag;
        }

        public override string GetHtml()
        {
            Tag tag = getTag();
            tag.Name = "table";

            string s = "";
            s += tag.GetHtml();
            foreach (HtmlRow row in Rows)
            {
                s += row.GetHtml();
            }
            s += tag.GetHtmlEnd();
            return s;
        }
    }

    public class Parameter
    {
        public string Name;
        public string Value;
    }

    public class ParameterList : List<Parameter>
    {
        public Parameter Add(string name, string value)
        {
            Parameter parameter = new Parameter();
            parameter.Name = name;
            parameter.Value = value;
            Add(parameter);
            return parameter;
        }
    }

    public class Sides<T>
    {
        public T Left;
        public T Right;
        public T Top;
        public T Bottom;

        public T All
        {
            set
            {
                Left = value;
                Right = value;
                Top = value;
                Bottom = value;
            }
            get
            {
                if (
                     (Left.Equals(Right)) &&
                     (Right.Equals(Top)) &&
                     (Top.Equals(Bottom))
                    )
                {
                    return Left; // is egal, hauptsache einer wird back, sind eh alle gleich
                }
                else
                {
                    return default(T);
                }
            }
        }

        public T Horizontal
        {
            set
            {
                Left = value;
                Right = value;
            }
            get
            {
                if (Left.Equals(Right))
                {
                    return Left; // is egal, hauptsache einer wird back, sind eh alle gleich
                }
                else
                {
                    return default(T);
                }
            }
        }

        public T Vertical
        {
            set
            {
                Top = value;
                Bottom = value;
            }
            get
            {
                if (Top.Equals(Bottom))
                {
                    return Top; // is egal, hauptsache einer wird back, sind eh alle gleich
                }
                else
                {
                    return default(T);
                }
            }
        }
    }

    public enum TableAlignment { None, Left, Center, Right };
    public enum TableFrame { None, Void, Above, Below, Hsides, Vsides, Lhs, Rhs, Box, Border };
    public enum TableRules { None, Groups, Rows, Cols, All };
    public enum HorizontalAlignment { None, Left, Right, Center, Justify };
    public enum VerticalAlignment { None, Length, Percent, Baseline, Sub, Super, Top, TextTop, Middle, Bottom, TextBottom };
    public enum FontFamily { None, Serif, SansSerif, Cursive, Fantasy, Monospace, Arial, Courier, TimesNewRoman, Verdana };
    public enum FontSizeTyp { Length, Percent, SmallXX, SmallX, Small, Smaller, Larger, Medium, Large, LargeX, LargeXX };
    public enum FontWeight { None, Normal, Bold, Bolder, Lighter };
    public enum FontStyle { None, Normal, Italic, Olique };    // Achtung, Namensüberscheidung mit system.drawing.fontstyle
    public enum TextDecoration { None, Normal, Underline, Overline, LineTrought, Blink };
    public enum LinkTarget { None, Blank, Parent, Self, Top, Frame };
    public enum ParagraphTyp { Paragraph, HeadLine1, HeadLine2, HeadLine3, /*Quellcode*/ };
    public enum BorderStyle { None, Hidden, Dotted, Dashed, Solid, Double, Groove, Ridge, Inset, Outset };
    public enum ValueTyp { Length, Percent };
    public enum FloatTyp { None, Left, Right };

    public class HtmlElement : HtmlObject
    {
        public FontFamily FontFamily = FontFamily.None;
        public int FontSize = 0;
        public FontSizeTyp FontSizeTyp = FontSizeTyp.Length;
        public FontWeight FontWeight = FontWeight.None;
        public FontStyle FontStyle = FontStyle.None;
        public HorizontalAlignment HorizontalAlign = HorizontalAlignment.None;
        public VerticalAlignment VerticalAlign = VerticalAlignment.None;
        public int VerticalAlignmentLength = 0;
        public Color BackgroundColor;
        public TextDecoration TextDecoration = TextDecoration.None;
        public int LineHeight = 0;
        public ValueTyp LineHeightTyp = ValueTyp.Length;
        public Color Color;
        public int Width = 0;
        public ValueTyp WidthTyp = ValueTyp.Length;
        public int Height = 0;
        public ValueTyp HeightTyp = ValueTyp.Length;
        public string Text = "";
        public string ToolTip = "";
        public FloatTyp Float = FloatTyp.None;
        public Sides<int> Padding = new Sides<int>();          //public Padding Padding = Padding.Empty;
        public Sides<int> Margin = new Sides<int>();
        public Sides<int> BorderWidth = new Sides<int>();
        public Sides<BorderStyle> BorderStyle = new Sides<BorderStyle>();
        public Sides<Color> BorderColor = new Sides<Color>();


        protected override Tag getTag()
        {
            Tag tag = base.getTag();
            tag.Parameters.Add("title", HttpUtility.HtmlEncode(ToolTip));  // Tooltip ist Title
            tag.Styles.Add("font-family", GetString(FontFamily));
            tag.Styles.Add("font-size", GetString(FontSize,FontSizeTyp));   
            tag.Styles.Add("font-weight", GetString(FontWeight));
            tag.Styles.Add("font-style", GetString(FontStyle));
            tag.Styles.Add("text-align", GetString(HorizontalAlign));
            tag.Styles.Add("vertical-align", GetString(VerticalAlign, VerticalAlignmentLength));
            tag.Styles.Add("color", GetString(Color));
            tag.Styles.Add("background-color", GetString(BackgroundColor));
            tag.Styles.Add("text-decoration", GetString(TextDecoration));
            tag.Styles.Add("width", GetString(Width, WidthTyp));
            tag.Styles.Add("height", GetString(Height, HeightTyp));
            tag.Styles.Add("line-height", GetString(LineHeight, LineHeightTyp));
            tag.Styles.Add("padding", GetString(Padding));
            tag.Styles.Add("margin", GetString(Margin));
            tag.Styles.Add("border-width", GetString(BorderWidth));
            tag.Styles.Add("border-left-style", GetString(BorderStyle.Left));
            tag.Styles.Add("border-right-style", GetString(BorderStyle.Right));
            tag.Styles.Add("border-top-style", GetString(BorderStyle.Top));
            tag.Styles.Add("border-bottom-style", GetString(BorderStyle.Bottom));
            tag.Styles.Add("border-left-color", GetString(BorderColor.Left));
            tag.Styles.Add("border-right-color", GetString(BorderColor.Right));
            tag.Styles.Add("border-top-color", GetString(BorderColor.Top));
            tag.Styles.Add("border-bottom-color", GetString(BorderColor.Bottom));
            tag.Styles.Add("float", GetString(Float));
            return tag;
        }

        public override string GetHtml()
        {
            string s = "";
            s += HttpUtility.HtmlEncode(Text);
            s += base.GetHtml();
            return s;
        }

        public int Border
        {
            set
            {
                BorderStyle.All = HTML.BorderStyle.Solid;
                BorderWidth.All = value;
            }
            get
            {
                return BorderWidth.All;
            }
        }
    }

    public class HtmlObject
    {
        public List<HtmlObject> Items;


        public HtmlObject()
        {
            Items = new List<HtmlObject>();
        }

        public void Add(HtmlObject element)
        {
            Items.Add(element);
        }

        protected virtual Tag getTag()
        {
            return new Tag();
        }

        public virtual string GetHtml()
        {
            string s = "";
            foreach (HtmlObject element in Items)
            {
                s += element.GetHtml();
            }

            return s;
        }

        protected string GetString(HorizontalAlignment align)
        {
            string p = "";
            switch (align)
            {
                case HorizontalAlignment.Left:
                    p += "left";
                    break;
                case HorizontalAlignment.Right:
                    p += "right";
                    break;
                case HorizontalAlignment.Center:
                    p += "center";
                    break;
                case HorizontalAlignment.Justify:
                    p += "justify";
                    break;
                default:
                    break;
            }
            return p;
        }

        protected string GetString(VerticalAlignment valign, int length)
        {
            string p = "";
            switch (valign)
            {
                case VerticalAlignment.Baseline:
                    p = "baseline";
                    break;
                case VerticalAlignment.Length:
                    p = length.ToString() + "px";
                    break;
                case VerticalAlignment.Percent:
                    p = length.ToString() + "%";
                    break;
                case VerticalAlignment.Sub:
                    p = "sub";
                    break;
                case VerticalAlignment.Super:
                    p = "super";
                    break;
                case VerticalAlignment.Top:
                    p = "top";
                    break;
                case VerticalAlignment.TextTop:
                    p = "text-top";
                    break;
                case VerticalAlignment.Middle:
                    p = "middle";
                    break;
                case VerticalAlignment.Bottom:
                    p = "bottom";
                    break;
                case VerticalAlignment.TextBottom:
                    p = "text-bottom";
                    break;
            }
            return p;
        }

        protected string GetString(TableAlignment align)
        {
            string p = "";
            switch (align)
            {
                case TableAlignment.Left:
                    p = "left";
                    break;
                case TableAlignment.Center:
                    p = "center";
                    break;
                case TableAlignment.Right:
                    p = "right";
                    break;
            }
            return p;
        }

        protected string GetString(TableFrame frame)
        {
            string p = "";
            switch (frame)
            {
                case TableFrame.Void:
                    p = "void";
                    break;
                case TableFrame.Above:
                    p = "above";
                    break;
                case TableFrame.Below:
                    p = "below";
                    break;
                case TableFrame.Hsides:
                    p = "hsides";
                    break;
                case TableFrame.Vsides:
                    p = "vsides";
                    break;
                case TableFrame.Lhs:
                    p = "lhs";
                    break;
                case TableFrame.Rhs:
                    p = "rhs";
                    break;
                case TableFrame.Box:
                    p = "box";
                    break;
                case TableFrame.Border:
                    p = "border";
                    break;
                default:
                    break;
            }
            return p;
        }

        protected string GetString(BorderStyle style)
        {
            string p = "";
            switch (style)
            {
                case BorderStyle.Hidden:
                    p = "hidden";
                    break;
                case BorderStyle.Dotted:
                    p = "dotted";
                    break;
                case BorderStyle.Dashed:
                    p = "dashed";
                    break;
                case BorderStyle.Solid:
                    p = "solid";
                    break;
                case BorderStyle.Double:
                    p = "double";
                    break;
                case BorderStyle.Groove:
                    p = "groove";
                    break;
                case BorderStyle.Ridge:
                    p = "ridge";
                    break;
                case BorderStyle.Inset:
                    p = "inset";
                    break;
                case BorderStyle.Outset:
                    p = "outset";
                    break;
            }
            return p;
        }

        protected string GetString(TableRules rules)
        {
            string p = "";
            switch (rules)
            {
                case TableRules.Groups:
                    p = "groups";
                    break;
                case TableRules.Rows:
                    p = "rows";
                    break;
                case TableRules.Cols:
                    p = "cols";
                    break;
                case TableRules.All:
                    p = "all";
                    break;
                default:
                    break;
            }
            return p;
        }

        protected string GetString(Color color)
        {
            string p = "";
            if (!color.IsEmpty)
            {
                p += "rgb(";
                p += color.R.ToString() + ",";
                p += color.G.ToString() + ",";
                p += color.B.ToString() + ")";
            }
            return p;
        }

        protected string GetString(FontFamily fontfamily)
        {
            string p = "";
            switch (fontfamily)
            {
                case FontFamily.Serif:
                    p = "serif";
                    break;
                case FontFamily.SansSerif:
                    p = "sans-serif";
                    break;
                case FontFamily.Cursive:
                    p = "cursive";
                    break;
                case FontFamily.Fantasy:
                    p = "fantasy";
                    break;
                case FontFamily.Monospace:
                    p = "monospace";
                    break;
                case FontFamily.Arial:
                    p = "Arial";
                    break;
                case FontFamily.Courier:
                    p = "Courier New";
                    break;
                case FontFamily.TimesNewRoman:
                    p = "Times New Roman";
                    break;
                case FontFamily.Verdana:
                    p = "Verdana";
                    break;
                default:
                    break;
            }
            return p;
        }

        protected string GetString(FontWeight weight)
        {
            string p = "";
            switch (weight)
            {
                case FontWeight.Normal:
                    p = "normal";
                    break;
                case FontWeight.Bold:
                    p = "bold";
                    break;
                case FontWeight.Bolder:
                    p = "bolder";
                    break;
                case FontWeight.Lighter:
                    p = "lighter";
                    break;
                default:
                    break;
            }
            return p;
        }

        protected string GetString(TextDecoration decoration)
        {
            string p = "";
            switch (decoration)
            {       
                case TextDecoration.Normal:   // Normal bedeuted: Text OHNE Decoration (text-decoration:none)
                    p = "none";
                    break;
                case TextDecoration.Underline:
                    p = "underline";
                    break;
                case TextDecoration.Overline:
                    p = "overline";
                    break;
                case TextDecoration.LineTrought:
                    p = "line-through";
                    break;
                case TextDecoration.Blink:
                    p = "blink";
                    break;
                default:
                    break;
            }
            return p;
        }

        protected string GetString(int FontSize,FontSizeTyp sizetyp)
        {
            string p = "";
            switch (sizetyp)
            {
                case FontSizeTyp.Length:
                    p = FontSize<=0 ? "" :  GetString(FontSize)+"px";
                    break;
                case FontSizeTyp.Percent:
                    p = FontSize <= 0 ? "" : GetString(FontSize) + "%";
                    break;
                case FontSizeTyp.SmallXX:
                    p = "xx-small";
                    break;
                case FontSizeTyp.SmallX:
                    p = "x-small";
                    break;
                case FontSizeTyp.Small:
                    p = "small";
                    break;
                case FontSizeTyp.Smaller:
                    p = "smaller";
                    break;
                case FontSizeTyp.Larger:
                    p = "larger";
                    break;
                case FontSizeTyp.Medium:
                    p = "medium";
                    break;
                case FontSizeTyp.Large:
                    p = "large";
                    break;
                case FontSizeTyp.LargeX:
                    p = "x-large";
                    break;
                case FontSizeTyp.LargeXX:
                    p = "xx-large";
                    break;
                default:
                    break;
            }
            return p;
        }

        protected string GetString(FloatTyp floattyp)
        {
            string p = "";
            switch (floattyp)
            {
                case FloatTyp.Left:
                    p = "left";
                    break;
                case FloatTyp.Right:
                    p = "right";
                    break;
            }
            return p;
        }

        protected string GetString(FontStyle fontstyle)
        {
            string p = "";
            switch (fontstyle)
            {
                case FontStyle.Normal:
                    p = "normal";
                    break;
                case FontStyle.Italic:
                    p = "italic";
                    break;
                case FontStyle.Olique:
                    p = "oblique";
                    break;
            }
            return p;
        }

        protected string GetString(LinkTarget target, string frameName)
        {
            string p = "";
            switch (target)
            {
                case LinkTarget.Blank:
                    p = "_blank";
                    break;
                case LinkTarget.Parent:
                    p = "_parent";
                    break;
                case LinkTarget.Self:
                    p = "_self";
                    break;
                case LinkTarget.Top:
                    p = "_top";
                    break;
                case LinkTarget.Frame:
                    p = frameName;
                    break;
            }
            return p;
        }

        protected string GetString(int value)
        {
            return (value <= 0) ? "" : value.ToString();
        }

        protected string GetString(int value, ValueTyp typ)
        {
            string p = "";
            if (value > 0)
            {
                p += value.ToString();
                switch (typ)
                {
                    case ValueTyp.Length:
                        p += "px";
                        break;
                    case ValueTyp.Percent:
                        p += "%";
                        break;
                }
            }
            return p;
        }

        protected string GetString(Sides<int> sides)   // für die Eigenschaften padding und margin
        {
            string p = "";
            if ((sides.Left > 0) | (sides.Right > 0) | (sides.Top > 0) | (sides.Bottom > 0))
            {
                p += sides.Top.ToString() + "px ";
                p += sides.Right.ToString() + "px ";
                p += sides.Bottom.ToString() + "px ";
                p += sides.Left.ToString() + "px";
            }
            return p;
        }
    }
}
  
