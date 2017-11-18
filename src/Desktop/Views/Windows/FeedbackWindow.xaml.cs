// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 14:40
// Creation: 18:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="FeedbackWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Views.Windows
{
    using System.Windows;

    using AstroSoundBoard.Objects;

    using SharpRaven;
    using SharpRaven.Data;

    public partial class FeedbackWindow : Window
    {
        public FeedbackWindow()
        {
            InitializeComponent();
        }

        private void IssueSubmit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(IssueTitle.Text) && !string.IsNullOrWhiteSpace(IssueDescription.Text))
                {
                    var data = new SentryEvent("USER ISSUE REPORT!")
                    {
                        Level = ErrorLevel.Warning,
                        Message = $"USER ISSUE REPORT: {IssueTitle.Text} \nKIND: {IssueKind.Text} \n\nDESCRIPTION: \n{IssueDescription.Text} \n \nContact: \n{IssueContact.Text}"
                    };

                    data.Tags.Add("ReportType", "Issue");

                    new RavenClient(Credentials.SentryApiKey).Capture(data);

                    IssueStatus.Content = "Issue Sent! - Thanks for the Feedback";
                    ResetInterface();
                }
                else
                {
                    IssueStatus.Content = "Please at least fill the Title and the Description!";
                }
            }
            catch
            {
                IssueStatus.Content = "Error while sending... Try again...";
            }
        }

        private void FeatureSubmit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(FeatureTitle.Text) && !string.IsNullOrWhiteSpace(FeatureDescription.Text))
                {
                    var data = new SentryEvent("USER FEATURE REQUEST!")
                    {
                        Level = ErrorLevel.Info,
                        Message = $"USER FEATURE REQUEST: {FeatureTitle.Text} \nDESCRIPTION:\n{FeatureDescription.Text} \n \nContact: \n{FeatureContact.Text}"
                    };

                    data.Tags.Add("ReportType", "Feature");

                    new RavenClient(Credentials.SentryApiKey).Capture(data);

                    FeatureStatus.Content = "Issue Sent! - Thanks for the Feedback";
                    ResetInterface();
                }
                else
                {
                    FeatureStatus.Content = "Please at least fill the Title and the Description!";
                }
            }
            catch
            {
                FeatureStatus.Content = "Error while sending... Try again...";
            }
        }

        private void SoundSubmit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(SoundTitle.Text) && !string.IsNullOrWhiteSpace(SoundDescription.Text))
                {
                    var data = new SentryEvent("USER SOUND REQUEST!")
                    {
                        Level = ErrorLevel.Info,
                        Message = $"USER SOUND REQUEST: {SoundTitle.Text} \nDESCRIPTION:\n{SoundDescription.Text} \n \nContact: \n{SoundContact.Text}"
                    };

                    data.Tags.Add("ReportType", "Sound");

                    new RavenClient(Credentials.SentryApiKey).Capture(data);

                    IssueStatus.Content = "Issue Sent! - Thanks for the Feedback";
                    ResetInterface();
                }
                else
                {
                    SoundStatus.Content = "Please at least fill the Title and the Description!";
                }
            }
            catch
            {
                SoundStatus.Content = "Error while sending... Try again...";
            }
        }

        private void ResetInterface()
        {
            FeatureTitle.Text = string.Empty;
            FeatureDescription.Text = string.Empty;
            FeatureContact.Text = string.Empty;

            SoundTitle.Text = string.Empty;
            SoundDescription.Text = string.Empty;
            SoundContact.Text = string.Empty;

            IssueTitle.Text = string.Empty;
            IssueDescription.Text = string.Empty;
            IssueContact.Text = string.Empty;
        }
    }
}