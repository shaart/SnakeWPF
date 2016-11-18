using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SnakeWPF.Code;

namespace SnakeWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Time
        System.Windows.Threading.DispatcherTimer gameTimer;
        TimeSpan time, timerInterval = new TimeSpan(330000);    // 300 000 = 00:00:00.3
        bool paused = false;

        // Snake
        SnakeWPF.Code.DrawableSnake snake;
        Image snakeHead, snakeTail;
        List<Image> snakeBody;

        //PointCollection snakePoints = new PointCollection();
        //List<Polyline> snakeSegments = new List<Polyline>();
        //List<Image> snake = new List<Image>();
        //Color snakeColor = Colors.White;

        // Snake moving
        int distance = 16;   // images height&width
        //Code.SnakeDirection curSnakeDirection;

        public MainWindow()
        {
            InitializeComponent();
            // Язык
            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;
            // Заполняем меню смены языка:
            languageList.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                languageList.Items.Add(menuLang);
            }
            // Игровые инициализации
            time = new TimeSpan(0, 0, 0);

            // temp
            Menu_Play_Click(new object(), new RoutedEventArgs());
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;
            // Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in languageList.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Menu_Play_Click(object sender, RoutedEventArgs e)
        {
            GameGrid.Visibility = Visibility.Visible;
            MenuGrid.Visibility = Visibility.Collapsed;
            //snake = new Snake()
            var tempBody = new List<Code.DirPoint>();
            tempBody.Add(new Code.DirPoint(GameField.ActualWidth / 2 - distance, GameField.ActualHeight / 2));
            for (int i = 2; i < 15; i++)
            {
                tempBody.Add(new Code.DirPoint(GameField.ActualWidth / 2 - i * distance, GameField.ActualHeight / 2));
            }
            snake = new DrawableSnake(
                new Code.DirPoint(GameField.ActualWidth / 2, GameField.ActualHeight / 2),                  // Head
                new Code.DirPoint(GameField.ActualWidth / 2 - 16 * distance, GameField.ActualHeight / 2),   // Tail
                tempBody,                                                                               // Body
                new Uri("Images\\snake_spritemap.gif", UriKind.Relative));                              // Sprite map     

            gameTimer = new System.Windows.Threading.DispatcherTimer();
            gameTimer.Tick += new EventHandler(FrameTick);
            gameTimer.Interval = timerInterval;
            gameTimer.Start();

            // === DEAD CODE ===================================================================
            /*
            //snakePoints.Clear();
            // Default:
            /*Image dumpImg = new Image();
            BitmapImage dumpBitmapImg = new BitmapImage();
            dumpBitmapImg.BeginInit();
            dumpBitmapImg.UriSource = new Uri("Images\\snake_head.png", UriKind.Relative);
            dumpBitmapImg.EndInit();
            dumpImg.Source = dumpBitmapImg;
            snakePoints.Add(new DirPoint(GameField.ActualWidth / 2, GameField.ActualHeight / 2));
            Canvas.SetLeft(dumpImg, snakePoints[0].X);
            Canvas.SetTop(dumpImg, snakePoints[0].Y);
            snake.Add(dumpImg);
            */
            //curSnakeDirection = SnakeDirection.RIGHT;
            //snake.Add(new BitmapImage(new Uri("Images\\snake_head.png", UriKind.Relative)));
            //snakePoints.Add(new DirPoint(GameField.ActualWidth / 2 - distance, GameField.ActualHeight / 2));
            //snake.Add(new BitmapImage(new Uri("Images\\snake_body.png", UriKind.Relative)));
            //snakePoints.Add(new DirPoint(GameField.ActualWidth / 2 - 2 * distance, GameField.ActualHeight / 2));
            //snake.Add(new BitmapImage(new Uri("Images\\snake_tail.png", UriKind.Relative)));
            /*
            for (int i = 0; i < snake.BodyLength; i++)
            {
                //GameField.Children.Add(snake[i]);
            }
            //snake.DataContext = snakePoints;
            
            //Rectangle snakeSegm = new Rectangle();
            //snakeSegm.Height = snakeSegm.Width = window.Height / 20;
            //snakeSegm.SetValue(Canvas.LeftProperty, (GameField.Width / 2));
            //snakeSegm.SetValue(Canvas.TopProperty, (GameField.Height / 2));
            //GameField.Children.Add(snakeSegm);
            */
            // === END OF DEAD CODE ===================================================================
        }

        private void UpdateField()
        {
            //for (int i = 1; i < snakePoints.Count - 1; i++)
            //{
            //}
            GameField.UpdateLayout();
        }

        private void FrameTick(object sender, EventArgs e)
        {
            time = time.Add(timerInterval);
            TimeValue.Header = time.ToString();

            // Canvas

            // Game logic

            // === DEAD CODE ===================================================================
            /*
            //DirPoint newCoord = snakePoints[0];    // Head
            switch (snake.Direction) // curSnakeDirection)
            {
                case SnakeDirection.UP:
                    newCoord.Y = newCoord.Y - distance;
                    if (newCoord.Y <= 0)
                    {
                        newCoord.Y = GameField.ActualHeight;
                    }
                    break;
                case SnakeDirection.RIGHT:
                    newCoord.X = newCoord.X + distance;
                    if (newCoord.X >= GameField.ActualWidth)
                    {
                        newCoord.X = 0;
                    }
                    break;
                case SnakeDirection.DOWN:
                    newCoord.Y = newCoord.Y + distance;
                    if (newCoord.Y >= GameField.ActualHeight)
                    {
                        newCoord.Y = 0;
                    }
                    break;
                case SnakeDirection.LEFT:
                    newCoord.X = newCoord.X - distance;
                    if (newCoord.X <= 0)
                    {
                        newCoord.X = GameField.ActualWidth;
                    }
                    break;
                default:
                    break;
            }
            snakePoints.Insert(0, newCoord);                // Insert first (new snake head)
            snakePoints.RemoveAt(snakePoints.Count - 1);    // Delete last
            */
            // === END OF DEAD CODE ===================================================================

            snake.Move(distance, new Code.Size(GameField.ActualHeight, GameField.ActualWidth));

            debug_snakePointsNum.Content = snake.BodyLength + 2; //snakePoints.Count.ToString();
            debug_snakeX.Content = snake.Head.X; //newCoord.X.ToString();
            debug_snakeY.Content = snake.Head.Y; //newCoord.Y.ToString();

            RedrawObjects();

            // Game over
            if (time == new TimeSpan(0, 1, 0))
            {
                gameTimer.Stop();
                time = new TimeSpan(0, 0, 0);
                MenuGrid.Visibility = Visibility.Visible;
                GameGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void RedrawObjects()
        {
            // Clear and redraw objects
            GameField.Children.Clear();
            RotateTransform rotate = new RotateTransform(0);
            // Add head
            snakeHead = snake.GetHeadImage();
            Canvas.SetLeft(snakeHead, snake.Head.X);
            Canvas.SetTop(snakeHead, snake.Head.Y);
            GameField.Children.Add(snakeHead);
            // Add body
            snakeBody = snake.GetBodyImages(); // new List<Image>();
            for (int i = 0; i < snake.BodyLength; i++)
            {
                Canvas.SetLeft(snakeBody[i], snake.BodyPoints[i].X);
                Canvas.SetTop(snakeBody[i], snake.BodyPoints[i].Y);
                //Ellipse el = new Ellipse();
                //el.Width = el.Height = 15;
                //el.StrokeThickness = 2;
                //el.Stroke = Brushes.Black;
                //Canvas.SetLeft(el, snake.BodyPoints[i].X);
                //Canvas.SetTop(el, snake.BodyPoints[i].Y);
                //GameField.Children.Add(el);
                GameField.Children.Add(snakeBody[i]);
            }
            // Add tail
            snakeTail = snake.GetTailImage();// new Image();
            Canvas.SetLeft(snakeTail, snake.Tail.X);
            Canvas.SetTop(snakeTail, snake.Tail.Y);
            GameField.Children.Add(snakeTail);
            
            //var debugRotate = new RotateTransform();
            //var debugBorder = new Rectangle();
            //debugBorder.Width = debugBorder.Height = 40;
            //debugBorder.StrokeThickness = 2;
            //debugBorder.Stroke = Brushes.Black;
            //Canvas.SetLeft(debugBorder, 100);
            //Canvas.SetTop(debugBorder, 100);
            //GameField.Children.Add(debugBorder);

            //var debugImg = new Image();
            //debugImg.Height = debugImg.Width = 40;
            //debugImg.Source = new BitmapImage(new Uri("Images\\debug.jpg", UriKind.Relative));
            //Canvas.SetLeft(debugImg, 100);
            //Canvas.SetTop(debugImg, 100);
            ////debugRotate.CenterX = 0.5;
            ////debugRotate.CenterY = 0.5;
            //debugRotate.Angle = 180;
            //debugImg.RenderTransform = debugRotate;
            //debugImg.RenderTransformOrigin = new Point(0.5, 0.5);
            //GameField.Children.Add(debugImg);

            //var debugImg2 = new Image();
            //debugImg2.Height = debugImg2.Width = 20;
            //debugImg2.Source = new BitmapImage(new Uri("Images\\debug.jpg", UriKind.Relative));
            //Canvas.SetLeft(debugImg2, 100);
            //Canvas.SetTop(debugImg2, 100);
            //GameField.Children.Add(debugImg2);

            GameField.UpdateLayout();
        }

        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            debug_lastButton.Content = e.Key.ToString();
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    if (snake.Head.Direction != SnakeDirection.DOWN)
                    {
                        snake.Head.Direction = SnakeDirection.UP;
                    }
                    break;
                case Key.S:
                case Key.Down:
                    if (snake.Head.Direction != SnakeDirection.UP)
                    {
                        snake.Head.Direction = SnakeDirection.DOWN;
                    }
                    break;
                case Key.D:
                case Key.Right:
                    if (snake.Head.Direction != SnakeDirection.LEFT)
                    {
                        snake.Head.Direction = SnakeDirection.RIGHT;
                    }
                    break;
                case Key.A:
                case Key.Left:
                    if (snake.Head.Direction != SnakeDirection.RIGHT)
                    {
                        snake.Head.Direction = SnakeDirection.LEFT;
                    }
                    break;
                case Key.P:
                    if (paused)
                    {
                        gameTimer.Start();
                    }
                    else
                    {
                        gameTimer.Stop();
                    }
                    paused = !paused;
                    break;
                default:
                    break;                    
            };
        }
    }
}
