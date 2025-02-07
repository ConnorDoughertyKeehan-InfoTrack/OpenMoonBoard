﻿using MediatR;
using OpenMoonBoard.Application.Features.ActualMoonBoardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoonBoard.Application.Features.ActualMoonBoardApi.Commands.SyncRavioliBicepsAndHoseokLeesLogBookSetters;
public class SyncRBicepsAndHLeesLogBookSettersCommand : IRequest<Unit>
{
    public required MoonBoardCredentials Credentials { get; set; }
}
