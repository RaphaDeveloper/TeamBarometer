using System;
using System.Collections.Generic;

namespace DomainEventManager
{
	public class DomainEvent
	{
		private static Dictionary<Type, List<IHandler>> HandlersByEvent { get; set; } = new Dictionary<Type, List<IHandler>>();

		public static void Bind<TEvent, THandler>(IServiceProvider handlerProvider)
			where THandler : IHandler
		{
			Type eventType = typeof(TEvent);

			if (!HandlersByEvent.TryGetValue(eventType, out List<IHandler> handlers))
			{
				handlers = new List<IHandler>();

				HandlersByEvent.Add(eventType, handlers);
			}

			handlers.Add((IHandler)handlerProvider.GetService(typeof(THandler)));
		}

		public static void Dispatch(object domainEvent)
		{
			if (HandlersByEvent.TryGetValue(domainEvent.GetType(), out List<IHandler> handlers))
			{
				handlers.ForEach(h => h.Handle(domainEvent));
			}
		}
	}
}
