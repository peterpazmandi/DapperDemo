﻿using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.CompanyVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.Company
{
    public class BackToListCommand : ICommand
    {
        public readonly CreateCompanyViewModel _companyViewModel;
        private readonly IRenavigator _renavigator;

        public BackToListCommand(CreateCompanyViewModel createCompanyViewModel, IRenavigator renavigator)
        {
            _companyViewModel = createCompanyViewModel;
            _renavigator = renavigator;

            _companyViewModel.PropertyChanged += CreateCompanyViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _renavigator.Renavigate();
        }

        private void CreateCompanyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}