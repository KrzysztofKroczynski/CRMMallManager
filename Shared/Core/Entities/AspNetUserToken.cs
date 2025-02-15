﻿namespace Shared.Core.Entities;

public sealed partial class AspNetUserToken
{
    public string UserId { get; set; } = null!;

    public string LoginProvider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public AspNetUser User { get; set; } = null!;
}
