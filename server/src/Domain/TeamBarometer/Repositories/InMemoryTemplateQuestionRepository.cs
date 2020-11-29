using Domain.TeamBarometer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.TeamBarometer.Repositories
{
	public class InMemoryTemplateQuestionRepository : TemplateQuestionRepository
	{
		private readonly List<TemplateQuestion> questions = new List<TemplateQuestion>();

		public InMemoryTemplateQuestionRepository()
		{
			TemplateQuestion trust = CreateQuestion("Trust",
				"We have the courage to be honest with each other. We don’t hesitate to engage in constructive conflicts.",
				"Members rarely speak their mind. We avoid conflicts. Discussions are tentative and polite.");

			TemplateQuestion collaboration = CreateQuestion("Collaboration",
				"The team cross-pollinates, sharing perspectives, context and innovations with other teams, and other parts of the organization..",
				"Work is done individually. Little or no collaboration within the team or with other teams.");

			TemplateQuestion feedback = CreateQuestion("Feedback",
				"We give positive feedback, but also call out one another’s deficiencies and unproductive behaviors.",
				"We rarely praise each other or give feedback or criticize each other for acting irresponsibly or breaking our Working Agreement.");

			TemplateQuestion meetingEngagement = CreateQuestion("Meeting Engagement",
				"People are engaged in meetings. They want to be there. Discussions are passionate.",
				"Many feels like prisoners in the meeting. Only a few participate in discussions.");

			TemplateQuestion commitment = CreateQuestion("Commitment",
				"We commit to our plans and hold each other accountable for doing our best to reach our goals and execute assigned action points.",
				"We don’t have real consensus about our goals. We don’t really buy in to the plan or follow up that people keep their commitments.");

			TemplateQuestion improving = CreateQuestion("Improving",
				"We passionately strive to figure out how to work better and more efficiently as a team. We try to “know” if we get better.",
				"We don’t focus on questioning our process or way of working. If someone asked us to prove that we’ve gotten better we have no clue how we would demonstrate that.");

			TemplateQuestion mutuallyResponsible = CreateQuestion("Mutually Responsible",
				"We feel mutually responsible for achieving our goals. We win and fail as a team.",
				"When we fail we try to figure out who did what wrong. When we succeed we celebrate individuals. If we pay attention to it at all…");

			TemplateQuestion power = CreateQuestion("Power",
				"We go out of our way to unblock ourselves when we run into impediments or dependencies.",
				"When we run into problems or dependencies we alert managers, ask for their help, and then wait.");

			TemplateQuestion pride = CreateQuestion("Pride",
				"We feel pride in our work and what we accomplish.",
				"We feel ashamed of our pace and the quality of our results.");

			TemplateQuestion relationships = CreateQuestion("Relationships",
				"Team members spend time and effort building strong relationships among themselves, as well with partners outside the team.",
				"We don’t really know each other or what makes others “tick”.");

			TemplateQuestion ownership = CreateQuestion("Ownership",
				"We engage in defining our own goals and take ownership of our destiny.",
				"We act as pawns in a game of chess. We don’t demand involvement in defining our goals and destiny.");

			TemplateQuestion sharing = CreateQuestion("Sharing",
				"We share what we know and learn. No one withholds information that affects the team.",
				"People do stuff under the radar and often forget to share news or relevant information.");

			TemplateQuestion boostsEachOther = CreateQuestion("Boosts each other",
				"We unleash each other’s passion and care for each other’s personal development. We leverage our differences.",
				"We don’t know in which areas people want to grow. We have trouble collaborating since we are very different and view things differently.");

			TemplateQuestion loyalty = CreateQuestion("Loyalty",
				"No one has hidden agendas. We feel that everyone’s loyalty is with THIS team.",
				"The team feels like a diverse group of people with different goals and loyalties that lies elsewhere.");

			TemplateQuestion passion = CreateQuestion("Passion",
				"Each member wants THIS team to be great and successful.",
				"People just come to work for 8 hours and focus on their own tasks.");

			TemplateQuestion integrity = CreateQuestion("Integrity",
				"We honor our processes and working agreements even when we are put under pressure.",
				"Our behaviors, collaboration and communication fall apart when we get stressed.");

			questions.Add(trust);
			questions.Add(collaboration);
			questions.Add(feedback);
			questions.Add(meetingEngagement);
			questions.Add(commitment);
			questions.Add(improving);
			questions.Add(mutuallyResponsible);
			questions.Add(power);
			questions.Add(pride);
			questions.Add(relationships);
			questions.Add(ownership);
			questions.Add(sharing);
			questions.Add(boostsEachOther);
			questions.Add(loyalty);
			questions.Add(passion);
			questions.Add(integrity);
		}

		public IEnumerable<TemplateQuestion> GetAll()
		{
			return questions;
		}

		private TemplateQuestion CreateQuestion(string name, string greenDescription, string redDescription)
		{
			Dictionary<Answer, string> descriptionByAnswer = new Dictionary<Answer, string>
			{
				{ Answer.Red, redDescription },
				{ Answer.Green, greenDescription}
			};

			return new TemplateQuestion(name, descriptionByAnswer);
		}
	}
}
