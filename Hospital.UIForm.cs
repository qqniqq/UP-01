using System;
using System.Configuration;
using System.Windows.Forms;
using Hospital.Core;
using Hospital.Data; // реализация


namespace HospitalSolution
{
    public partial class FormAddDrug : Form
    {
        private readonly DrugService _drugService;

        public FormAddDrug()
        {
            InitializeComponent();
            // получаем connection string из app.config
            string connStr = ConfigurationManager.ConnectionStrings["HospitalMySql"].ConnectionString;
            var repo = new MySqlDrugRepository(connStr);
            _drugService = new DrugService(repo);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            // Считываем данные из полей
            var name = txtName.Text.Trim();
            var form = txtForm.Text.Trim();
            var dosage = txtDosage.Text.Trim();
            var expDate = dtpExpirationDate.Value.Date;

            var drug = new Drug
            {
                Name = name,
                Form = string.IsNullOrEmpty(form) ? null : form,
                Dosage = string.IsNullOrEmpty(dosage) ? null : dosage,
                ExpirationDate = expDate
            };

            string validationResult = _drugService.AddDrug(drug);
            if (string.IsNullOrEmpty(validationResult))
            {
                lblStatus.Text = "Препарат успешно добавлен.";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                // Очистить поля или оставить для редактирования
                txtName.Text = "";
                txtForm.Text = "";
                txtDosage.Text = "";
                dtpExpirationDate.Value = DateTime.Now.Date;
            }
            else
            {
                lblStatus.Text = validationResult;
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
