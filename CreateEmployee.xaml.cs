using Funcionários.Models;
using Funcionários.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;

namespace Funcionários
{
    /// <summary>
    /// Lógica interna para CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        private MainWindow _mainWindow;
        public CreateEmployee(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Name = txtName.Text;
            employee.Email = txtEmail.Text;
            employee.Wage = decimal.Parse(txtWage.Text);
            employee.Gender = (bool)rbMasc.IsChecked ? "M" : "F";
            employee.ContractType = (bool)rbClt.IsChecked ? "CLT" : "PJ";
            employee.CreatedAt = DateTime.Now;

            ValidateRegister(employee);

            if (EmployeeRepository.Create(employee))
            {
                _mainWindow.ShowEmployees();
                this.Close();
            }
            else
                lblErros.Content = $"Erro no interno de cadastro";

        }

        private void ValidateRegister(Employee employee)
        {
            lblErros.Content = "";
            List<ValidationResult> listErrors = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(employee);
            bool validate = Validator.TryValidateObject(employee, context, listErrors, true);
            if (!validate)
            {
                foreach (ValidationResult error in listErrors)
                {
                    lblErros.Content += $"{error.ErrorMessage} \n";
                }
            }
        }
    }
}
