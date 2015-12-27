using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_TP1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Storyboard sbRectangleRotateTransform;
        Storyboard sbCircleRotateTransform;
        Storyboard sbImageRotateTransform;
        Storyboard sbRectangleScaleTransform;
        Storyboard sbCircleScaleTransform;
        Storyboard sbImageScaleTransform;
        Storyboard sbCircleMoveTransform;
        Storyboard sbRectangleMoveTransform;
        Storyboard sbImageMoveTransform;

        PathFigure pathFigure = new PathFigure();
        PathFigureCollection pathFigureCollection;

        public MainWindow(string startType)
        {
            InitializeComponent();

            sbRectangleRotateTransform = (Storyboard)RectangleItem.FindResource("RectangleRotateTransformStoryBoard");
            sbCircleRotateTransform = (Storyboard)CircleItem.FindResource("CircleRotateTransformStoryBoard");
            sbImageRotateTransform = (Storyboard)ImageItem.FindResource("ImageRotateTransformStoryBoard");
            sbRectangleScaleTransform = (Storyboard)RectangleItem.FindResource("RectangleScaleTransformStoryboard");
            sbCircleScaleTransform = (Storyboard)CircleItem.FindResource("CircleScaleTransformStoryboard");
            sbImageScaleTransform = (Storyboard)ImageItem.FindResource("ImageScaleTransformStoryboard");
            sbCircleMoveTransform = (Storyboard)CircleItem.FindResource("CircleMoveTransformStoryBoard");
            sbRectangleMoveTransform = (Storyboard)RectangleItem.FindResource("RectangleMoveTransformStoryBoard");
            sbImageMoveTransform = (Storyboard)ImageItem.FindResource("ImageMoveTransformStoryBoard");

            if (startType == "circle")
            {
                this.CircleItem.Visibility = Visibility.Visible;
                sbCircleRotateTransform.Begin();
                sbCircleScaleTransform.Begin();
            }
            else if (startType == "rectangle")
            {
                this.RectangleItem.Visibility = Visibility.Visible;
                sbRectangleRotateTransform.Begin();
                sbRectangleScaleTransform.Begin();
            } 
            else
            {
                this.ImageItem.Visibility = Visibility.Visible;
                sbImageRotateTransform.Begin();
                sbImageScaleTransform.Begin();
            }

            pathFigureCollection = (PathFigureCollection)FindResource("PathFigure");
            pathFigureCollection.Add(pathFigure);

        }

        private void StartDrawMenu_Click(object sender, RoutedEventArgs e)
        {
            string message = "1. Click your mouse and use it to draw a path on the panel@2. Click menu Draw->StartMove to start the movement@3. Click menu Draw->StopMove to stop the movement";
            message = message.Replace("@", System.Environment.NewLine);
            MessageBoxResult result = MessageBox.Show(message, "Guide", MessageBoxButton.YesNo, MessageBoxImage.Information);
            this.DrawCanvas.Visibility = Visibility.Visible;
            this.DrawIndication.Visibility = Visibility.Visible;
        }

        private void StopDrawMenu_Click(object sender, RoutedEventArgs e)
        {
            pathFigure.Segments.Clear();

            this.DrawCanvas.Visibility = Visibility.Collapsed;
            this.DrawIndication.Visibility = Visibility.Collapsed;

            this.CircleItem.HorizontalAlignment = HorizontalAlignment.Center;
            this.CircleItem.VerticalAlignment = VerticalAlignment.Center;
            this.RectangleItem.HorizontalAlignment = HorizontalAlignment.Center;
            this.RectangleItem.VerticalAlignment = VerticalAlignment.Center;
            this.ImageItem.HorizontalAlignment = HorizontalAlignment.Center;
            this.ImageItem.VerticalAlignment = VerticalAlignment.Center;
            sbCircleMoveTransform.Stop();
            sbImageMoveTransform.Stop();
            sbRectangleMoveTransform.Stop();
        }

        private void DrawCanvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            pathFigure.Segments.Clear();
            if (e.ButtonState == MouseButtonState.Pressed)
                pathFigure.StartPoint = e.GetPosition(this);
        }

        private void DrawCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                LineSegment segment = new LineSegment();
                segment.Point = e.GetPosition(this);
                pathFigure.Segments.Add(segment);
            }

        }

        private void StartMoveMenu_Click(object sender, RoutedEventArgs e)
        {
            this.CircleItem.HorizontalAlignment = HorizontalAlignment.Left;
            this.CircleItem.VerticalAlignment = VerticalAlignment.Top;
            this.RectangleItem.HorizontalAlignment = HorizontalAlignment.Left;
            this.RectangleItem.VerticalAlignment = VerticalAlignment.Top;
            this.ImageItem.HorizontalAlignment = HorizontalAlignment.Left;
            this.ImageItem.VerticalAlignment = VerticalAlignment.Top;
            sbCircleMoveTransform.Begin();
            sbRectangleMoveTransform.Begin();
            sbImageMoveTransform.Begin();
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This application is created with passion by Zelong CHEN.", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
