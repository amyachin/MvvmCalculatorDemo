using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmCalculatorDemo.Model;
using System;

namespace MvvmCalculatorDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    /// 


    public class MainViewModel : ViewModelBase
    {
        public RelayCommand<OperationType> OperationCommand { get; }

        public RelayCommand<string> SymbolCommand { get; }

        public RelayCommand ClearCommand { get; }

        public RelayCommand EvaluateCommand { get; }

        public RelayCommand BackspaceCommand { get; }

        static bool IsValidSymbol(string symbol)
        {
            return symbol.Length == 1 && char.IsDigit(symbol, 0) || symbol[0] == '.';
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ICalculator calculator)
        {

            _calculator = calculator;

            OperationCommand = new RelayCommand<OperationType>(
                execute: SetOperation
            );

            SymbolCommand = new RelayCommand<string>(
                execute: (symbol) => AppendSymbol(symbol)
            );

            BackspaceCommand = new RelayCommand(
                execute: () => RemoveLastSymbol(),
                canExecute: () => !string.IsNullOrEmpty(_inputText) 
            );

            EvaluateCommand = new RelayCommand(
                execute: () => EvaluateResults()
            );

            ClearCommand = new RelayCommand(
                execute: Clear
            );

        }


        void RaiseInputCommandsStateChanged()
        {
            EvaluateCommand.RaiseCanExecuteChanged();
            BackspaceCommand.RaiseCanExecuteChanged();
        }

        void Clear()
        {
            _inputText = null;
            _calculator.Clear();
            RaiseEvaluateResultsEvents();
        }


        void SetOperation(OperationType operation)
        {

            ConsumeInput();

            _calculator.PushOperation(operation);

            RaiseEvaluateResultsEvents();        
        }

        void ConsumeInput()
        {
            if (double.TryParse(_inputText, out var newValue))
            {
                _calculator.PushValue(newValue);
            }

            _inputText = null;
        }

        void AppendSymbol(string symbol)
        {

            string newValue = (_inputText ?? "") + symbol;

            if (Decimal.TryParse(newValue, out var dummy))
            {
                bool wasEmpty = string.IsNullOrEmpty(_inputText);
                _inputText = newValue;

                RaisePropertyChanged(nameof(TextOrResult));

                if (wasEmpty)
                {
                    RaiseInputCommandsStateChanged();
                }
            }

        }

        void RemoveLastSymbol()
        {

            if (!string.IsNullOrEmpty(_inputText))
            {
                _inputText = _inputText.Substring(0, _inputText.Length - 1);
                RaisePropertyChanged(nameof(TextOrResult));
            }


            if (string.IsNullOrEmpty(_inputText))
            {
                RaiseInputCommandsStateChanged();
            }
        }

        void RaiseEvaluateResultsEvents()
        {
            RaisePropertyChanged(nameof(Expression));
            RaisePropertyChanged(nameof(TextOrResult));
            RaiseInputCommandsStateChanged();
        }

        void EvaluateResults()
        {
            ConsumeInput();

            _calculator.Flush();
            RaiseEvaluateResultsEvents();
        }


        public string TextOrResult
        {
            get
            {
                if (!string.IsNullOrEmpty(_inputText))
                {
                    return _inputText;
                }
                else 
                {
                    return (_calculator.CurrentValue?.ToString()) ?? "0";
                }
            }
        }

        string _inputText;

        public Expression Expression
        {
            get { return _calculator.Expression; }
        }


        ICalculator _calculator;

    }
}