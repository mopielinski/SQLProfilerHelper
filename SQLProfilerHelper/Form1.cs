using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SQLProfilerHelper
{
    public partial class Form1 : Form
    {
        readonly SpExecuteSqlConversionService _spExecuteSqlConversionService;

        public Form1()
        {
            InitializeComponent();
            _spExecuteSqlConversionService = new SpExecuteSqlConversionService();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void convertExecuteSQLToSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessConverting(true);
        }

        private void convertExecuteSQLToTightSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessConverting(false);
        }

        private void ProcessConverting(bool addNewLinesAndIdents)
        {
            var idat = Clipboard.GetDataObject();
            var text = idat.GetData(DataFormats.Text) as string;

            _spExecuteSqlConversionService.SPExecuteSQLInput = text;

            try
            {
                _spExecuteSqlConversionService.Convert(addNewLinesAndIdents);
            }
            catch (InvalidInputSpExecuteSqlTextException ex)
            {
                DisplayErrorMessage(ex.Message);
            }

            Debug.WriteLine("-----");
            Debug.WriteLine(_spExecuteSqlConversionService.SQLOutput);

            Clipboard.SetText(_spExecuteSqlConversionService.SQLOutput);

            //////////////////////////////////////////////////////////

            void DisplayErrorMessage(string message)
            {
                MessageBox.Show(
                   message,
                   "Conversion failed",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1
                );
            }
        }


    }
}
