using ExpenseTracker.SliceFactory.Models;
using ExpenseTracker.SliceFactory.Components.Pages;

namespace ExpenseTracker.SliceFactory.Services;

/// <summary>
/// Service responsible for managing MAUI Shell navigation routes
/// </summary>
public class NavigationManagementService
{
    private readonly ILogger<NavigationManagementService> _logger;
    private readonly PluralizationService _pluralizationService;

    public NavigationManagementService(
        ILogger<NavigationManagementService> logger,
        PluralizationService pluralizationService)
    {
        _logger = logger;
        _pluralizationService = pluralizationService;
    }

    /// <summary>
    /// Updates MAUI Shell navigation for a newly created feature
    /// Note: MAUI navigation is typically configured in AppShell.xaml manually
    /// This logs guidance for the developer
    /// </summary>
    public async Task UpdateNavigationForFeatureAsync(Feature feature, List<Project> projects)
    {
        try
        {
            if (!feature.HasListing)
            {
                _logger.LogInformation("Feature {ComponentPrefix} has no listing, skipping navigation guidance", feature.ComponentPrefix);
                return;
            }

            var pluralizedRoute = _pluralizationService.Pluralize(feature.ComponentPrefix).ToLowerInvariant();

            _logger.LogInformation(
                "Navigation guidance for feature '{ComponentPrefix}':\n" +
                "Add the following to your MAUI AppShell.xaml:\n" +
                "<ShellContent Title=\"{ComponentPrefix}\" Route=\"{Route}\" ContentTemplate=\"{{DataTemplate local:ProductListPage}}\" />",
                feature.ComponentPrefix, pluralizedRoute);

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate navigation guidance for feature: {ComponentPrefix}", feature.ComponentPrefix);
            throw;
        }
    }

    /// <summary>
    /// Removes navigation entries for a deleted feature
    /// </summary>
    public async Task RemoveNavigationForFeatureAsync(Feature feature, List<Project> projects)
    {
        _logger.LogWarning("Navigation removal requires manual update to AppShell.xaml for feature: {ComponentPrefix}", feature.ComponentPrefix);
        await Task.CompletedTask;
    }
}
