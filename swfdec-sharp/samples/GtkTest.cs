using System;
using Gtk;
using Swfdec;

namespace SwfdecGtkTest
{
	public class MainWindow : Gtk.Window
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow window = new MainWindow ();
			window.ShowAll ();
			Application.Run ();
		}
		
		public MainWindow ()
			: base (WindowType.Toplevel)
		{
			VBox vBox = new VBox ();
			
			Swfdec.GtkWidget swf = new Swfdec.GtkWidget (new Player (null));
			vBox.PackStart (swf);
			
			HButtonBox btnBox = new HButtonBox ();
			
			Button btnOpen = new Button ();
			btnOpen.Label = "Open";
			
			btnBox.Add (btnOpen);
			
			vBox.PackStart (btnBox, false, false, 3);
			
			Add (vBox);
		}
	}
}

