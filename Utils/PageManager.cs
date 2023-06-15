using ChatApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ChatApp.Pages.Message;
using ChatApp.Pages.Auth;

namespace ChatApp.Utils
{
    internal class PageManager
    {
        public static User CurrentUser;
        public static Frame CurrentFrame;
        public static void AttachFrameInstance(Frame frame)
        {
            CurrentFrame = frame;

        }

        public static void CreateLayout(dynamic page)
        {
            if (CurrentFrame == null)
                throw new Exception("Frame instance is not initialized!");

            CurrentFrame.Navigate(page);
        }

        public static void UserLogout()
        {
            CreateLayout(new LoginPage());
        }

        public static void UserLogin(string username, string password)
        {
            try
            {
                if (username.Length < 4 || username.Length > 64)
                    throw new Exception("Неверная длина почты!");

                if (password.Length < 4 || password.Length > 64)
                    throw new Exception("Неверная длина пароля!");

                var user = DB.entities.User.FirstOrDefault(x =>
                    x.Password == password && x.Login == username);

                if (user == null)
                    throw new Exception("Неверный логин или пароль!");

                CurrentUser = user;

                MessageBox.Show($"Здраствуйте, {user.Login}, вы успешно авторизовались", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                CreateLayout(new MessagesPage());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void UserRegister(string username, string password)
        {
            try
            {
                if (username.Length < 4 || username.Length > 64)
                    throw new Exception("Неверная длина почты!");

                if (password.Length < 6 || password.Length > 64)
                    throw new Exception("Неверная длина пароля!");

                dynamic isEmailExist = DB.entities.User.FirstOrDefault(x =>
                    x.Login == username);

                if (isEmailExist != null)
                    throw new Exception("Данный почтовый адресс уже зарегистрирован!");

                User newUser = new User
                {
                    Login = username,
                    Password = password,
                };

                dynamic user = DB.entities.User.Add(newUser);

                if (user == null)
                    throw new Exception("Ошибка регистрации, попробуйте позже!");

                CurrentUser = user;

                DB.entities.SaveChanges();

                MessageBox.Show($"Здраствуйте, {user.Login}, вы успешно зарегистрировались", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                CreateLayout(new MessagesPage());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
    }
}
