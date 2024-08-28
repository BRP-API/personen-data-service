using Rvig.Api.Gezag.ResponseModels;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.Exceptions;

namespace Rvig.HaalCentraalApi.Personen.Interfaces
{
	public class GezagTransformer : IGezagTransformer
	{
		public IEnumerable<AbstractGezagsrelatie> TransformGezagsrelaties(IEnumerable<Gezagsrelatie> gezagsrelaties, string? persoonBurgerservicenummer, List<GbaPartner>? persoonPartners, List<GbaKind>? persoonKinderen, List<GbaOuder>? persoonOuders) =>
			// There has already been a null check above this line.
			gezagsrelaties
						.GroupBy(gezagsrelatie => new { gezagsrelatie.SoortGezag, gezagsrelatie.BsnMinderjarige })
						.Select(p => TransformGezagsrelatie(p.Select(o => o), persoonBurgerservicenummer, persoonPartners, persoonKinderen, persoonOuders))
						.Where(p => p != null)
						.ToList()!;

		private AbstractGezagsrelatie? TransformGezagsrelatie(IEnumerable<Gezagsrelatie> gezagsrelaties, string? persoonBurgerservicenummer, List<GbaPartner>? persoonPartners, List<GbaKind>? persoonKinderen, List<GbaOuder>? persoonOuders)
		{
			if (!gezagsrelaties.Any(gezagsrelatie => gezagsrelatie.BsnMeerderjarige == persoonBurgerservicenummer || gezagsrelatie.BsnMinderjarige == persoonBurgerservicenummer))
			{
				return default;
			}
			var soortGezag = gezagsrelaties.FirstOrDefault()?.SoortGezag;
			return soortGezag switch
			{
				Gezagsrelatie.SoortGezagEnum.OG1Enum => CreateEenhoofdigOuderlijkGezag(gezagsrelaties),
				Gezagsrelatie.SoortGezagEnum.OG2Enum => CreateTweehoofdigOuderlijkGezag(gezagsrelaties),
				Gezagsrelatie.SoortGezagEnum.GGEnum => CreateGezamenlijkGezag(gezagsrelaties, persoonBurgerservicenummer, persoonPartners, persoonKinderen, persoonOuders),
				Gezagsrelatie.SoortGezagEnum.VEnum => CreateVoogdij(gezagsrelaties),
				Gezagsrelatie.SoortGezagEnum.GEnum => CreateTijdelijkGeenGezag(gezagsrelaties),
				Gezagsrelatie.SoortGezagEnum.NEnum => CreateGezagNietTeBepalen(gezagsrelaties),
				_ => throw new CustomInvalidOperationException($"Onbekend type gezag: {soortGezag}"),
			};
		}

		private EenhoofdigOuderlijkGezag CreateEenhoofdigOuderlijkGezag(IEnumerable<Gezagsrelatie> gezagsrelaties)
		{
			var gezagsrelatie = gezagsrelaties.SingleOrDefault();
			return new EenhoofdigOuderlijkGezag
			{
				Ouder = new GezagOuder
				{
					Burgerservicenummer = gezagsrelatie?.BsnMeerderjarige
				},
				Minderjarige = new Minderjarige
				{
					Burgerservicenummer = gezagsrelatie?.BsnMinderjarige
				},
				Type = nameof(EenhoofdigOuderlijkGezag)
			};
		}

		private TweehoofdigOuderlijkGezag CreateTweehoofdigOuderlijkGezag(IEnumerable<Gezagsrelatie> gezagsrelaties)
		{
			return new TweehoofdigOuderlijkGezag
			{
				Ouders = gezagsrelaties.Select(gezagsrelatie => new GezagOuder()
				{
					Burgerservicenummer = gezagsrelatie.BsnMeerderjarige
				}).ToList(),
				Minderjarige = new Minderjarige
				{
					Burgerservicenummer = gezagsrelaties.First().BsnMinderjarige
				},
				Type = nameof(TweehoofdigOuderlijkGezag)
			};
		}

		private GezamenlijkGezag CreateGezamenlijkGezag(IEnumerable<Gezagsrelatie> gezagsrelaties, string? persoonBurgerservicenummer, List<GbaPartner>? persoonPartners, List<GbaKind>? persoonKinderen, List<GbaOuder>? persoonOuders)
		{
			var bsnMinderjarige = gezagsrelaties.First().BsnMinderjarige;
			string? ouderBsn;

			if (bsnMinderjarige == persoonBurgerservicenummer)
			{
				ouderBsn = gezagsrelaties.FirstOrDefault(gezagsrelatie => persoonOuders?.Any(ouder => gezagsrelatie.BsnMeerderjarige == ouder.Burgerservicenummer) == true)?.BsnMeerderjarige;
			}
			else
			{
				ouderBsn = gezagsrelaties.SingleOrDefault(gezagsrelatie => gezagsrelatie.BsnMeerderjarige == persoonBurgerservicenummer && persoonKinderen?.Any(kind => kind.Burgerservicenummer == bsnMinderjarige) == true)?.BsnMeerderjarige
						   ?? gezagsrelaties.FirstOrDefault(gezagsrelatie => persoonPartners?.Any(partner => gezagsrelatie.BsnMeerderjarige == partner.Burgerservicenummer) == true)?.BsnMeerderjarige;
			}
			var derdeBsn = gezagsrelaties.FirstOrDefault(gezagsrelatie => gezagsrelatie.BsnMeerderjarige != ouderBsn)?.BsnMeerderjarige;

			return new GezamenlijkGezag
			{
				Ouder = new GezagOuder()
				{
					Burgerservicenummer = ouderBsn
				},
				Derde = new Meerderjarige
				{
					Burgerservicenummer = derdeBsn
				},
				Minderjarige = new Minderjarige
				{
					Burgerservicenummer = bsnMinderjarige
				},
				Type = nameof(GezamenlijkGezag)
			};
		}

		private Voogdij CreateVoogdij(IEnumerable<Gezagsrelatie> gezagsrelaties)
		{
			var derden = gezagsrelaties.Select(gezagsrelatie => new Meerderjarige()
			{
				Burgerservicenummer = gezagsrelatie.BsnMeerderjarige
			}).ToList();

			return new Voogdij
			{
				Derden = derden.Any(derde => !string.IsNullOrWhiteSpace(derde.Burgerservicenummer)) ? derden : new List<Meerderjarige>(),
				Minderjarige = new Minderjarige
				{
					Burgerservicenummer = gezagsrelaties.First().BsnMinderjarige
				},
				Type = nameof(Voogdij)
			};
		}

		private TijdelijkGeenGezag CreateTijdelijkGeenGezag(IEnumerable<Gezagsrelatie> _) => new()
		{
			Type = nameof(TijdelijkGeenGezag)
		};

		private GezagNietTeBepalen CreateGezagNietTeBepalen(IEnumerable<Gezagsrelatie> _) => new()
		{
			Type = nameof(GezagNietTeBepalen)
		};
	}
}