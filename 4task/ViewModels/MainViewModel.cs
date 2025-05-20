using ReflectionInvokeApp.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace ReflectionInvokeApp.ViewModels
{
    public class ParameterViewModel : ViewModelBase
    {
        public string Name { get; }
        public Type ParameterType { get; }
        private string? _value;
        public string? Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        public ParameterViewModel(string name, Type type)
        {
            Name = name;
            ParameterType = type;
        }
    }

    public class MethodViewModel : ViewModelBase
    {
        public string Name => Method.Name;
        public MethodInfo Method { get; }
        public ObservableCollection<ParameterViewModel> Parameters { get; }

        public MethodViewModel(MethodInfo method)
        {
            Method = method;
            Parameters = new ObservableCollection<ParameterViewModel>(
                method.GetParameters().Select(p => new ParameterViewModel(p.Name ?? "param", p.ParameterType)));
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private Assembly? _assembly;
        private Type? _selectedType;

        public ObservableCollection<string> ClassNames { get; } = new();
        public ObservableCollection<MethodViewModel> Methods { get; } = new();

        private string _assemblyPath = string.Empty;
        public string AssemblyPath
        {
            get => _assemblyPath;
            set { if (_assemblyPath != value) { _assemblyPath = value; OnPropertyChanged(); } }
        }

        private string? _selectedClass;
        public string? SelectedClass
        {
            get => _selectedClass;
            set
            {
                if (_selectedClass != value)
                {
                    _selectedClass = value;
                    OnPropertyChanged();
                    LoadMethods();
                }
            }
        }

        private MethodViewModel? _selectedMethod;
        public MethodViewModel? SelectedMethod
        {
            get => _selectedMethod;
            set { if (_selectedMethod != value) { _selectedMethod = value; OnPropertyChanged(); } }
        }

        private string _output = string.Empty;
        public string Output
        {
            get => _output;
            set { if (_output != value) { _output = value; OnPropertyChanged(); } }
        }

        public ICommand LoadAssemblyCommand { get; }
        public ICommand ExecuteMethodCommand { get; }

        public MainViewModel()
        {
            LoadAssemblyCommand = new RelayCommand(LoadAssembly);
            ExecuteMethodCommand = new RelayCommand(ExecuteMethod, () => SelectedMethod != null && _selectedType != null);
        }

        private void LoadAssembly()
        {
            try
            {
                _assembly = Assembly.LoadFrom(AssemblyPath);
                var interfaceType = typeof(object); // Placeholder, will search all types
                ClassNames.Clear();
                foreach (var t in _assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
                {
                    ClassNames.Add(t.FullName ?? t.Name);
                }
                Output = $"Loaded {ClassNames.Count} classes.";
            }
            catch (Exception ex)
            {
                Output = ex.Message;
            }
        }

        private void LoadMethods()
        {
            Methods.Clear();
            _selectedType = null;
            if (_assembly == null || string.IsNullOrEmpty(SelectedClass))
                return;

            _selectedType = _assembly.GetType(SelectedClass!);
            if (_selectedType == null)
                return;

            foreach (var m in _selectedType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (m.IsSpecialName) continue;
                Methods.Add(new MethodViewModel(m));
            }
        }

        private void ExecuteMethod()
        {
            if (_selectedType == null || SelectedMethod == null) return;

            try
            {
                object? instance = Activator.CreateInstance(_selectedType);
                if (instance == null)
                {
                    Output = "Unable to create instance.";
                    return;
                }

                var parameters = SelectedMethod.Parameters
                    .Select(p => Convert.ChangeType(p.Value, p.ParameterType))
                    .ToArray();
                var result = SelectedMethod.Method.Invoke(instance, parameters);
                Output = result != null ? result.ToString() ?? string.Empty : "Method executed";
            }
            catch (Exception ex)
            {
                Output = ex.Message;
            }
        }
    }
}
