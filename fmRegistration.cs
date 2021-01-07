using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamientoV4
{
    public partial class fmRegistration : Form
    {
        public fmRegistration()
        {
            InitializeComponent();
        }
        const int ProductCode = 1;

        private void btnOk_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProductID.Text);
            string productKey = txtProductKey.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "FoxLearn";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;

                    }
                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);
                    MessageBox.Show("Te haz registrado exitosamente", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
            }
            else
                MessageBox.Show("La llave del producto es invalido", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void fmRegistration_Load(object sender, EventArgs e)
        {
            txtProductID.Text = ComputerInfo.GetComputerId();
        }
    }
}
