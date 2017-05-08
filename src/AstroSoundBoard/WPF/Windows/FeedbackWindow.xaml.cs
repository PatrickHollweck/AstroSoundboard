// ****************************** Module Header ****************************** //
//
//
// Last Modified: 29:04:2017 / 20:53
// Creation: 29:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="FeedbackWindow.xaml.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.WPF.Windows
{
    using System.Reflection;
    using System.Windows;

    using AstroSoundBoard.Core.Objects;

    using log4net;

    using SharpRaven;
    using SharpRaven.Data;

    public partial class FeedbackWindow : Window
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
                        Level = ErrorLevel.Info,
                        Message = $"USER ISSUE REPORT: {IssueTitle.Text} \nKIND: {IssueKind.Text} \n\nDESCRIPTION: \n{IssueDescription.Text} \n \nContact: \n{IssueContact.Text}"
                    };

                    new RavenClient(Credentials.SentryApiKey).Capture(data);

                    IssueStatus.Content = "Issue Sent! - Thanks for the Feedback";

                    IssueTitle.Text = string.Empty;
                    IssueDescription.Text = string.Empty;
                    IssueContact.Text = string.Empty;
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

                    new RavenClient(Credentials.SentryApiKey).Capture(data);

                    FeatureStatus.Content = "Issue Sent! - Thanks for the Feedback";

                    FeatureTitle.Text = string.Empty;
                    FeatureDescription.Text = string.Empty;
                    FeatureContact.Text = string.Empty;
                }
                else
                {
                    FeatureStatus.Content = "Please atlease fill the Title and the Description!";
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

                    new RavenClient(Credentials.SentryApiKey).Capture(data);

                    IssueStatus.Content = "Issue Sent! - Thanks for the Feedback";

                    SoundTitle.Text = string.Empty;
                    SoundDescription.Text = string.Empty;
                    SoundContact.Text = string.Empty;
                }
                else
                {
                    SoundStatus.Content = "Please atlease fill the Title and the Description!";
                }
            }
            catch
            {
                SoundStatus.Content = "Error while sending... Try again...";
            }
        }
    }
}