using ClassLibrary1;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Threads
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Thread> premiers { get; private set; }
        public ObservableCollection<Thread> ballons { get; private set; }
        public ObservableCollection<Thread> tp1Windows { get; private set; }
        public ObservableCollection<Thread> allThreads { get; private set; }

        private bool isPaused = false;

        public MainWindow()
        {
            InitializeComponent();

            Application.Current.Exit += Current_Exit;

            premiers = new ObservableCollection<Thread>();
            ballons = new ObservableCollection<Thread>();
            allThreads = new ObservableCollection<Thread>();
            tp1Windows = new ObservableCollection<Thread>();

            DataContext = this;
        }

        private void StartBallon_Click(object sender, RoutedEventArgs e)
        {
            Work work = new Work();
            work.ThreadAbort += HandleBallonThreadAbort;
            Thread thread = new Thread(work.StartBallon);
            work.PID = thread.ManagedThreadId;
            ballons.Add(thread);
            allThreads.Add(thread);
            thread.Name = "ballon";
            thread.Start();
        }

        private void StartPremier_Click(object sender, RoutedEventArgs e)
        {
            Work work = new Work();
            Thread thread = new Thread(work.StartPremier);
            work.PID = thread.ManagedThreadId;
            premiers.Add(thread);
            allThreads.Add(thread);
            thread.Name = "premier";
            thread.Start();
        }

        private void StartCircle_Click(object sender, RoutedEventArgs e)
        {
            Work work = new Work();
            work.ThreadAbort += HandleTP1WindowThreadAbort;
            Thread thread = new Thread(work.StartTP1Circle);
            work.PID = thread.ManagedThreadId;
            thread.SetApartmentState(ApartmentState.STA);
            tp1Windows.Add(thread);
            allThreads.Add(thread);
            thread.Name = "TP1 window circle";
            thread.Start();
        }

        private void StartRectangle_Click(object sender, RoutedEventArgs e)
        {
            Work work = new Work();
            work.ThreadAbort += HandleTP1WindowThreadAbort;
            Thread thread = new Thread(work.StartTP1Rectangle);
            work.PID = thread.ManagedThreadId;
            thread.SetApartmentState(ApartmentState.STA);
            tp1Windows.Add(thread);
            allThreads.Add(thread);
            thread.Name = "TP1 window rectangle";
            thread.Start();
        }

        private void StartImage_Click(object sender, RoutedEventArgs e)
        {
            Work work = new Work();
            work.ThreadAbort += HandleTP1WindowThreadAbort;
            Thread thread = new Thread(work.StartTP1Image);
            work.PID = thread.ManagedThreadId;
            thread.SetApartmentState(ApartmentState.STA);
            tp1Windows.Add(thread);
            allThreads.Add(thread);
            thread.Name = "TP1 window image";
            thread.Start();
        }

        private void RemoveLastOne_Click(object sender, RoutedEventArgs e)
        {
            if (allThreads.Count == 0)
            {
                MessageBox.Show("There is no ballon or premier or tp1 window thread running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Thread thread = allThreads[allThreads.Count - 1];
                if (ballons.Contains(thread))
                {
                    removeLastBallon();
                } 
                else if (premiers.Contains(thread))
                {
                    removeLastPremier();
                }
                else
                {
                    removeLastTP1Window();
                }
            }

        }

        private void RemoveLastBallon_Click(object sender, RoutedEventArgs e)
        {
            if (ballons.Count > 0)
            {
                removeLastBallon();
            }
            else
            {
                MessageBox.Show("There is no ballon thread running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveLastPremier_Click(object sender, RoutedEventArgs e)
        {
            if (premiers.Count > 0)
            {
                removeLastPremier();
            }
            else
            {
                MessageBox.Show("There is no premier thread running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void RemoveLastTP1Window_Click(object sender, RoutedEventArgs e)
        {
            if (tp1Windows.Count > 0)
            {
                removeLastTP1Window();
            }
            else
            {
                MessageBox.Show("There is no tp1 window thread running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveAll_Click(object sender, RoutedEventArgs e)
        {
            if (ballons.Count > 0 || premiers.Count > 0 || tp1Windows.Count > 0)
            {
                removeAll();
            }
            else
            {
                MessageBox.Show("There is no ballon or premier or tp1 window thread running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (!isPaused)
            {
                for (int i = 0; i < allThreads.Count; i++)
                {
                    allThreads[i].Suspend();
                }

                isPaused = true;
            }

        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            if (isPaused)
            {
                for (int i = 0; i < allThreads.Count; i++)
                {
                    allThreads[i].Resume();
                }

                isPaused = false;
            }

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            if (removeAll())
            {
                Application.Current.Shutdown();
                Environment.Exit(0);
            }  
        }


        private void removeLastBallon()
        {
            if (!isPaused)
            {
                Thread thread = ballons[ballons.Count - 1];
                allThreads.Remove(thread);
                ballons.Remove(thread);
                thread.Abort();
            }
            else
            {
                MessageBox.Show("Cannot abort a thread suspended.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void removeLastPremier()
        {
            if (!isPaused)
            {
                Thread thread = premiers[premiers.Count - 1];
                allThreads.Remove(thread);
                premiers.Remove(thread);
                thread.Abort();
            }
            else
            {
                MessageBox.Show("Cannot abort a thread suspended.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void removeLastTP1Window()
        {
            if (!isPaused)
            {
                Thread thread = tp1Windows[tp1Windows.Count - 1];
                allThreads.Remove(thread);
                tp1Windows.Remove(thread);
                thread.Abort();
            }
            else
            {
                MessageBox.Show("Cannot abort a thread suspended.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool removeAll()
        {
            if (!isPaused)
            {
                for (int i = 0; i < allThreads.Count; i++)
                {
                    allThreads[i].Abort();
                }
                allThreads.Clear();
                ballons.Clear();
                premiers.Clear();
                tp1Windows.Clear();

                return true;
            }
            else
            {
                MessageBox.Show("Cannot abort threads suspended.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            if (removeAll())
            {
                Application.Current.Shutdown();
                Environment.Exit(0);
            }

        }

        void HandleBallonThreadAbort(object sender, EventArgs e)
        {
            int id = (sender as Work).PID;
            var thread = ballons.FirstOrDefault(x => x.ManagedThreadId == id);
            if (thread != null)
            {
                Application.Current.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    (Action)delegate ()
                    {
                        ballons.Remove(thread);
                        allThreads.Remove(thread);
                    });
                thread.Abort();
            }
        }

        void HandleTP1WindowThreadAbort(object sender, EventArgs e)
        {
            int id = (sender as Work).PID;
            var thread = tp1Windows.FirstOrDefault(x => x.ManagedThreadId == id);
            if (thread != null)
            {
                Application.Current.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    (Action)delegate ()
                    {
                        tp1Windows.Remove(thread);
                        allThreads.Remove(thread);
                    });
                thread.Abort();
            }
        }

    }

    class Work
    {
        public event EventHandler ThreadAbort;
        public int PID;

        public void StartBallon()
        {

            Ballon ballon = new Ballon();
            ballon.Go();

            if (ThreadAbort != null)
                ThreadAbort(this, EventArgs.Empty);

        }

        public void StartPremier()
        {

            Premier premier = new Premier(PID);
            premier.NombrePremiers();

        }

        public void StartTP1Image()
        {

            WPF_TP1.MainWindow main = new WPF_TP1.MainWindow("image");
            main.ShowDialog();

            if (ThreadAbort != null)
                ThreadAbort(this, EventArgs.Empty);
        }

        public void StartTP1Circle()
        {
            WPF_TP1.MainWindow main = new WPF_TP1.MainWindow("circle");
            main.ShowDialog();

            if (ThreadAbort != null)
                ThreadAbort(this, EventArgs.Empty);
        }

        public void StartTP1Rectangle()
        {
            WPF_TP1.MainWindow main = new WPF_TP1.MainWindow("rectangle");
            main.ShowDialog();

            if (ThreadAbort != null)
                ThreadAbort(this, EventArgs.Empty);
        }
    }

}
