using BITCollege_NN.Data;
using BITCollege_NN.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class StudentData : Form
    {
        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();

        /// <summary>
        /// This constructor will be used when this form is opened from
        /// the MDI Frame.
        /// </summary>
        public StudentData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to StudentData
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form.
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public StudentData(ConstructorData constructor)
        {
            InitializeComponent();

            //Further code to be added.
            if (constructorData != null)
            {
                // Set the constructorData instance variable to the value of the corresponding argument.
                this.constructorData.student = constructor.student;
                this.constructorData.registration = constructor.registration;

                // Set the Student Number MaskedTextBox value using the Student property of the constructor argument.
                studentNumberMaskedTextBox.Text = this.constructorData.student.StudentNumber.ToString();

                // Call the MaskedTextBox_Leave event passing null for each of the event arguments.
                studentNumberMaskedTextBox_Leave(null, null);
            }
        }

        /// <summary>
        /// given: Open grading form passing constructor data.
        /// </summary>
        private void lnkUpdateGrade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            constructorData.registration = (Registration)registrationBindingSource.Current;

            Grading grading = new Grading(constructorData);
            grading.MdiParent = this.MdiParent;
            grading.Show();
            this.Close();
        }


        /// <summary>
        /// given: Open history form passing constructor data.
        /// </summary>
        private void lnkViewDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            constructorData.registration = (Registration)registrationBindingSource.Current;

            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Opens the form in top right corner of the frame.
        /// </summary>
        private void StudentData_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);
        }

        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            // Instantiate an object of the BITCollege_NNContext class.
            BITCollege_NNContext db = new BITCollege_NNContext();

            // Ensure the user has completed the requirements for the Mask
            if (!string.IsNullOrWhiteSpace(studentNumberMaskedTextBox.Text) && long.TryParse(studentNumberMaskedTextBox.Text, out long studentNumber))
            {
                Student student = db.Students.Where(x => x.StudentNumber == studentNumber).SingleOrDefault();

                if (student != null)
                {
                    constructorData.student = student;

                    // Set the DataSource property of the BindingSource object representing the Student controls to the result set of the Student query.
                    studentBindingSource.DataSource = student;

                    // Define a LINQ-to-SQL Server query selecting all Registrations in which the StudentId corresponds to the record represented by the StudentNumber value in the MaskedTextBox.
                    IQueryable<Registration> registrationsListing = db.Registrations.Where(x => x.StudentId == student.StudentId);

                    if (registrationsListing.Any())
                    {
                        // Set the DataSource property of the BindingSource object representing the Registration controls to the result set of the Registration query.
                        registrationBindingSource.DataSource = registrationsListing.ToList();

                        // If the Registration object of ConstructorData is not null
                        if (this.constructorData.registration != null)
                        {
                            // Set the RegistrationNumber Combobox’s text property to the value of the Registration Number returned to this form.
                            registrationNumberComboBox.Text = this.constructorData.registration.RegistrationNumber.ToString();
                        }

                        // Enabling the link labels when student number is populated.
                        lnkUpdateGrade.Enabled = true;
                        lnkViewDetails.Enabled = true;
                    }
                    else
                    {
                        // Disable link labels at the bottom of the form
                        lnkUpdateGrade.Enabled = false;
                        lnkViewDetails.Enabled = false;

                        // Clear the Registration BindingSource object such that any previous results do not remain on the form
                        registrationBindingSource.DataSource = typeof(Registration);
                    }

                }
                else
                {
                    // Disable link labels at the bottom of the form
                    lnkUpdateGrade.Enabled = false;
                    lnkViewDetails.Enabled = false;

                    // Set focus back to the MaskedTextBox control.
                    studentNumberMaskedTextBox.Focus();

                    // Display a MessageBox to the end user indicatingthat the student number entered does not exist
                    MessageBox.Show($"Student {studentNumber} does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Clear Student BindingSource object such that any previous results do not remain on the form.
                    studentBindingSource.DataSource = typeof(Student);
                }
            }
        }

        private void grpRegistration_Enter(object sender, EventArgs e)
        {

        }
    }
}
