// V.1.1
using System;
   // L'espace de nom Systeme  contient les classes de bases du framework .NET: type de donnée,exception.... 
using System.Drawing;
  // Espace de nom reserv?aux fonctions de base de GDI+: Bitmap, Line..
using System.Collections;
 //  Fournit les classes de collections:les listes ,les files d'attente ,les dictionnaires..
using System.ComponentModel;
 // Fournit les classes de base des composants et controles. Fournit la convertion de type,
 // la gestion de licence  
using System.Windows.Forms;
 //Gestion des formulaires et des controles windows
using System.Data;
 // Gestion dees classes de base d'ADO.NET

namespace ClassLibrary1
{
	//Classe Form1  herite de la classe System.Windows.Forms.Form
	public class Ballon : System.Windows.Forms.Form 
	{
		//déclaration de la variable rayon
		public int rayon;
		//création des objets x1, x2 de la classe System.Windows.Forms.Timer
		//Pour la gestion des évenements :  intervalle de temps en milliseconde
		public Timer x1;
		public Timer x2;

		//création d'un objet de la classe abstraite Brush responsable 
		//du remlissage des formes (rectangle,ellipse...) 
		public  Brush couleur;
		
		// code écrit automatiquement par VS au moment de la création des éléments de controles: 
		// MainMenu-menuItem (gonfler dégonfler...)
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem7;
		
		private System.ComponentModel.Container components = null;

		public Ballon()
		{
			// Required for Windows Form Designer support 
			
			InitializeComponent();
			rayon=0;
			//instanciation de la classe Timer
			x1=new Timer();
			//methode qui manipule l' evenement augmenter rayon. Se produit quand l'intervalle de temps s'est écoul?
			x1.Tick +=new EventHandler(AugmenterRayon);
			x1.Interval=30;//définit le temps entre deux evenements
			x2= new Timer();
			// manipuler l' evenement d'abaissement du rayon ?chaque interval de temps
			x2.Tick +=new EventHandler(BaisserRayon); 
			x2.Interval=30;
			//instanciation de la classe Brush ?l'intermidiaire de la classe Brushes pour toutes les couleurs bleues
			couleur=Brushes.Blue;
		}
		//
		// Clean up any resources being used. 
		// 
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem7});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3});
			this.menuItem1.Text = "Action";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Gonfler";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Degonfler";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 1;
			this.menuItem7.Text = "";
			// 
			// Ballon
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(350, 338);
			this.Location = new System.Drawing.Point(200, 50);
			this.Menu = this.mainMenu1;
			this.Name = "Ballon";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = " Ballons";

		}
		#endregion

		/// The main entry point for the application.

		//commencement de genération des evenements pour dessiner
		protected override void OnPaint(PaintEventArgs e)//methode qui donne la main pour dessiner  
		{
			Graphics g=e.Graphics;
			//changer la couleur du font de la fenêtre en blanc
			g.FillRectangle(Brushes.White,ClientRectangle);
		}
		//génération d'évenement gonflement du ballon si on click sur l'element gonfler du menu action.
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			Graphics g=this.CreateGraphics();//objet de controle du graphe
			g.FillRectangle(Brushes.White,ClientRectangle);
			x1.Start();//debut d'evenement : gonfler (augmenter rayon)
			x2.Stop();// arret d'evenement : degonfler (baisser rayon)
		}
		

		public void AugmenterRayon(object sender,System.EventArgs e)
		{
			Graphics g=this.CreateGraphics();
			if(rayon<ClientRectangle.Width/2)//si le rayon < au rayon de la fenêtre
			{
				g.FillRectangle(Brushes.White,ClientRectangle);//colore la fenêtre en blanc

				// fonction qui dessine un disque plein ,elle a 5 parametres : la couleur coordonnée du centre
				// et les coord du rectangle ?l'interieur duquel il y aurait le disque
				g.FillEllipse(couleur,ClientRectangle.Width/2-rayon,ClientRectangle.Width/2-rayon,2*rayon,2*rayon);

				//incrementation du rayon
				rayon++;

			}
			else  //si le rayon arrive au bord de la fenêtre client			
			{
				x2.Start();//debut événememt baisser-rayon (degonfler)
				x1.Stop();//arreter le gonflement	
		
			}
		}
		public void BaisserRayon(object sender,System.EventArgs e)
		{
			Graphics g=this.CreateGraphics();
			g.FillRectangle(Brushes.White,ClientRectangle);
			if(rayon>0)
			{
				g.FillRectangle(Brushes.White,ClientRectangle);
				g.FillEllipse(couleur,ClientRectangle.Width/2-rayon,ClientRectangle.Width/2-rayon,2*rayon,2*rayon);
				rayon--; //decrementation du rayon
			}
			else //si le rayon est nul
			{
				x1.Start(); //gonflement du ballon
				x2.Stop(); //degonflement du ballon	
		
			}
		}

		//génération de l'évenement dégonflement du ballon si on click sur l'element dégonfler du menu action.
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			Graphics g=this.CreateGraphics();
			g.FillRectangle(Brushes.White,ClientRectangle);
			
			x2.Start();
			x1.Stop();
		}
	
		public void Go()
		{
			Ballon b = new Ballon();
			b.menuItem2_Click(this, null);  // pour lancer le gonflement du ballon au départ
			Application.Run(b); //commencement de l' application
		} 
	}
}

