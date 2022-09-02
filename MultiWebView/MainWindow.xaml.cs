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

            JObject config;
            try
            {
                config = LoadConfigFile();
            }
            catch
            {
                MessageBox.Show("Error parsing config, please validate json format.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

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

            if (config["ShowGridLines"] != null)
            {
                try
                {
                    dynamicGrid.ShowGridLines = Boolean.Parse(config["ShowGridLines"].Value<string>());
                }
                catch
                {
                    MessageBox.Show("ShowGridLines configured must be a bool.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }                
            }
            
            int columns = 0;
            if (config["Columns"] == null)
            {
                MessageBox.Show("Number of colums required in config.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }
            else
            {
                try
                {
                    columns = Int32.Parse(config["Columns"].Value<string>());
                }
                catch
                {
                    MessageBox.Show("Number of columns configured must be an int.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }
            }

            int rows = 0;
            if (config["Rows"] == null)
            {
                MessageBox.Show("Number of rows required in config.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }
            else
            {
                try
                {
                    rows = Int32.Parse(config["Rows"].Value<string>());
                }
                catch
                {
                    MessageBox.Show("Number of rows configured must be an int.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }
            }

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
                if (webViewConfig["Url"] == null)
                {
                    MessageBox.Show("Url required for webview config.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }

                Uri uri;
                try
                {
                    uri = new Uri(webViewConfig["Url"].Value<string>());
                }
                catch
                {
                    MessageBox.Show("Url configured for webview must be in a valid format.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }


                int row = 0;
                if (webViewConfig["Row"] == null)
                {
                    MessageBox.Show("Row number required for webview config.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }
                else
                {
                    try
                    {
                        row = Int32.Parse(webViewConfig["Row"].Value<string>()) - 1;
                    }
                    catch
                    {
                        MessageBox.Show("Row number configured for webview must be an int.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                        return;
                    }
                }

                int column = 0;
                if (webViewConfig["Column"] == null)
                {
                    MessageBox.Show("Column number required for webview config.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }
                else
                {
                    try
                    {
                        column = Int32.Parse(webViewConfig["Column"].Value<string>()) - 1;
                    }
                    catch
                    {
                        MessageBox.Show("Column number configured for webview must be an int.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                        return;
                    }
                }

                int rowSpan = 1;
                if (webViewConfig["RowSpan"] != null)
                {
                    try
                    {
                        rowSpan = Int32.Parse(webViewConfig["RowSpan"].Value<string>());
                    }
                    catch
                    {
                        MessageBox.Show("RowSpan number configured for webview must be an int.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                        return;
                    }
                }

                int columnSpan = 1;
                if (webViewConfig["ColumnSpan"] != null)
                {
                    try
                    {
                        columnSpan = Int32.Parse(webViewConfig["ColumnSpan"].Value<string>());
                    }
                    catch
                    {
                        MessageBox.Show("ColumnSpan number configured for webview must be an int.", "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                        return;
                    }
                }

                WebView2 webView = new WebView2();
                webView.Source = uri;
                dynamicGrid.Children.Add(webView);

                Grid.SetRow(webView, row);
                Grid.SetColumn(webView, column);

                Grid.SetRowSpan(webView, rowSpan);
                Grid.SetColumnSpan(webView, columnSpan);
            }
        }
    }
}
