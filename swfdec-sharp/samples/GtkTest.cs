using System;
using Gtk;
using Swfdec;

namespace SwfdecGtkTest
{
	public class MainWindow : Gtk.Window
	{
		Swfdec.GtkWidget _swf;

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
			
			_swf = new Swfdec.GtkWidget (new Player (null));
			_swf.SetSizeRequest (400, 300);
			vBox.PackStart (_swf);
			
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
			Application.Quit ();
			args.RetVal = true;
		}
		
		void ButtonOpenClicked (object sender, EventArgs args)
		{
			FileChooserDialog dialog = new FileChooserDialog ("Open an Swf", this, FileChooserAction.Open, new object[] { "Cancel", ResponseType.Cancel, "Open", ResponseType.Accept });
			FileFilter filter = new FileFilter ();
			filter.AddPattern ("*.[sS][wW][fF]");
			dialog.Filter = filter;
			dialog.SetCurrentFolder (Environment.GetFolderPath (Environment.SpecialFolder.Personal));
			
			if (dialog.Run () == (int)ResponseType.Accept)
			{
				URL url = URL.NewFromInput (dialog.Filename);
				
				GtkPlayer player = new GtkPlayer (null);
				player.Url = url;
				
				_swf.Player = player;
				
				player.Playing = true;
			}
			
			dialog.Destroy ();
		}
	}
}

