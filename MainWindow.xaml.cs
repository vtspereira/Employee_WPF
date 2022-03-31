using Funcionários.Repository;
using System;
using System.Data;
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
            new Employee(this).Show();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            DataRowView row_selected = dgvTableEmployee.SelectedItem as DataRowView;

            if (row_selected != null)
                new Employee(this, Convert.ToInt32(row_selected["Id"])).Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            DataRowView row_selected = dgvTableEmployee.SelectedItem as DataRowView;

            if (row_selected != null)
                new Employee(this, Convert.ToInt32(row_selected["Id"]), true);

            ShowEmployees();
        }
    }
}
