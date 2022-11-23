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
using Budget;
using Microsoft.Win32;
using System.Data.SQLite;
using System.IO;

namespace HomeBudgetGUI
{
    //Properties.Setting.Default code to save file name is from here: https://www.youtube.com/watch?v=tIOWI0JBFkg&ab_channel=KIJUKA
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string saveLocation;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void NewDatabasebtn_Click(object sender, RoutedEventArgs e)
        {
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Database File|*.db";
            if(Convert.ToString(FileName.Text) == "")
            {
                MessageBox.Show("Please specify a filename in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Opens the dialog in the Documents/Budgets folder then the user can either save it there to choose their location.
                openFileDialog.Filter = "Database Files|*.db";
                openFileDialog.FileName = Convert.ToString(FileName.Text);
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Budgets"));
                openFileDialog.InitialDirectory = documentsFolder+"\\Budgets";
                openFileDialog.CheckFileExists = false;

                if (openFileDialog.ShowDialog() == true)
                {
                    saveLocation = openFileDialog.FileName;

                    //Sets the new created file as the last one opened
                    Properties.Settings.Default.previousFile = openFileDialog.FileName;
                }
                else
                {
                    return;
                }

                //Saves the new created file as the last one opened
                Properties.Settings.Default.Save();

                HomeBudget budget = new HomeBudget(saveLocation, true);
                AddCatORExp window2 = new AddCatORExp(budget);
                window2.ShowDialog();
            }
        }

        private void ExistingDatabasebtn_Click(object sender, RoutedEventArgs e)
        {
            //Opens the File Dialog with the last opened file in the filename box
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = Properties.Settings.Default.previousFile;
            openFile.Filter = "Database File|*.db";
            if (openFile.ShowDialog() == true)
            {
                saveLocation = openFile.FileName;

                //Sets the opened file as the last one opened
                Properties.Settings.Default.previousFile = openFile.FileName;
            }
            else
            {
                return;
            }
            //Saves the opened file as the last one opened
            Properties.Settings.Default.Save();
            
            
            HomeBudget budget = new HomeBudget(saveLocation, false);
            AddCatORExp window2 = new AddCatORExp(budget);
            window2.ShowDialog();
        }
    }
}
