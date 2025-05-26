using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;
using DiplomErshov.WindowFolder.DirectorWindowFolder;
using DiplomErshov.WindowFolder.EmployeeWindowFolder;

namespace DiplomErshov.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTB.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTB.Focus();
            }
            if (string.IsNullOrEmpty(PasswordPB.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordPB.Focus();
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext().User.FirstOrDefault
                        (u => u.LoginUser == LoginTB.Text);
                    if (user == null)
                    {
                        MBClass.ErrorMB("Пользователь не найден");
                        LoginTB.Focus();
                        return;
                    }

                    else if (user.LoginUser != LoginTB.Text)
                    {
                        MBClass.ErrorMB("Пользователь не найден");
                        LoginTB.Focus();
                        return;
                    }

                    if (user.PasswordUser != PasswordPB.Password)
                    {
                        MBClass.ErrorMB("Введен неправильный пароль");
                        PasswordPB.Focus();
                        return;
                    }
                    else
                    {
                        switch (user.IdRole)
                        {
                            case 1:
                                new WindowFolder.AdministratorWindowFolder.AdministratorMenuWindow().Show();
                                this.Close();
                                break;

                            case 2:
                                DirectorMenuWindow DirectorMenuWindow = new DirectorMenuWindow();
                                DirectorMenuWindow.Show();
                                this.Close();
                                break;

                            case 3:
                                EmployeeMenuWindow EmployeeMenuWindow = new EmployeeMenuWindow();
                                EmployeeMenuWindow.Show();
                                this.Close();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void Minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard minimizeStoryboard = (Storyboard)FindResource("MinimizeStoryboard");
            minimizeStoryboard.Completed += MinimizeStoryboard_Completed;
            minimizeStoryboard.Begin(this);
        }

        private void MinimizeStoryboard_Completed(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.BeginAnimation(Window.HeightProperty, null);
                this.BeginAnimation(Window.WidthProperty, null);
                this.BeginAnimation(Window.OpacityProperty, null);
                this.Height = 750;
                this.Width = 1200;
                this.Opacity = 1;
            }
        }

        private void Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void BorderTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
