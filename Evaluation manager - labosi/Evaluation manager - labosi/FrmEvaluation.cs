using Evaluation_manager___labosi.Models;
using Evaluation_manager___labosi.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluation_manager___labosi
{
    public partial class FrmEvaluation : Form
    {
        private Student student; // Student koji se prikazuje u ovoj formi

        public Student SelectedStudent { get => student; set => student = value; }

        public FrmEvaluation(Student selectedStudent)
        {
            InitializeComponent();
            student = selectedStudent;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Nek ostane ovo
        }

        private void FrmEvaluation_Load(object sender, EventArgs e)
        {
            SetFormText(); // Postavlja naziv forme kao ime studenta
            var activities = ActivityRepository.GetActivities();
            cboActivities.DataSource = activities;
        }

        private void SetFormText()
        {
            Text = student.FirstName + " " + student.LastName;
        }

        private void cboActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentActivity = cboActivities.SelectedItem as Activity;
            txtActivityDescription.Text = currentActivity.Description;
            txtMinForGrade.Text = currentActivity.MinPointsForGrade + "/" + currentActivity.MaxPoints;
            txtMinForSignature.Text = currentActivity.MinPointsForSignature + "/" + currentActivity.MaxPoints;

            numPoints.Minimum = 0;
            numPoints.Maximum = currentActivity.MaxPoints;

            var evaluation = EvaluationRepository.GetEvaluation(SelectedStudent, currentActivity);
            if (evaluation != null)
            {
                txtTeacher.Text = evaluation.Evaluator.ToString();
                txtEvaluationDate.Text = evaluation.EvaluationDate.ToString();
                numPoints.Value=evaluation.Points;
            }
            else
            {
                txtTeacher.Text = FrmLogin.LoggedTeacher.ToString();
                txtEvaluationDate.Text = "-";
                numPoints.Value = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var activity=cboActivities.SelectedItem as Activity;
            var teacher = FrmLogin.LoggedTeacher;

            int points = (int)numPoints.Value;

            teacher.PerformEvaluation(SelectedStudent, activity, points);
            Close();
        }
    }
}
