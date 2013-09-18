using System;
using System.Windows.Forms;
using CCT.NUI.Core.Clustering;
using CCT.NUI.HandTracking;
using CCT.NUI.Core.Shape;

namespace FingerSpelling
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(ClusterDataSourceSettings clusterSettings, ShapeDataSourceSettings shapeDataSourceSettings, HandDataSourceSettings handDetectionSettings)
            : this()
        {
            this.propertyGridClustering.SelectedObject = clusterSettings;
            this.propertyGridShape.SelectedObject = shapeDataSourceSettings;
            this.propertyGridHandDetection.SelectedObject = handDetectionSettings;
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
