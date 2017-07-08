using System.Windows;
using StructureMap;
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

        private IFibonacciLogicFacade<long> _clientFacade;
        private IFibonacciLogicFacade<long> CreateClientFacade() {
            return Container.GetInstance<IFibonacciLogicFacade<long>>();
        }


        private IFibonacciLogicFacade<long> FibonacciClientFacade {
            get { return _clientFacade ?? (_clientFacade = CreateClientFacade()); }
        }

        private ILogger _logger;
        private ILogger Logger
        {
            get { return _logger ?? (_logger = Container.GetInstance<ILogger>()); }
        }

        public MainWindow()
        {
            InitializeComponent();
            Logger.LogInfoMessage("Приложение запущено");
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {  
            if (!string.IsNullOrEmpty(txtInputCount.Text))
            {
                long countOfCalculatingCicles = long.Parse(txtInputCount.Text);
                labelResultPresentation.Content = "Ожидается вычисление....";

                Logger.LogInfoMessage(string.Format("Вычисление запущено с количеством циклов {0}",countOfCalculatingCicles));

                var result = FibonacciClientFacade.Evaluate(countOfCalculatingCicles);


                if (result == -1)
                {
                    labelResultPresentation.Content = "Истёк таймаут операции";
                    Logger.LogInfoMessage(string.Format("Вычисление завершено истёк таймаут операции"));
                }
                else {
                    labelResultPresentation.Content = result;
                    Logger.LogInfoMessage(string.Format("Вычисление завершено с результатом {0}", result));
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
