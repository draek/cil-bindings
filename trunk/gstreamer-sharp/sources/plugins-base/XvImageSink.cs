using System;
using System.Runtime.InteropServices;

namespace Gst
{
	public class XvImageSink : Element, XOverlayImplementor
	{
		public XvImageSink (IntPtr raw)
			: base (raw) 
		{
		}
		
		public static XvImageSink Make (string name)
		{
			return ElementFactory.Make ("xvimagesink", name) as XvImageSink;
		}
	}
}

