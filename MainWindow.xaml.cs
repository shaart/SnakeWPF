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
        const string spriteMapPath = "Images\\snake_spritemap.gif";
        // Time
        System.Windows.Threading.DispatcherTimer gameTimer;
        TimeSpan time, timerInterval = new TimeSpan(500000);    // 300 000 = 00:00:00.3
        bool paused = false;
        bool _gameOver = false;
        int _score = 0;
        int _appleScoreCost = 10;
        Point GameArea;

        // Snake
        SnakeWPF.Code.DrawableSnake snake;
        Image snakeHead, snakeTail;
        List<Image> snakeBody;
        SnakeWPF.Code.Apple apple;
        Image appleImage;
        Rectangle fieldBorder;
        
        // Snake moving
        int distance = 16;   // images height&width

        public MainWindow()
        {
            InitializeComponent();
            // Language
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
            if (GameField.ActualWidth > 0 && GameField.ActualHeight > 0)
            {
                GameArea = new Point(GameField.ActualWidth, GameField.ActualHeight);
            }
            else if (GameField.Width > 0 && GameField.Height > 0)
            {
                GameArea = new Point(GameField.Width, GameField.Height);
            }
            else
            {
                GameArea = new Point(400, 400); // 18 - menu place
            }

            //GameArea = new Point(200, 200);
            fieldBorder = new Rectangle();
            fieldBorder.Stroke = Brushes.Black;
            fieldBorder.HorizontalAlignment = HorizontalAlignment.Left;
            fieldBorder.VerticalAlignment = VerticalAlignment.Top;
            fieldBorder.Width = GameArea.X;
            fieldBorder.Height = GameArea.Y;

            var tempBody = new List<Code.DirPoint>();
            tempBody.Add(new Code.DirPoint(GameArea.X / 2 - distance, GameArea.Y / 2));
            //for (int i = 2; i < 15; i++)
            //{
            //    tempBody.Add(new Code.DirPoint(GameField.ActualWidth / 2 - i * distance, GameField.ActualHeight / 2));
            //}
            snake = new DrawableSnake(
                new Code.DirPoint(GameArea.X / 2, GameArea.Y / 2),                   // Head
                new Code.DirPoint(GameArea.X / 2 - 2 * distance, GameArea.Y / 2),    // Tail
                tempBody,                                                                                   // Body
                new Uri(spriteMapPath, UriKind.Relative));                                                  // Sprite map     

            paused = false;
            _gameOver = false;
            _score = 0;
            ScoreValue.Header = "0";
            apple = new Code.Apple(
                (GameArea.X / 2) + 2 * distance,            // X
                GameArea.Y / 2,                             // Y
                new Uri(spriteMapPath, UriKind.Relative));  // Sprite map

            GameLogic.MakeNewApple(snake, GameArea.X, GameArea.Y, ref apple);

            gameTimer = new System.Windows.Threading.DispatcherTimer();
            gameTimer.Tick += new EventHandler(FrameTick);
            gameTimer.Interval = timerInterval;
            gameTimer.Start();            
        }

        private void FrameTick(object sender, EventArgs e)
        {
            time = time.Add(timerInterval);
            TimeValue.Header = time.ToString();
            
            snake.Move(distance, new Code.Size(GameArea.X, GameArea.Y), snake.HeadSprite.FrameSize);

            GameLogic.CheckSnakeCollisions(snake, out _gameOver);

            if (GameLogic.IsSnakeAteApple(snake, apple, ref _score, _appleScoreCost))
            {
                // Set new apple
                GameLogic.MakeNewApple(snake, GameArea.X, GameArea.Y, ref apple);
                ScoreValue.Header = _score.ToString();
                // Add snake segment
                snake.AddBodyPointToEnd();
            }

            RedrawObjects();

            // Game over
            if (_gameOver)
            {
                gameTimer.Stop();
                time = new TimeSpan(0, 0, 0);
                MenuStatus.Content = "GAME OVER! \n\nYour score: "+ _score.ToString();
                MenuGrid.Visibility = Visibility.Visible;
                GameGrid.Visibility = Visibility.Hidden;
            }
            //debug_snakePointsNum.Content = snake.BodyLength + 2; //snakePoints.Count.ToString();
            //debug_snakeX.Content = snake.Head.X; //newCoord.X.ToString();
            //debug_snakeY.Content = snake.Head.Y; //newCoord.Y.ToString();
        }


        private void RedrawObjects()
        {
            // Clear and redraw objects
            GameField.Children.Clear();

            GameField.Children.Add(fieldBorder);

            RotateTransform rotate = new RotateTransform(0);
            // Add head
            snakeHead = snake.GetHeadImage();
            Canvas.SetLeft(snakeHead, snake.Head.X);
            Canvas.SetTop(snakeHead, snake.Head.Y);
            GameField.Children.Add(snakeHead);
            // Add body
            snakeBody = snake.GetBodyImages();
            for (int i = 0; i < snake.BodyLength; i++)
            {
                Canvas.SetLeft(snakeBody[i], snake.BodyPoints[i].X);
                Canvas.SetTop(snakeBody[i], snake.BodyPoints[i].Y);
                GameField.Children.Add(snakeBody[i]);
            }
            // Add tail
            snakeTail = snake.GetTailImage();
            Canvas.SetLeft(snakeTail, snake.Tail.X);
            Canvas.SetTop(snakeTail, snake.Tail.Y);
            GameField.Children.Add(snakeTail);
            // Add apple
            appleImage = apple.GetImage();
            Canvas.SetLeft(appleImage, apple.X);
            Canvas.SetTop(appleImage, apple.Y);
            GameField.Children.Add(appleImage);

            GameField.UpdateLayout();
        }

        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            debug_lastButton.Content = e.Key.ToString();
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    if (snake.BodyPoints[0].Direction != SnakeDirection.DOWN)
                    {
                        snake.Head.Direction = SnakeDirection.UP;
                    }
                    break;
                case Key.S:
                case Key.Down:
                    if (snake.BodyPoints[0].Direction != SnakeDirection.UP)
                    {
                        snake.Head.Direction = SnakeDirection.DOWN;
                    }
                    break;
                case Key.D:
                case Key.Right:
                    if (snake.BodyPoints[0].Direction != SnakeDirection.LEFT)
                    {
                        snake.Head.Direction = SnakeDirection.RIGHT;
                    }
                    break;
                case Key.A:
                case Key.Left:
                    if (snake.BodyPoints[0].Direction != SnakeDirection.RIGHT)
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
