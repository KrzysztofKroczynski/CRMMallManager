using Ardalis.Result;
using Ardalis.SharedKernel;
using Shared.Web;

namespace Shared.UseCases.GetUserState;

public sealed record GetUserPersonalFormStateQuery(string UserId) : IQuery<Result<UserState<PersonalForm>>>;