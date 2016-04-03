﻿using System.Windows;
using StructureMap;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Interfaces;


namespace FibonacciApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IContainer _container;
        private IContainer Container { get { return _container ??(_container = ((App)Application.Current).Container); }}

        private IFibonacciLogicFacade<int> _clientFacade;
        private IFibonacciLogicFacade<int> CreateClientFacade() {
            return Container.GetInstance<IFibonacciLogicFacade<int>>();
        }


        private IFibonacciLogicFacade<int> FibonacciClientFacade {
            get { return _clientFacade ?? (_clientFacade = CreateClientFacade()); }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {  
            if (!string.IsNullOrEmpty(txtInputCount.Text))
            {
                int countOfCalculatingCicles = int.Parse(txtInputCount.Text);
                labelResultPresentation.Content = "Ожидается вычисление....";

                var result = FibonacciClientFacade.Evaluate(countOfCalculatingCicles);

                if (result == -1)
                {
                    labelResultPresentation.Content = "Истёк таймаут операции";
                }
                else {
                    labelResultPresentation.Content = result;
                }
                
            }   
        }


        private void NumericOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);

        }


        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);

        }
    }
}