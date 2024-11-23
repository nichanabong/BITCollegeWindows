using BITCollege_NN.Data;
using BITCollege_NN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class History : Form
    {
        // Instantiate an object of your BITCollege_FLContext class for use throughout this form.
        BITCollege_NNContext db = new BITCollege_NNContext();

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
        public History(ConstructorData constructorData)
        {
            InitializeComponent();

            if (constructorData == null || constructorData.registration == null)
            {
                MessageBox.Show("ConstructorData or its associated data is null. Please ensure all values are initialized.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            //Assign the passed data to the instance variable.
            this.constructorData = constructorData;

            // Populate the form with data.
            this.studentNumberLabel1.Text = constructorData.registration.Student.StudentNumber.ToString();
            this.descriptionLabel1.Text = constructorData.registration.Course.AcademicProgram.Description;
            this.fullNameLabel1.Text = constructorData.registration.Student.FullName;
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
        /// given:  Open this form in top right corner of the frame.
        /// further code required:
        /// </summary>
        private void History_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            // Define a LINQ-to-SQL Server query selecting data from the Registrations and Courses tables whose StudentId corresponds to Student passed to this form.
            try
            {
                // Ensure constructorData.registration and its properties are valid
                if (constructorData.registration == null || constructorData.registration.Student == null)
                {
                    MessageBox.Show(
                        "No valid registration or student data is available.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                var innerJoinQuery =
                    from registration in db.Registrations
                    join course in db.Courses on registration.CourseId equals course.CourseId
                    where registration.StudentId == constructorData.registration.Student.StudentId
                    select new
                    {
                        RegistrationNumber = registration.RegistrationNumber,
                        RegistrationDate = registration.RegistrationDate,
                        Course = registration.Course.Title,
                        Grade = registration.Grade,
                        Notes = registration.Notes
                    };

                if (!innerJoinQuery.Any())
                {
                    MessageBox.Show(
                        "No registration data found for this student.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Proceed with binding only if data exists.
                registrationBindingSource.DataSource = innerJoinQuery.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while loading student data:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
    }
}
