using System;
using System.Windows;
using Proxy;

namespace Demo.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //ITestGetMethodClient client = new TestGetMethodClient(new Uri("http://localhost:36761"));
            //var res = await client.GetWithTwoCustomParamAsync(AllDataTypesModel.FilledAllPropertiesInstance, new User { Id = 1024 });
        }
    }
}
