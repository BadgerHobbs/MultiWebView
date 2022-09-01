using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiWebView
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            JObject config = LoadConfigFile();

            CreateGridAsync(config);
        }

        public JObject ConvertJsonStringToObject(string jsonString)
        {
            // Create json object from string data
            JObject jsonObject = JObject.Parse(jsonString);

            // Return new json object
            return jsonObject;
        }

        private void GenerateDefaultConfigFile()
        {
            dynamic config = new JObject();
            config.WebViews = new JArray();
            config.Rows = 1;
            config.Columns = 1;
            config.ShowGridLines = true;

            dynamic webView = new JObject();
            webView.Url = "https://github.com/BadgerHobbs/MultiWebView";
            webView.Row = 1;
            webView.Column = 1;
            config.WebViews.Add(webView);

            string updatedConfigJson = JsonConvert.SerializeObject(config, Formatting.Indented);

            File.WriteAllText("config.json", updatedConfigJson);
        }

        public JObject LoadConfigFile()
        {
            if (!File.Exists("config.json"))
            {
                GenerateDefaultConfigFile();
            }

            string configJson = File.ReadAllText("config.json");

            return ConvertJsonStringToObject(configJson);
        }

        public void CreateGridAsync(JObject config)
        {
            dynamicGrid.ShowGridLines = Boolean.Parse(config["ShowGridLines"].Value<string>());

            int columns = Int32.Parse(config["Columns"].Value<string>());
            int rows = Int32.Parse(config["Columns"].Value<string>());

            // Create Columns
            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                dynamicGrid.ColumnDefinitions.Add(column);
            }

            // Create Rows
            for (int i = 0; i < rows; i++)
            {
                RowDefinition row = new RowDefinition();
                dynamicGrid.RowDefinitions.Add(row);

            }

            // Create objects
            foreach (JObject webViewConfig in config["WebViews"])
            {
                String url = webViewConfig["Url"].Value<string>();
                int row = Int32.Parse(webViewConfig["Row"].Value<string>()) - 1;
                int column = Int32.Parse(webViewConfig["Column"].Value<string>()) - 1;

                WebView2 webView = new WebView2();
                webView.Source = new Uri(url);
                dynamicGrid.Children.Add(webView);

                Grid.SetRow(webView, row);
                Grid.SetColumn(webView, column);
            }
        }
    }
}
