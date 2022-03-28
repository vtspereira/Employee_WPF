using Funcionários.Repository;
using System.Windows;

namespace Funcionários
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowEmployees();
        }

        public void ShowEmployees()
        {
            dgvTableEmployee.ItemsSource = EmployeeRepository.GetEmployee().DefaultView;
        }

        private void NewEmployee(object sender, RoutedEventArgs e)
        {
            new CreateEmployee(this).Show();
        }
    }
}
