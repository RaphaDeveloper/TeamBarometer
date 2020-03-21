using Moq;
using NUnit.Framework;
using System;

namespace DomainEventManager.Test
{
	public class DomainEventShould
	{
		private readonly Mock<IServiceProvider> serviceProviderMock;

		public DomainEventShould()
		{
			serviceProviderMock = new Mock<IServiceProvider>();
			serviceProviderMock.Setup(s => s.GetService(typeof(FirstTestHandler))).Returns(new FirstTestHandler());
			serviceProviderMock.Setup(s => s.GetService(typeof(SecondTestHandler))).Returns(new SecondTestHandler());
		}

		[Test]
		public void DispatchEventWhenABindExists()
		{
			DomainEvent.Bind<TestEvent, FirstTestHandler>(serviceProviderMock.Object);

			DomainEvent.Dispatch(new TestEvent());

			Assert.True(FirstTestHandler.WasCalled);
		}

		[Test]
		public void BindMoreThanOneHandlerToTheSameEvent()
		{
			DomainEvent.Bind<TestEvent, FirstTestHandler>(serviceProviderMock.Object);
			DomainEvent.Bind<TestEvent, SecondTestHandler>(serviceProviderMock.Object);

			DomainEvent.Dispatch(new TestEvent());

			Assert.True(FirstTestHandler.WasCalled);
			Assert.True(SecondTestHandler.WasCalled);
		}

		[Test]
		public void NotDispatchEventWhenABindNotExists()
		{
			DomainEvent.Dispatch(new TestEvent());

			Assert.False(ThirdTestHandler.WasCalled);
		}
	}	

	public class FirstTestHandler : Handler<TestEvent>
	{
		public static bool WasCalled { get; set; }

		public override void Handle(TestEvent domainEvent)
		{
			WasCalled = true;
		}
	}

	public class SecondTestHandler : Handler<TestEvent>
	{
		public static bool WasCalled { get; set; }

		public override void Handle(TestEvent domainEvent)
		{
			WasCalled = true;
		}
	}

	public class ThirdTestHandler : Handler<TestEvent>
	{
		public static bool WasCalled { get; set; }

		public override void Handle(TestEvent domainEvent)
		{
			WasCalled = true;
		}
	}

	public class TestEvent
	{

	}
}