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
    public partial class Employee : Window
    {
        private MainWindow _mainWindow;
        private Models.Employee _employee;
        public Employee(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        public Employee(MainWindow mainWindow, int id, bool delete = false)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            if (delete)
                EmployeeRepository.Delete(id);

            _employee = EmployeeRepository.Get(id);
            BuildDataTable(_employee);
        }

        private void SaveBtn(object sender, RoutedEventArgs e)
        {
            if (_employee != null)
                Update();
            else
                Register();
        }

        private void BuildDataTable(Models.Employee employee)
        {
            txtName.Text = employee.Name;
            txtEmail.Text = employee.Email;
            txtWage.Text = employee.Wage.ToString();

            if (employee.Gender == "M")
                rbMasc.IsChecked = true;
            else
                rbFem.IsChecked = true;
            
            if (employee.ContractType == "CLT")
                rbClt.IsChecked = true;
            else
                rbPJ.IsChecked = true;
        }

        private void Update()
        {
            FillData(_employee, true);

            if (EmployeeRepository.Update(_employee))
            {
                _mainWindow.ShowEmployees();
                this.Close();
            }
            else
                lblErros.Content = $"Erro no interno de cadastro";
        }

        private void Register()
        {
            _employee = new Models.Employee();

            FillData(_employee);
            ValidateRegister(_employee);

            if (EmployeeRepository.Create(_employee))
            {
                _mainWindow.ShowEmployees();
                this.Close();
            }
            else
                lblErros.Content = $"Erro no interno de cadastro";
        }

        private void Delete()
        {

        }

        private void FillData(Models.Employee employee, bool updateEmployee = false)
        {
            employee.Name = txtName.Text.Trim();
            employee.Email = txtEmail.Text.Trim();
            employee.Wage = decimal.Parse(txtWage.Text);
            employee.Gender = (bool)rbMasc.IsChecked ? "M" : "F";
            employee.ContractType = (bool)rbClt.IsChecked ? "CLT" : "PJ";

            if(updateEmployee)
                employee.UpdatedAt = DateTime.Now;
            else
                employee.CreatedAt = DateTime.Now;
        }

        private void ValidateRegister(Models.Employee employee)
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
