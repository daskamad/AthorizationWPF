using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoWPF.MyForms
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        string[] captchaFile = new string[]
        {
            "Images/1tts.png", "Images/ssft.png","Images/tasg.png"
        };
        string[] captchaIsTry = new string[]
        {
            "1337","7642","2ASG"
        };

        int captchaIstn;

        public SignInWindow()
        {
            InitializeComponent();
            this.Loaded += SignInWindow_Loaded;
        }

        private void SignInWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            captchaIstn = random.Next(0, captchaFile.Length);
            Uri uri = new Uri(captchaFile[captchaIstn], UriKind.RelativeOrAbsolute);
            BitmapImage bitmapImage = new BitmapImage(uri);
            ImageCaptcha.Source = bitmapImage;
        }

        private void BtSignIn2_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(tbLogin.Text) || String.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Введите данные корректно");
                return;
            }
            DB.IAuto auto = DB.MyBuild.GetAuto();
            if (tbCaptch.Text == captchaIsTry[captchaIstn])
            {
                try
                {
                    if (auto.IsLogIn(tbLogin.Text, tbPassword.Text) == true)
                    {
                        var user = auto.GetUser(tbLogin.Text, tbPassword.Text);
                        MessageBox.Show($"Логин {user.Login}, Возраст - {user.Age}, Ваш порядковый номер - {user.UserId}");

                    }
                    else
                    {
                        MessageBox.Show("неверный логин?, пароль или капча");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Капча не верна");
                for (int i = 10; i > 0; i--)
                {
                    this.Title = $"блокировка {i} cек";
                    Thread.Sleep(1000);                    
                }                              
            }

        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWibdow = new MainWindow();
            mainWibdow.Show();
            Close();
        }

        private void btReflesh_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow_Loaded(sender, e);
        }
    }
}
