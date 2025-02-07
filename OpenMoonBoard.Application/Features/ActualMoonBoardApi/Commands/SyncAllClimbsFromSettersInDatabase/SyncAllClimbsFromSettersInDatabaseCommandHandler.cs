using MediatR;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
using OpenMoonBoard.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMoonBoard.Domain.Interfaces;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Commands.SyncAllClimbsFromSettersInDatabase;
public class SyncAllClimbsFromSettersInDatabaseCommandHandler(
    ISettersRepository settersRepository,
    IMoonBoardClient moonBoardClient,
    IGradesRepository gradesRepository,
    IMoonBoardHoldsRepository moonBoardHoldsRepository,
    IMoonBoardRoutesRepository moonBoardRoutesRepository
    ) : IRequestHandler<SyncAllClimbsFromSettersInDatabaseCommand, Unit>
{
    public async Task<Unit> Handle(SyncAllClimbsFromSettersInDatabaseCommand query, CancellationToken cancellationToken)
    {
        var setters = await settersRepository.GetAllSetters();

        foreach (var setter in setters)
        {
            var problems = await moonBoardClient.GetAllClimbsBySetterIds(query.Credentials, [setter.SetterIdentifier]);
            var routes = await MapApiProblemsToRoute(problems);
            await moonBoardRoutesRepository.AddRoutes(routes);
            await settersRepository.SetSetterToSynced(setter.Id);
        }

        return Unit.Value;
    }

    private async Task<List<MoonBoardRoute>> MapApiProblemsToRoute(List<Problem> problems)
    {
        List<MoonBoardRoute> routes = [];
        foreach (var problem in problems)
        {
            var gradeId = await gradesRepository.GetGradeByName(problem.Grade);
            if (!problem.IsBenchmark)
            {
                //I don't want to insert non-benchmarks for now.
                continue;
            }

            var route = new MoonBoardRoute
            {
                DateAdded = DateTimeOffset.Now,
                Setter = problem.Setter.Nickname,
                GradeId = gradeId!.Id,
                BoardId = 1,
                Name = problem.Name,
                IsBenchmark = problem.IsBenchmark
            };

            List<HoldsUsed> holdsUsed = [];
            bool shouldSkip = false;
            foreach (var move in problem.Moves)
            {
                //Hard coding 2016 board right now.
                var hold = await moonBoardHoldsRepository.GetMoonBoardHoldByPositionAndBoardId(move.Description);

                if (hold == null)
                {
                    //Some climbs people have entered holds that don't exist??? Just ignore this whole route in that case.
                    shouldSkip = true;
                    break;
                };

                holdsUsed.Add(new HoldsUsed
                {
                    MoonBoardHoldId = hold.Id,
                    IsStartHold = move.IsStart,
                    isEndHold = move.IsEnd
                });
            }
            //Don't add the route if we mark the climb as invalid.
            if (shouldSkip) continue;

            route.HoldsUsed = holdsUsed;
            routes.Add(route);
        }

        return routes;
    }
}
