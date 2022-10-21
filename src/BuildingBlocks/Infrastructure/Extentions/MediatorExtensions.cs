using Contracts.Common.Events;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extentions
{
    public static class MediatorExtensions
    {
        //public static async Task DispatchDomainEventAsync(this IMediator mediator, 
        //    DbContext context, ILogger logger)
        // {
        //     var domainEntities = context.ChangeTracker.Entries<IEventEntity>()
        //         .Select(x=>x.Entity).Where(x=>x.DomainEvents().Any()).ToList();
        //     var domainEvents = domainEntities.SelectMany(x => x.DomainEvents()).ToList();
        //     domainEntities.ForEach(x => x.ClearDomainEvent());

        public static async Task DispatchDomainEventAsync(this IMediator mediator,
          List<BaseEvent> domainEvents, ILogger logger)
        {

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
                var data = new SerializeService().Serialize(domainEvent);
                logger.Information($"\n-----\nA domain event has been published!\n" +
                             $"Event: {domainEvent.GetType().Name}\n" +
                             $"Data: {data})\n-----\n");
            }
        }
    }
}
