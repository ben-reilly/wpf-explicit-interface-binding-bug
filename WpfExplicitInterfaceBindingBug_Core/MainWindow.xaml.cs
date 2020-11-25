using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfExplicitInterfaceBindingBug_Core
{
    public interface IData
    {
        public string Name { get; }
        public string OnlyOnInterface { get; }
    }

    public class Data : IData
    {
        string IData.Name { get; } = "Interface Property";
        string IData.OnlyOnInterface { get; } = "Interface-Only Property";

        public string Name { get; } = "Class Property";

        // Including an additional property so that it's clear in the XAML editor which type it
        // thinks the bound object is: it won't show this property if it think the object is of
        // type IData.
        public int OtherProperty { get; } = 123;
    }

    public class ViewModel
    {
        public IData DataAsInterface { get; } = new Data();
        public Data DataAsClass { get; } = new Data();

        public string CSharpInterfaceName { get; }
        public string CSharpClassName { get; }
        public string CSharpCastClassName { get; }

        public ViewModel()
        {
            CSharpInterfaceName = DataAsInterface.Name;
            CSharpClassName = DataAsClass.Name;
            CSharpCastClassName = ((IData)DataAsClass).Name;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var viewModel = new ViewModel();

            InitializeComponent();
            DataContext = viewModel;

            Console.WriteLine($"DataAsInterface.Name: {viewModel.DataAsInterface.Name}");
            Console.WriteLine($"DataAsClass.Name: {viewModel.DataAsClass.Name}");
        }
    }
}
