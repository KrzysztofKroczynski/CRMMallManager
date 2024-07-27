using Ardalis.Result;

namespace Shared.Usecases.SampleUsecase;

public sealed record DoTheThingCommand (int I) : Ardalis.SharedKernel.ICommand<Result<int>>;