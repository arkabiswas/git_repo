namespace WebControlsTest.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for TrackingHeader.
	/// </summary>
	public abstract class TrackingHeader : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblWelcome;
		protected System.Web.UI.WebControls.Label lblInfoBar;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//string strInfoBarLabel;

			if(Session["full_name"] != null)
			{
				lblWelcome.Text = "Welcome " + Session["full_name"].ToString() + " today is " + DateTime.Now.ToLongDateString();
			} 
			else
			{
				lblWelcome.Text = "Welcome today is " + DateTime.Now.ToLongDateString();
			}

			// if ( Session["access_level"] == TAccessLevel.ManageLevel ) 
			if(Session["access_level"] != null)
			{
				// create the hyperlink for the new if they have the right access level. { enumerated }
			}
			
	}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
