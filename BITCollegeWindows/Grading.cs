using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace BITCollegeWindows
{
    public partial class Grading : Form
    {

        // Create an Object of the service type then instantiate
        BITCollegeService.CollegeRegistrationClient service =
            new BITCollegeService.CollegeRegistrationClient();

        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;


        /// <summary>
        /// given:  This constructor will be used when called from the
        /// Student form.  This constructor will receive 
        /// specific information about the student and registration
        /// further code required:  
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public Grading(ConstructorData constructor)
        {
            InitializeComponent();

            // Populate the Grading form’s constructorData object with the corresponding data received in the constructor.
            this.constructorData = constructor;

            // Populate the upper controls with the corresponding data received in the constructor.
            this.studentNumberLabel1.Text = this.constructorData.student.StudentNumber.ToString();
            this.descriptionLabel1.Text = this.constructorData.student.AcademicProgram.Description;
            this.fullNameLabel1.Text = this.constructorData.student.FullName;

            // Populate the upper controls with the corresponding data received in the constructor.
            this.courseNumberLabel1.Text = this.constructorData.registration.Course.CourseNumber;
            this.courseTypeLabel1.Text = this.constructorData.registration.Course.CourseType;
            this.titleLabel1.Text = this.constructorData.registration.Course.Title;
            if (this.gradeTextBox.Text != null && this.constructorData.registration.Grade.HasValue)
            {
                this.gradeTextBox.Text = this.constructorData.registration.Grade.Value.ToString("P2");
            }

        }

        /// <summary>
        /// given: This code will navigate back to the Student form with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to student with the data selected for this form
            StudentData student = new StudentData(constructorData);
            student.MdiParent = this.MdiParent;
            student.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Always open in this form in the top right corner of the frame.
        /// further code required:
        /// </summary>
        private void Grading_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            // Initially set the “Existing grades cannot…” label as invisible.
            lblExisting.Enabled = false;

            // Initially set the Update LinkLabel as disabled.
            lnkUpdate.Enabled = false;

            if (this.constructorData != null && this.constructorData.registration != null)
            {
                // If a grade has previously been entered:
                if (this.constructorData.registration.Grade != null)
                {
                    // Disable the Grade Textbox.
                    this.gradeTextBox.Enabled = false;

                    // Disable the Update LinkLabel.
                    this.lnkUpdate.Enabled = false;

                    // Make visible the label indicating that grading is not possible
                    this.lblExisting.Enabled = true;
                }

                // No grade has been previously entered:
                else
                {

                    // Enable Grade TextBox
                    this.gradeTextBox.Enabled = true;

                    // Enable the Update Linklabel
                    this.lnkUpdate.Enabled = true;

                    // Make invisible the label indicating that grading is not possible
                    this.lblExisting.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handles the logic for updating a student grade
        /// </summary>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Define a String which will contain the value from the TextBox without the percent formatting.
            string formattedGradeString = Numeric.ClearFormatting(gradeTextBox.Text, "%");

            // Use given functionality in the Utility project’s Numeric class to ensure that the above value is numeric.
            if (Numeric.IsNumeric(formattedGradeString, System.Globalization.NumberStyles.AllowDecimalPoint))
            {
                // Divide the above value by 100 and ensure this value is within the 0 - 1 range of numeric values.
                if (int.TryParse(formattedGradeString, out int formattedGradeNumber))
                {
                    double grade = (double) formattedGradeNumber / 100;

                    if (grade >= 0 && grade <= 1)
                    {
                        service.UpdateGrade(grade, this.constructorData.registration.RegistrationId, this.constructorData.registration.Notes);

                        gradeTextBox.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a decimal format of the grade.");
                    }
                }
            }
            else
            {
                // Display an appropriate MessageBox to the end user and do not proceed with the update.
                MessageBox.Show("The grade format entered is not supported. Grade update not completed.");
            }
        }

        private void lblExisting_Click(object sender, EventArgs e)
        {

        }
    }
}
