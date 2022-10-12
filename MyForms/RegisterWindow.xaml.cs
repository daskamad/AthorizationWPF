using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AutoWPF.MyForms
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
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

        public RegisterWindow()
        {
            InitializeComponent();
            this.Loaded += RegisterWindow_Loaded;        
        }

        private void RegisterWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            captchaIstn = random.Next(0, captchaFile.Length);
            Uri uri = new Uri(captchaFile[captchaIstn], UriKind.RelativeOrAbsolute);
            BitmapImage bitmapImage = new BitmapImage(uri);
            ImageCaptcha.Source = bitmapImage;
        }

        private void BtReg2_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbLogin.Text) || String.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Введите данные");
                return;
            }
            int age = 0;

            try
            {
                age = Convert.ToInt32(tbAge.Text);
                if(age < 15 || age > 120)
                {
                    MessageBox.Show("напишите нормальный возвраст");
                    return;
                }

            }
            catch
            {
                MessageBox.Show("Введены не числа, введите свой возраст", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DB.User addUser = new DB.User()
            {
                Login = tbLogin.Text,
                Age = age,
                Password = tbPassword.Text,
            };

            DB.MyContext myContext = new DB.MyContext();


            if (tbCaptch.Text == captchaIsTry[captchaIstn])
            {
                try
                {
                    myContext.Users.Add(addUser);
                    myContext.SaveChanges();
                    MessageBox.Show($"Congratulations,{addUser.Login} has been added ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, try again" + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Capcha не правельна");
                
            }

        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btReflesh_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow_Loaded(sender, e);
        }
    }
}
