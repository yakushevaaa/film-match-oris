﻿using MediatR;

namespace FilmMatch.Domain.Entities.Common;

public abstract class BaseEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}