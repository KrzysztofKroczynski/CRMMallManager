using Ardalis.Result;
using Ardalis.SharedKernel;
using Shared.Core.Entities;

namespace Shared.UseCases.GetUserState;

public sealed record GetEditedLeaseApplicationStateQuery(string leaseApplicationId) : IQuery<Result<UserState<LeaseApplication>>>;