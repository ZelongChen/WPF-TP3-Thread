// V1.1
namespace ClassLibrary1
{
	using System;
	using System.Threading;


	public class Premier
	{
		// pour précisser le numéro de thread ?l'affichage
		private int numeroThread ;

		 // indique le numéro de thread lanc?
		public Premier(int k)
		{
			numeroThread = k ;
		}

		public  void NombrePremiers()
		{
			for(int p=1;p<1000000; p++)
			{ 
				int i=2;
				while((p%i)!=0 && i<p)
				{
					i++;
				}
				if(i==p)
					Console.WriteLine( "thread(" + this.numeroThread +") = " + p.ToString());
					Thread.Sleep(50);
				
			}
		}
	}
}

