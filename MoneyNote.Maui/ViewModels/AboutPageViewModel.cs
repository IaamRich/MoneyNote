namespace MoneyNote.Maui;

public sealed class AboutPageViewModel : BaseViewModel
{
    private string _actionStatus;

    public AboutPageViewModel(LegacyLocalizationService localizationService)
    {
        Title = localizationService["about_title"];
        AppName = localizationService["app_name"];
        VersionText = localizationService["about_version"];
        CreatedByText = $"{localizationService["created_by"]} {localizationService["creator_name"]}.";
        SupportedByText = $"{localizationService["supported_by"]} {localizationService["supporter_name"]}.";
        PlatformsText = $"{localizationService["about_platforms"]} {localizationService["platforms_available"]}.";
        EnvironmentText = $"{localizationService["environment"]} {localizationService["company_name"]}";
        TermsTitle = localizationService["terms"];
        TermsBody = "This MAUI slice focuses on visual parity first and keeps legal, telemetry-free, and persistence-aware behavior deferred.";
        PolicyTitle = localizationService["policy"];
        PolicyBody = "No new analytics, sync, or storage migration was added in this migration slice.";
        LicenseTitle = localizationService["license"];
        LicenseBody = "TODO: port the real legal text and destination links after the shared content services are migrated.";
        FooterText = localizationService["about_footer"];
        LearnMoreText = localizationService["learn_more"];
        _actionStatus = localizationService["in_developing"];

        LearnMoreCommand = new Command(() => ActionStatus = "TODO: reconnect the original external links when the MAUI about flow is finalized.");
        VisitCompanyCommand = new Command(() => ActionStatus = "TODO: reconnect company and support links from the legacy about page.");
    }

    public string Title { get; }

    public string AppName { get; }

    public string VersionText { get; }

    public string CreatedByText { get; }

    public string SupportedByText { get; }

    public string PlatformsText { get; }

    public string EnvironmentText { get; }

    public string TermsTitle { get; }

    public string TermsBody { get; }

    public string PolicyTitle { get; }

    public string PolicyBody { get; }

    public string LicenseTitle { get; }

    public string LicenseBody { get; }

    public string FooterText { get; }

    public string LearnMoreText { get; }

    public Command LearnMoreCommand { get; }

    public Command VisitCompanyCommand { get; }

    public string ActionStatus
    {
        get => _actionStatus;
        private set => SetProperty(ref _actionStatus, value);
    }
}
