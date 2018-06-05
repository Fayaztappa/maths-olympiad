using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Input;

namespace Maths.Olympiad.Host.ViewModels
{
    public class MathematicsScreenViewModel : ViewModelBase
    {
        public DelegateCommand GenerateSamplesCommand { get; private set; }

        public List<IOperationViewModel> Operations { get; private set; }


        private IOperationViewModel _selectedOperation;

        public IOperationViewModel SelectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                if (value == _selectedOperation)
                {
                    return;
                }
                _selectedOperation = value;
                RaisePropertyChanged();
            }
        }

        public MathematicsScreenViewModel()
        {
            GenerateSamplesCommand = new DelegateCommand(OnGenerateSamples);

            Operations = new List<IOperationViewModel>(){new AdditionalOperationViewModel(), new SubstractionOperationViewModel()};
        }

        private void OnGenerateSamples(object obj)
        {
        }
    }

    #region Addition

    public class AdditionalOperationViewModel : BaseOperationViewModel<AAndSTestSessionInputViewModel>
    {
        public override string OperationType => "Additions";

        public AdditionalOperationViewModel() : base(new AAndSTestSessionInputViewModel())
        {
        }

        protected override ITestSessionViewModel GetTestSessionViewModel()
        {
            return new AdditionTestSessionViewModel(TestSessionInput);
        }
        
        protected override bool CanGenerateTestSession()
        {
            return TestSessionInput.IsValid();
        }
    }

    public class AdditionTestSessionViewModel : BaseTestSessionViewModel
    {
        private AAndSTestSessionInputViewModel _testSessionInput;

        public AdditionTestSessionViewModel(AAndSTestSessionInputViewModel testSessionInput) : base(Generate(testSessionInput))
        {
            _testSessionInput = testSessionInput;
        }

        static IList<IQuestionViewModel> Generate(AAndSTestSessionInputViewModel testSessionInput)
        {
            string format = $"N{testSessionInput.MaxDecimalPlaces}";

            List<IQuestionViewModel> questions = new List<IQuestionViewModel>();
            Random random = new Random();

            for (int i = 0; i < testSessionInput.MaxQuestions; i++)
            {
                var firstNumber = GetNextNumber(random, testSessionInput.MaxDigits, testSessionInput.MaxDecimalPlaces);
                var secondNumber = GetNextNumber(random, testSessionInput.MaxDigits, testSessionInput.MaxDecimalPlaces);

                questions.Add(new AdditionQuestionViewModel(i + 1, firstNumber, secondNumber, format, i == testSessionInput.MaxQuestions - 1));
            }

            return questions;
        }
    }

    public class AdditionQuestionViewModel : BaseQuestionViewModel
    {
        public override string Operation => "+";

        public AdditionQuestionViewModel(int index, double item1, double item2, string format, bool isLastQuestion) :
            base(index, item1, item2, format, isLastQuestion)
        {
        }

        protected override bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue)
        {
            return (leftOperand + rightOperand) == totalValue;
        }
    }

    #endregion

    #region substraction
    public class SubstractionOperationViewModel : BaseOperationViewModel<AAndSTestSessionInputViewModel>
    {
        public override string OperationType => "Substractions";
        
        public SubstractionOperationViewModel():base(new AAndSTestSessionInputViewModel())
        {
        }

        protected override ITestSessionViewModel GetTestSessionViewModel()
        {
            return new SubstractionTestSessionViewModel(TestSessionInput);
        }

        protected override bool CanGenerateTestSession()
        {
            return TestSessionInput.IsValid();
        }
    }

    public class SubstractionTestSessionViewModel : BaseTestSessionViewModel
    {
        private readonly AAndSTestSessionInputViewModel _testSessionInput;

        public SubstractionTestSessionViewModel(AAndSTestSessionInputViewModel testSessionInput) : base(Generate(testSessionInput))
        {
            _testSessionInput = testSessionInput;
        }

        static IList<IQuestionViewModel> Generate(AAndSTestSessionInputViewModel testSessionInput)
        {
            string format = $"N{testSessionInput.MaxDecimalPlaces}";

            List<IQuestionViewModel> questions = new List<IQuestionViewModel>();
            Random random = new Random();

            for (int i = 0; i < testSessionInput.MaxQuestions; i++)
            {
                var firstNumber = GetNextNumber(random, testSessionInput.MaxDigits, testSessionInput.MaxDecimalPlaces);
                var secondNumber = GetNextNumber(random, testSessionInput.MaxDigits, testSessionInput.MaxDecimalPlaces);

                if (firstNumber < secondNumber)
                {
                    var tempNumber = firstNumber;
                    firstNumber = secondNumber;
                    secondNumber = tempNumber;
                }

                questions.Add(new SubstractionQuestionViewModel(i + 1, firstNumber, secondNumber, format, i == testSessionInput.MaxQuestions - 1));
            }

            return questions;
        }
    }

    public class SubstractionQuestionViewModel : BaseQuestionViewModel
    {
        public override string Operation => "-";

        public SubstractionQuestionViewModel(int index, double item1, double item2, string format, bool isLastQuestion) :
            base(index, item1, item2, format, isLastQuestion)
        {
        }

        protected override bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue)
        {
            return (leftOperand - rightOperand) == totalValue;
        }
    }
    #endregion

    public abstract class BaseOperationViewModel<TTestSessionInput> : ViewModelBase, IOperationViewModel
        where TTestSessionInput : class, ITestSessionInputViewModel
    {
        public TTestSessionInput TestSessionInput { get; }
        public abstract string OperationType { get; }

        private ITestSessionViewModel _session;

        public ITestSessionViewModel Session
        {
            get { return _session; }
            set
            {
                if (value == _session)
                {
                    return;
                }
                _session = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand GenerateCommand { get; private set; }


        public BaseOperationViewModel(TTestSessionInput testSessionInput)
        {
            TestSessionInput = testSessionInput;

            testSessionInput.ValidityStatusChanged += TestSessionInputOnValidityStatusChanged;

            GenerateCommand = new DelegateCommand(OnGenerate, o => CanGenerateTestSession());
        }

        private void TestSessionInputOnValidityStatusChanged(object sender, EventArgs eventArgs)
        {
            ValidateForGenerateEnabled();
        }

        private void OnGenerate(object obj)
        {
            if (!CanGenerateTestSession())
            {
                return;
            }

            if (Session != null)
            {
                Session.Dispose();
            }

            Session = GetTestSessionViewModel();
        }

        void ValidateForGenerateEnabled()
        {
            GenerateCommand.RaiseCanExecuteChanged();
        }

        protected abstract ITestSessionViewModel GetTestSessionViewModel();
        protected abstract bool CanGenerateTestSession();
    }

    public abstract class BaseTestSessionViewModel : ViewModelBase, ITestSessionViewModel
    {
        public int MaxDigits { get; }
        public int MaxDecimalPlaces { get; }
        public int MaxQuestions { get; }

        public BaseTestSessionViewModel(IList<IQuestionViewModel> questions)
        {
            EllapsedTime = TimeSpan.Zero;

            PreviousCommand = new DelegateCommand(OnPrevious, o => Questions.Count > 0 && Questions[0] != SelectedQuestion);
            NextCommand = new DelegateCommand(OnNext, o => Questions.Count > 0 && Questions.Last() != SelectedQuestion);
            FinishCommand = new DelegateCommand(OnFinish, o => Questions.Count > 0);

            _timer = new Timer(1000) { AutoReset = false };

            _timer.Elapsed += TimerOnElapsed;

            Questions = questions;
            SelectedQuestion = Questions.FirstOrDefault();

            foreach (var questionViewModel in Questions)
            {
                questionViewModel.Accept += QuestionViewModelOnAccept;
            }

            _timer.Enabled = true;
        }

        private void QuestionViewModelOnAccept(object sender, EventArgs eventArgs)
        {
            OnNext(sender);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                SelectedQuestion?.IncrementSecond();
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

        private void OnFinish(object obj)
        {
            IsFinished = true;
            _timer.Enabled = false;
            foreach (var questionViewModel in Questions)
            {
                questionViewModel.Validate();
            }
        }

        private void OnNext(object obj)
        {
            SelectedQuestion = Questions[Questions.IndexOf(SelectedQuestion) + 1];
        }

        private void OnPrevious(object obj)
        {
            SelectedQuestion = Questions[Questions.IndexOf(SelectedQuestion) - 1];
        }



        protected static double GetNextNumber(Random random, int maxDigits, int maxDecimalPlaces)
        {
            return random.Next((int)Math.Pow(10, maxDigits) - 1) + Math.Round(random.NextDouble(), maxDecimalPlaces);
        }

        public void IncrementSecond()
        {
            if (IsFinished)
            {
                return;
            }

            EllapsedTime = EllapsedTime.Add(TimeSpan.FromSeconds(1));
        }

        public override void Dispose()
        {
            _timer.Dispose();

            foreach (var questionViewModel in Questions)
            {
                questionViewModel.Accept -= QuestionViewModelOnAccept;
            }
        }

        private IList<IQuestionViewModel> _questions = new List<IQuestionViewModel>();
        public IList<IQuestionViewModel> Questions
        {
            get { return _questions; }
            set
            {
                if (value == _questions)
                {
                    return;
                }
                _questions = value;
                RaisePropertyChanged();
            }
        }

        private IQuestionViewModel _selectedQuestion;
        public IQuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                if (value == _selectedQuestion)
                {
                    return;
                }
                _selectedQuestion = value;
                RaisePropertyChanged();

                PreviousCommand.RaiseCanExecuteChanged();
                NextCommand.RaiseCanExecuteChanged();
            }
        }


        private TimeSpan _ellapsedTime;

        public TimeSpan EllapsedTime
        {
            get { return _ellapsedTime; }
            set
            {
                if (value == _ellapsedTime)
                {
                    return;
                }
                _ellapsedTime = value;
                RaisePropertyChanged();
            }
        }


        private bool _isFinished;

        public bool IsFinished
        {
            get { return _isFinished; }
            set
            {
                if (value == _isFinished)
                {
                    return;
                }
                _isFinished = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand PreviousCommand { get; private set; }
        public DelegateCommand NextCommand { get; private set; }

        public DelegateCommand FinishCommand { get; private set; }
        readonly Timer _timer;
    }

    public abstract class BaseQuestionViewModel : ViewModelBase, IQuestionViewModel
    {
        public int Index { get; }
        public double Item1 { get; }
        public double Item2 { get; }
        public ICommand AcceptCommand { get; }
        public string FormatValue { get; }

        public string StrItem1 => string.Format(FormatValue, Item1);
        public string StrItem2 => string.Format(FormatValue, Item2);

        private double? _total;
        public double? Total
        {
            get { return _total; }
            set
            {
                if (value == _total)
                {
                    return;
                }
                _total = value;
                RaisePropertyChanged();
                RaisePropertyChanged("HasValue");
            }
        }

        private bool _isCorrect;
        public bool IsCorrect
        {
            get { return _isCorrect; }
            set
            {
                if (value == _isCorrect)
                {
                    return;
                }
                _isCorrect = value;
                RaisePropertyChanged();
            }
        }


        private bool _isFinished;
        public bool IsFinished
        {
            get { return _isFinished; }
            set
            {
                if (value == _isFinished)
                {
                    return;
                }
                _isFinished = value;
                RaisePropertyChanged();
            }
        }


        private TimeSpan _ellapsedTime;
        public TimeSpan EllapsedTime
        {
            get { return _ellapsedTime; }
            set
            {
                if (value == _ellapsedTime)
                {
                    return;
                }
                _ellapsedTime = value;
                RaisePropertyChanged();
            }
        }

        public bool HasValue => Total.HasValue;
        public abstract string Operation { get; }

        public BaseQuestionViewModel(int index, double item1, double item2, string format, bool isLastQuestion)
        {
            Index = index;
            Item1 = item1;
            Item2 = item2;
            AcceptCommand = new DelegateCommand(OnAccept, o => !isLastQuestion);
            FormatValue = "{0:" + format + "}";
            EllapsedTime = TimeSpan.Zero;
        }

        private void OnAccept(object obj)
        {
            Accept?.Invoke(this, EventArgs.Empty);
        }

        public void Validate()
        {
            IsCorrect = Total.HasValue && IsCorrectValues(Item1, Item2, Total.Value);
            IsFinished = true;
        }

        public void IncrementSecond()
        {
            if (IsFinished)
            {
                return;
            }

            EllapsedTime = EllapsedTime.Add(TimeSpan.FromSeconds(1));
        }

        public event EventHandler Accept;

        protected abstract bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue);
    }

    public interface IQuestionViewModel
    {
        bool HasValue { get; }
        void Validate();
        void IncrementSecond();

        event EventHandler Accept;
    }

    public interface ITestSessionViewModel : IDisposable
    {
    }

    public interface ITestSessionInputViewModel
    {
        event EventHandler ValidityStatusChanged;
        bool IsValid();
    }

    public class AAndSTestSessionInputViewModel : ViewModelBase, ITestSessionInputViewModel
    {
        private int _maxDecimalPlaces;
        public int MaxDecimalPlaces
        {
            get { return _maxDecimalPlaces; }
            set
            {
                if (value == _maxDecimalPlaces)
                {
                    return;
                }
                _maxDecimalPlaces = value;
                RaisePropertyChanged();

                ValidateForGenerateEnabled();
            }
        }


        private int _maxQuestions;
        public int MaxQuestions
        {
            get { return _maxQuestions; }
            set
            {
                if (value == _maxQuestions)
                {
                    return;
                }
                _maxQuestions = value;
                RaisePropertyChanged();
                ValidateForGenerateEnabled();
            }
        }


        private int _maxDigits;
        public int MaxDigits
        {
            get { return _maxDigits; }
            set
            {
                if (value == _maxDigits)
                {
                    return;
                }
                _maxDigits = value;
                RaisePropertyChanged();
                ValidateForGenerateEnabled();
            }
        }

        private void ValidateForGenerateEnabled()
        {
            ValidityStatusChanged?.Invoke(this, EventArgs.Empty);
        }

        public AAndSTestSessionInputViewModel()
        {
            MaxDecimalPlaces = 2;
            MaxDigits = 3;
            MaxQuestions = 5;
        }

        public bool IsValid()
        {
            return (MaxDigits > 0 && MaxQuestions>0 && MaxDecimalPlaces>=0);
        }

        public event EventHandler ValidityStatusChanged;
    }
}
