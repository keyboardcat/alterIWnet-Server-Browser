using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace aIWServerBrowser
{
    public delegate void onFilterChanged(Filter f, string name);
    public partial class FiltersDlg : Form
    {
        private int selectedFilter = -1;
        private List<Filter> availableFilters;
        private onFilterChanged callback;
        public FiltersDlg(onFilterChanged fcallback)
        {
            InitializeComponent();
            callback = fcallback;
            availableFilters = new List<Filter>();
        }

        private void loadFiltersIntoUI()
        {
            availableFilters.Clear();
            filterList.Items.Clear();
            filterList.Items.Add("All servers");
            filterList.Items.Add("Favourites only");

            availableFilters.Add(Filter.getDefaultFilter());

            // Favourites filter
            Filter temp = Filter.getDefaultFilter();
            temp.Favourites = Filter.YNA.Yes;
            availableFilters.Add(temp);

            try
            {
                foreach (string f in Directory.GetFiles("filters/", "*.osl", SearchOption.TopDirectoryOnly))
                {
                    string fn = Path.GetFileNameWithoutExtension(f);
                    Filter f2 = FilterLoader.LoadFilterClass(fn);
                    availableFilters.Add(f2);
                    filterList.Items.Add(f2.filterName);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while loading filters! " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFilter = filterList.SelectedIndex;
            if (selectedFilter > 0)
            {
                useFilter.Enabled = true;
                newFilter.Enabled = true;
                delFilter.Enabled = true;
                editFilter.Enabled = true;
            }
        }

        private void FiltersDlg_Load(object sender, EventArgs e)
        {
            loadFiltersIntoUI();
        }

        private void useFilter_Click(object sender, EventArgs e)
        {
            callback(availableFilters[selectedFilter], (string)filterList.SelectedItem);
            this.Hide();
        }

        private void FiltersDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void newFilter_Click(object sender, EventArgs e)
        {
            new FilterCreateDlg(Filter.getDefaultFilter(), filterUpdated, true).ShowDialog();
        }

        private void filterUpdated(Filter f)
        {
            loadFiltersIntoUI();
        }

        private void editFilter_Click(object sender, EventArgs e)
        {
            new FilterCreateDlg(availableFilters[selectedFilter], filterUpdated, false).ShowDialog();
        }

        private void delFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("filters/" + availableFilters[selectedFilter].filterName.ToLower() + ".osl"))
                    File.Delete("filters/" + availableFilters[selectedFilter].filterName.ToLower() + ".osl");
                else
                    MessageBox.Show("You cannot delete this filter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ef)
            {
                MessageBox.Show("Error while deleting filter! " + ef.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            loadFiltersIntoUI();
        }
    }
}
