using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeWork_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        //Re-initializes objects to start state.
        private void Initialize_Screen()
        {
            title.Text = "Choose Your Exercise";
            button1.Content = "Stretch";

            title.Opacity = 1;
            stackPanel1.Opacity = 1;
        }

        //Sets up screen for exercise.
        private void Exercise_Screen(string titleText)
        {
            stackPanel1.Opacity = 0;
            back_button.Opacity = 1;
            title.Text = titleText;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            button1.Content = "All Stretch";
            title.Text = "Choose Your Stretch";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(button2.Content.ToString());
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(button3.Content.ToString());
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(button4.Content.ToString());
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(button5.Content.ToString());
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(button6.Content.ToString());
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            Initialize_Screen();
        }
    }
}
