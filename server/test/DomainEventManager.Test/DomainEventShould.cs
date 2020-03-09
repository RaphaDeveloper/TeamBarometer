using NUnit.Framework;

namespace DomainEventManager.Test
{
	public class DomainEventShould
	{
		[Test]
		public void DispatchEventWhenABindExists()
		{
			DomainEvent.Bind<TestEvent, FirstTestHandler>();

			DomainEvent.Dispatch(new TestEvent());

			Assert.True(FirstTestHandler.WasCalled);
		}

		[Test]
		public void BindMoreThanOneHandlerToTheSameEvent()
		{
			DomainEvent.Bind<TestEvent, FirstTestHandler>();
			DomainEvent.Bind<TestEvent, SecondTestHandler>();

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