using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using Maths.Olympiad.Dal.Data;
using Maths.Olympiad.Dal.Interfaces;

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

        public MathematicsScreenViewModel(ITestDal testDal)
        {
            GenerateSamplesCommand = new DelegateCommand(OnGenerateSamples);

            Operations = new List<IOperationViewModel>()
            {
                new SimpleOperationViewModel(new AdditionOperation(), testDal),
                new SimpleOperationViewModel(new SubstractionOperation(), testDal),
                new SimpleOperationViewModel(new MultiplicationOperation(), testDal),
                new SimpleOperationViewModel(new DivisionOperation(), testDal),
            };
        }

        private void OnGenerateSamples(object obj)
        {
        }
    }

    //#region Addition

    //public class AdditionalOperationViewModel : BaseOperationViewModel<AAndSTestSessionInputViewModel>
    //{
    //    public override string OperationType => "Additions";

    //    public AdditionalOperationViewModel() : base(new AAndSTestSessionInputViewModel())
    //    {
    //    }

    //    protected override ITestSessionViewModel GetTestSessionViewModel()
    //    {
    //        return new AdditionTestSessionViewModel(TestSessionInput);
    //    }

    //    protected override bool CanGenerateTestSession()
    //    {
    //        return TestSessionInput.IsValid();
    //    }
    //}

    //public class AdditionTestSessionViewModel : BaseTestSessionViewModel
    //{
    //    private AAndSTestSessionInputViewModel _testSessionInput;

    //    public AdditionTestSessionViewModel(AAndSTestSessionInputViewModel testSessionInput) : base(Generate(testSessionInput))
    //    {
    //        _testSessionInput = testSessionInput;
    //    }

    //    static IList<IQuestionViewModel> Generate(AAndSTestSessionInputViewModel testSessionInput)
    //    {
    //        string format = $"N{testSessionInput.MaxDecimalPlaces}";

    //        List<IQuestionViewModel> questions = new List<IQuestionViewModel>();
    //        Random random = new Random();

    //        for (int i = 0; i < testSessionInput.MaxQuestions; i++)
    //        {
    //            var firstNumber = GetNextNumber(random, testSessionInput.MaxDigits, testSessionInput.MaxDecimalPlaces);
    //            var secondNumber = GetNextNumber(random, testSessionInput.MaxDigits, testSessionInput.MaxDecimalPlaces);

    //            questions.Add(new AdditionQuestionViewModel(i + 1, firstNumber, secondNumber, format, i == testSessionInput.MaxQuestions - 1));
    //        }

    //        return questions;
    //    }
    //}

    //public class AdditionQuestionViewModel : BaseQuestionViewModel
    //{
    //    public override string Operation => "+";

    //    public AdditionQuestionViewModel(int index, double item1, double item2, string format, bool isLastQuestion) :
    //        base(index, item1, item2, format, isLastQuestion)
    //    {
    //    }

    //    protected override bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue)
    //    {
    //        return (leftOperand + rightOperand) == totalValue;
    //    }
    //}

    //#endregion

    //#region substraction
    //public class SubstractionOperationViewModel : BaseOperationViewModel<AAndSTestSessionInputViewModel>
    //{
    //    public override string OperationType => "Substractions";

    //    public SubstractionOperationViewModel():base(new AAndSTestSessionInputViewModel())
    //    {
    //    }

    //    protected override ITestSessionViewModel GetTestSessionViewModel()
    //    {
    //        return new SubstractionTestSessionViewModel(TestSessionInput);
    //    }

    //    protected override bool CanGenerateTestSession()
    //    {
    //        return TestSessionInput.IsValid();
    //    }
    //}

    //public class SubstractionTestSessionViewModel : BaseTestSessionViewModel
    //{
    //    private readonly AAndSTestSessionInputViewModel _testSessionInput;

    //    public SubstractionTestSessionViewModel(AAndSTestSessionInputViewModel testSessionInput) : base(Generate(testSessionInput))
    //    {
    //        _testSessionInput = testSessionInput;
    //    }

    //    static IList<IQuestionViewModel> Generate(AAndSTestSessionInputViewModel testSessionInput)
    //    {
    //        string format = $"N{testSessionInput.MaxDecimalPlaces}";

    //        List<IQuestionViewModel> questions = new List<IQuestionViewModel>();
    //        Random random = new Random();

    //        for (int i = 0; i < testSessionInput.MaxQuestions; i++)
    //        {
    //            var firstNumber = GetNextNumber(random, testSessionInput.MaxDigits, testSessionInput.MaxDecimalPlaces);
    //            var secondNumber = GetNextNumber(random, testSessionInput.MaxDigits, testSessionInput.MaxDecimalPlaces);

    //            if (firstNumber < secondNumber)
    //            {
    //                var tempNumber = firstNumber;
    //                firstNumber = secondNumber;
    //                secondNumber = tempNumber;
    //            }

    //            questions.Add(new SubstractionQuestionViewModel(i + 1, firstNumber, secondNumber, format, i == testSessionInput.MaxQuestions - 1));
    //        }

    //        return questions;
    //    }
    //}

    //public class SubstractionQuestionViewModel : BaseQuestionViewModel
    //{
    //    public override string Operation => "-";

    //    public SubstractionQuestionViewModel(int index, double item1, double item2, string format, bool isLastQuestion) :
    //        base(index, item1, item2, format, isLastQuestion)
    //    {
    //    }

    //    protected override bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue)
    //    {
    //        return (leftOperand - rightOperand) == totalValue;
    //    }
    //}
    //#endregion

    //#region Multiplication

    //public class MultiplicationOperationViewModel : BaseOperationViewModel<MAndDTestSessionInputViewModel>
    //{
    //    public override string OperationType => "Multiplication";

    //    public MultiplicationOperationViewModel():base(new MAndDTestSessionInputViewModel())
    //    {
    //    }

    //    protected override ITestSessionViewModel GetTestSessionViewModel()
    //    {
    //        return new MultiplicationTestSessionViewModel(TestSessionInput);
    //    }

    //    protected override bool CanGenerateTestSession()
    //    {
    //        return TestSessionInput.IsValid();
    //    }
    //}

    //public class MultiplicationTestSessionViewModel : BaseTestSessionViewModel
    //{
    //    private readonly MAndDTestSessionInputViewModel _testSessionInput;

    //    public MultiplicationTestSessionViewModel(MAndDTestSessionInputViewModel testSessionInput) : base(Generate(testSessionInput))
    //    {
    //        _testSessionInput = testSessionInput;
    //    }

    //    static IList<IQuestionViewModel> Generate(MAndDTestSessionInputViewModel testSessionInput)
    //    {
    //        string format = $"N{testSessionInput.FirstOperandMaxDecimalPlaces}";

    //        List<IQuestionViewModel> questions = new List<IQuestionViewModel>();
    //        Random random = new Random();

    //        for (int i = 0; i < testSessionInput.MaxQuestions; i++)
    //        {
    //            var firstNumber = GetNextNumber(random, testSessionInput.FirstOperandMaxDigits, testSessionInput.FirstOperandMaxDigits);
    //            var secondNumber = GetNextNumber(random, testSessionInput.SecondOperandMaxDigits, testSessionInput.SecondOperandMaxDigits);

    //            if (firstNumber < secondNumber)
    //            {
    //                var tempNumber = firstNumber;
    //                firstNumber = secondNumber;
    //                secondNumber = tempNumber;
    //            }

    //            questions.Add(new MultiplicationQuestionViewModel(i + 1, firstNumber, secondNumber, format, i == testSessionInput.MaxQuestions - 1));
    //        }

    //        return questions;
    //    }
    //}

    //public class MultiplicationQuestionViewModel : BaseQuestionViewModel
    //{
    //    public override string Operation => "X";

    //    public MultiplicationQuestionViewModel(int index, double item1, double item2, string format, bool isLastQuestion) :
    //        base(index, item1, item2, format, isLastQuestion)
    //    {
    //    }

    //    protected override bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue)
    //    {
    //        return (leftOperand * rightOperand) == totalValue;
    //    }
    //}
    //#endregion

    //#region Division
    //public class DivisionOperationViewModel : BaseOperationViewModel<MAndDTestSessionInputViewModel>
    //{
    //    public override string OperationType => "Division";

    //    public DivisionOperationViewModel():base(new MAndDTestSessionInputViewModel())
    //    {
    //    }

    //    protected override ITestSessionViewModel GetTestSessionViewModel()
    //    {
    //        return new DivisionTestSessionViewModel(TestSessionInput);
    //    }

    //    protected override bool CanGenerateTestSession()
    //    {
    //        return TestSessionInput.IsValid();
    //    }
    //}

    //public class DivisionTestSessionViewModel : BaseTestSessionViewModel
    //{
    //    private readonly MAndDTestSessionInputViewModel _testSessionInput;

    //    public DivisionTestSessionViewModel(MAndDTestSessionInputViewModel testSessionInput) : base(Generate(testSessionInput))
    //    {
    //        _testSessionInput = testSessionInput;
    //    }

    //    static IList<IQuestionViewModel> Generate(MAndDTestSessionInputViewModel testSessionInput)
    //    {
    //        string format = $"N{testSessionInput.FirstOperandMaxDecimalPlaces}";

    //        List<IQuestionViewModel> questions = new List<IQuestionViewModel>();
    //        Random random = new Random();

    //        for (int i = 0; i < testSessionInput.MaxQuestions; i++)
    //        {
    //            var firstNumber = GetNextNumber(random, testSessionInput.FirstOperandMaxDigits, testSessionInput.FirstOperandMaxDecimalPlaces);
    //            var secondNumber = GetNextNumber(random, testSessionInput.SecondOperandMaxDigits, testSessionInput.SecondOperandMaxDigits);

    //            if (firstNumber < secondNumber)
    //            {
    //                var tempNumber = firstNumber;
    //                firstNumber = secondNumber;
    //                secondNumber = tempNumber;
    //            }

    //            questions.Add(new DivisionQuestionViewModel(i + 1, firstNumber, secondNumber, format, i == testSessionInput.MaxQuestions - 1));
    //        }

    //        return questions;
    //    }
    //}

    //public class DivisionQuestionViewModel : BaseQuestionViewModel
    //{
    //    public override string Operation => "/";

    //    public DivisionQuestionViewModel(int index, double item1, double item2, string format, bool isLastQuestion) :
    //        base(index, item1, item2, format, isLastQuestion)
    //    {
    //    }

    //    protected override bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue)
    //    {
    //        return (leftOperand / rightOperand) == totalValue;
    //    }
    //}
    //#endregion
    //public class AAndSTestSessionInputViewModel : BaseTestSessionInputViewModel
    //{
    //    private int _maxDecimalPlaces;
    //    public int MaxDecimalPlaces
    //    {
    //        get { return _maxDecimalPlaces; }
    //        set
    //        {
    //            if (value == _maxDecimalPlaces)
    //            {
    //                return;
    //            }
    //            _maxDecimalPlaces = value;
    //            RaisePropertyChanged();

    //            ValidateForGenerateEnabled();
    //        }
    //    }


    //    private int _maxQuestions;
    //    public int MaxQuestions
    //    {
    //        get { return _maxQuestions; }
    //        set
    //        {
    //            if (value == _maxQuestions)
    //            {
    //                return;
    //            }
    //            _maxQuestions = value;
    //            RaisePropertyChanged();
    //            ValidateForGenerateEnabled();
    //        }
    //    }


    //    private int _maxDigits;
    //    public int MaxDigits
    //    {
    //        get { return _maxDigits; }
    //        set
    //        {
    //            if (value == _maxDigits)
    //            {
    //                return;
    //            }
    //            _maxDigits = value;
    //            RaisePropertyChanged();
    //            ValidateForGenerateEnabled();
    //        }
    //    }

    //    private void ValidateForGenerateEnabled()
    //    {
    //        ValidityStatusChanged?.Invoke(this, EventArgs.Empty);
    //    }

    //    public AAndSTestSessionInputViewModel()
    //    {
    //        MaxDecimalPlaces = 2;
    //        MaxDigits = 3;
    //        MaxQuestions = 5;
    //    }

    //    public bool IsValid()
    //    {
    //        return (MaxDigits > 0 && MaxQuestions>0 && MaxDecimalPlaces>=0);
    //    }

    //    public event EventHandler ValidityStatusChanged;
    //}

    //public class MAndDTestSessionInputViewModel : ViewModelBase, ITestSessionInputViewModel
    //{

    //    private int _firstOperandMaxDecimalPlaces;
    //    public int FirstOperandMaxDecimalPlaces
    //    {
    //        get { return _firstOperandMaxDecimalPlaces; }
    //        set
    //        {
    //            if (value == _firstOperandMaxDecimalPlaces)
    //            {
    //                return;
    //            }
    //            _firstOperandMaxDecimalPlaces = value;
    //            RaisePropertyChanged();

    //            ValidateForGenerateEnabled();
    //        }
    //    }


    //    private int _firstOperandMaxDigits;

    //    public int FirstOperandMaxDigits
    //    {
    //        get { return _firstOperandMaxDigits; }
    //        set
    //        {
    //            if (value == _firstOperandMaxDigits)
    //            {
    //                return;
    //            }
    //            _firstOperandMaxDigits = value;
    //            RaisePropertyChanged();
    //            ValidateForGenerateEnabled();
    //        }
    //    }


    //    private int _secondOperandMaxDigits;

    //    public int SecondOperandMaxDigits
    //    {
    //        get { return _secondOperandMaxDigits; }
    //        set
    //        {
    //            if (value == _secondOperandMaxDigits)
    //            {
    //                return;
    //            }
    //            _secondOperandMaxDigits = value;
    //            RaisePropertyChanged();
    //            ValidateForGenerateEnabled();
    //        }
    //    }


    //    private int _secondOperandMaxDecimalPlaces;

    //    public int SecondOperandMaxDecimalPlaces
    //    {
    //        get { return _secondOperandMaxDecimalPlaces; }
    //        set
    //        {
    //            if (value == _secondOperandMaxDecimalPlaces)
    //            {
    //                return;
    //            }
    //            _secondOperandMaxDecimalPlaces = value;
    //            RaisePropertyChanged();
    //            ValidateForGenerateEnabled();
    //        }
    //    }


    //    private int _maxQuestions;
    //    public int MaxQuestions
    //    {
    //        get { return _maxQuestions; }
    //        set
    //        {
    //            if (value == _maxQuestions)
    //            {
    //                return;
    //            }
    //            _maxQuestions = value;
    //            RaisePropertyChanged();
    //            ValidateForGenerateEnabled();
    //        }
    //    }

    //    private void ValidateForGenerateEnabled()
    //    {
    //        ValidityStatusChanged?.Invoke(this, EventArgs.Empty);
    //    }

    //    public MAndDTestSessionInputViewModel()
    //    {
    //        FirstOperandMaxDecimalPlaces = 2;
    //        FirstOperandMaxDigits = 3;
    //        SecondOperandMaxDecimalPlaces = 0;
    //        SecondOperandMaxDigits = 2;
    //        MaxQuestions = 5;
    //    }

    //    public bool IsValid()
    //    {
    //        return (FirstOperandMaxDigits > 0 && MaxQuestions>0 && FirstOperandMaxDecimalPlaces >= 0 && SecondOperandMaxDigits > 0 && SecondOperandMaxDecimalPlaces >= 0);
    //    }

    //    public event EventHandler ValidityStatusChanged;
    //}

    #region SimmpleOperation

    public class AdditionOperation : IOperation
    {
        public string OperationType => "Addition";
        public string OperationSymbol => "+";
        public double GetComputedValue(double leftOperand, double rightOperand)
        {
            return leftOperand + rightOperand;
        }
    }
    public class MultiplicationOperation : IOperation
    {
        public string OperationType => "Multiplication";
        public string OperationSymbol => "X";
        public double GetComputedValue(double leftOperand, double rightOperand)
        {
            return leftOperand * rightOperand;
        }
    }
    public class SubstractionOperation : IOperation
    {
        public string OperationType => "Substraction";
        public string OperationSymbol => "-";
        public double GetComputedValue(double leftOperand, double rightOperand)
        {
            return leftOperand - rightOperand;
        }
    }
    public class DivisionOperation : IOperation
    {
        public string OperationType => "Division";
        public string OperationSymbol => "/";
        public double GetComputedValue(double leftOperand, double rightOperand)
        {
            return leftOperand - rightOperand;
        }
    }

    public class SimpleOperationViewModel : BaseOperationViewModel<SimpleOperationTestSessionInputViewModel>
    {
        private readonly IOperation _operation;
        public override string OperationType => _operation.OperationType;


        public SimpleOperationViewModel(IOperation operation, ITestDal testDal) : base(new SimpleOperationTestSessionInputViewModel(), testDal)
        {
            _operation = operation;
        }

        protected override ITestSessionViewModel GetTestSessionViewModel()
        {
            return new SimpleOperationTestSessionViewModel(TestSessionInput, _operation);
        }

        protected override bool CanGenerateTestSession()
        {
            return TestSessionInput.IsValid();
        }
    }

    public class SimpleOperationTestSessionViewModel : BaseTestSessionViewModel
    {
        private readonly SimpleOperationTestSessionInputViewModel _testSessionInput;
        private readonly IOperation _operation;

        public SimpleOperationTestSessionViewModel(SimpleOperationTestSessionInputViewModel testSessionInput, IOperation operation)
            : base(Generate(testSessionInput, operation))
        {
            _testSessionInput = testSessionInput;
            _operation = operation;
        }

        static IList<IQuestionViewModel> Generate(SimpleOperationTestSessionInputViewModel testSessionInput, IOperation operation)
        {
            string format = testSessionInput.Format;

            List<IQuestionViewModel> questions = new List<IQuestionViewModel>();

            for (int i = 0; i < testSessionInput.MaxQuestions; i++)
            {
                var operands = testSessionInput.GetNextOperands();

                questions.Add(new SimpleOperationQuestionViewModel(operation, i + 1, operands.Item1, operands.Item2, format, i == testSessionInput.MaxQuestions - 1));
            }

            return questions;
        }

        protected override TestHeader GetTestHeader()
        {
            return new TestHeader()
            {
                LeftMaxDecimals = _testSessionInput.FirstOperandMaxDecimalPlaces,
                LeftMaxDigits = _testSessionInput.FirstOperandMaxDigits,
                RightMaxDigits = _testSessionInput.SecondOperandMaxDigits,
                RightMaxDecimals = _testSessionInput.SecondOperandMaxDecimalPlaces,
                OperationType = _operation.OperationType,
                TotalQuestions = _testSessionInput.MaxQuestions
            };
        }
    }

    public class SimpleOperationQuestionViewModel : BaseQuestionViewModel
    {
        private readonly IOperation _operation;
        public override string OperationSymbol => _operation.OperationSymbol;

        public SimpleOperationQuestionViewModel(IOperation operation, int index, double item1, double item2, string format, bool isLastQuestion) :
            base(index, item1, item2, format, isLastQuestion)
        {
            _operation = operation;
        }

        protected override bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue)
        {
            return _operation.GetComputedValue(leftOperand, rightOperand) == totalValue;
        }
    }
    #endregion

    public abstract class BaseOperationViewModel<TTestSessionInput> : ViewModelBase, IOperationViewModel
        where TTestSessionInput : class, ITestSessionInputViewModel
    {
        private readonly ITestDal _testDal;
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


        private bool _inProgress;
        public bool InProgress
        {
            get { return _inProgress; }
            set
            {
                if (value == _inProgress)
                {
                    return;
                }
                _inProgress = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand GenerateCommand { get; private set; }


        public BaseOperationViewModel(TTestSessionInput testSessionInput, ITestDal testDal)
        {
            _testDal = testDal;
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
                Session.Finished -= SessionOnFinished;
                Session.Dispose();
            }

            Session = GetTestSessionViewModel();
            Session.Finished += SessionOnFinished;
            InProgress = true;
        }

        private void SessionOnFinished(object sender, EventArgs eventArgs)
        {
            if (!(sender is ITestSessionViewModel session))
            {
                return;
            }

            var testDetail = session.GetTestDetail();
            testDetail.UserLogin = "ruhina.tappa";

            _testDal.SaveTest(testDetail);
            InProgress = false;
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

            _performedDateTime = DateTime.UtcNow;
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

            Finished?.Invoke(this, EventArgs.Empty);
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
            var nextNumber = random.Next((int)Math.Pow(10, maxDigits) - 1) + Math.Round(random.NextDouble(), maxDecimalPlaces);

            if (nextNumber == 0.0)
            {
                return GetNextNumber(random, maxDigits, maxDecimalPlaces);
            }

            return nextNumber;
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
        private DateTime _performedDateTime;
        public event EventHandler Finished;

        public TestDetail GetTestDetail()
        {
            var questions = Questions.Select(model => model.GetQuestionDetails()).ToList();
            return new TestDetail()
            {
                Duration = EllapsedTime,
                PerformedDateTime = _performedDateTime,
                Questions = questions,
                CorrectQuestions = questions.Count(question => question.IsCorrect),
                WrongQuestions = questions.Count(question => !question.IsCorrect),
                Header = GetTestHeader()
            };
        }

        protected abstract TestHeader GetTestHeader();
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
        public abstract string OperationSymbol { get; }

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
        public TestQuestion GetQuestionDetails()
        {
            return new TestQuestion()
            {
                Duration = EllapsedTime,
                Index= Index,
                Answer= Total ?? 0.0,
                LOperand = Item1,
                ROperand = Item2,
                OperationType = OperationSymbol,
                IsCorrect = IsCorrect
            };
        }

        protected abstract bool IsCorrectValues(double leftOperand, double rightOperand, double totalValue);
    }

    public abstract class BaseTestSessionInputViewModel : ViewModelBase, ITestSessionInputViewModel
    {

        protected void ValidateForGenerateEnabled()
        {
            ValidityStatusChanged?.Invoke(this, EventArgs.Empty);
        }

        public BaseTestSessionInputViewModel()
        {
        }
        public abstract bool IsValid();

        public abstract Tuple<double, double> GetNextOperands();
        public abstract string Format { get; }
        public event EventHandler ValidityStatusChanged;



        protected static double GetNextNumber(Random random, int maxDigits, int maxDecimalPlaces)
        {
            var nextNumber = random.Next((int)Math.Pow(10, maxDigits) - 1) + Math.Round(random.NextDouble(), maxDecimalPlaces);

            if (nextNumber == 0.0)
            {
                return GetNextNumber(random, maxDigits, maxDecimalPlaces);
            }

            return nextNumber;
        }
    }

    public interface IQuestionViewModel
    {
        bool HasValue { get; }
        void Validate();
        void IncrementSecond();

        event EventHandler Accept;
        TestQuestion GetQuestionDetails();
    }

    public interface ITestSessionViewModel : IDisposable
    {
        event EventHandler Finished;
        TestDetail GetTestDetail();
    }

    public interface ITestSessionInputViewModel
    {
        Tuple<double, double> GetNextOperands();

        string Format { get; }
        event EventHandler ValidityStatusChanged;
        bool IsValid();
    }

    public interface IOperation
    {
        string OperationType { get; }
        string OperationSymbol { get; }
        double GetComputedValue(double leftOperand, double rightOperand);
    }

    public class SimpleOperationTestSessionInputViewModel : BaseTestSessionInputViewModel
    {
        public SimpleOperationTestSessionInputViewModel()
        {
            FirstOperandMaxDecimalPlaces = 2;
            FirstOperandMaxDigits = 3;
            SecondOperandMaxDecimalPlaces = 0;
            SecondOperandMaxDigits = 2;
            MaxQuestions = 5;
        }

        private int _firstOperandMaxDecimalPlaces;
        public int FirstOperandMaxDecimalPlaces
        {
            get { return _firstOperandMaxDecimalPlaces; }
            set
            {
                if (value == _firstOperandMaxDecimalPlaces)
                {
                    return;
                }
                _firstOperandMaxDecimalPlaces = value;
                RaisePropertyChanged();

                ValidateForGenerateEnabled();
            }
        }


        private int _firstOperandMaxDigits;

        public int FirstOperandMaxDigits
        {
            get { return _firstOperandMaxDigits; }
            set
            {
                if (value == _firstOperandMaxDigits)
                {
                    return;
                }
                _firstOperandMaxDigits = value;
                RaisePropertyChanged();
                ValidateForGenerateEnabled();
            }
        }


        private int _secondOperandMaxDigits;

        public int SecondOperandMaxDigits
        {
            get { return _secondOperandMaxDigits; }
            set
            {
                if (value == _secondOperandMaxDigits)
                {
                    return;
                }
                _secondOperandMaxDigits = value;
                RaisePropertyChanged();
                ValidateForGenerateEnabled();
            }
        }


        private int _secondOperandMaxDecimalPlaces;

        public int SecondOperandMaxDecimalPlaces
        {
            get { return _secondOperandMaxDecimalPlaces; }
            set
            {
                if (value == _secondOperandMaxDecimalPlaces)
                {
                    return;
                }
                _secondOperandMaxDecimalPlaces = value;
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

        public override bool IsValid()
        {
            return (FirstOperandMaxDigits > 0 && MaxQuestions > 0 && FirstOperandMaxDecimalPlaces >= 0 &&
                    SecondOperandMaxDigits > 0 && SecondOperandMaxDecimalPlaces >= 0 &&
                    FirstOperandMaxDecimalPlaces >= SecondOperandMaxDecimalPlaces && FirstOperandMaxDigits >= SecondOperandMaxDigits) ;
        }

        readonly Random _random = new Random();

        public override Tuple<double, double> GetNextOperands()
        {
            var leftOperand = GetNextNumber(_random, FirstOperandMaxDigits, FirstOperandMaxDecimalPlaces);
            var rightOperand = GetNextNumber(_random, SecondOperandMaxDigits, SecondOperandMaxDecimalPlaces);
            while(leftOperand < rightOperand)
            {
                rightOperand = GetNextNumber(_random, SecondOperandMaxDigits, SecondOperandMaxDecimalPlaces);
            }

            return Tuple.Create(leftOperand, rightOperand);
        }

        public override string Format => $"N{FirstOperandMaxDecimalPlaces}";
    }
}
