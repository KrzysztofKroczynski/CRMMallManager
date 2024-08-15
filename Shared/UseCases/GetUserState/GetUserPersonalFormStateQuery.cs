using Ardalis.Result;
using Ardalis.SharedKernel;
using Shared.Web.FormModels;

namespace Shared.UseCases.GetUserState;

public sealed record GetUserPersonalFormStateQuery(string UserId) : IQuery<Result<UserState<PersonalForm>>>;