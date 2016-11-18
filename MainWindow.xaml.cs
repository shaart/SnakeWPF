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
            snakeHead = new Image();
            snake.HeadSprite.Update();
            snakeHead.Source = snake.HeadSprite.GetRenderedImage();
            switch (snake.Head.Direction)
            {
                case SnakeDirection.UP:
                    rotate = new RotateTransform(270);
                    break;
                case SnakeDirection.RIGHT:
                    rotate = new RotateTransform(0);
                    break;
                case SnakeDirection.DOWN:
                    rotate = new RotateTransform(90);
                    break;
                case SnakeDirection.LEFT:
                    rotate = new RotateTransform(180);
                    break;
                default:
                    break;
            }
            snakeHead.RenderTransform = rotate;
            Canvas.SetLeft(snakeHead, snake.Head.X);
            Canvas.SetTop(snakeHead, snake.Head.Y);
            GameField.Children.Add(snakeHead);

            // Add body
            snakeBody = new List<Image>();
            for (int i = 0; i < snake.BodyLength; i++)
            {
                snakeBody.Add(new Image());
                snake.BodySprite.Update();
                snake.BodyRotateSprite.Update();
                switch (snake.BodyPoints[i].Direction)
                {
                    case SnakeDirection.UP:
                        snakeBody[i].Source = snake.BodySprite.GetRenderedImage();
                        rotate = new RotateTransform(270);
                        break;
                    case SnakeDirection.RIGHT:
                        snakeBody[i].Source = snake.BodySprite.GetRenderedImage();
                        rotate = new RotateTransform(0);
                        break;
                    case SnakeDirection.DOWN:
                        snakeBody[i].Source = snake.BodySprite.GetRenderedImage();
                        rotate = new RotateTransform(90);
                        break;
                    case SnakeDirection.LEFT:
                        snakeBody[i].Source = snake.BodySprite.GetRenderedImage();
                        rotate = new RotateTransform(180);
                        break;
                    case SnakeDirection.UP_LEFT:
                        snakeBody[i].Source = snake.BodyRotateSprite.GetRenderedImage();
                        //rotate = new RotateTransform(0);
                        break;
                    case SnakeDirection.UP_RIGHT:
                        snakeBody[i].Source = snake.BodyRotateSprite.GetRenderedImage();
                        //rotate = new RotateTransform(90);
                        break;
                    case SnakeDirection.DOWN_LEFT:
                        snakeBody[i].Source = snake.BodyRotateSprite.GetRenderedImage();
                        //rotate = new RotateTransform(270);
                        break;
                    case SnakeDirection.DOWN_RIGHT:
                        snakeBody[i].Source = snake.BodyRotateSprite.GetRenderedImage();
                        //rotate = new RotateTransform(180);
                        break;
                    default:
                        break;
                }
                snakeBody[i].RenderTransform = rotate;
                Canvas.SetLeft(snakeBody[i], snake.BodyPoints[i].X);
                Canvas.SetTop(snakeBody[i], snake.BodyPoints[i].Y);
                GameField.Children.Add(snakeBody[i]);
            }

            // Add tail
            snakeTail = new Image();
            snake.TailSprite.Update();
            snakeTail.Source = snake.TailSprite.GetRenderedImage();
            switch (snake.Tail.Direction)
            {
                case SnakeDirection.UP:
                    rotate = new RotateTransform(270);
                    break;
                case SnakeDirection.RIGHT:
                    rotate = new RotateTransform(0);
                    break;
                case SnakeDirection.DOWN:
                    rotate = new RotateTransform(90);
                    break;
                case SnakeDirection.LEFT:
                    rotate = new RotateTransform(180);
                    break;
                default:
                    break;
            }
            snakeTail.RenderTransform = rotate;
            Canvas.SetLeft(snakeTail, snake.Tail.X);
            Canvas.SetTop(snakeTail, snake.Tail.Y);
            GameField.Children.Add(snakeTail);

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
