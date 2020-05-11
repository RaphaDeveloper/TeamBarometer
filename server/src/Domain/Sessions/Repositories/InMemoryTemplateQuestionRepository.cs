using Domain.Sessions.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Sessions.Repositories
{
	public class InMemoryTemplateQuestionRepository : TemplateQuestionRepository
	{
		private readonly List<TemplateQuestion> questions = new List<TemplateQuestion>();

		public InMemoryTemplateQuestionRepository()
		{
			TemplateQuestion confianca = CreateQuestion("Confiança",
				"Raramente dizemos o que nós pensamos. Nós preferimos evitar conflitos e não nos expor.",
				"Temos a coragem de ser honestos com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.");

			TemplateQuestion teamCollaboration = CreateQuestion("Colaboração entre times",
				"Nosso trabalho é individual. Colaboramos pouco ou nada dentro do time.",
				"Trabalhamos em equipe. Nós trocamos conhecimentos, pontos de vista e idéias dentro do time.");

			TemplateQuestion feedback = CreateQuestion("Feedback",
				"Nós raramente nos elogiamos ou chamamos a atenção quando alguém age de forma irresponsável ou quenbra nossos acordos de trabalho.",
				"Nós damos feedback regularmente sobre aspectos positivos e também sobre aspectos a melhorar.");

			TemplateQuestion participatoryMeetings = CreateQuestion("Reuniões Participativas",
				"Muitos se sentem como prisioneiros na reunião. Poucos participam das discussões.",
				"Participamos ativamente nas reuniões. Queremos estar nelas e nos envolvemos interessadamente nas conversas.");

			TemplateQuestion commitment = CreateQuestion("Compromisso",
				"Não existe um consenso real sobre nossos objetivos. Não acreditamos no planejamento ou não estamos comprometidos com ele.",
				"Nos comprometemos com o planejamento e contamos com os outros para realizar as tarefas. Damos nosso melhor para alcançar nossos objetivos.");

			TemplateQuestion continuousImprovement = CreateQuestion("Melhora Contínua",
				"Não questionamos nossa forma de trabalho. Não podemos comprovar ou demonstrar se estamos melhorando.",
				"Nos esforçamos para descobrir melhores formas de trabalhar em equipe. Nos interessamos em sabe se estamos melhorando.");

			TemplateQuestion mutualResponsibility = CreateQuestion("Responsabilidade Mútua",
				"Buscamos culpados quando falhamos. Diante do sucesso o trabalho individual é mais reconhecido que o trabalho em equipe.",
				"Nos sentimos mutuamente responsáveis por alcançar nossos objetivos. Triunfamos e fracassamos como uma equipe.");

			TemplateQuestion autonomy = CreateQuestion("Autonomia",
				"Quando existe algum impedimento, nos limitamos à alertar dos problemas.",
				"Agimos diante dos problemas e impedimentos com autonomia.");

			TemplateQuestion pride = CreateQuestion("Orgulho",
				"Não nos sentimos orgulhosos do nosso ritmo de trabalho, da forma como trabalhamos ou dos resultados que alcançamos.",
				"Nos sentimos orgulhosos do trabalho que fazemos, da forma como fazemos e o dos resultados que alcançamos.");

			TemplateQuestion relationships = CreateQuestion("Relações",
				"Não conhecemos realmente os interesses, objetivos e motivações dos nossos companheiros de equipe.",
				"Investimos tempo e esforço em alimentar relações interpessoais, dentro e fora da equipe.");

			TemplateQuestion Protagonism = CreateQuestion("Protagonismo",
				"Nos sentimos como peões. Não somos convidados à participar da definição de nossos objetivos.",
				"Participamos ativamente da definição de nossos objetivos. Somos protagonistas do nosso destino.");

			TemplateQuestion sharing = CreateQuestion("Compartilhamento",
				"Alguns se omitem de compartilhar as novidades ou as informações importantes. Não sabemos no que algumas pessoas estão trabalhando.",
				"Compartilhamos toda informação relevante e as novidades quando tomamos conhecimento.");

			TemplateQuestion empowerOurselves = CreateQuestion("Nos Capacitar",
				"Desconhecemos os interesses pessoais de nossos colegas. Encontramos problemas de colaboração devido às nossas diferenças e pontos de vista.",
				"Nos interessamos pelas motivações e desenvolvimento pessoal de nossos colegas. Aproveitamos nossas diferenças para nos capacitar.");

			TemplateQuestion loyalty = CreateQuestion("Lealdade",
				"Nos sentimos como um grupo de pessoas com objetivos distintos. A fidelidade está fora da equipe.",
				"Somos leais ao time, às confidencialidades de nossos colegas e nossos objetivos.");

			questions.Add(confianca);
			questions.Add(teamCollaboration);
			questions.Add(feedback);
			questions.Add(participatoryMeetings);
			questions.Add(commitment);
			questions.Add(continuousImprovement);
			questions.Add(mutualResponsibility);
			questions.Add(autonomy);
			questions.Add(pride);
			questions.Add(relationships);
			questions.Add(Protagonism);
			questions.Add(sharing);
			questions.Add(empowerOurselves);
			questions.Add(loyalty);
		}

		public IEnumerable<TemplateQuestion> GetAll()
		{
			return questions;
		}

		private TemplateQuestion CreateQuestion(string name, string redDescription, string greenDescription)
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
