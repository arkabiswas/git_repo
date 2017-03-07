using System;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
 
namespace PPC.FDA.Controls
{
	[
	ToolboxData("<{0}:NumberBox runat=server></{0}:NumberBox>"),
	DefaultProperty("DecimalPlaces")
	]
	public class NumberBox : TextBox
	{

		private int mDecimalPlaces = 0;
		private char mDecimalSymbol = '.';
		private bool mAllowNegatives = true;

		/// <summary>
		/// Gets or sets the number of decimals for the number box.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(0),
		Description("Indicates the number of decimal places to display.")
		]
		public virtual int DecimalPlaces
		{
			get { return mDecimalPlaces; }
			set { mDecimalPlaces = value; }
		}

		/// <summary>
		/// Gets or sets the digit grouping symbol for the number box.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue("."),
		Description("The digit grouping symbol.")
		]
		public virtual char DecimalSymbol
		{
			get { return mDecimalSymbol; }
			set { mDecimalSymbol = value; }
		}

		/// <summary>
		/// Gets or sets wheter negative numbers are allowed in the number box.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(true),
		Description("True when negative values are allowed")
		]
		public virtual bool AllowNegatives
		{
			get { return mAllowNegatives; }
			set { mAllowNegatives = value; }
		}

		/// <summary>
		/// Gets or sets the value of the number box.
		/// </summary>
		public virtual double Value
		{
			get
			{
				try 
				{
					return ParseStringToDouble(this.Text);
				} 
				catch (FormatException e) 
				{
					throw new 
						InvalidOperationException("NumberBox does nog contain a valid Number.");
				} 
				catch (Exception e) 
				{
					throw e;
				}
				
			}
			set
			{
				if ((value < 0) & !AllowNegatives) 
					throw new ArgumentOutOfRangeException("Only positive values are allowed for this NumberBox");
					
					//base.Text = value.ToString(this.Format);
					base.Text = value.ToString(GetFormat()).Replace(".", DecimalSymbol.ToString());
			}
		}

		/// <summary>
		/// Gets or sets the text content of the number box.
		/// </summary>
		override public string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				try 
				{
					this.Value = ParseStringToDouble(value);
				} 
				catch (FormatException e) 
				{
					base.Text = value;
				} 
				catch (Exception e) 
				{
					throw e;
				}
			}
		}
			
			
		/// <summary>
		///	Add a JavaScript to the Page and call it from the onKeyPress event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnPreRender(EventArgs e) 
		{

			if (this.Page.Request.Browser.JavaScript == true) 
			{
				// Build JavaScript		
				StringBuilder s = new StringBuilder();
				s.Append("\n<script type='text/javascript' language='JavaScript'>\n");
				s.Append("<!--\n");
				s.Append("    function NumberBoxKeyPress(event, dp, dc, n) {\n");
				s.Append("	     var myString = new String(event.srcElement.value);\n");
				s.Append("	     var pntPos = myString.indexOf(String.fromCharCode(dc));\n");
				s.Append("	     var keyChar = window.event.keyCode;\n");
				s.Append("       if ((keyChar < 48) || (keyChar > 57)) {\n");
				s.Append("          if (keyChar == dc) {\n");
				s.Append("              if ((pntPos != -1) || (dp < 1)) {\n");
				s.Append("                  return false;\n");
				s.Append("              }\n");
				s.Append("          } else \n");			
				s.Append("if (((keyChar == 45) && (!n || myString.length != 0)) || (keyChar != 45)) \n");
				s.Append("		     return false;\n");
				s.Append("       }\n");
				s.Append("       return true;\n");
				s.Append("    }\n");
				s.Append("// -->\n");
				s.Append("</script>\n");
				
				// Add the Script to the Page
				this.Page.RegisterClientScriptBlock("NumberBoxKeyPress", s.ToString());

				// Add KeyPress Event
				try 
				{
					this.Attributes.Remove("onKeyPress");
				} 
				finally 
				{
					this.Attributes.Add("onKeyPress", "return NumberBoxKeyPress(event, " 
						+ DecimalPlaces.ToString() + ", " 
						+ ((int)DecimalSymbol).ToString() + ", " 
						+ AllowNegatives.ToString().ToLower() + ")");
				}
			}
		}
		
		/// <summary>
		/// Returns the RegularExpression string which can be used for validating 
		/// using a RegularExpressionValidator.
		/// </summary>
		virtual public string ValidationRegularExpression
		{
			get
			{
				StringBuilder regexp = new StringBuilder();
				
				if (AllowNegatives)
					regexp.Append("([-]|[0-9])");
				
				regexp.Append("[0-9]*");
				
				if (DecimalPlaces > 0) 
				{
					regexp.Append("([");
					regexp.Append(DecimalSymbol);
					regexp.Append("]|[0-9]){0,1}[0-9]{0,");
					regexp.Append(DecimalPlaces.ToString());
					regexp.Append("}$");
				}
				
				return regexp.ToString();
			}
		}

		/// <summary>
		/// Parse a String to a Double
		/// </summary>
		/// <param name="s">string to be parsed to a double</param>
		/// <returns>double value</returns>
		virtual protected double ParseStringToDouble(string s)
		{
			s = s.Replace(DecimalSymbol.ToString(), ".");
			return double.Parse(s);
		}		
		
		/// <summary>
		/// Returns the FormatString used to display the value in the number box
		/// </summary>
		/// <returns>Format string</returns>
		virtual protected string GetFormat()
		{
			StringBuilder f = new StringBuilder();
			f.Append("0");
			if (DecimalPlaces > 0) 
			{
				f.Append(".");
				f.Append('0', DecimalPlaces);
			}
			
			return f.ToString();
		}
		
	}
}
