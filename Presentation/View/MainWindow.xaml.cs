﻿using Presentation.Model;
using Presentation.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            this.viewModel = (MainViewModel)DataContext;
        }

        //private void Next_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Register();
        }

        //private void Next_Click(object sender, RoutedEventArgs e)
        //{
        //    UserModel u = viewModel.Login();
        //    if (u != null)
        //    {
        //        BoardView boardView = new BoardView(u);
        //        boardView.Show();
        //        this.Close();
        //    }
        //}


    }
}
