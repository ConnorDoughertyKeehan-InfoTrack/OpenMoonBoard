using MediatR;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Client.Responses;
using OpenMoonBoard.Domain.Interfaces;
using OpenMoonBoard.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Commands.SyncRavioliBicepsAndHoseokLeesLogBookSetters;
public class SyncRBicepsAndHLeesLogBookSettersCommandHandler(
    IMoonBoardClient moonBoardClient,
    ISettersRepository settersRepository
    ) : IRequestHandler<SyncRBicepsAndHLeesLogBookSettersCommand, Unit>
{
    public async Task<Unit> Handle(SyncRBicepsAndHLeesLogBookSettersCommand command, CancellationToken cancellationToken)
    {
        //Ben Moon: 3069743d-2452-4b75-a074-dea06979a4e7
        //Ravioli Biceps: E786F934-B0FF-4422-98A5-716DBAFDC7FB
        //Hoseok Lee: F3484E0C-4620-41C8-BCC3-0114563AB1AA
        List<string> setterIds = [
            "3069743d-2452-4b75-a074-dea06979a4e7",
            "E786F934-B0FF-4422-98A5-716DBAFDC7FB",
            "F3484E0C-4620-41C8-BCC3-0114563AB1AA"
        ];

        List<Setter> setters = [];

        foreach (string setterId in setterIds) {
            var settersReponse = await moonBoardClient.GetAllSettersFromUserLogBook(command.Credentials, setterId);
            List<Setter> settersEntities = settersReponse.Select(x => new Setter { 
                SetterIdentifier = x.Id, 
                Name = x.Nickname,
                Synced = false
            }).ToList();

            //Actually do the insert to the DB for these setters
            await settersRepository.InsertSetters(settersEntities);
        }

        return Unit.Value;
    }
}