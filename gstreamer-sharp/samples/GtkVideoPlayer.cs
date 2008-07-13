using System;
using System.Runtime.InteropServices;

using Gtk;
using Gst;

public class MainWindow : Gtk.Window
{
	DrawingArea _da;
	Pipeline _pipeline;

	public static void Main (string[] args)
	{
		Gtk.Application.Init ();
		Gst.Application.Init ();
		MainWindow window = new MainWindow ();
		window.ShowAll ();
		Gtk.Application.Run ();
	}
		
	public MainWindow ()
		: base (WindowType.Toplevel)
	{
		VBox vBox = new VBox ();
			
		_da = new DrawingArea ();
		_da.SetSizeRequest (400, 300);
		vBox.PackStart (_da);
			
		HButtonBox btnBox = new HButtonBox ();
		
		Button btnOpen = new Button ();
		btnOpen.Label = "Open";
		btnOpen.Clicked += ButtonOpenClicked;
		
		btnBox.Add (btnOpen);
		
		vBox.PackStart (btnBox, false, false, 3);
		
		Add (vBox);
	
		WindowPosition = Gtk.WindowPosition.Center;
		DeleteEvent += OnDeleteEvent;
	}
	
	void OnDeleteEvent (object sender, DeleteEventArgs args)
	{
		Gtk.Application.Quit ();
		args.RetVal = true;
	}
	
	void ButtonOpenClicked (object sender, EventArgs args)
	{
		FileChooserDialog dialog = new FileChooserDialog ("Open", this, FileChooserAction.Open, new object[] { "Cancel", ResponseType.Cancel, "Open", ResponseType.Accept });
		dialog.SetCurrentFolder (Environment.GetFolderPath (Environment.SpecialFolder.Personal));
		
		if (dialog.Run () == (int)ResponseType.Accept)
		{
			if (_pipeline != null)
			{
				_pipeline.SetState (Gst.State.Null);
				_pipeline.Dispose ();
			}

			_pipeline = new Pipeline (string.Empty);

			Element playbin = ElementFactory.Make ("playbin", "playbin");
			XvImageSink sink = XvImageSink.Make ("sink");

			if (_pipeline == null)
				Console.WriteLine ("Unable to create pipeline");
			if (playbin == null)
				Console.WriteLine ("Unable to create element 'playbin'");
			if (sink == null)
				Console.WriteLine ("Unable to create element 'sink'");

			_pipeline.Add (playbin);
			
			XOverlayAdapter sinkadapter = new XOverlayAdapter (sink);
			sinkadapter.XwindowId = gdk_x11_drawable_get_xid (_da.GdkWindow.Handle);
			
			playbin.SetProperty ("video-sink", sink);
			playbin.SetProperty ("uri", "file://" + dialog.Filename);

			StateChangeReturn sret = _pipeline.SetState (Gst.State.Playing);
			
			if (sret == StateChangeReturn.Async)
			{
				State state, pending;

				if (StateChangeReturn.Success != _pipeline.GetState (out state, out pending, Clock.Second * 5))
					Console.WriteLine ("State change failed for {0}\n", dialog.Filename);
			}
			else if (sret != StateChangeReturn.Success)
				Console.WriteLine ("State change failed for {0} ({1})\n", dialog.Filename, sret);
		}
		
		dialog.Destroy ();
	}
	
	[DllImport ("libgdk-x11-2.0")]
	static extern uint gdk_x11_drawable_get_xid (IntPtr handle);
}

