// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.Dashboard.Extensions;

namespace Aspire.Dashboard.Model;

public abstract class ResourceViewModel
{
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
    public required string Uid { get; init; }
    public string? State { get; init; }
    public DateTime? CreationTimeStamp { get; init; }
    public List<EnvironmentVariableViewModel> Environment { get; } = new();
    public required ILogSource LogSource { get; init; }
    public List<string> Endpoints { get; } = new();
    public List<ResourceService> Services { get; } = new();
    public int? ExpectedEndpointsCount { get; init; }
    public abstract string ResourceType { get; }

    public static string GetResourceName(ResourceViewModel resource, IEnumerable<ResourceViewModel> allResources)
    {
        var count = 0;
        foreach (var item in allResources)
        {
            if (item.DisplayName == resource.DisplayName)
            {
                count++;
                if (count >= 2)
                {
                    return ResourceFormatter.GetName(resource.DisplayName, resource.Uid);
                }
            }
        }

        return resource.DisplayName;
    }

    internal virtual bool MatchesFilter(string filter)
    {
        return Name.Contains(filter, StringComparisons.UserTextSearch);
    }
}

public sealed class ResourceService(string name, string? allocatedAddress, int? allocatedPort)
{
    public string Name { get; } = name;
    public string? AllocatedAddress { get; } = allocatedAddress;
    public int? AllocatedPort { get; } = allocatedPort;
    public string AddressAndPort { get; } = $"{allocatedAddress}:{allocatedPort}";
}
