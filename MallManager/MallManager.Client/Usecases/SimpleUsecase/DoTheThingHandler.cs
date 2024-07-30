using Ardalis.Result;
using Ardalis.SharedKernel;
using Shared.Usecases.SampleUsecase;

namespace MallManager.Client.Usecases.SimpleUsecase;

public class DoTheThingHandler: ICommandHandler<DoTheThingCommand,Result<int>>
{
    public async Task<Result<int>> Handle(DoTheThingCommand request, CancellationToken cancellationToken)
    {
        if (request.I % 2 == 1)
        {
            return Result.NotFound();
        }

        return 2137;
    }
}