﻿using System;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace PrismSample.ViewModels
{
    public class TermsDialogViewModel : BaseViewModel, IDialogAware, IAutoInitialize
    {
        public TermsDialogViewModel()
        {
            Title = "Terms and Conditions";
            SubmitCommand = new DelegateCommand(OnSubmitTapped)
                .ObservesCanExecute(() => CanContinue);
            CancelCommand = new DelegateCommand(OnCancelTapped);
        }

        public DelegateCommand SubmitCommand { get; }
        public DelegateCommand CancelCommand { get; }

        void OnSubmitTapped()
        {
            RequestClose(new DialogParameters { { "accepted", true } });
        }

        bool _cancelled;
        void OnCancelTapped()
        {
            _cancelled = true;
            RequestClose(new DialogParameters { { "accepted", false } });
        }

        private string _name = "User Name";
        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        private bool _canContinue;
        public bool CanContinue
        {
            get { return _canContinue; }
            set { SetProperty(ref _canContinue, value); }
        }

        private string _emailAddress = "Email Address";
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => _cancelled || CanContinue;

        public void OnDialogClosed()
        {
            Console.WriteLine("*** Dialog closed");
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("email"))
                EmailAddress = parameters.GetValue<string>("email");
        }
    }
}
