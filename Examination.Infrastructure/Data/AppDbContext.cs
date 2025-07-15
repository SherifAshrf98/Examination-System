using Examination.Domain.Entities;
using Examination.Domain.Entities.Enums;
using Examination.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
			SeedData(modelBuilder);
		}

		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<QuestionOption> QuestionOptions { get; set; }
		public DbSet<Exam> Exams { get; set; }
		public DbSet<ExamQuestion> ExamQuestions { get; set; }
		public DbSet<ExamSubmission> Submissions { get; set; }
		public DbSet<SubmissionAnswer> SubmissionAnswers { get; set; }
		public DbSet<StudentSubject> StudentSubjects { get; set; }

		private void SeedData(ModelBuilder modelBuilder)
		{
			var subjects = new List<Subject>
			{
			new Subject { Id = 1, Name = "Mathematics" },
			new Subject { Id = 2, Name = "Physics" },
			new Subject { Id = 3, Name = "Chemistry" }

			};
			modelBuilder.Entity<Subject>().HasData(subjects);

			var questions = new List<Question>();
			var questionOptions = new List<QuestionOption>();
			int questionId = 1;
			int optionId = 1;

			var mathQuestions = new List<(string Text, string[] Options, int CorrectOption, DifficultyLevel Difficulty)>
		{
			("What is the derivative of x^2?", new[] { "2x", "x", "x^2", "2x^2" }, 0, DifficultyLevel.Easy),
			("What is the integral of 3x^2?", new[] { "x^3 + C", "3x^3 + C", "x^2 + C", "3x + C" }, 0, DifficultyLevel.Medium),
			("What is the Pythagorean theorem?", new[] { "a^2 + b^2 = c^2", "a^2 - b^2 = c^2", "a + b = c", "a^2 = b^2 + c^2" }, 0, DifficultyLevel.Easy),
			("What is the derivative of sin(x)?", new[] { "cos(x)", "-sin(x)", "sin(x)", "-cos(x)" }, 0, DifficultyLevel.Medium),
			("Solve: x^2 - 4 = 0", new[] { "x = ±2", "x = ±4", "x = 2", "x = 0" }, 0, DifficultyLevel.Medium),
			("Solve for x: 2x + 3 = 7", new[] { "x = 3", "x = 2", "x = 4", "x = 1" }, 1, DifficultyLevel.Easy),
			("What is 2^3?", new[] { "6", "8", "4", "9" }, 1, DifficultyLevel.Easy),
			("What is the slope of y = 2x + 3?", new[] { "3", "2", "1", "0" }, 1, DifficultyLevel.Easy),
			("What is the area of a circle with radius 3?", new[] { "6π", "9π", "3π", "12π" }, 1, DifficultyLevel.Medium),
			("What is the limit of 1/x as x approaches infinity?", new[] { "1", "0", "∞", "-∞" }, 1, DifficultyLevel.Hard),
			("What is the value of sin(π/2)?", new[] { "0", "-1", "1", "1/2" }, 2, DifficultyLevel.Medium),
			("What is cos(0)?", new[] { "0", "-1", "1", "1/2" }, 2, DifficultyLevel.Easy),
			("What is 5! (factorial)?", new[] { "60", "100", "120", "24" }, 2, DifficultyLevel.Medium),
			("What is the sum of angles in a triangle?", new[] { "360°", "90°", "180°", "270°" }, 2, DifficultyLevel.Easy),
			("What is the quadratic formula?", new[] { "x = (-b ± √(b^2 + 4ac))/(2a)", "x = (b ± √(b^2 - 4ac))/(2a)", "x = (-b ± √(b^2 - 4ac))/(2a)", "x = (-b ± √(b^2 - 4ac))/a" }, 2, DifficultyLevel.Medium),
			("What is the value of log_2(8)?", new[] { "2", "4", "1", "3" }, 3, DifficultyLevel.Medium),
			("What is the area of a square with side 5?", new[] { "20", "15", "30", "25" }, 3, DifficultyLevel.Easy),
			("Solve: 3x - 9 = 0", new[] { "x = 2", "x = 4", "x = 1", "x = 3" }, 3, DifficultyLevel.Easy),
			("What is tan(π/4)?", new[] { "0", "√2", "1/√2", "1" }, 3, DifficultyLevel.Medium),
			("What is the sum of the first 5 positive integers?", new[] { "10", "20", "25", "15" }, 3, DifficultyLevel.Easy)
		};

			var physicsQuestions = new List<(string Text, string[] Options, int CorrectOption, DifficultyLevel Difficulty)>
		{
			("What is the SI unit of force?", new[] { "Newton", "Joule", "Watt", "Pascal" }, 0, DifficultyLevel.Easy),
			("What is Ohm's Law?", new[] { "V = IR", "I = VR", "R = VI", "V = I/R" }, 0, DifficultyLevel.Easy),
			("What is the law of universal gravitation?", new[] { "F = G(m1m2)/r^2", "F = ma", "E = mc^2", "V = IR" }, 0, DifficultyLevel.Medium),
			("What is Newton's second law?", new[] { "F = ma", "F = Gm1m2/r^2", "V = IR", "E = mc^2" }, 0, DifficultyLevel.Easy),
			("What is the formula for potential energy?", new[] { "mgh", "1/2 mv^2", "F = ma", "P = W/t" }, 0, DifficultyLevel.Easy),
			("What is the acceleration due to gravity on Earth?", new[] { "10 m/s^2", "9.8 m/s^2", "8 m/s^2", "12 m/s^2" }, 1, DifficultyLevel.Easy),
			("What is the formula for kinetic energy?", new[] { "mv^2", "1/2 mv^2", "mgh", "1/2 mv" }, 1, DifficultyLevel.Medium),
			("What is the unit of electric current?", new[] { "Volt", "Ampere", "Ohm", "Watt" }, 1, DifficultyLevel.Easy),
			("What is the formula for work?", new[] { "F = ma", "W = Fd", "P = W/t", "E = mc^2" }, 1, DifficultyLevel.Easy),
			("What is the unit of power?", new[] { "Joule", "Watt", "Newton", "Pascal" }, 1, DifficultyLevel.Easy),
			("What is the unit of pressure?", new[] { "Newton", "Joule", "Pascal", "Watt" }, 2, DifficultyLevel.Easy),
			("What is the first law of thermodynamics?", new[] { "Entropy always increases", "Energy equals mass times c^2", "Energy cannot be created or destroyed", "Force equals mass times acceleration" }, 2, DifficultyLevel.Medium),
			("What is the momentum of a 2 kg object moving at 3 m/s?", new[] { "5 kg·m/s", "4 kg·m/s", "6 kg·m/s", "8 kg·m/s" }, 2, DifficultyLevel.Medium),
			("What is the unit of energy?", new[] { "Watt", "Newton", "Joule", "Pascal" }, 2, DifficultyLevel.Easy),
			("What is the speed of light in a vacuum?", new[] { "3x10^6 m/s", "3x10^7 m/s", "3x10^8 m/s", "3x10^9 m/s" }, 2, DifficultyLevel.Medium),
			("What is the wavelength of a wave with frequency 2 Hz and speed 4 m/s?", new[] { "1 m", "4 m", "0.5 m", "2 m" }, 3, DifficultyLevel.Medium),
			("What is the Doppler effect?", new[] { "Change in wave amplitude", "Change in wave speed", "Change in wave direction", "Change in wave frequency due to motion" }, 3, DifficultyLevel.Medium),
			("What is the unit of resistance?", new[] { "Volt", "Ampere", "Watt", "Ohm" }, 3, DifficultyLevel.Easy),
			("What is the speed of sound in air at 20°C?", new[] { "300 m/s", "400 m/s", "500 m/s", "343 m/s" }, 3, DifficultyLevel.Medium),
			("What is the formula for power?", new[] { "W = Fd", "F = ma", "E = mc^2", "P = W/t" }, 3, DifficultyLevel.Medium)
		};

			var chemistryQuestions = new List<(string Text, string[] Options, int CorrectOption, DifficultyLevel Difficulty)>
		{
			("What is the chemical symbol for water?", new[] { "H2O", "HO", "H2O2", "OH" }, 0, DifficultyLevel.Easy),
			("What is the chemical formula for table salt?", new[] { "NaCl", "KCl", "NaOH", "HCl" }, 0, DifficultyLevel.Easy),
			("What is Avogadro's number?", new[] { "6.022x10^23", "6.626x10^-34", "3.14x10^23", "9.8x10^23" }, 0, DifficultyLevel.Medium),
			("What is the chemical symbol for gold?", new[] { "Au", "Ag", "Fe", "Cu" }, 0, DifficultyLevel.Easy),
			("What is an acid?", new[] { "Proton donor", "Proton acceptor", "Electron donor", "Electron acceptor" }, 0, DifficultyLevel.Medium),
			("What is the atomic number of carbon?", new[] { "12", "6", "8", "14" }, 1, DifficultyLevel.Easy),
			("What is the pH of a neutral solution?", new[] { "0", "7", "14", "1" }, 1, DifficultyLevel.Easy),
			("What gas is most abundant in Earth's atmosphere?", new[] { "Oxygen", "Nitrogen", "Carbon Dioxide", "Argon" }, 1, DifficultyLevel.Medium),
			("What is the molar mass of water (H2O)?", new[] { "16 g/mol", "18 g/mol", "20 g/mol", "22 g/mol" }, 1, DifficultyLevel.Medium),
			("What type of bond is formed by sharing electrons?", new[] { "Ionic", "Covalent", "Metallic", "Hydrogen" }, 1, DifficultyLevel.Easy),
			("What is the primary gas in exhaled breath?", new[] { "Oxygen", "Nitrogen", "Carbon Dioxide", "Hydrogen" }, 2, DifficultyLevel.Easy),
			("What is the periodic table group of noble gases?", new[] { "Group 1", "Group 17", "Group 18", "Group 2" }, 2, DifficultyLevel.Medium),
			("What is the pH of a strong acid?", new[] { "7", "14", "0-1", "10" }, 2, DifficultyLevel.Medium),
			("What is the oxidation state of oxygen in H2O?", new[] { "+2", "0", "-2", "+1" }, 2, DifficultyLevel.Medium),
			("What is the main source of energy for Earth's climate system?", new[] { "Geothermal", "Nuclear", "Solar", "Wind" }, 2, DifficultyLevel.Easy),
			("What is the formula for methane?", new[] { "C2H6", "CH3OH", "C2H4", "CH4" }, 3, DifficultyLevel.Easy),
			("What is the name of Fe2O3?", new[] { "Iron(II) oxide", "Iron oxide", "Ferric oxide", "Iron(III) oxide" }, 3, DifficultyLevel.Medium),
			("What is the chemical formula for ammonia?", new[] { "NH4", "N2H4", "NO2", "NH3" }, 3, DifficultyLevel.Easy),
			("What is the symbol for sodium?", new[] { "S", "N", "Sn", "Na" }, 3, DifficultyLevel.Easy),
			("What is the product of a neutralization reaction?", new[] { "Acid and base", "Gas and water", "Salt and gas", "Salt and water" }, 3, DifficultyLevel.Medium)
		};


			var subjectQuestions = new Dictionary<int, List<(string Text, string[] Options, int CorrectOption, DifficultyLevel Difficulty)>>
		{
			{ 1, mathQuestions },
			{ 2, physicsQuestions },
			{ 3, chemistryQuestions }
		};

			foreach (var subject in subjects)
			{
				var questionList = subjectQuestions[subject.Id];
				for (int i = 0; i < questionList.Count; i++)
				{
					var q = questionList[i];
					var question = new Question
					{
						Id = questionId,
						SubjectId = subject.Id,
						Text = q.Text,
						Difficulty = q.Difficulty,
					};
					questions.Add(question);


					for (int j = 0; j < q.Options.Length; j++)
					{
						questionOptions.Add(new QuestionOption
						{
							Id = optionId,
							QuestionId = questionId,
							Text = q.Options[j],
							IsCorrect = j == q.CorrectOption,
						});
						optionId++;
					}
					questionId++;
				}
			}

			modelBuilder.Entity<Question>().HasData(questions);

			modelBuilder.Entity<QuestionOption>().HasData(questionOptions);
		}
	}
}

