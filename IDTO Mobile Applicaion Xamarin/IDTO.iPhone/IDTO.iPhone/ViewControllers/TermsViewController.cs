// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace IDTO.iPhone
{
	public delegate void TermsEventHandler(bool isAccepted);
	public partial class TermsViewController : UIViewController
	{
		public event TermsEventHandler TermsDismissedEvent;

		public TermsViewController (IntPtr handle) : base (handle)
		{

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			string url = "http://idtotravelerportal.azurewebsites.net/Content/Terms/Terms.html";
			webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));

		}

		partial void acceptTerms (MonoTouch.Foundation.NSObject sender)
		{
			if(TermsDismissedEvent!=null)
				TermsDismissedEvent(true);
		}
			
		partial void cancelTerms (MonoTouch.Foundation.NSObject sender)
		{
			if(TermsDismissedEvent!=null)
				TermsDismissedEvent(false);
		}
	}
}